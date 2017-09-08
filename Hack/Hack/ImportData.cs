using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Hack
{
    public class ImportData
    {
        public static List<Song> songs = new List<Song>();
        public static void SongsToDisplay(int num)
        {
            Stopwatch stopWatch = new Stopwatch();

            TimeSpan ts = stopWatch.Elapsed;
            char[] delimiterChars = { ',' };
            string[] analysisLines = File.ReadAllLines("analysis.txt");
            string[] yearLines = File.ReadAllLines("year.txt");

            List<string> methaLines = new List<string>();
            methaLines.AddRange(File.ReadAllLines("0-99999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("100000-199999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("200000-299999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("300000-399999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("400000-499999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("500000-599999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("600000-699999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("700000-799999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("800000-899999 metha.txt"));
            methaLines.AddRange(File.ReadAllLines("900000-999999 metha.txt"));

            Console.WriteLine("Start making objects");
            stopWatch.Start();
            for (int i = 0; i < num; i++)
            {
                string[] a = analysisLines[i].Split(delimiterChars); //Analysis words
                string[] c = yearLines[i].Split(delimiterChars);    //Year words
                string[] b = methaLines[i].Split(delimiterChars);        //Metha Words
                Song song = new Song(float.Parse(a[3], CultureInfo.InvariantCulture), float.Parse(a[5], CultureInfo.InvariantCulture), int.Parse(a[21], CultureInfo.InvariantCulture), float.Parse(a[23], CultureInfo.InvariantCulture), int.Parse(a[24], CultureInfo.InvariantCulture), float.Parse(a[27], CultureInfo.InvariantCulture), int.Parse(a[28], CultureInfo.InvariantCulture), a[30], b[5], b[10], b[12], b[15], b[18], b[19], int.Parse(c[1], CultureInfo.InvariantCulture));
                songs.Add(song);
            }

            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed.ToString());

        }
    }
}
