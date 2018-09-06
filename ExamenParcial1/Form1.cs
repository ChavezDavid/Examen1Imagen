using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ExamenParcial1
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> inputImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    inputImage = new Image<Bgr, byte>(ofd.FileName);
                    imgImagen.Image = inputImage;
                    imgImagen.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rojoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Limpiar histogrma
            if (inputImage == null)
            {
                return;
            }

            htgRojo.ClearHistogram();

            DenseHistogram histRojo = new DenseHistogram(256, new RangeF(0, 255));
            histRojo.Calculate(new Image<Gray, byte>[] { inputImage[0] }, false, null);

            Mat m = new Mat();
            histRojo.CopyTo(m);

            htgRojo.AddHistogram("Histograma Rojo", Color.Red, m, 256, new float[] { 2, 255 });
            htgRojo.Refresh();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro que deseas salir?", "System Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputImage == null)
            {
                return;
            }

            Image<Gray, byte> imgCanny = new Image<Gray, byte>(inputImage.Width, inputImage.Height, new Gray(0));
            imgCanny = inputImage.Canny(50, 30);
            imgImagen2.Image = imgCanny;
            imgImagen2.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
        }
    }
}
