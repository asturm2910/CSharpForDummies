using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading;

namespace LINQCSVRead
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\alstu\Downloads\zuordnung_plz_ort.csv";
            string[] plzFile = File.ReadAllLines(path);

            var query = from csvLine in plzFile let valueString = csvLine.Split(",") select new { id = valueString[0], ort=valueString[1]  ,plz = valueString[2], bundesland = valueString[3]};

            var list = query.ToList();

            foreach (var dataLine in list) {
                Console.Write($"{dataLine.id} / {dataLine.ort} / {dataLine.plz} / {dataLine.bundesland}");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
