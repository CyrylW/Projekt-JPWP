using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        /// Ilość figur potrzebna do przejścia na nastepny poziom
        /// </summary>
        public int ShapeToNextLevel = 0;
        /// <summary>
        /// Stoper 
        /// </summary>
        public int TimeToEnd = 0;
        /// <summary>
        /// Czas na runde
        /// </summary>
        public int Time = 0;
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

        /// <summary>
        /// Lista przechowujące informacje o poziomach
        /// </summary>
        public readonly List<Level> Levels = new List<Level>()
        {
            new Level() { Id = 1, Point = 100, CountFigure = 5, Time = 10 },
            new Level() { Id = 2, Point = 1000, CountFigure = 5, Time = 5 },
            new Level() { Id = 3, Point = 10000, CountFigure = 5, Time = 3 }
        };

        public LevelManager(Label point)
        {
            this.PointText = point;
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

        /// <summary>
        /// Resetowanie timera
        /// </summary>
        public void ResteTime()
        {
            Time = TimeToEnd;
        }

        /// <summary>
        /// Dodanie punktów w zależności od aktualnego poziomu
        /// </summary>
        public void AddPoint()
        {
            ShapeToNextLevel -= 1;
            Point += PointForFigure;
            if (ShapeToNextLevel == 0)
            {
                CurrentLevel += 1;
                SetLevel(CurrentLevel);
            }

            ResteTime();
            UpdatePoint();
        }
    }
}
