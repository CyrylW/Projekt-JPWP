using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ProjektCCS
{
    public partial class Form1 : Form
    {
        private readonly HeartManager _heartManager;
        private readonly GameManager _gameManager;
        private readonly LevelManager _levelManager;
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();

            _levelManager = new LevelManager(label_point_result, label_level,timer1);
            List<PictureBox> heartList = new List<PictureBox>();
            
            heartList.Add(pictureBox_heart_1);
            heartList.Add(pictureBox_heart_2);
            heartList.Add(pictureBox_heart_3);
            heartList.Add(pictureBox_heart_4);
            heartList.Add(pictureBox_heart_5);
            _heartManager = new HeartManager(heartList);

            List<PictureBox> shapeList = new List<PictureBox>();
            shapeList.Add(pictureBox_shape_circle);
            shapeList.Add(pictureBox_shape_square);
            shapeList.Add(pictureBox_shape_triangle);

            _gameManager = new GameManager(
                pictureResoult, 
                shapeList, 
                imageList2, 
                panelGame, 
                _heartManager,
                _levelManager,
                labelInfo);

            _gameManager.WaitngForStart("Kliknij przycisk aby rozpocz¹æ!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _gameManager.Start();
        }

        private void pictureBox_shape_circle_Click(object sender, EventArgs e)
        {
            if(pictureBox_shape_circle.Visible)
            {
                pictureBox_shape_circle.Visible = false;
                _gameManager.CheckIsCorrect(ShapeType.circle);
            }
        }

        private void pictureBox_shape_square_Click(object sender, EventArgs e)
        {
            if(pictureBox_shape_square.Visible) 
            {
                pictureBox_shape_square.Visible = false;
                _gameManager.CheckIsCorrect(ShapeType.square);
            }
        }

        private void pictureBox_shape_triangle_Click(object sender, EventArgs e)
        {
            if (pictureBox_shape_triangle.Visible)
            {
                pictureBox_shape_triangle.Visible = false;
                _gameManager.CheckIsCorrect(ShapeType.triangle);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            _levelManager.Time -= 0.1;
            if (_levelManager.Time <= 0)
            {
                _gameManager.TimeOut();
            };
        }
    }
}