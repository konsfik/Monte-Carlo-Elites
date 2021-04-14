using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_Maze_Queries
    {
        public static int Q_Width(this PM_Maze maze)
        {
            return maze.cells.GetLength(0);
        }

        public static int Q_Height(this PM_Maze maze)
        {
            return maze.cells.GetLength(1);
        }

        public static bool Q_Is_Cell_Unconnected(this PM_Maze maze, Vec2i position)
        {
            return maze.Q_Cell_Directions(position) == Directions_Ortho_2D.None;
        }

        public static bool Q_Is_Cell_Connected(this PM_Maze maze, Vec2i position)
        {
            return maze.Q_Cell_Directions(position) != Directions_Ortho_2D.None;
        }

        public static bool Q_Is_Edge_InBounds(this PM_Maze maze, UEdge2i edge)
        {
            return maze.Q_Is_Cell_InBounds(edge.origin) && maze.Q_Is_Cell_InBounds(edge.exit);
        }

        public static bool Q_Is_Cell_InBounds(this PM_Maze maze, Vec2i position)
        {
            return position.x >= 0 && position.x < maze.Q_Width() &&
                   position.y >= 0 && position.y < maze.Q_Height();
        }

        public static bool Q_Is_Area_InBounds(this PM_Maze maze, Rect2i area)
        {
            return maze.Q_Is_Cell_InBounds(area.min_coords) && maze.Q_Is_Cell_InBounds(area.max_coords);
        }

        public static Directions_Ortho_2D Q_Cell_Directions(this PM_Maze maze, int x, int y)
        {
            return maze.cells[x, y];
        }

        public static Directions_Ortho_2D Q_Cell_Directions(this PM_Maze maze, Vec2i pos)
        {
            return maze.cells[pos.x, pos.y];
        }

        public static Directions_Ortho_2D[,] Q_CellsCopy(this PM_Maze maze)
        {
            return maze.cells.DeepCopy();
        }

        public static string Properties(this PM_Maze maze)
        {
            string props = "";
            props += "num cells: " + maze.NumCells__All() + "\n";
            props += "num leaf nodes: " + maze.NumLeafNodes() + "\n";
            props += "num crosses: " + maze.Num_Cells_Crosses().ToString() + "\n";
            props += "num t-junctions: " + maze.Num_T_Junctions().ToString() + "\n";
            props += "num corners: " + maze.Num_Cells_Corners().ToString() + "\n";
            props += "num corridors: " + maze.Num_Cells_Corridors().ToString() + "\n";
            props += "num horizontal corridors: " + maze.Num_Cells_HorizontalCorridors().ToString() + "\n";
            props += "num vertical corridors: " + maze.Num_Cells_VerticalCorridors().ToString() + "\n";

            return props;
        }
    }
}
