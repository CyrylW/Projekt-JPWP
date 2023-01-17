namespace ProjektCCS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            int i = rd.Next(imageList2.Images.Count);
            pictureBox2.Image = imageList2.Images[i];
        }
    }
}