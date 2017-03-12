using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkowanie
{

    //schemat ponizszych klas jest powtarzalny dlatego opisałem pierwszą i ostatnią klasę

    class OdlegloscY : AbsCollection<ChartItem3Value>
    {
        public OdlegloscY()
        {
            CreateData();
        }

        private void CreateData()
        {//zakresy w jakich operujemy
            min = -100 ;//zaBlisko
            mid = 0;//ok
            max = 100 ;//zaDaleko
            range = 100;
            LimitLeft = (int)(min-range);
            LimitRight = (int)(max+range);
            //tworzenie danych np ze dla odleglosci 50  min=0.5 mid=0.5 max=0.0
            for (int i = LimitLeft; i <= LimitRight; i++)
            {
                ChartItem3Value result = new ChartItem3Value();

                result.X = i;
                result.Min = RozmywanieTriangle(i, min, range);
                result.Mid = RozmywanieTriangle(i, mid, range);
                result.Max = RozmywanieTriangle(i, max, range);
                base.Add(result);
            }
        }
    }
    class RuchY : AbsCollection<ChartItem3Value>
    {
        public RuchY()
        {
            CreateData();
        }

        private void CreateData()
        {
            min = -2;//przed
            max = 2;//za parkingiem
            mid = 0;//ok
            range = 2;
            LimitLeft = (int)(min-range);
            LimitRight = (int)(max +range);
            for (int i = LimitLeft; i <= LimitRight; i++)
            {
                ChartItem3Value result = new ChartItem3Value();

                result.X = i;
                result.Min = RozmywanieTriangle(i, min, range);
                result.Mid = RozmywanieTriangle(i, mid, range);
                result.Max = RozmywanieTriangle(i, max, range);
                base.Add(result);
            }
        }
    }
    class OdlegloscX : AbsCollection<ChartItem3Value>
    {
        public OdlegloscX()
        {
            CreateData();
        }

        private void CreateData()
        {
            min = -100;//przed
            max = 100;//za parkingiem
            mid = 0;//ok
            range = 100;
            LimitLeft = -200;
            LimitRight = 200;
            for (int i = LimitLeft; i <= LimitRight; i++)
            {
                ChartItem3Value result = new ChartItem3Value();

                result.X = i;
                result.Min = RozmywanieTriangle(i, min, range);
                result.Mid = RozmywanieTriangle(i, mid, range);
                result.Max = RozmywanieTriangle(i, max, range);
                base.Add(result);
            }
        }
    }
    class RuchX : AbsCollection<ChartItem3Value>
    {
        public RuchX()
        {
            CreateData();
        }

        private void CreateData()
        {
            min = -10;//przed
            max = 10;//za parkingiem
            mid = 0;//ok
            range = 10;
            LimitLeft = -20;
            LimitRight = 20;
            for (int i = LimitLeft; i <= LimitRight; i++)
            {
                ChartItem3Value result = new ChartItem3Value();

                result.X = i;
                result.Min = RozmywanieTriangle(i, min, range);
                result.Mid = RozmywanieTriangle(i, mid, range);
                result.Max = RozmywanieTriangle(i, max, range);
                base.Add(result);
            }
        }
    }
    class ChartItem3Value
    {
        public int X { get; set; }
        public double Min { get; set; }
        public double Mid { get; set; }
        public double Max { get; set; }
        public double _ZbiorWnioskowania;
        public double ZbiorWnioskowania
        {
            get { return _ZbiorWnioskowania; }
            set
            {
                if (_ZbiorWnioskowania != value)
                {
                    _ZbiorWnioskowania = value;
                }
            }
        }
    }


    //klasa bazowa dla "rozmyć"
    abstract class AbsCollection<T> : List<T>
    {
        protected double min, mid, max, range;

        public int LimitLeft { get; set; }
        public int LimitRight { get; set; }

        //rozmywanie w postaci trojkąta dla ostrej wartosci
        protected double RozmywanieTriangle(double i, double center, double range)
        {
            double begin = center - range;
            double end = center + range;

            if ((i >= begin) && (i <= center)) return (i - begin) / (center - begin);
            else if ((i >= center) && (i <= end)) return (end - i) / (end - center);
            else return 0;
        }
    }
}
