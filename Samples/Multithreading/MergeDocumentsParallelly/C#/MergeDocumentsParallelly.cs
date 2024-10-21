using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class MergeDocumentsParallelly
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            Stream[] documentsToMerge = GetDocumentsToMerge(1000);

            int rangeSize = 50;
            while (documentsToMerge.Length > rangeSize)
            {
                int partitionCount = (int)Math.Ceiling(documentsToMerge.Length / (double)rangeSize);
                var result = new Stream[partitionCount];

                var partitioner = Partitioner.Create(0, documentsToMerge.Length, rangeSize);
                Parallel.ForEach(partitioner, range =>
                {
                    int startIndex = range.Item1;
                    int count = range.Item2 - range.Item1;
                    result[startIndex / rangeSize] = MergeToStream(documentsToMerge, startIndex, count);
                });
                documentsToMerge = result;
            }

            string pathToFile = "MergeDocumentsParallelly.pdf";
            using PdfDocument final = GetMergedDocument(documentsToMerge, 0, documentsToMerge.Length);
            final.Save(pathToFile);

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static Stream MergeToStream(Stream[] streams, int startIndex, int count)
        {
            using PdfDocument pdf = GetMergedDocument(streams, startIndex, count);

            var result = new MemoryStream();

            var options = new PdfSaveOptions
            {
                // This speeds up writing of intermediate documents
                UseObjectStreams = false
            };
            pdf.Save(result, options);
            return result;
        }

        private static PdfDocument GetMergedDocument(Stream[] streams, int startIndex, int count)
        {
            var pdf = new PdfDocument();
            try
            {
                for (int i = 0; i < count; ++i)
                {
                    var s = streams[startIndex + i];
                    pdf.Append(s);
                    s.Dispose();
                }

                pdf.RemovePage(0);

                pdf.ReplaceDuplicateObjects();

                return pdf;
            }
            catch
            {
                pdf.Dispose();
                throw;
            }
        }

        private static Stream[] GetDocumentsToMerge(int count)
        {
            var streams = new Stream[count];
            for (int i = 0; i < streams.Length; ++i)
                streams[i] = File.OpenRead(@"..\Sample Data\jfif3.pdf");

            return streams;
        }
    }
}