using System;
using Eto.Forms;

namespace BitMiracle.Docotic.Pdf.Samples.PrintPdfEtoForms.Mac
{
    class MainClass
    {
        [STAThread]
        public static void Main()
        {
            new Application(Eto.Platforms.Mac64).Run(new MainForm());
        }
    }
}
