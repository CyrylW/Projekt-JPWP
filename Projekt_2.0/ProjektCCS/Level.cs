using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektCCS
{
    /// <summary>
    /// Klasa przechowująca podstawowe informacje o poziomie
    /// </summary>
    internal class Level
    {
        /// <summary>
        /// Numer poziomu
        /// </summary>
        public int Id;
        /// <summary>
        /// Ilośc punktów za poprawny wybór
        /// </summary>
        public int Point;
        /// <summary>
        /// Ilośc figur potrzebna do przejścia na kolejny poziom
        /// </summary>
        public int CountFigure;
        /// <summary>
        /// Ile czasu jest na poziom
        /// </summary>
        public int Time;
    }
}
