using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StatistikaCasoveRady.Bussines
{
    public class FileManager : IFileManager
    {
        public string[] GetAllFiles()
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\Data"));
            return Directory.GetFiles(path);
        }

        public List<Obed> NactiObedy()
        {
            throw new System.NotImplementedException();
        }

        public List<Obed> ReadFile(string file)
        {
            List<Obed> obedy = new List<Obed>();
            //using OfficeOpenXml;
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(file)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

               var sb = new StringBuilder(); //this is your your data
                for (int rowNum = 2; rowNum <= totalRows; rowNum++) //selet starting row here
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? "0" : c.Value.ToString());
                    string s = string.Join(";", row);

                    string[] radek = s.Split(';');
                    if(radek.Length > 6)
                    {
                        Obed o = new Obed();
                        DateTime.TryParse( radek[0],out DateTime vlozeno);
                        DateTime.TryParse(radek[1], out DateTime cas);
                        o.Datum = vlozeno;
                        o.Cas = cas;
                        DateTime.TryParse(radek[2], out DateTime zuctovano);
                        o.Zuctovano = zuctovano;
                        o.Typ = radek[3];
                        o.Popis = radek[4];
                        
                        decimal.TryParse(radek[5], out decimal uhrada);
                        o.Cena = uhrada;
                        decimal.TryParse(radek[6], out decimal zustatek);
                        o.zustatek = zustatek;

                        obedy.Add(o);
                    }
                }
            }

            return obedy;
        }
    }
}
