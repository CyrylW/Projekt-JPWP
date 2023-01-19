using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProjektCCS
{
    /// <summary>
    /// Klasa odpowiedzialna za zarządzanie sercami
    /// </summary>
    internal class HeartManager
    {
        /// <summary>
        /// Stała która informuje ile jest serc napoczatku każdego poziomu
        /// </summary>
        private const int INIT_HEART = 5;
        /// <summary>
        /// Ilośc serc
        /// </summary>
        public int Hearts { get; set; }
        /// <summary>
        /// List obrazków serc
        /// </summary>
        public List<PictureBox> HeartsPicture { get; set;}

        public HeartManager(List<PictureBox> pictureHeart)
        {
            this.HeartsPicture = pictureHeart;
            ResetHearts();
        }

        /// <summary>
        /// Funkcja odpowiedzialna za odejmowanie serc
        /// </summary>
        /// <param name="cout">ile serc ma zostać odjetych</param>
        /// <returns>Zwrócenie aktualnej ilości serc</returns>
        public int LostHeart(int cout)
        {
            Hearts -= cout;
            UpdateVisable();
            return Hearts;
        }

        /// <summary>
        /// Wczytanie początkowej ilości serc
        /// </summary>
        public void ResetHearts()
        {
            Hearts = INIT_HEART;
            UpdateVisable();
        }

        /// <summary>
        /// Funkcja odpowiedzalna za wyświetlanie ilości serc w formie graficznej
        /// </summary>
        private void UpdateVisable()
        {
            for (int i = 0; i < INIT_HEART; i++)
            {
                if (i < Hearts)
                {
                    HeartsPicture[i].Show();
                }
                else
                {
                    HeartsPicture[i].Hide();
                }
            }
        }
    }
}
