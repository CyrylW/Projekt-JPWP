using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektCCS
{
    /// <summary>
    /// Klasa odpowiedzialna za zarządzenie całą rozgrywką, rysowanie figur/serc
    /// </summary>
    internal class GameManager
    {
        /// <summary>
        /// Id figury która jest oczekiwana do wyboru
        /// </summary>
        public int CurrectShapeId;
        /// <summary>
        /// Element do wyświetlania aktualnego obrazku oczekiwanego do wyboru
        /// </summary>
        public PictureBox correctShapePicture;
        /// <summary>
        /// List przechowująca rysowane figury
        /// </summary>
        public List<PictureBox> displayedShapePicture;
        /// <summary>
        /// List wszystkich dostepnych figur
        /// </summary>
        public ImageList listShape;
        /// <summary>
        /// Element na którym bedą umieszczane elementy
        /// </summary>
        public Panel panelGame;
        /// <summary>
        /// Element odpowiedzialny za zarządzanie sercami
        /// </summary>
        public HeartManager heartManager;
        /// <summary>
        /// Element odpowiedzialny za zarządzenie poziomami
        /// </summary>
        public LevelManager levelManager;

        public Label labelInfo;


        private Random _rnd = new Random();

        public GameManager(PictureBox correctShapePicture, 
            List<PictureBox> displayedShapePicture, 
            ImageList listShape, 
            Panel panelGame, 
            HeartManager heartManager,
            LevelManager levelManager,
            Label labelInfo)
        {
            this.correctShapePicture = correctShapePicture;
            this.displayedShapePicture = displayedShapePicture;
            this.listShape = listShape;
            this.panelGame = panelGame;
            this.heartManager = heartManager;
            this.labelInfo = labelInfo;
            this.levelManager = levelManager;
        }
        /// <summary>
        /// Uruchamienia początkowych ustawien
        /// </summary>
        public void Start()
        {
            labelInfo.Visible = false;
            levelManager.Start();
            heartManager.ResetHearts();
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            GenerateCorrectSymbol();
            GeneratePlaceShape();
            if (heartManager.Hearts == 0)
            {
                Start();
            }
        }

        /// <summary>
        /// Funkcja odpowiedzlna za mechanikę wrazie przekroczenia czasu
        /// </summary>
        public void TimeOut()
        {
            heartManager.LostHeart(1);
            
            if (heartManager.Hearts == 0)
            {
                WaitngForStart($"Twoj wynik: {levelManager.Point}");
            }
            else
            {
                levelManager.ResteTime();
                Update();
            }
            
        }

        /// <summary>
        /// Funkcja odpowiedzialna za wybranie sumbolu z listy
        /// </summary>
        private void GenerateCorrectSymbol()
        {
            CurrectShapeId = _rnd.Next(listShape.Images.Count);
            correctShapePicture.Image = listShape.Images[CurrectShapeId];
        }

        /// <summary>
        /// Funkcja odpowiedzialna za rozmieszczenie i skalowanie figur
        /// </summary>
        private void GeneratePlaceShape()
        {
            SetVisableShapesPicture(false);

            foreach (var picturShape in displayedShapePicture)
            {
                int sizexy = _rnd.Next(100, 300);
                picturShape.Size = new Size(sizexy, sizexy);

                picturShape.Location = RandomPoint(picturShape);
                picturShape.Visible = true;
            }
        }

        /// <summary>
        /// Funkcja odpowiedzialna za ustawienie widoczności figur
        /// </summary>
        /// <param name="isVisable">czy ma być widoczna</param>
        private void SetVisableShapesPicture(bool isVisable)
        {
            foreach (var picturShape in displayedShapePicture)
            {
                picturShape.Visible = isVisable;
            }
        }

        /// <summary>
        /// Funkcja losuje polożenie figur
        /// </summary>
        /// <param name="pictureBox">Figura która jest rozmieszczana</param>
        /// <returns>Zwaraca cordy</returns>
        private Point RandomPoint(PictureBox pictureBox)
        {
            int maxX = panelGame.ClientSize.Width - pictureBox.Width;
            int maxY = panelGame.ClientSize.Height - pictureBox.Height;
            int randX, randY;
            Point cord;
            do
            {
                randX = _rnd.Next(maxX);
                randY = _rnd.Next(maxY);
                cord = new Point(randX, randY);
            } while (CheckIsInsiadeFigure(cord, pictureBox.Size));

            return cord;
        }

        /// <summary>
        /// Sprawdzenie czy figura nie koliduje z inną
        /// </summary>
        /// <param name="point">położenie figury</param>
        /// <param name="size">wielkość figury</param>
        /// <returns></returns>
        private bool CheckIsInsiadeFigure(Point point, Size size)
        {
            foreach (var shape in displayedShapePicture)
            {
                if (shape.Visible && shape.Bounds.IntersectsWith(new Rectangle(point.X, point.Y, size.Width, size.Height)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sprawdzenie czy figury jest poprawna z oczekiwaną
        /// </summary>
        /// <param name="idFigure"></param>
        public void CheckIsCorrect(ShapeType idFigure)
        {
            if (CurrectShapeId == (int)idFigure)
            {
                if (!levelManager.AddPoint())
                {
                    Update();
                }
                else
                {
                    WaitngForStart($"Twoj wynik: {levelManager.Point}");
                }
            }
            else
            {
                heartManager.LostHeart(1);
                if (heartManager.Hearts == 0)
                {
                    WaitngForStart($"Twoj wynik: {levelManager.Point}");
                }
            }
        }

        public void WaitngForStart(string stringText)
        {
            SetVisableShapesPicture(false);
            levelManager.Stop();
            labelInfo.Visible = true;
            labelInfo.Text = stringText;
        }
    }
}
