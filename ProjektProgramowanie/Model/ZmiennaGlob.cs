using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Model
{
    public class ZmiennaGlob
    {
        public static int LiczbaKolumn = 3;
        public static int LiczbaWierszy = 3;
        public static int Zmienna = LiczbaKolumn * LiczbaWierszy;
        public static string Kategoria = null;
        public static int Id { get; set; }
        public static int TwojIndexLiczbaMiejscEqu = 5;
        public static int TwojIndexLiczbaMiejsc = TwojIndexLiczbaMiejscEqu;
    }
}
