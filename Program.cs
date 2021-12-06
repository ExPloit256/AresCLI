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
            string docName = "", docPath = "", testTitle = "";
            int docNumber = 0;

            PdfDocument stubDoc = new PdfDocument();
            PdfPage stubPage = stubDoc.AddPage();

            XGraphics render = XGraphics.FromPdfPage(stubPage);

            do
            {
                ConsoleDrawInit();
                DocInit(docName, docPath, testTitle, docNumber, render, stubDoc, stubPage);
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
             string testTitle,
             int docNumber,
             XGraphics render,
             PdfDocument stubDoc,
             PdfPage stubPage)
        {
        #region DocName&DefInfoinit
        DocNameCheck:
            Console.WriteLine("Inserisci il nome del File: ");
            docName = Console.ReadLine() + ".pdf";

            if (string.IsNullOrWhiteSpace(docName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Il nome del file non può essere nullo! \n(premi un qualsiasi tasto per continuare)\n");
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
        #endregion

        #region DocNum
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

            render.DrawString($"Verifica Numero {docNumber}", Fonts.SmallTitle, XBrushes.Black,
            new XRect(10, 10, stubPage.Width, stubPage.Height), XStringFormat.TopLeft);
        #endregion

        #region TestTitle
        TestTitle:
            Console.WriteLine("\nInserisci il titiolo della verifica: ");
            testTitle =  Console.ReadLine();
            if (string.IsNullOrWhiteSpace(testTitle))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Il titolo della verifica non può essere nullo! \n(premi un qualsiasi tasto per continuare)\n");
                Console.ReadKey();

                Console.ForegroundColor = ConsoleColor.White;
                goto TestTitle;
            }
            else
            {
                if (testTitle.Length > 35)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Il titolo della verifica non può essere più lungo di 35 caratteri! \n(premi un qualsiasi tasto per continuare)\n");
                    Console.ReadKey();

                    Console.ForegroundColor = ConsoleColor.White;
                    goto TestTitle;
                }
            }
            render.DrawString($"{testTitle}", Fonts.Title, XBrushes.Black,
            new XRect(0, 25, stubPage.Width, stubPage.Height), XStringFormat.TopCenter);
            Console.WriteLine(testTitle.Length);
            #endregion
            stubDoc.Save(docPath);
        }
    }

    class Fonts
    {
        public static XFont Title = new XFont("Verdana", 20, XFontStyle.Bold);
        public static XFont SubTitle = new XFont("Verdana", 13, XFontStyle.Bold);
        public static XFont SmallTitle = new XFont("Verdana", 8, XFontStyle.Bold);
        public static XFont NDC = new XFont("Verdana", 15, XFontStyle.Regular);
    }
}
