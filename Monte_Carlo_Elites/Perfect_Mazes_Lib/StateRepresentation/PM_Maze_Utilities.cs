using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_Maze_Utilities
    {
        public static PM_Maze DecodeMazeFromString(string encodedMaze, char free, char blocked)
        {
            char[,] charTable = CharTableFromString(encodedMaze);

            int encodedWidth = charTable.GetLength(0);
            int encodedHeight = charTable.GetLength(1);

            int width = ShrinkSize(encodedWidth);
            int height = ShrinkSize(encodedHeight);

            Directions_Ortho_2D[,] directionsTable = new Directions_Ortho_2D[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    directionsTable[x, y] = Directions_Ortho_2D.ORTHO;
                }
            }


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int ex = ExpandSize(x);
                    int ey = ExpandSize(y);

                    if (charTable[ex, ey + 1] == blocked)
                    {
                        directionsTable[x, y] = directionsTable[x, y].RemoveFlag(Directions_Ortho_2D.U);
                    }
                    if (charTable[ex, ey - 1] == blocked)
                    {
                        directionsTable[x, y] = directionsTable[x, y].RemoveFlag(Directions_Ortho_2D.D);
                    }
                    if (charTable[ex - 1, ey] == blocked)
                    {
                        directionsTable[x, y] = directionsTable[x, y].RemoveFlag(Directions_Ortho_2D.L);
                    }
                    if (charTable[ex + 1, ey] == blocked)
                    {
                        directionsTable[x, y] = directionsTable[x, y].RemoveFlag(Directions_Ortho_2D.R);
                    }
                }
            }

            PM_Maze maze = new PM_Maze(directionsTable);

            return maze;
        }

        public static string StringTableFromCharTable(char[,] charTable)
        {
            string stringTable = "";

            for (int y = 0; y < charTable.GetLength(1); y++)
            {
                for (int x = 0; x < charTable.GetLength(0); x++)
                {
                    stringTable += charTable[x, y];
                }
                stringTable += "\n";
            }

            return stringTable;
        }

        public static char[,] CharTableFromString(string stringTable)
        {
            List<string> parts = stringTable.Split('\n').ToList();
            parts.RemoveAll(x => x == "");

            int height = parts.Count;
            int width = parts[0].Length;

            char[,] charTable = new char[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    charTable[x, y] = parts[y][x];
                }
            }

            return charTable;
        }

        public static string EncodeMazeToString(PM_Maze maze, char free, char blocked)
        {
            char[,] charTable = EncodeMazeToCharTable(maze, free, blocked);

            string stringEncoding = "";

            for (int y = 0; y < charTable.GetLength(1); y++)
            {
                for (int x = 0; x < charTable.GetLength(0); x++)
                {
                    stringEncoding += charTable[x, y];
                }
                stringEncoding += "\n";
            }

            return stringEncoding;
        }

        public static char[,] EncodeMazeToCharTable(PM_Maze maze, char free, char blocked)
        {
            return EncodeDirectionsTableToCharTable(maze.cells, free, blocked);
        }

        public static char[,] EncodeDirectionsTableToCharTable(Directions_Ortho_2D[,] directionsTable, char free, char blocked)
        {
            int width = directionsTable.GetLength(0);
            int height = directionsTable.GetLength(1);

            int expandedWidth = ExpandSize(width);
            int expandedHeight = ExpandSize(height);

            char[,] mazeChars = new char[expandedWidth, expandedHeight];

            // initialize all values to blocked
            for (int ex = 0; ex < expandedWidth; ex++)
            {
                for (int ey = 0; ey < expandedHeight; ey++)
                {
                    mazeChars[ex, ey] = blocked;
                }
            }

            // start freeing up values...
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Directions_Ortho_2D dir = directionsTable[x, y];

                    int ex = ExpandSize(x);
                    int ey = ExpandSize(y);

                    mazeChars[ex, ey] = free;
                    if (dir.HasFlag(Directions_Ortho_2D.U))
                    {
                        mazeChars[ex, ey + 1] = free;
                    }
                    if (dir.HasFlag(Directions_Ortho_2D.D))
                    {
                        mazeChars[ex, ey - 1] = free;
                    }
                    if (dir.HasFlag(Directions_Ortho_2D.L))
                    {
                        mazeChars[ex - 1, ey] = free;
                    }
                    if (dir.HasFlag(Directions_Ortho_2D.R))
                    {
                        mazeChars[ex + 1, ey] = free;
                    }

                }
            }

            return mazeChars;
        }

        private static int ExpandSize(int size)
        {
            return size * 2 + 1;
        }

        private static int ShrinkSize(int size)
        {
            return (size - 1) / 2;
        }

        private static double Directions_Table_Similarity_Score(Directions_Ortho_2D[,] dirTable1, Directions_Ortho_2D[,] dirTable2)
        {
            if (
                dirTable1.GetLength(0) != dirTable2.GetLength(0) || dirTable1.GetLength(1) != dirTable2.GetLength(1)
                )
            {
                throw new System.Exception("dissimilar tables!");
            }

            int width = dirTable1.GetLength(0);
            int height = dirTable1.GetLength(1);
            double numElements = width * height;
            double scoreSum = 0.0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    scoreSum += Directions_Similarity_Score(dirTable1[x, y], dirTable2[x, y]);
                }
            }

            double score = scoreSum / numElements;

            return score;
        }

        private static double Directions_Similarity_Score(Directions_Ortho_2D dir1, Directions_Ortho_2D dir2)
        {
            double score = 0;
            if (
                (dir1.HasFlag(Directions_Ortho_2D.U) && dir2.HasFlag(Directions_Ortho_2D.U))
                ||
                (dir1.HasFlag(Directions_Ortho_2D.U) == false && dir2.HasFlag(Directions_Ortho_2D.U) == false)
                )
            {
                score++;
            }
            if (
                (dir1.HasFlag(Directions_Ortho_2D.D) && dir2.HasFlag(Directions_Ortho_2D.D))
                ||
                (dir1.HasFlag(Directions_Ortho_2D.D) == false && dir2.HasFlag(Directions_Ortho_2D.D) == false)
                )
            {
                score++;
            }
            if (
                (dir1.HasFlag(Directions_Ortho_2D.L) && dir2.HasFlag(Directions_Ortho_2D.L))
                ||
                (dir1.HasFlag(Directions_Ortho_2D.L) == false && dir2.HasFlag(Directions_Ortho_2D.L) == false)
                )
            {
                score++;
            }
            if (
                (dir1.HasFlag(Directions_Ortho_2D.R) && dir2.HasFlag(Directions_Ortho_2D.R))
                ||
                (dir1.HasFlag(Directions_Ortho_2D.R) == false && dir2.HasFlag(Directions_Ortho_2D.R) == false)
                )
            {
                score++;
            }
            return score / 4.0;
        }

    }
}
