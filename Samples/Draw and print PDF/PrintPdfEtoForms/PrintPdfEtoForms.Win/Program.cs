using System;
using Eto.Forms;

namespace BitMiracle.Docotic.Pdf.Samples.PrintPdfEtoForms.Win
{
    class MainClass
    {
        [STAThread]
        public static void Main()
        {
            new Application(Eto.Platforms.WinForms).Run(new MainForm());
        }
    }
}
