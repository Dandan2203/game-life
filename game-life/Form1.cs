using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace game_life
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private bool[,] field;
        private const int rows  = 40;
        private const int cols  = 40;
        private const int cellSize = 15;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartGane()
        {
            if (timer.Enabled)
                return;

            field = new bool[cols, rows];

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer.Start();
        }

        private void Generation()
        {
            graphics.Clear(Color.White);

            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = Neighbours(x, y);
                    var lifeNeighbour = field[x, y];

                    if(!lifeNeighbour && neighboursCount == 3)
                        newField[x, y] = true;
                    else if ( lifeNeighbour && (neighboursCount < 2 || neighboursCount > 3) )   
                        newField[x, y] = false;
                    else
                        newField[x, y] = field[x, y];

                    if (lifeNeighbour)
                        graphics.FillRectangle(Brushes.Black, x * cellSize, y * cellSize, cellSize - 1, cellSize - 1);
                }
            }
            field = newField;
            pictureBox1.Refresh();
        }

        private int Neighbours(int x, int y)
        {
            int count = 0;

            for (int i = -1;i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    var isSelfCheck = col == x && row == y;
                    var lifeNeighbour = field[col, row];

                    if (lifeNeighbour && !isSelfCheck)
                        count++;
                }
            }
            return count;
        }
        private void StopGame()
        {
            if (!timer.Enabled)
                return;
            timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Generation();
            timer.Interval = SpeedReg.Value;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGane();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled) 
                return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / 15;
                var y = e.Location.Y / 15;
                var validationPassed = ValidateMousePos(x, y);
                if (validationPassed)
                    field[x, y] = true;
            }
            
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / 15;
                var y = e.Location.Y / 15;
                var validationPassed = ValidateMousePos(x, y);
                if (validationPassed)
                    field[x, y] = false;
            }
        }

        private bool ValidateMousePos(int x , int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }
    }
}