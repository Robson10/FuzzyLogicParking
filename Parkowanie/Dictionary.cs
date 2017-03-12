using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkowanie
{
    public static class Dictionary
    {
        //najlepiej nic tu nie zmieniac procz kolorów i TimerInterval
        public static double Skala = 0.2;//skala 
        private static int _SzerokoscJezdni = 600;//określenie szerokości jezdni 
        public static int SzerokoscJezdni { get { return (int) (_SzerokoscJezdni* Skala); } }

        private static Size _RozmiarPojazdu = new Size(400,200);
        public static Size RozmiarPojazdu { get { return new Size((int)(_RozmiarPojazdu.Width * Skala), (int)(_RozmiarPojazdu.Height * Skala)); } }

        private static Size _RozmiarMiejscaParkingowego = new Size(250, 600);
        public static Size RozmiarMiejscaParkingowego { get { return new Size((int)(_RozmiarMiejscaParkingowego.Width * Skala), (int)(_RozmiarMiejscaParkingowego.Height * Skala)); } }


        public static Color KolorZajete = Color.Red;
        public static Color KolorDocelowe = Color.Green;
        public static Color KolorWolne = Color.DarkGray;

        public static int TimerInterval = 25;
    }
}
