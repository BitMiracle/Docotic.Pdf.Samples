using System;
using System.IO;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class AwsSigner : IPdfSigner
    {
        private readonly string m_keyId;
        private readonly SigningAlgorithmSpec m_signingAlgorithm;

        public AwsSigner(string keyId, SigningAlgorithmSpec signingAlgorithm)
        {
            if (signingAlgorithm == SigningAlgorithmSpec.SM2DSA)
                throw new ArgumentException("SM2DSA is not supported", nameof(signingAlgorithm));

            m_keyId = keyId;
            m_signingAlgorithm = signingAlgorithm;
        }

        public PdfSignatureAlgorithm SignatureAlgorithm
        {
            get
            {
                if (m_signingAlgorithm == SigningAlgorithmSpec.ECDSA_SHA_256 ||
                    m_signingAlgorithm == SigningAlgorithmSpec.ECDSA_SHA_384 ||
                    m_signingAlgorithm == SigningAlgorithmSpec.ECDSA_SHA_512)
                    return PdfSignatureAlgorithm.Ecdsa;

                if (m_signingAlgorithm == SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_256 ||
                    m_signingAlgorithm == SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_384 ||
                    m_signingAlgorithm == SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_512)
                    return PdfSignatureAlgorithm.Rsa;

                if (m_signingAlgorithm == SigningAlgorithmSpec.RSASSA_PSS_SHA_256 ||
                    m_signingAlgorithm == SigningAlgorithmSpec.RSASSA_PSS_SHA_384 ||
                    m_signingAlgorithm == SigningAlgorithmSpec.RSASSA_PSS_SHA_512)
                    return PdfSignatureAlgorithm.RsaSsaPss;

                throw new InvalidOperationException($"Unsupported {nameof(SigningAlgorithmSpec)} value: {m_signingAlgorithm}");
            }
        }

        public PdfDigestAlgorithm DigestAlgorithm
        {
            get
            {
                string alg = m_signingAlgorithm.Value;
                if (alg.EndsWith("256"))
                    return PdfDigestAlgorithm.Sha256;

                if (alg.EndsWith("384"))
                    return PdfDigestAlgorithm.Sha384;

                if (alg.EndsWith("512"))
                    return PdfDigestAlgorithm.Sha512;

                throw new InvalidOperationException($"Unsupported {nameof(SigningAlgorithmSpec)} value: {m_signingAlgorithm}");
            }
        }

        public byte[] Sign(byte[] message)
        {
            using var kmsClient = new AmazonKeyManagementServiceClient();
            using var stream = new MemoryStream(message);
            var signRequest = new SignRequest()
            {
                SigningAlgorithm = m_signingAlgorithm,
                KeyId = m_keyId,
                MessageType = MessageType.RAW,
                Message = stream,
            };
            SignResponse signResponse = kmsClient.SignAsync(signRequest).GetAwaiter().GetResult();
            return signResponse.Signature.ToArray();
        }
    }
}
