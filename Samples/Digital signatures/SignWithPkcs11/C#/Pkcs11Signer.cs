using System;
using System.Security.Cryptography.X509Certificates;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.Factories;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class Pkcs11Signer : IPdfSigner, IDisposable
    {
        private readonly ISession m_session;
        private readonly IObjectHandle m_privateKey;
        private readonly PdfSignatureAlgorithm m_signatureAlgorithm;
        private readonly PdfDigestAlgorithm m_digestAlgorithm;
        private readonly X509Certificate2[] m_chain;

        private bool m_disposed;

        public Pkcs11Signer(
            PdfSignatureAlgorithm signatureAlgorithm,
            PdfDigestAlgorithm digestAlgorithm,
            X509Certificate2[] chain,
            ISession session,
            IObjectHandle privateKey)
        {
            m_signatureAlgorithm = signatureAlgorithm;
            m_digestAlgorithm = digestAlgorithm;
            m_chain = chain;
            m_session = session;
            m_privateKey = privateKey;
        }

        public PdfSignatureAlgorithm SignatureAlgorithm => m_signatureAlgorithm;

        public byte[] Sign(byte[] message)
        {
            var mechanismFactory = new MechanismFactory();
            IMechanism mechanism = m_signatureAlgorithm switch
            {
                PdfSignatureAlgorithm.Ecdsa => m_digestAlgorithm switch
                {
                    PdfDigestAlgorithm.Sha1 => mechanismFactory.Create(CKM.CKM_ECDSA_SHA1),
                    PdfDigestAlgorithm.Sha256 => mechanismFactory.Create(CKM.CKM_ECDSA_SHA256),
                    PdfDigestAlgorithm.Sha384 => mechanismFactory.Create(CKM.CKM_ECDSA_SHA384),
                    PdfDigestAlgorithm.Sha512 => mechanismFactory.Create(CKM.CKM_ECDSA_SHA512),
                    _ => throw new InvalidOperationException("Unexpected digest algorithm " + m_digestAlgorithm),
                },
                PdfSignatureAlgorithm.Rsa => m_digestAlgorithm switch
                {
                    PdfDigestAlgorithm.Sha1 => mechanismFactory.Create(CKM.CKM_SHA1_RSA_PKCS),
                    PdfDigestAlgorithm.Sha256 => mechanismFactory.Create(CKM.CKM_SHA256_RSA_PKCS),
                    PdfDigestAlgorithm.Sha384 => mechanismFactory.Create(CKM.CKM_SHA384_RSA_PKCS),
                    PdfDigestAlgorithm.Sha512 => mechanismFactory.Create(CKM.CKM_SHA512_RSA_PKCS),
                    _ => throw new InvalidOperationException("Unexpected digest algorithm " + m_digestAlgorithm),
                },

                _ => throw new InvalidOperationException("Unexpected signature algorithm " + m_signatureAlgorithm),
            };
            return m_session.Sign(mechanism, m_privateKey, message);
        }

        public X509Certificate2[] GetChain() => m_chain;

        public void Dispose()
        {
            if (m_disposed)
                return;

            m_disposed = true;

            m_session.Dispose();

            foreach (var c in m_chain)
                c.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
