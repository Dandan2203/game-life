using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_life
{
    public class Logic
    {
        private bool[,] field;
        private const int rows = 40;
        private const int cols = 40;

        public Logic(int rows, int cols)
        {
            field = new bool[rows, cols];
        }

        private int Neighbours(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
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

        public void NextGeneration()
        {
            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = Neighbours(x, y);
                    var lifeNeighbour = field[x, y];

                    if (!lifeNeighbour && neighboursCount == 3)
                        newField[x, y] = true;
                    else if (lifeNeighbour && (neighboursCount < 2 || neighboursCount > 3))
                        newField[x, y] = false;
                    else
                        newField[x, y] = field[x, y];
                }
            }
            field = newField;
        }

        public bool[,] CurrentGeneration()
        {
            return field.Clone() as bool[,];
        }

        private bool ValidateCellPos(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private void UpdateCell(int x, int y, bool state)
        {
            if (ValidateCellPos(x, y))
                field[x, y] = state;
        }

        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, true);
        }

        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, false);
        }

        public void SaveFieldToFile(string filename)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    sb.Append(field[x, y] ? "1" : "0");
                }
                sb.AppendLine();
            }
            File.WriteAllText(filename, sb.ToString());
        }

        public void LoadFieldFromFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            for (int y = 0; y < Math.Min(rows, lines.Length); y++)
            {
                for (int x = 0; x < Math.Min(cols, lines[y].Length); x++)
                {
                    field[x, y] = lines[y][x] == '1';
                }
            }
        }

        public void ClearField()
        {
            field = new bool[rows, cols];
        }
    }
}
