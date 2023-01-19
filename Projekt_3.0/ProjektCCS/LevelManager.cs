using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ProjektCCS
{
    /// <summary>
    /// Klasa odpowiedzialna za zarządzenie poziomami
    /// </summary>
    internal class LevelManager
    {
        /// <summary>
        /// Aktualny poziom
        /// </summary>
        public int CurrentLevel = 0;
        /// <summary>
        /// Element odpowiedzialny za wyświetlanie poziiomu
        /// </summary>
        public Label LevelText;
        /// <summary>
        /// Ilość figur potrzebna do przejścia na nastepny poziom
        /// </summary>
        public int ShapeToNextLevel = 99999;
        /// <summary>
        /// Stoper 
        /// </summary>
        public double TimeToEnd = 99999;
        /// <summary>
        /// Czas na runde
        /// </summary>
        public double Time = 99999;
        /// <summary>
        /// Ilość punktów za poprawne wybranie figury
        /// </summary>
        public int PointForFigure;
        /// <summary>
        /// Ilośc punktów
        /// </summary>
        public int Point;
        /// <summary>
        /// Element odpowiedzialny za wyświetlanie ilości punktów
        /// </summary>
        public Label PointText;
        public Timer Timer;
        /// <summary>
        /// Lista przechowujące informacje o poziomach
        /// </summary>
        public readonly List<Level> Levels = new List<Level>()
        {
            new Level() { Id = 1, Point = 100, CountFigure = 5, Time = 1.5 },
            new Level() { Id = 2, Point = 1000, CountFigure = 5, Time = 1 },
            new Level() { Id = 3, Point = 10000, CountFigure = 5, Time = 0.75 }
        };

        public LevelManager(Label point, Label level, Timer timer)
        {
            this.PointText = point;
            this.LevelText = level;
            this.Timer = timer;
        }

        /// <summary>
        /// Ustawianie poziomu
        /// </summary>
        /// <param name="id">Poziom na który ma przejść</param>
        public void SetLevel(int id)
        {
            if(id > 0)
            {
                var level = Levels.FirstOrDefault(x => x.Id == id);
                if (level == null)
                {
                    level = Levels.Last();
                }
                CurrentLevel = level.Id;
                ShapeToNextLevel = level.CountFigure;
                TimeToEnd = level.Time;
                Time = level.Time;
                PointForFigure = level.Point;
                UpdateLevel();
            }
        }

        /// <summary>
        /// Inicalizacja ustawień dla pierwszego poziomu
        /// </summary>
        public void Start()
        {
            Point = 0;
            SetLevel(1);
            UpdatePoint();
        }

        /// <summary>
        /// Aktualizacja napisu ilości punktów na UI
        /// </summary>
        public void UpdatePoint()
        {
            PointText.Text = Point.ToString();
        }

        public void UpdateLevel()
        {
            LevelText.Text = CurrentLevel.ToString();
        }

        /// <summary>
        /// Resetowanie timera
        /// </summary>
        public void ResteTime()
        {
            Timer.Stop();
            Time = TimeToEnd;
            Timer.Start();
        }

        /// <summary>
        /// Dodanie punktów w zależności od aktualnego poziomu
        /// </summary>
        public bool AddPoint()
        {
            ShapeToNextLevel -= 1;
            Point += PointForFigure;
            UpdatePoint();
            if (ShapeToNextLevel == 0)
            {
                if (CurrentLevel == Levels.Count())
                {
                    Time = 99999;
                    return true;
                }
                CurrentLevel += 1;
                SetLevel(CurrentLevel);
            }

            ResteTime();
            return false;
        }

        public void Stop()
        {
            Time = 99999;
        }
    }
}
