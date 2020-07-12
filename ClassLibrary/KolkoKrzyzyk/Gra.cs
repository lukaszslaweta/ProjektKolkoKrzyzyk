using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClassLibrary.KolkoKrzyzyk
{
    public class Gra
    {
        public enum RUCH { KOMPUTERA, CZLOWIEKA }
        private RUCH Ruch;
        private Plansza Pola;
        private Plansza.OBIEKT Obiekt = Plansza.OBIEKT.kolko;
        private Plansza.OBIEKT Wygrana = Plansza.OBIEKT.puste;
        private int Size;
        public enum WYNIK_GRY { WYGRANA_KOLKO, WYGRANA_KRZYZYK, REMIS, W_TRAKCIE}
        public WYNIK_GRY Wynik;
        public Gra(int size, RUCH ruch)
        {
            Pola = new Plansza(size, size);
            Size = size-1; // zero-based
            Ruch = ruch;
            Wynik = WYNIK_GRY.W_TRAKCIE;
        }

        public bool Rusz(int x, int y)
        {
            if (Ruch != RUCH.CZLOWIEKA || Pola.Pole(x, y) != Plansza.OBIEKT.puste) return false;
            Pola.Zaznacz(Obiekt, x, y);
            if (Pola.SprawdzWygrana()) Wygrana = Obiekt;
            else ZmienRuch();
            return true;
        }


        public WYNIK_GRY CzyWygrana()
        {
            switch (Wygrana)
            {
                case Plansza.OBIEKT.kolko:
                    return WYNIK_GRY.WYGRANA_KOLKO;
                case Plansza.OBIEKT.krzyzyk:
                    return WYNIK_GRY.WYGRANA_KRZYZYK;
            }
            if (!Pola.CzySaWolnePola()) return WYNIK_GRY.REMIS;
            return WYNIK_GRY.W_TRAKCIE;
        }

        /// <summary>
        ///  Zwraca co znajduje sie w grze na danych wspolrzednych
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>0 - jesli puste, 1 jak kolko, 2 jak krzyzyk</returns>
        public int Pole(int x, int y)
        {
            if (x < 0 || x > Size || y < 0 || y > Size) return 0;
            switch(Pola.Pole(x, y))
            {
                case Plansza.OBIEKT.kolko:
                    return 1;
                case Plansza.OBIEKT.krzyzyk:
                    return 2;
            }
            return 0;
        }

        private void ZmienRuch()
        {
            if (Obiekt == Plansza.OBIEKT.kolko) Obiekt = Plansza.OBIEKT.krzyzyk;
            else Obiekt = Plansza.OBIEKT.kolko;
            if (Ruch == RUCH.CZLOWIEKA)
            {
                Ruch = RUCH.KOMPUTERA;
                RuchKomputera();
            }
            else Ruch = RUCH.CZLOWIEKA;
        }

        private void RuchKomputera()
        {
            int x = -1, y = -1, max=-1;
            for(int i = 0; i <= Size; i++)
            {
                for(int j = 0; j <= Size; j++)
                {
                    int wartoscRuchu = ObliczWartoscRuchu(i, j);
                    // element losowosci ;)
                    if (max == wartoscRuchu && (new Random().Next() % 5 > 2)) continue;
                    if(wartoscRuchu >= max)
                    {
                        max = wartoscRuchu;
                        x = i;
                        y = j;
                    }
                }
            }
            if(x != -1 || y != -1)
            {
                Pola.Zaznacz(Obiekt, x, y);
                if (Pola.SprawdzWygrana()) Wygrana = Obiekt;
                ZmienRuch();
            }
        }

        private int WartoscPola(int x, int y)
        {
            Plansza.OBIEKT o = Pola.Pole(x, y);
            if (o == Obiekt)
            {
                return 5;
            }
            else if(o == Plansza.OBIEKT.puste)
            {
                return 1;
            }
            else
            {
                return 6;
            }
        }

        private int SprawdzCzyPowodujeWygrana(int x, int y)
        {
            Plansza.OBIEKT przeciwnik = Obiekt == Plansza.OBIEKT.kolko ? Plansza.OBIEKT.krzyzyk : Plansza.OBIEKT.kolko;
            Pola.Zaznacz(przeciwnik, x, y);
            if (Pola.SprawdzWygrana())
            {
                Pola.Zaznacz(Plansza.OBIEKT.puste, x, y);
                return 200;
            }
            Pola.Zaznacz(Obiekt, x, y);
            if (Pola.SprawdzWygrana())
            {
                Pola.Zaznacz(Plansza.OBIEKT.puste, x, y);
                return 300;
            }
            Pola.Zaznacz(Plansza.OBIEKT.puste, x, y);
            return 0;
        }

        private int ObliczWartoscRuchu(int x, int y)
        {
            int wartosc = -1;
            if (Pola.Pole(x, y) != Plansza.OBIEKT.puste) return wartosc;
            for (int i = x - 1; i < x + 1; i++) 
            {
                for (int j = y - 1; j < y + 1; j++) 
                {
                    if (i == x && j == y)
                    {
                        wartosc += SprawdzCzyPowodujeWygrana(x, y);
                        continue;
                    }
                    if (i < 0 || j < 0 || i > Size || j > Size) wartosc -= 1;
                    else wartosc += WartoscPola(i, j);
                }
            }
            return wartosc;
        }
    }
}
