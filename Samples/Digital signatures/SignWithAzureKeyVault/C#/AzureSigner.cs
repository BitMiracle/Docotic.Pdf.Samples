using System;
using Azure.Security.KeyVault.Keys.Cryptography;
using AzureSignatureAlgorithm = Azure.Security.KeyVault.Keys.Cryptography.SignatureAlgorithm;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class AzureSigner : IPdfSigner
    {
        private readonly CryptographyClient m_client;
        private readonly AzureSignatureAlgorithm m_signingAlgorithm;

        public AzureSigner(CryptographyClient client, AzureSignatureAlgorithm signingAlgorithm)
        {
            m_client = client;
            m_signingAlgorithm = signingAlgorithm;
        }

        public PdfSignatureAlgorithm SignatureAlgorithm
        {
            get
            {
                if (m_signingAlgorithm == AzureSignatureAlgorithm.ES256 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES384 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES512 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES256K)
                    return PdfSignatureAlgorithm.Ecdsa;

                if (m_signingAlgorithm == AzureSignatureAlgorithm.PS256 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.PS384 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.PS512)
                    return PdfSignatureAlgorithm.RsaSsaPss;

                if (m_signingAlgorithm == AzureSignatureAlgorithm.RS256 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.RS384 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.RS512)
                    return PdfSignatureAlgorithm.Rsa;

                throw new InvalidOperationException($"Unsupported {nameof(SignatureAlgorithm)} value: {m_signingAlgorithm}");
            }
        }

        public PdfDigestAlgorithm DigestAlgorithm
        {
            get
            {
                if (m_signingAlgorithm == AzureSignatureAlgorithm.RS256 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.PS256 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES256 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES256K)
                    return PdfDigestAlgorithm.Sha256;

                if (m_signingAlgorithm == AzureSignatureAlgorithm.RS384 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.PS384 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES384)
                    return PdfDigestAlgorithm.Sha384;

                if (m_signingAlgorithm == AzureSignatureAlgorithm.RS512 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.PS512 ||
                    m_signingAlgorithm == AzureSignatureAlgorithm.ES512)
                    return PdfDigestAlgorithm.Sha512;

                throw new InvalidOperationException($"Unsupported {nameof(SignatureAlgorithm)} value: {m_signingAlgorithm}");
            }
        }

        public byte[] Sign(byte[] message)
        {
            return m_client.SignData(m_signingAlgorithm, message).Signature;
        }
    }
}
