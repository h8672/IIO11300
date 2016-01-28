using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava2
{
    class BLLotto
    {
        #region Attribuutit
        private int game;
        static Random rnd = new Random();
        #endregion
        #region Ominaisuudet
        public int Game {
            get { return game; }
            set { game = value; }
        }
        #endregion
        #region Rakentajat
        #endregion
        #region Funktiot
        static public string GameName(int ID) {
            if (ID == 0) return "Suomi";
            else if (ID == 1) return "VikingLotto";
            else if (ID == 2) return "Eurojackpot";
            else return "";
        }
        static public int[] DrawNumber(int ID)
        {
            int[] luvut;
            if (ID == 0) {
                luvut = new int[7];
                for (int i = 0; i < 7; i++)
                {
                    luvut[i] = (int)(39 * rnd.NextDouble());
                }
                return luvut;
            }
            else if (ID == 1)
            {
                luvut = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    luvut[i] = (int)(48 * rnd.NextDouble());
                }
                return luvut;
            }
            else if (ID == 2)
            {
                luvut = new int[7];
                //Päänumerot
                for (int i = 0; i < 5; i++)
                {
                    luvut[i] = (int)(49 * rnd.NextDouble() + 1);
                }
                //Tähtinumerot
                for (int i = 5; i < 7; i++)
                {
                    luvut[i] = (int)(7 * rnd.NextDouble() + 1);
                }
                return luvut;
            }
            else return null;
        }
        #endregion
    }

}
