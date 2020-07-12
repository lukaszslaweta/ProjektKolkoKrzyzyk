using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClassLibrary.KolkoKrzyzyk;

namespace kolko_krzyzyk
{
    public partial class Form1 : Form
    {
        private const int KOLKO = 1;
        private const int KRZYZYK = 2;
        private Gra Game;
        private int GameSize = 3;
        private List<Point> kolka = new List<Point>();
        private List<Point> krzyzyki = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            Game = new Gra(GameSize, Gra.RUCH.CZLOWIEKA);
        }

        private void Klikniecie(object sender, MouseEventArgs e)
        {
            int x = e.X / (ClientSize.Width / GameSize);
            int y = e.Y / (ClientSize.Height / GameSize);
            if (x >= GameSize || y >= GameSize) return;
            if (!Game.Rusz(x, y))
            {
                MessageBox.Show(this, "Nie możesz tutaj ruszyć!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Invalidate();
            }
            DialogResult result;
            switch (Game.CzyWygrana())
            {
            case Gra.WYNIK_GRY.WYGRANA_KOLKO:
                result = MessageBox.Show(this, "Wygrało kółko! Chcesz zagrać jeszcze raz?", "Alert", MessageBoxButtons.YesNo);
                break;
            case Gra.WYNIK_GRY.WYGRANA_KRZYZYK:
                result = MessageBox.Show(this, "Wygrał krzyżyk! Chcesz zagrać jeszcze raz?", "Alert", MessageBoxButtons.YesNo);
                break;
            case Gra.WYNIK_GRY.REMIS:
                result = MessageBox.Show(this, "Remis! Chcesz zagrać jeszcze raz?", "Alert", MessageBoxButtons.YesNo);
                break;
            default:
                return;
            }
            if (result == DialogResult.Yes)
            {
                Game = new Gra(GameSize, Gra.RUCH.CZLOWIEKA);
                Invalidate();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Grafika grafika = new Grafika(e.Graphics, e.ClipRectangle.Width, e.ClipRectangle.Height, GameSize);
            grafika.RysujLinie();
            int x = 0, y = 0;
            for(int i = 0; i < Width; i += Width / GameSize)
            {
                for(int j = 0; j < Height; j += Height / GameSize)
                {
                    switch(Game.Pole(x, y))
                    {
                        case KOLKO:
                            grafika.RysujKolko(x, y);
                            break;
                        case KRZYZYK:
                            grafika.RysujKrzyzyk(x, y);
                            break;
                    }
                    y++;
                }
                y = 0;
                x++;
            }
            grafika.Dispose();
        }
    }
}
