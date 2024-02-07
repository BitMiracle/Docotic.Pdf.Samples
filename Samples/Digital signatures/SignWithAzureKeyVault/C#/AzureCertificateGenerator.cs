using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class AzureCertificateGenerator : X509SignatureGenerator
    {
        private const string SubjectDn = "CN=self-generated cert,OU=Docotic.Pdf samples,O=Bit Miracle";
        private static readonly byte[] SerialNumber = new byte[] { 17, 59, 46 };
        private static readonly DateTimeOffset NotBefore = DateTime.UtcNow;
        private static readonly DateTimeOffset NotAfter = NotBefore.AddYears(2);

        private readonly X509SignatureGenerator m_baseGenerator;
        private readonly CryptographyClient m_client;
        private readonly SignatureAlgorithm m_signingAlgorithm;

        private AzureCertificateGenerator(CryptographyClient client, SignatureAlgorithm alg, X509SignatureGenerator baseGenerator)
        {
            m_baseGenerator = baseGenerator;
            m_client = client;
            m_signingAlgorithm = alg;
        }

        public static X509Certificate2 Generate(
            KeyVaultKey key, CryptographyClient client, SignatureAlgorithm signingAlgorithm)
            => Generate(key, client, signingAlgorithm, SubjectDn, NotBefore, NotAfter, SerialNumber);

        public static X509Certificate2 Generate(
            KeyVaultKey key,
            CryptographyClient client,
            SignatureAlgorithm signingAlgorithm,
            string subjectDN,
            DateTimeOffset notBefore,
            DateTimeOffset notAfter,
            byte[] serialNumber)
        {
            var type = key.KeyType;
            HashAlgorithmName hashAlg = GetHashAlgorithmName(signingAlgorithm.ToString());
            CertificateRequest certificateRequest;
            X509SignatureGenerator baseGenerator;
            if (type == KeyType.Ec)
            {
                // At the moment (7 Feb 2024), this generation of self-signed certificates for Azure KeyVault ECDSA keys
                // does not work. Use a certificate from KeyVault Secrets or KeyVault Certificates instead.
                ECDsa ecdsa = key.Key.ToECDsa();
                certificateRequest = new CertificateRequest(subjectDN, ecdsa, hashAlg);
                baseGenerator = CreateForECDsa(ecdsa);
            }
            else if (type == KeyType.Rsa)
            {
                RSA rsa = key.Key.ToRSA();

                string alg = signingAlgorithm.ToString();
                RSASignaturePadding padding = alg.StartsWith("RS")
                    ? RSASignaturePadding.Pkcs1
                    : RSASignaturePadding.Pss;
                certificateRequest = new CertificateRequest(subjectDN, rsa, hashAlg, padding);
                baseGenerator = CreateForRSA(rsa, padding);
            }
            else
            {
                throw new ArgumentException("Cannot determine encryption algorithm for " + type, nameof(key));
            }

            var generator = new AzureCertificateGenerator(client, signingAlgorithm, baseGenerator);
            return certificateRequest.Create(new X500DistinguishedName(subjectDN), generator, notBefore, notAfter, serialNumber);
        }

        public override byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm)
        {
            return m_baseGenerator.GetSignatureAlgorithmIdentifier(hashAlgorithm);
        }

        public override byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
        {
            return m_client.SignData(m_signingAlgorithm, data).Signature;
        }

        protected override PublicKey BuildPublicKey()
        {
            return m_baseGenerator.PublicKey;
        }

        private static HashAlgorithmName GetHashAlgorithmName(string signingAlgorithm)
        {
            if (signingAlgorithm.EndsWith("256"))
                return HashAlgorithmName.SHA256;

            if (signingAlgorithm.EndsWith("384"))
                return HashAlgorithmName.SHA384;

            if (signingAlgorithm.EndsWith("512"))
                return HashAlgorithmName.SHA512;

            throw new ArgumentException("Cannot determine hash algorithm for " + signingAlgorithm, nameof(signingAlgorithm));
        }
    }
}
