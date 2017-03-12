using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Parkowanie
{
    public class MiejsceParkingowe
    {
        //wartosci jakie mozna odczytać na temat miejsca parkingowego
        public bool zajete = false;//czy jest zajete
        public bool docelowe = false;//czy jest miejscem docelowym
        public bool gorne = false;//czy jest na gorze(true) w okienku czy na dole(false)
        public Point polozenie;//miejsce gdzie jest lewy gorny róg każdego z miejsc parkingowych
        public static Size Wymiary { get; } = Dictionary.RozmiarMiejscaParkingowego;
        public Rectangle rect;//x,y,szer,wys parkingu

        public Color Kolor
        {
            get
            {
                if (zajete == true) return Dictionary.KolorZajete;
                else if (docelowe == true) return Dictionary.KolorDocelowe;
                else return Dictionary.KolorWolne;
            }
        }
    }
}
