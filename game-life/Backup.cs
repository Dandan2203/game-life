using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace game_life
{
    internal class Backup
    {

        public void save(string path, bool[,] field)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    sb.Append(field[x, y] ? "1" : "0");
                }
                sb.AppendLine();
            }
            File.WriteAllText(path, sb.ToString());

        }

        public bool[,] load(string path, int rows, int cols)
        {
            string[] lines = File.ReadAllLines(path);
            bool[,] field = new bool[rows, cols];
            for (int y = 0; y < Math.Min(rows, lines.Length); y++)
            {
                for (int x = 0; x < Math.Min(cols, lines[y].Length); x++)
                {
                    field[x, y] = lines[y][x] == '1';
                }
            }

            return field;
        }
    }
}
