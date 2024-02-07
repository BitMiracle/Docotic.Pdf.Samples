using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class AwsCertificateGenerator : X509SignatureGenerator
    {
        private const string SubjectDn = "CN=self-generated cert,OU=Docotic.Pdf samples,O=Bit Miracle";
        private static readonly byte[] SerialNumber = new byte[] { 17, 59, 46 };
        private static readonly DateTimeOffset NotBefore = DateTime.UtcNow;
        private static readonly DateTimeOffset NotAfter = NotBefore.AddYears(2);

        private readonly X509SignatureGenerator m_baseGenerator;
        private readonly string m_keyId;
        private readonly SigningAlgorithmSpec m_signingAlgorithm;

        private AwsCertificateGenerator(string keyId, SigningAlgorithmSpec alg, X509SignatureGenerator baseGenerator)
        {
            m_baseGenerator = baseGenerator;
            m_keyId = keyId;
            m_signingAlgorithm = alg;
        }

        public static X509Certificate2 Generate(GetPublicKeyResponse key, SigningAlgorithmSpec signingAlgorithm)
            => Generate(key, signingAlgorithm, SubjectDn, NotBefore, NotAfter, SerialNumber);

        public static X509Certificate2 Generate(
            GetPublicKeyResponse key,
            SigningAlgorithmSpec signingAlgorithm,
            string subjectDN,
            DateTimeOffset notBefore,
            DateTimeOffset notAfter,
            byte[] serialNumber)
        {
            byte[] pkBytes = key.PublicKey.ToArray();
            HashAlgorithmName hashAlg = GetHashAlgorithmName(signingAlgorithm);
            string keySpecString = key.KeySpec.ToString();

            CertificateRequest certificateRequest;
            X509SignatureGenerator baseGenerator;
            if (keySpecString.StartsWith("ECC"))
            {
                ECDsa ecdsa = ECDsa.Create();
                ecdsa.ImportSubjectPublicKeyInfo(pkBytes, out _);
                certificateRequest = new CertificateRequest(subjectDN, ecdsa, hashAlg);
                baseGenerator = CreateForECDsa(ecdsa);
            }
            else if (keySpecString.StartsWith("RSA"))
            {
                RSA rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(pkBytes, out _);

                string alg = signingAlgorithm.ToString();
                RSASignaturePadding padding = alg.StartsWith("RSASSA_PKCS1_V1_5")
                    ? RSASignaturePadding.Pkcs1
                    : RSASignaturePadding.Pss;

                certificateRequest = new CertificateRequest(subjectDN, rsa, hashAlg, padding);
                baseGenerator = CreateForRSA(rsa, padding);
            }
            else
            {
                throw new ArgumentException("Cannot determine encryption algorithm for " + keySpecString, nameof(key));
            }

            var generator = new AwsCertificateGenerator(key.KeyId, signingAlgorithm, baseGenerator);
            return certificateRequest.Create(new X500DistinguishedName(subjectDN), generator, notBefore, notAfter, serialNumber);
        }

        public override byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm)
        {
            return m_baseGenerator.GetSignatureAlgorithmIdentifier(hashAlgorithm);
        }

        public override byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
        {
            using var kmsClient = new AmazonKeyManagementServiceClient();
            using var stream = new MemoryStream(data);
            var signRequest = new SignRequest()
            {
                SigningAlgorithm = m_signingAlgorithm.ToString(),
                KeyId = m_keyId,
                MessageType = MessageType.RAW,
                Message = stream,
            };
            SignResponse signResponse = kmsClient.SignAsync(signRequest).GetAwaiter().GetResult();
            return signResponse.Signature.ToArray();
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
