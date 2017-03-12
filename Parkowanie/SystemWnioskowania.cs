using System;
using System.Drawing;
using System.Linq;

namespace Parkowanie
{
    public static class SystemWnioskowania
    {
        static OdlegloscX OdlegloscX = new OdlegloscX();//przy danej odleglosci jakie są wartosci min mid max
        static RuchX ruchX = new RuchX();//ile pixeli ma przejechać
        static OdlegloscY OdlegloscY = new OdlegloscY();//przy danej odleglosci jakie są wartosci min mid max
        static RuchY ruchY = new RuchY();//ile pixeli ma przejechać

        public static bool Przygotowane = false;
        static int kierunek = 1;

        public static void Rozmywanie(ref Point CarLocation, MiejsceParkingowe parking, ref float Kat)
        {
            int odlegloscx = parking.polozenie.X - CarLocation.X - (Dictionary.RozmiarPojazdu.Width * 5 / 4);//określenie odległości pojazdu od parkingu oraz doliczenie offsetu
            int odlegloscy = parking.polozenie.Y - CarLocation.Y - ((parking.gorne) ? -Dictionary.RozmiarPojazdu.Height : (Dictionary.RozmiarPojazdu.Height * 2));//określenie odległości pojazdu od parkingu oraz doliczenie offsetu w zależnosci od tego czy parkujemy na gorze czy na dole
            int ymove = LiczRuchPoY(odlegloscy);//wyliczenie o ile po osi y powinien sie przesunąć pojazd
            if (odlegloscx == 0 && odlegloscy == 0) //jeżeli odleglosci sie nie zmieniają flaga Przygotowane zmieniana jest na true a nastepnie wykonywany jest kod odpowiedzialny za manewr parkowania
                Przygotowane = true;
            else
                Przygotowane = false;
            CarLocation.Y += ymove;//przesuniecie pojazdu

            if (odlegloscy != 0)//jeżeli pojazd jeszcze nie znajduje sie na docelowej pozycji Y nie interesuje nas pozycja X wiec samochód jezdzi tylko po to by nie jezdził bokiem
            {
                Kat += (parking.gorne) ? ymove : -ymove;//obracanie pojazdu w razie skręcania
                kierunek = (parking.gorne) ? 1 : -1;
                CarLocation.X += LiczRuchPoX(odlegloscy * kierunek);//przemieszczanie pojazdu w odpowiednią stronę względem przemieszczania sie po osi Y
            }
            else
            {
                if (Kat != 0) //wyrownywanie toru jazdy - ma być prostopadle do parkingu
                {
                    Kat = (int)Math.Round(Kat + ((Kat < 0) ? 1 : -1));//zmniejszanie kąta do 0
                    CarLocation.X += LiczRuchPoX(kierunek * 30);//przesówanie pojazdu zeby go wyrównać czyli znow zeby nie jezdził bokiem
                }
                else //w końcu kiedy Y jest osiągnięty pora na X
                    CarLocation.X += LiczRuchPoX(odlegloscx);//określanie ile ma przejechać by trafić na docelowy punkt X
            }
        }
        private static int LiczRuchPoX(int odleglosc)
        {
            int x = 0;

            if (OdlegloscX[OdlegloscX.Count - 1].X < odleglosc) x = BlokWnioskowaniaX(1, 0, 0);//jeżeli jesteśmy poza skalą(z prawej strony) i pkt docelowy jest przed nami ustawiamy max=1
            else if (OdlegloscX[0].X > odleglosc) x = BlokWnioskowaniaX(0, 0, 1);//jeżeli jesteśmy poza skalą(z lewej strony) i pkt docelowy jest za nami ustawiamy min=1
            else//w przeciwnym razie szukamy wartosci min,mid,max z tablicy dla określonej odleglosci
                for (int i = 0; i < OdlegloscX.Count; i++)
                    if (OdlegloscX[i].X == odleglosc) x = BlokWnioskowaniaX(OdlegloscX[i].Max, OdlegloscX[i].Mid, OdlegloscX[i].Min);
            return x;
        }
        private static int BlokWnioskowaniaX(double MaxX, double MidX, double MinX)
        {
            //tu jest kod opowiedzialny za liczenie środka ciezkosci.
            double value = 0;
            double up = 0;
            double above = 0;
            for (int i = 0; i < ruchX.Count; i++)
            {
                value = Max(Min(ruchX[i].Min, MinX), Min(ruchX[i].Mid, MidX), Min(ruchX[i].Max, MaxX));
                up += ruchX[i].X * value;
                above += value;
            }
            double result = (above == 0) ? 0 : (up / above);
            return (int)((result < 0) ? Math.Floor(result) : Math.Ceiling(result));
        }
        //te same komentarze co dla LiczRuchPoX i BlokWnioskowaniaX
        private static int LiczRuchPoY(int odleglosc)
        {
            int y = 0;

            if (OdlegloscY[OdlegloscY.Count - 1].X < odleglosc)
                y = BlokWnioskowaniaY(1, 0, 0);
            else if (OdlegloscY[0].X > odleglosc)
                y = BlokWnioskowaniaY(0, 0, 1);
            else
                for (int i = 0; i < OdlegloscY.Count; i++)
                    if (OdlegloscY[i].X == odleglosc)
                    {
                        y = BlokWnioskowaniaY(OdlegloscY[i].Max, OdlegloscY[i].Mid, OdlegloscY[i].Min);
                        break;
                    }
            return y;
        }
        private static int BlokWnioskowaniaY(double MaxX, double MidX, double MinX)
        {
            double value = 0;
            double up = 0;
            double above = 0;
            for (int i = 0; i < ruchY.Count; i++)
            {
                value = Max(Min(ruchY[i].Min, MinX), Min(ruchY[i].Mid, MidX), Min(ruchY[i].Max, MaxX));
                up += ruchY[i].X * value;
                above += value;
            }
            double result = (above == 0) ? 0 : (up / above);
            return (int)((result < 0) ? Math.Floor(result) : Math.Ceiling(result));
        }
        private static double Min(params double[] Values)
        {
            return Values.Min();
        }
        private static double Max(params double[] Values)
        {
            return Values.Max();
        }

    }
}



