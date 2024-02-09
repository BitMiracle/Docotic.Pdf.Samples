using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.Factories;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class Pkcs11SignerFactory : IDisposable
    {
        private readonly IPkcs11Library m_pkcs11Library;
        private readonly ISlot m_slot;

        private bool m_disposed;

        public Pkcs11SignerFactory(string libraryPath, ulong slotId)
        {
            var factories = new Pkcs11InteropFactories();
            m_pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, libraryPath, AppType.MultiThreaded);
            m_slot = m_pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent).First(slot => slot.SlotId == slotId);
        }

        public Pkcs11Signer? Load(string alias, string certLabel, string pin, PdfDigestAlgorithm digestAlgorithm)
        {
            var pkAttributeKeys = new List<CKA>()
            {
                CKA.CKA_KEY_TYPE,
                CKA.CKA_LABEL,
                CKA.CKA_ID
            };
            var certAttributeKeys = new List<CKA>()
            {
                CKA.CKA_VALUE,
                CKA.CKA_LABEL
            };

            bool disposeSession = true;
            ISession session = m_slot.OpenSession(SessionType.ReadOnly);
            try
            {
                session.Login(CKU.CKU_USER, pin);

                var objectAttributeFactory = new ObjectAttributeFactory();
                var attributes = new List<IObjectAttribute>
                {
                    objectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY)
                };
                List<IObjectHandle> keys = session.FindAllObjects(attributes);

                PdfSignatureAlgorithm? signatureAlgorithm = null;
                var chain = new List<X509Certificate2>();
                IObjectHandle? privateKey = null;
                bool found = false;
                foreach (IObjectHandle key in keys)
                {
                    List<IObjectAttribute> keyAttributes = session.GetAttributeValue(key, pkAttributeKeys);

                    ulong type = keyAttributes[0].GetValueAsUlong();
                    switch (type)
                    {
                        case (ulong)CKK.CKK_RSA:
                            signatureAlgorithm = PdfSignatureAlgorithm.Rsa;
                            break;

                        case (ulong)CKK.CKK_ECDSA:
                            signatureAlgorithm = PdfSignatureAlgorithm.Ecdsa;
                            break;

                        default:
                            continue;
                    }

                    string thisAlias = keyAttributes[1].GetValueAsString();
                    if (thisAlias == null || thisAlias.Length == 0)
                        thisAlias = keyAttributes[2].GetValueAsString();
                    if (alias != null && !alias.Equals(thisAlias))
                        continue;

                    attributes.Clear();
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509));
                    if (certLabel == null && thisAlias != null && thisAlias.Length > 0)
                        certLabel = thisAlias;

                    if (certLabel != null)
                        attributes.Add(objectAttributeFactory.Create(CKA.CKA_LABEL, certLabel));

                    List<IObjectHandle> certificates = session.FindAllObjects(attributes);
                    if (certificates.Count != 1)
                        continue;

                    IObjectHandle certificate = certificates[0];
                    List<IObjectAttribute> certificateAttributes = session.GetAttributeValue(certificate, certAttributeKeys);
                    var cert = new X509Certificate2(certificateAttributes[0].GetValueAsByteArray());

                    chain = new List<X509Certificate2> { cert };
                    try
                    {
                        attributes.Clear();
                        attributes.Add(objectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                        attributes.Add(objectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509));
                        List<IObjectHandle> otherCertificates = session.FindAllObjects(attributes);
                        foreach (IObjectHandle otherCertificate in otherCertificates)
                        {
                            if (!certificate.ObjectId.Equals(otherCertificate.ObjectId))
                            {
                                certificateAttributes = session.GetAttributeValue(otherCertificate, certAttributeKeys);
                                var otherCert = new X509Certificate2(certificateAttributes[0].GetValueAsByteArray());
                                chain.Add(otherCert);
                            }
                        }
                    }
                    catch
                    {
                        foreach (var c in chain)
                            c.Dispose();

                        throw;
                    }

                    found = true;
                    privateKey = key;
                    break;
                }

                if (!found)
                    return null;

                disposeSession = false;
                return new Pkcs11Signer(signatureAlgorithm!.Value, digestAlgorithm, chain.ToArray(), session, privateKey!);
            }
            finally
            {
                if (disposeSession)
                    session.Dispose();
            }
        }

        public void Dispose()
        {
            if (m_disposed)
                return;

            m_disposed = true;

            m_pkcs11Library?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
