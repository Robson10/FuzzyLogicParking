using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parkowanie
{
    public partial class Form1 : Form
    {
        Parking Parking;

        public Form1()
        {
            InitializeComponent();
            Width = 390;//szerokosc okna
            Height = 410;//wysokosc okna
            Parking =  new Parking(ClientSize);//utworzenie Parkingu o rozmiarze okna-obszaru dla uzytkownika-bez ramki
            Controls.Add(Parking);//Dodanie controlki do Form1 (czyli do głównego okienka)
            timer1.Start(); //wystartowanie licznika czasu
            timer1.Interval = Dictionary.TimerInterval;//ustawienie 25ms jako czas co ile jest wywolywana jego metoda Tick
        }
        //Metoda od Timera wywolywana co 25 ms
        private void timer1_Tick(object sender, EventArgs e)
        {
           Parking.przemieszczajSamochod(); //metoda z klasy parking. Przemieszcza samochód
        }
    }
}
