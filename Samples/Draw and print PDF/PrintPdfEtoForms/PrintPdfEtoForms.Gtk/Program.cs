using System;
using Eto.Forms;

namespace BitMiracle.Docotic.Pdf.Samples.PrintPdfEtoForms.Gtk
{
    class MainClass
    {
        [STAThread]
        public static void Main()
        {
            new Application(Eto.Platforms.Gtk).Run(new MainForm());
        }
    }
}
