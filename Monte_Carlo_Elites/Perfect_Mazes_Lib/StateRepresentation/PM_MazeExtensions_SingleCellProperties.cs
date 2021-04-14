using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_MazeExtensions_SingleCellProperties
    {
        public static int Cell__Centrality(
            this PM_Maze maze,
            int x,
            int y
            )
        {
            int centrality = 0;
            Directions_Ortho_2D cellDirections = maze.Q_Cell_Directions(x, y);
            if (cellDirections.HasFlag(Directions_Ortho_2D.U))
            {
                centrality++;
            }
            if (cellDirections.HasFlag(Directions_Ortho_2D.D))
            {
                centrality++;
            }
            if (cellDirections.HasFlag(Directions_Ortho_2D.L))
            {
                centrality++;
            }
            if (cellDirections.HasFlag(Directions_Ortho_2D.R))
            {
                centrality++;
            }
            return centrality;
        }

        public static int Cell__Centrality(
            this PM_Maze maze,
            Vec2i cellPosition
            )
        {
            return maze.Cell__Centrality(cellPosition.x, cellPosition.y);
        }

        public static int Cell__StraightLineVisibility(
            this PM_Maze maze,
            int x,
            int y)
        {
            int visibility = 1;

            if (maze.Q_Cell_Directions(x, y).HasFlag(Directions_Ortho_2D.R))
            {
                Vec2i currentCell = new Vec2i(x, y);
                while (maze.Q_Cell_Directions(currentCell).HasFlag(Directions_Ortho_2D.R))
                {
                    currentCell = currentCell.To_Right();
                    visibility++;
                }
            }
            if (maze.Q_Cell_Directions(x, y).HasFlag(Directions_Ortho_2D.L))
            {
                Vec2i currentCell = new Vec2i(x, y);
                while (maze.Q_Cell_Directions(currentCell).HasFlag(Directions_Ortho_2D.L))
                {
                    currentCell = currentCell.To_Left();
                    visibility++;
                }
            }
            if (maze.Q_Cell_Directions(x, y).HasFlag(Directions_Ortho_2D.U))
            {
                Vec2i currentCell = new Vec2i(x, y);
                while (maze.Q_Cell_Directions(currentCell).HasFlag(Directions_Ortho_2D.U))
                {
                    currentCell = currentCell.To_Up();
                    visibility++;
                }
            }
            if (maze.Q_Cell_Directions(x, y).HasFlag(Directions_Ortho_2D.D))
            {
                Vec2i currentCell = new Vec2i(x, y);
                while (maze.Q_Cell_Directions(currentCell).HasFlag(Directions_Ortho_2D.D))
                {
                    currentCell = currentCell.To_Down();
                    visibility++;
                }
            }

            return visibility;
        }


        public static int Cell__NumCellsReachable_WithinSteps(this PM_Maze maze, Vec2i root, int numSteps)
        {
            int num = maze.BFS_ReachableNodes_MaximumSteps(root, false, numSteps).Count;
            return num;
        }

        public static bool IsCell_Cross(this PM_Maze maze, Vec2i cellPosition)
        {
            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cellPosition);
            return directions == Directions_Ortho_2D.ORTHO;
        }

        public static bool IsCell_Corridor(this PM_Maze maze, Vec2i cellPosition)
        {
            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cellPosition);
            return directions == Directions_Ortho_2D.X || directions == Directions_Ortho_2D.Y;
        }

        public static bool IsCell_HorizontalCorridor(this PM_Maze maze, Vec2i cellPosition)
        {
            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cellPosition);
            return directions == Directions_Ortho_2D.X;
        }

        public static bool IsCell_VerticalCorridor(this PM_Maze maze, Vec2i cellPosition)
        {
            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cellPosition);
            return directions == Directions_Ortho_2D.Y;
        }

        public static bool IsCell_Corner(this PM_Maze maze, Vec2i cellPosition)
        {
            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cellPosition);
            return
                directions == (Directions_Ortho_2D.R | Directions_Ortho_2D.U)
                ||
                directions == (Directions_Ortho_2D.U | Directions_Ortho_2D.L)
                ||
                directions == (Directions_Ortho_2D.L | Directions_Ortho_2D.D)
                ||
                directions == (Directions_Ortho_2D.D | Directions_Ortho_2D.R)
                ;
        }

        public static bool IsCell_DeadEnd(this PM_Maze maze, Vec2i cellPosition)
        {
            Directions_Ortho_2D cellDirections = maze.Q_Cell_Directions(cellPosition);

            if (cellDirections.IsSingle() || cellDirections == Directions_Ortho_2D.None)
            {
                return true;
            }
            return false;
        }

        public static bool IsCell_DeadEnd(this PM_Maze maze, int x, int y)
        {
            Directions_Ortho_2D cellDirections = maze.Q_Cell_Directions(x, y);

            if (cellDirections.IsSingle() || cellDirections == Directions_Ortho_2D.None)
            {
                return true;
            }
            return false;
        }

    }
}
