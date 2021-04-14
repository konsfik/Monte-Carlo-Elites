using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_MazeExtensions_Properties
    {

        #region num cells of specific properties
        public static int NumCells__All(
            this PM_Maze maze
            )
        {
            return maze.Q_Width() * maze.Q_Height();
        }

        public static int NumCells__OfCentrality(
            this PM_Maze maze,
            int requestedCentrality
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int num = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze.Cell__Centrality(x, y) == requestedCentrality)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public static int NumCells__OfSpecificDirections(
            this PM_Maze maze,
            Directions_Ortho_2D directions
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int numCellsOfSpecificDirections = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze.Q_Cell_Directions(x, y) == directions)
                    {
                        numCellsOfSpecificDirections++;
                    }
                }
            }
            return numCellsOfSpecificDirections;
        }

        public static int NumCells__IncludingDirections(
            this PM_Maze maze,
            Directions_Ortho_2D directions
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int numCellsIncludingDirections = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze.Q_Cell_Directions(x, y).HasFlag(directions))
                    {
                        numCellsIncludingDirections++;
                    }
                }
            }
            return numCellsIncludingDirections;
        }

        public static int Num_Cells_HorizontalCorridors(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int num = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Directions_Ortho_2D dir = maze.Q_Cell_Directions(x, y);
                    if (dir == Directions_Ortho_2D.X)
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static int Num_Cells_VerticalCorridors(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int num = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Directions_Ortho_2D dir = maze.Q_Cell_Directions(x, y);
                    if (dir == Directions_Ortho_2D.Y)
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static int Num_Cells_Crosses(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int num = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    Directions_Ortho_2D dir = maze.Q_Cell_Directions(cell);
                    if (dir == Directions_Ortho_2D.ORTHO)
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static int NumLeafNodes(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int num = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Directions_Ortho_2D dir = maze.Q_Cell_Directions(x, y);
                    if (dir.IsSingle())
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static int Num_T_Junctions(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int num = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Directions_Ortho_2D dir = maze.Q_Cell_Directions(x, y);
                    if (
                        dir == (Directions_Ortho_2D.Y | Directions_Ortho_2D.L)
                        ||
                        dir == (Directions_Ortho_2D.Y | Directions_Ortho_2D.R)
                        ||
                        dir == (Directions_Ortho_2D.X | Directions_Ortho_2D.U)
                        ||
                        dir == (Directions_Ortho_2D.X | Directions_Ortho_2D.D)
                        )
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        public static int Num_Cells_Cross_Junctions(this PM_Maze maze)
        {
            return maze.NumCells__OfCentrality(4);
        }

        public static int Num_Cells_T_Junctions(this PM_Maze maze)
        {
            return maze.NumCells__OfCentrality(3);
        }

        public static int Num_Cells_Passages(this PM_Maze maze)
        {
            return maze.NumCells__OfCentrality(2);
        }

        public static int Num_Cells_Deadends(this PM_Maze maze)
        {
            return maze.NumCells__OfCentrality(1);
        }

        public static int Num_Cells_Unconnected(this PM_Maze maze)
        {
            return maze.NumCells__OfCentrality(0);
        }

        public static int Num_Cells_Corridors(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int numCorridors = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze.cells[x, y] == Directions_Ortho_2D.X || maze.cells[x, y] == Directions_Ortho_2D.Y)
                    {
                        numCorridors++;
                    }
                }
            }
            return numCorridors;
        }

        public static int Num_Cells_Corners(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int numCorners = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (
                        maze.cells[x, y] == (Directions_Ortho_2D.R | Directions_Ortho_2D.U)
                        ||
                        maze.cells[x, y] == (Directions_Ortho_2D.U | Directions_Ortho_2D.L)
                        ||
                        maze.cells[x, y] == (Directions_Ortho_2D.L | Directions_Ortho_2D.D)
                        ||
                        maze.cells[x, y] == (Directions_Ortho_2D.D | Directions_Ortho_2D.R)
                        )
                    {
                        numCorners++;
                    }
                }
            }
            return numCorners;
        }
        #endregion

        #region comparisons between two cells
        public static double Cells_Similarity(this PM_Maze maze, Vec2i cell1, Vec2i cell2)
        {
            int sum = 0;
            Directions_Ortho_2D dir1 = maze.Q_Cell_Directions(cell1);
            Directions_Ortho_2D dir2 = maze.Q_Cell_Directions(cell2);
            if (
                    (dir1 & dir2).HasFlag(Directions_Ortho_2D.U)
                )
            {
                sum++;
            }
            if (
                    (dir1 & dir2).HasFlag(Directions_Ortho_2D.D)
                )
            {
                sum++;
            }
            if (
                    (dir1 & dir2).HasFlag(Directions_Ortho_2D.L)
                )
            {
                sum++;
            }
            if (
                    (dir1 & dir2).HasFlag(Directions_Ortho_2D.R)
                )
            {
                sum++;
            }
            return (double)sum / 4.0;
        }

        public static double Cells__Symmetry_Y_Axis(this PM_Maze maze, Vec2i cell1, Vec2i cell2)
        {
            double sum = 0;
            Directions_Ortho_2D dir1 = maze.Q_Cell_Directions(cell1);
            Directions_Ortho_2D dir2 = maze.Q_Cell_Directions(cell2);

            // left vs right
            if (dir1.HasFlag(Directions_Ortho_2D.L) && dir2.HasFlag(Directions_Ortho_2D.R))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.L) == false && dir2.HasFlag(Directions_Ortho_2D.R) == false)
            {
                sum++;
            }

            // right vs left
            if (dir1.HasFlag(Directions_Ortho_2D.R) && dir2.HasFlag(Directions_Ortho_2D.L))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.R) == false && dir2.HasFlag(Directions_Ortho_2D.L) == false)
            {
                sum++;
            }

            // up
            if (dir1.HasFlag(Directions_Ortho_2D.U) && dir2.HasFlag(Directions_Ortho_2D.U))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.U) == false && dir2.HasFlag(Directions_Ortho_2D.U) == false)
            {
                sum++;
            }

            // down
            if (dir1.HasFlag(Directions_Ortho_2D.D) && dir2.HasFlag(Directions_Ortho_2D.D))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.D) == false && dir2.HasFlag(Directions_Ortho_2D.D) == false)
            {
                sum++;
            }

            return sum / 4.0;
        }

        public static double Cells__Symmetry_X_Axis(this PM_Maze maze, Vec2i cell1, Vec2i cell2)
        {
            double sum = 0;
            Directions_Ortho_2D dir1 = maze.Q_Cell_Directions(cell1);
            Directions_Ortho_2D dir2 = maze.Q_Cell_Directions(cell2);

            // left 
            if (dir1.HasFlag(Directions_Ortho_2D.L) && dir2.HasFlag(Directions_Ortho_2D.L))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.L) == false && dir2.HasFlag(Directions_Ortho_2D.L) == false)
            {
                sum++;
            }

            // right
            if (dir1.HasFlag(Directions_Ortho_2D.R) && dir2.HasFlag(Directions_Ortho_2D.R))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.R) == false && dir2.HasFlag(Directions_Ortho_2D.R) == false)
            {
                sum++;
            }

            // up vs down
            if (dir1.HasFlag(Directions_Ortho_2D.U) && dir2.HasFlag(Directions_Ortho_2D.D))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.U) == false && dir2.HasFlag(Directions_Ortho_2D.D) == false)
            {
                sum++;
            }

            // down vs up
            if (dir1.HasFlag(Directions_Ortho_2D.D) && dir2.HasFlag(Directions_Ortho_2D.U))
            {
                sum++;
            }
            else if (dir1.HasFlag(Directions_Ortho_2D.D) == false && dir2.HasFlag(Directions_Ortho_2D.U) == false)
            {
                sum++;
            }

            return sum / 4.0;
        }
        #endregion

        public static double Average_Num_Cells_ReachableWithinSteps(this PM_Maze maze, int numSteps)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            int sum = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    sum += maze.Cell__NumCellsReachable_WithinSteps(new Vec2i(x, y), numSteps);
                }
            }

            return (double)sum / (double)maze.NumCells__All();
        }

        public static double Symmetry_Over_Y_Axis(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            double symmetrySum = 0.0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    Vec2i symmetricCell = maze.Find_SymmetricCell_Over_Y_Axis(cell);
                    symmetrySum += maze.Cells__Symmetry_Y_Axis(cell, symmetricCell);
                }
            }

            return symmetrySum / (double)maze.NumCells__All();
        }

        public static double Symmetry_Over_X_Axis(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            double symmetrySum = 0.0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    Vec2i symmetricCell = maze.Find_SymmetricCell_Over_X_Axis(cell);
                    symmetrySum += maze.Cells__Symmetry_X_Axis(cell, symmetricCell);
                }
            }

            return symmetrySum / (double)maze.NumCells__All();
        }

        public static double Average_Cell_StraightLineVisibility(this PM_Maze maze)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            double sum = 0.0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    sum += (double)maze.Cell__StraightLineVisibility(x, y);
                }
            }

            int numCells = maze.NumCells__All();

            return sum / (double)numCells;
        }

        public static double Average_Cell_Centrality(this PM_Maze maze)
        {
            double sum = 0.0;

            for (int x = 0; x < maze.Q_Width(); x++)
            {
                for (int y = 0; y < maze.Q_Height(); y++)
                {
                    sum += (double)maze.Cell__Centrality(x, y);
                }
            }

            int numCells = maze.NumCells__All();

            return sum / (double)numCells;
        }


        public static double Percent_Cells_OfCentrality(this PM_Maze maze, int requiredCentrality)
        {
            return (double)maze.NumCells__OfCentrality(requiredCentrality) / (double)maze.NumCells__All();
        }

        public static double Percent_Cells_CrossJunctions(this PM_Maze maze, int requiredCentrality)
        {
            return (double)maze.Num_Cells_Cross_Junctions() / (double)maze.NumCells__All();
        }
    }
}
