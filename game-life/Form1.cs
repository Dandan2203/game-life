using System;
using System.IO;
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
        private Logic logic;
        private Timer timer;
        private const int cellSize = 15;

        public Form1()
        {
            InitializeComponent();
            logic = new Logic(40, 40);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        private void StartGame()
        {
            if (!timer.Enabled)
            {
                timer.Start();
                StartButton.Enabled = false;
                StopButton.Enabled = true;
            }
        }

        private void StopGame()
        {
            if (timer.Enabled)
            {
                timer.Stop();
                StartButton.Enabled = true;
                StopButton.Enabled = false;
            }
        }

        private void ResetGaame()
        {
            StopGame();
            logic.ClearField();
            graphics.Clear(Color.White);
            pictureBox1.Refresh();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = SpeedReg.Value;
            DrawGeneration();
        }

        private void DrawGeneration()
        {
            graphics.Clear(Color.White);
            var field = logic.CurrentGeneration();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y])
                    {
                        graphics.FillRectangle(Brushes.Black, x * cellSize, y * cellSize, cellSize - 2, cellSize - 2);
                    }
                }
            }

            pictureBox1.Refresh();
            logic.NextGeneration();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetGaame();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.Location.X / cellSize;
            int y = e.Location.Y / cellSize;

            if (e.Button == MouseButtons.Left)
            {
                logic.AddCell(x, y);
                pictureBox1.Refresh();
            }

            if (e.Button == MouseButtons.Right)
            {
                logic.RemoveCell(x, y);
                pictureBox1.Refresh();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                logic.SaveFieldToFile(saveFileDialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                logic.LoadFieldFromFile(openFileDialog.FileName);
                DrawGeneration();
            }
        }
    }
}