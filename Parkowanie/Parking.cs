using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parkowanie
{
    class Parking:Panel
    {
        int SzerokoscJezdni = Dictionary.SzerokoscJezdni;
        double Skala = Dictionary.Skala;
        static Size RozmiarPojazdu=Dictionary.RozmiarPojazdu;
        List<MiejsceParkingowe> MiejscaParkingowe = new List<MiejsceParkingowe>();
        int offset=4;
        
        public Parking(Size clientRect)
        {
            this.DoubleBuffered = true;
            this.Size = new Size(clientRect.Height, clientRect.Height);
            this.BackColor = Color.DarkGray;
            utworzMiejscaParkingowe();
            Invalidate();
        }
        private void utworzMiejscaParkingowe()
        {
            for (int i = 3; i < Width / MiejsceParkingowe.Wymiary.Width; i++)
            {
                int x= (MiejsceParkingowe.Wymiary.Width + offset)*i;
                int y = 0;
                MiejscaParkingowe.Add(new MiejsceParkingowe() { zajete = false, docelowe = false, gorne = true,
                    rect = new Rectangle(new Point(x,y), MiejsceParkingowe.Wymiary),
                    polozenie = new Point(x, MiejsceParkingowe.Wymiary.Height) });

                y = (MiejsceParkingowe.Wymiary.Height + SzerokoscJezdni);
                MiejscaParkingowe.Add(new MiejsceParkingowe() { zajete = false, docelowe = false, gorne = false,
                    rect = new Rectangle(new Point(x,y), MiejsceParkingowe.Wymiary),
                    polozenie = new Point(x, y)});
            }
        }
        bool wybrano = false;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && wybrano==false)
            {
                for (int i = 0; i < MiejscaParkingowe.Count; i++)
                    if (MiejscaParkingowe[i].rect.Contains(e.Location) && MiejscaParkingowe[i].zajete == false)
                    {
                        MiejscaParkingowe[i].docelowe = true;
                        IndexParkingu = i;
                        wybrano = true;
                    }
                    else
                    {
                        MiejscaParkingowe[i].docelowe = false;
                    }
            }
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < MiejscaParkingowe.Count; i++)
                    if (MiejscaParkingowe[i].rect.Contains(e.Location) && !MiejscaParkingowe[i].docelowe)
                        MiejscaParkingowe[i].zajete = !MiejscaParkingowe[i].zajete;
            }
            this.Invalidate();
        }

        
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Width, Height));
            for (int i = 0; i < MiejscaParkingowe.Count; i++)
            {
                e.Graphics.FillRectangle(new SolidBrush(MiejscaParkingowe[i].Kolor), MiejscaParkingowe[i].rect);
            }
            Rectangle car = new Rectangle(carLocation, RozmiarPojazdu);
            RotateRectangle(e.Graphics, car, Kat);
        }
        public void RotateRectangle(Graphics g, Rectangle r, float angle)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(r.Left + (r.Width / 2),r.Top + (r.Height / 2)));
                g.Transform = m;
                g.FillRectangle(new SolidBrush(Color.Red), r);
                g.ResetTransform();
            }
        }

        Point carLocation = new Point(100, MiejsceParkingowe.Wymiary.Height + 20);
        float Kat = 0;
        int IndexParkingu = -1;
        public void przemieszczajSamochod()
        {
            if (IndexParkingu != -1)
            {
                if (SystemWnioskowania.Przygotowane == false)
                {
                    SystemWnioskowania.Rozmywanie(ref carLocation, MiejscaParkingowe[IndexParkingu], ref Kat);
                }
                else
                {
                    if (MiejscaParkingowe[IndexParkingu].gorne && Kat > -90)
                    {
                        Kat -= 5;
                        carLocation.X += 5;
                        carLocation.Y -= 2;
                    }
                    else if (!MiejscaParkingowe[IndexParkingu].gorne && Kat < 90)
                    {
                        Kat += 5;
                        carLocation.X += 5;
                        carLocation.Y += 2;
                    }
                    else if (!MiejscaParkingowe[IndexParkingu].gorne && Kat >= 90 && carLocation.Y<Height-Dictionary.RozmiarPojazdu.Width)
                    {
                        carLocation.Y += 2;
                    }
                    else if (MiejscaParkingowe[IndexParkingu].gorne && Kat >= -90 && carLocation.Y > 30)
                    {
                        carLocation.Y -= 2;
                    }
                }
                Invalidate();
            }
        }
    }
}
