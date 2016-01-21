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

        

        public static double CalculatePerimeter(double widht, double height)
        {
            if (widht <= 0 | height <= 0)
                throw new System.NotImplementedException();
            else
                return widht * height;
        }
    }
}
