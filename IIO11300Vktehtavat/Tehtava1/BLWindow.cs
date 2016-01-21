using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class Ikkuna
    {
        #region Muuttujat
        private double korkeus;
        private double leveys;

        #endregion
        #region Ominaisuudet (properties)
        //Olio suunnittelun aikana on päätetty että pinta-ala ja hinta ovat read-only ominaisuuksia
        public double PintaAla { get { return korkeus * leveys; } }
        public double Hinta { get { return LaskeHinta(); } }
        public double Korkeus
        {
            get { return korkeus; }
            set { korkeus = value; }
        }
        public double Leveys
        {
            get { return leveys; }
            set { leveys = value; }
        }
        
        #endregion
        #region Konstruktorit (constructors)
        #endregion
        #region Metodit (methods)
        public double LaskePintaAla()
        {
            return korkeus * leveys;
        }
        public float LaskeHinta()
        {
            //Hinta lasketaan työn, materiaalin ja katteen mukaan
            float kate = 100;
            float tyo = 200;
            float materiaali;
            materiaali = (float)(leveys * korkeus / 1000000) * 100;
            return (kate + tyo + materiaali);
        }
        #endregion
    }
    public class IkkunaVE0
    {
        //Esa ei suosittele, iha tosi?
        public double korkeus;
        public double leveys;
        public double LaskePintaAla()
        {
            return korkeus * leveys;
        }
    }
    public class BusinessLogicWindow
    {
        /// <summary>
        /// CalculatePerimeter calculates the perimeter of a window
        /// </summary>
        
        public static double CalculatePerimeter(double width, double height)
        {
            if (width <= 0 || height <= 0)
                throw new System.NotImplementedException();
            else
                return width * height;
        }
        public static double Calculate(double width, double height, double border)
        {
            if (width <= 0 || height <= 0 || border <= 0)
            {
                throw new System.NotImplementedException();
            }
            else
            {
                return (width + border * 2) * 2 + (height + border * 2) * 2;
            }

        }
        public static double CalculatePerimeter2(double width, double height, double border)
        {
            if (width <= 0 || height <= 0 || border <= 0)
            {
                throw new System.NotImplementedException();
            }
            else
            {
                //Laskee leikatut kulmat
                double leikkaus = (CalculatePerimeter(border, border) / 2) * 4;
                //Laskee pystysivun pituuden
                double heightside = border * 2 + height;
                //Laskee leveyssivun pituuden
                double widthside = border * 2 + width;
                //Lasketaan karmin pinta-ala
                double perimeter = CalculatePerimeter(heightside, border)
                    + CalculatePerimeter(widthside, border) - leikkaus;
                return perimeter;
            }

        }
    }
}
