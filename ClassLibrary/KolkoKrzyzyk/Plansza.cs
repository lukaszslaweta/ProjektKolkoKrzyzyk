using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.KolkoKrzyzyk
{
    class Plansza
    {
        private int X;
        private int Y;
        public enum OBIEKT { puste, kolko, krzyzyk }
        private Dictionary<int, Dictionary<int, OBIEKT>> Tablica;
        public Plansza(int x, int y)
        {
            X = x;
            Y = y;
            Tablica = new Dictionary<int, Dictionary<int, OBIEKT>>();
            Czysc();
        }

        public bool Zaznacz(OBIEKT obj, int x, int y)
        {
            Tablica[y][x] = obj;
            return true;
        }

        public OBIEKT Pole(int x, int y)
        {
            return Tablica[y][x];
        }
        public bool CzySaWolnePola()
        {
            foreach(Dictionary<int, OBIEKT> x in Tablica.Values)
            {
                foreach(OBIEKT y in x.Values)
                {
                    if (y == OBIEKT.puste) return true;
                }
            }
            return false;
        }
        public bool SprawdzWygrana()
        {
            for (int i = 0; i < X; i++)
            {
                for(int j = 0; j < Y; j++)
                {
                    if(SprawdzPionowo(i, j) || SprawdzPoziomo(i, j) || SprawdzUkos(i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool SprawdzPionowo(int x, int y)
        {
            if (Tablica[x][y] == OBIEKT.puste) return false;
            OBIEKT typ = Tablica[x][y];
            int count = 1;
            for (int i = y + 1; i < Y; i++)
            {
                if (Tablica[x][i] != typ) break;
                count++;
            }
            return count >= 3;
        }
        private bool SprawdzPoziomo(int x, int y)
        {
            if (Tablica[x][y] == OBIEKT.puste) return false;
            OBIEKT typ = Tablica[x][y];
            int count = 1;
            for (int i = x + 1; i < X; i++)
            {
                if (Tablica[i][y] != typ) break;
                count++;
            }
            return count >= 3;
        }
        private bool SprawdzUkos(int x, int y)
        {
            if (Tablica[x][y] == OBIEKT.puste) return false;
            OBIEKT typ = Tablica[x][y];
            int count = 1;
            for (int i = x + 1, j = y + 1; i < X && j < Y; )
            {
                if (Tablica[i][j] != typ) break;
                count++;
                j++; 
                i++;
            }
            if( count >= 3) return true;
            count = 1;
            for (int i = x - 1, j = y + 1; i >= 0 && j < Y;)
            {
                if (Tablica[i][j] != typ) break;
                count++;
                j++;
                i--;
            }
            return count >= 3;
        }
        private void Czysc()
        {
            for (int i = X - 1; i >= 0; i--)
            {
                Tablica[i] = new Dictionary<int, OBIEKT>();
                for (int j = Y - 1; j >= 0; j--)
                {
                    Tablica[i][j] = OBIEKT.puste;
                }
            }
        }
    }
}
