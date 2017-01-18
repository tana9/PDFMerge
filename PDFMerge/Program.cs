using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDFMerge
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("PDFの保存フォルダを指定してください。");
            var path = Console.ReadLine();
            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                Console.WriteLine("フォルダが存在しません。");
                return;
            }
            using (var outPdf = new PdfDocument())
            {
                foreach (var file in directory.GetFiles("*.pdf"))
                {
                    PdfDocument reader = PdfReader.Open(file.FullName, PdfDocumentOpenMode.Import);
                    CopyPages(reader, outPdf);
                }
                outPdf.Save("merge.pdf");
            }
        }

        private static void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (var i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
    }
}
