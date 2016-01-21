using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava1
{
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
