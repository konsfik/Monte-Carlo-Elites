using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    [Serializable]
    public class PM_Maze : IDeepCopyable, IEquatable<PM_Maze>
    {
        public Directions_Ortho_2D[,] cells;

        public PM_Maze(int width, int height)
        {
            this.cells = new Directions_Ortho_2D[width, height];
        }

        [JsonConstructor]
        public PM_Maze(Directions_Ortho_2D[,] cells)
        {
            this.cells = cells.DeepCopy();
        }

        private PM_Maze(PM_Maze maze_to_copy) {
            this.cells = maze_to_copy.cells.DeepCopy();
        }

        public object DeepCopy()
        {
            return new PM_Maze(this);
        }


        public override string ToString()
        {
            int plotWidth = 2 * this.Q_Width() + 1;
            int plotHeight = 2 * this.Q_Height() + 1;

            char[,] mazePlot = new char[plotWidth, plotHeight];

            for (int plotY = 0; plotY < plotHeight; plotY++)
            {
                for (int plotX = 0; plotX < plotWidth; plotX++)
                {
                    mazePlot[plotX, plotY] = 'X';
                    //if (plotX % 2 == 0 || plotY % 2 == 0)
                    //{
                    //    mazePlot[plotX, plotY] = 'X';
                    //}
                    //else
                    //{
                    //    mazePlot[plotX, plotY] = 'O';
                    //}
                }
            }

            for (int x = 0; x < this.Q_Width(); x++)
            {
                for (int y = 0; y < this.Q_Height(); y++)
                {
                    int plotX = 2 * x + 1;
                    int plotY = 2 * y + 1;
                    Vec2i plotPos = new Vec2i(plotX, plotY);
                    mazePlot[plotX, plotY] = ' ';

                    Vec2i plotPos_right = plotPos.To_Right();
                    Vec2i plotPos_left = plotPos.To_Left();
                    Vec2i plotPos_up = plotPos.To_Up();
                    Vec2i plotPos_down = plotPos.To_Down();

                    if (cells[x, y].HasFlag(Directions_Ortho_2D.U))
                    {
                        mazePlot[plotPos_up.x, plotPos_up.y] = ' ';
                    }
                    else
                    {
                        mazePlot[plotPos_up.x, plotPos_up.y] = 'X';
                    }

                    if (cells[x, y].HasFlag(Directions_Ortho_2D.D))
                    {
                        mazePlot[plotPos_down.x, plotPos_down.y] = ' ';
                    }
                    else
                    {
                        mazePlot[plotPos_down.x, plotPos_down.y] = 'X';
                    }

                    if (cells[x, y].HasFlag(Directions_Ortho_2D.L))
                    {
                        mazePlot[plotPos_left.x, plotPos_left.y] = ' ';
                    }
                    else
                    {
                        mazePlot[plotPos_left.x, plotPos_left.y] = 'X';
                    }

                    if (cells[x, y].HasFlag(Directions_Ortho_2D.R))
                    {
                        mazePlot[plotPos_right.x, plotPos_right.y] = ' ';
                    }
                    else
                    {
                        mazePlot[plotPos_right.x, plotPos_right.y] = 'X';
                    }
                }
            }

            string mazeString = "";
            for (int plotY = 0; plotY < plotHeight; plotY++)
            {
                for (int plotX = 0; plotX < plotWidth; plotX++)
                {
                    mazeString += mazePlot[plotX, plotY];
                }
                mazeString += "\n";
            }

            return mazeString;
        }

        #region equality override
        public bool Equals(PM_Maze other)
        {
            int width = this.Q_Width();
            int height = this.Q_Height();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (this.cells[x, y] != other.cells[x, y]) {
                        return false;
                    }
                }
            }

            return true;
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject is PM_Maze other_maze)
            {
                return Equals(other_maze);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int width = this.Q_Width();
            int height = this.Q_Height();

            int hash = 17;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    hash = (hash * 13) + cells[x, y].GetHashCode();
                }
            }

            return hash;
        }

        public static bool operator ==(PM_Maze c1, PM_Maze c2)
        {
            if (Object.ReferenceEquals(c1, null))
            {
                if (Object.ReferenceEquals(c2, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // c1 is not null
            {
                if (Object.ReferenceEquals(c2, null)) // c2 is null
                {
                    return false;
                }
            }
            return c1.Equals(c2);
        }
        public static bool operator !=(PM_Maze c1, PM_Maze c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}

