using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Figgle;

using PdfSharp.Charting;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using PdfSharp.Drawing;

namespace AresCLI
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string docName = "", docPath = "";
            int docNumber = 0;

            PdfDocument stubDoc = new PdfDocument();
            PdfPage stubPage = stubDoc.AddPage();

            XGraphics render = XGraphics.FromPdfPage(stubPage);

            do
            {
                ConsoleDrawInit();
                DocInit(docName, docPath, docNumber, render, stubDoc, stubPage);
                Console.ReadLine();
            } while (true);

        }
        static void ConsoleDrawInit()
        {
            //Fancy Ttile Drawing
            Console.Title = "ARES | (Console Version)";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(FiggleFonts.Standard.Render("ARES"));
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DocInit
            (string docName,
             string docPath,
             int docNumber,
             XGraphics render,
             PdfDocument stubDoc,
             PdfPage stubPage)
        {
            DocNameCheck:
            Console.WriteLine("Inserisci il titolo del File: ");
            docName = Console.ReadLine() + ".pdf";

            if (string.IsNullOrWhiteSpace(docName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Il titolo non può essere nullo! \n(premi un qualsiasi tasto per continuare)\n");
                Console.ReadKey();

                Console.ForegroundColor = ConsoleColor.White;
                goto DocNameCheck;
            }
            docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + docName;

            render.DrawString("NOME: _________", Fonts.NDC, XBrushes.Black,
            new XRect(25, 55, stubPage.Width, stubPage.Height), XStringFormat.TopLeft);
            render.DrawString("DATA: _________", Fonts.NDC, XBrushes.Black,
            new XRect(221, 55, stubPage.Width, stubPage.Height), XStringFormat.TopLeft);
            render.DrawString("CLASSE: _________", Fonts.NDC, XBrushes.Black,
            new XRect(412, 55, stubPage.Width, stubPage.Height), XStringFormat.TopLeft);

            
            DocNumCheck:
            Console.WriteLine("\nInserisci il numero della verifica: ");
            if(!int.TryParse(Console.ReadLine(), out docNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Il numero della verifica non può essere una stringa! \n(premi un qualsiasi tasto per continuare)\n");
                Console.ReadKey();

                Console.ForegroundColor = ConsoleColor.White;
                goto DocNumCheck;
            }

            render.DrawString($"Verifica Numero {docNumber}", Fonts.SubTitle, XBrushes.Black,
            new XRect(15, 15, stubPage.Width, stubPage.Height), XStringFormat.TopLeft);

            stubDoc.Save(docPath);
        }
    }

    class Fonts
    {
        public static XFont Title = new XFont("Verdana", 20, XFontStyle.Bold);
        public static XFont SubTitle = new XFont("Verdana", 13, XFontStyle.Bold);
        public static XFont NDC = new XFont("Verdana", 15, XFontStyle.Regular);
    }
}
