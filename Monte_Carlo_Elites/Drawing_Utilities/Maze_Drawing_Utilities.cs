using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Perfect_Mazes_Lib;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Drawing_Utilities
{
    public static class Maze_Drawing_Utilities
    {


        public static Bitmap Draw(
            this PM_Maze maze,
            Color background_color,
            Color wall_color,
            int cell_size
            )
        {
            int maze_width = maze.Q_Width();
            int maze_height = maze.Q_Height();

            int wall_thickness = 1;

            int bitmapWidth = maze_width * cell_size + wall_thickness;
            int bitmapHeight = maze_height * cell_size + wall_thickness;

            Bitmap image = new Bitmap(bitmapWidth, bitmapHeight);
            using (Graphics bufferGraphics = Graphics.FromImage(image))
            {
                bufferGraphics.Clear(background_color);
                //bufferGraphics.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);
                Vec2i offset = new Vec2i(cell_size / 2, cell_size / 2);

                Pen hardPen = new Pen(wall_color, wall_thickness);
                hardPen.SetLineCap(
                    System.Drawing.Drawing2D.LineCap.Square,
                    System.Drawing.Drawing2D.LineCap.Square,
                    System.Drawing.Drawing2D.DashCap.Flat
                    );

                var cellPositions = maze.CellsPositions_All_List();

                foreach (var cellPosition in cellPositions)
                {
                    int posX = cellPosition.x * cell_size;
                    int posY = cellPosition.y * cell_size;

                    Point top_left = new Point(
                        posX,
                        posY
                        );

                    Point top_right = new Point(
                        posX + cell_size,
                        posY
                        );

                    Point bottom_left = new Point(
                        posX,
                        posY + cell_size
                        );

                    Point bottom_right = new Point(
                        posX + cell_size,
                        posY + cell_size
                        );

                    var directions = maze.Q_Cell_Directions(cellPosition);

                    if (directions.HasFlag(Directions_Ortho_2D.U) == false)
                    {
                        bufferGraphics.DrawLine(hardPen, bottom_left, bottom_right);
                    }

                    if (directions.HasFlag(Directions_Ortho_2D.D) == false)
                    {
                        bufferGraphics.DrawLine(hardPen, top_left, top_right);
                    }

                    if (directions.HasFlag(Directions_Ortho_2D.L) == false)
                    {
                        bufferGraphics.DrawLine(hardPen, top_left, bottom_left);
                    }

                    if (directions.HasFlag(Directions_Ortho_2D.R) == false)
                    {
                        bufferGraphics.DrawLine(hardPen, top_right, bottom_right);
                    }
                }

                hardPen.Dispose();
            }

            return image;
        }

        /// <summary>
        /// Wall thickness is an inteeger, between 0 and ....
        /// The 
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="background_color"></param>
        /// <param name="wall_color"></param>
        /// <param name="wall_thickness"></param>
        /// <param name="cell_size"></param>
        /// <returns></returns>
        public static Bitmap Draw_2(
            this PM_Maze maze,
            Random rand,
            int cell_size,
            int wall_thickness,
            Color background_color,
            Color disconnected_cell_color,
            Color wall_color,
            bool draw_solution,
            bool draw_islands
            )
        {
            int maze_width = maze.Q_Width();
            int maze_height = maze.Q_Height();

            //int wall_thickness = 1;

            int image_width = maze_width * cell_size;
            int image_height = maze_height * cell_size;

            int wall_actual_thickness = (wall_thickness - 1) * 2 + 1;

            Bitmap image = new Bitmap(image_width, image_height);
            using (Graphics buffer_graphics = Graphics.FromImage(image))
            using (Pen wall_pen = new Pen(wall_color, wall_actual_thickness))
            using (SolidBrush disconnected_cells_brush = new SolidBrush(disconnected_cell_color))
            using (SolidBrush islands_brush = new SolidBrush(Color.Black))
            {
                buffer_graphics.Clear(background_color);
                //bufferGraphics.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);
                Vec2i offset = new Vec2i(cell_size / 2, cell_size / 2);

                //Pen hardPen = new Pen(wall_color, wall_thickness);
                wall_pen.SetLineCap(
                    System.Drawing.Drawing2D.LineCap.Square,
                    System.Drawing.Drawing2D.LineCap.Square,
                    System.Drawing.Drawing2D.DashCap.Flat
                    );

                var cells_coordinates = maze.CellsPositions_All_List();

                if (draw_islands) {
                    var islands = maze.Islands(rand);
                    if (islands.Count > 1)
                    {
                        List<Color> colors_per_island = new List<Color>();
                        foreach (var island in islands) {
                            int r = rand.Next(0, 256);
                            int g = rand.Next(0, 256);
                            int b = rand.Next(0, 256);
                            Color c = Color.FromArgb(r, g, b);
                            colors_per_island.Add(c);
                        }

                        for (int i = 0; i < islands.Count; i++) {
                            islands_brush.Color = colors_per_island[i];
                            foreach (var island_cell in islands[i]) {
                                // draw that cell with that color
                                buffer_graphics.FillRectangle(
                                    islands_brush,
                                    island_cell.x * cell_size,
                                    island_cell.y * cell_size,
                                    cell_size,
                                    cell_size
                                    );
                            }
                        }
                    }
                }

                // draw the disconnected cells
                foreach (var cell_coordinates in cells_coordinates)
                {
                    int position_x = cell_coordinates.x * cell_size;
                    int position_y = cell_coordinates.y * cell_size;

                    var cell_directions = maze.Q_Cell_Directions(cell_coordinates);

                    if (cell_directions == Directions_Ortho_2D.None)
                    {
                        buffer_graphics.FillRectangle(
                            disconnected_cells_brush,
                            position_x,
                            position_y,
                            cell_size,
                            cell_size
                            );
                    }
                }

                foreach (var cell_coordinates in cells_coordinates)
                {
                    int position_x = cell_coordinates.x * cell_size;
                    int position_y = cell_coordinates.y * cell_size;

                    Point bottom_left = new Point(
                        position_x + wall_thickness - 1,
                        position_y + wall_thickness - 1
                        );

                    Point bottom_right = new Point(
                        position_x + cell_size - wall_thickness,
                        position_y + wall_thickness - 1
                        );

                    Point top_left = new Point(
                        position_x + wall_thickness - 1,
                        position_y + cell_size - wall_thickness
                        );

                    Point top_right = new Point(
                        position_x + cell_size - wall_thickness,
                        position_y + cell_size - wall_thickness
                        );

                    var cell_directions = maze.Q_Cell_Directions(cell_coordinates);

                    // up - wall
                    if (cell_directions.HasFlag(Directions_Ortho_2D.U) == false)
                    {
                        buffer_graphics.DrawLine(wall_pen, top_left, top_right);
                    }

                    if (cell_directions.HasFlag(Directions_Ortho_2D.D) == false)
                    {
                        buffer_graphics.DrawLine(wall_pen, bottom_left, bottom_right);
                    }

                    if (cell_directions.HasFlag(Directions_Ortho_2D.L) == false)
                    {
                        buffer_graphics.DrawLine(wall_pen, bottom_left, top_left);
                    }

                    if (cell_directions.HasFlag(Directions_Ortho_2D.R) == false)
                    {
                        buffer_graphics.DrawLine(wall_pen, bottom_right, top_right);
                    }
                }

                if (draw_solution)
                {
                    List<Vec2i> solution = maze.BFS_ShortestPath(new Vec2i(0, 0), new Vec2i(maze.Q_Width() - 1, maze.Q_Height() - 1));
                    using (Pen solution_pen = new Pen(Color.Red, wall_thickness))
                    {
                        solution_pen.SetLineCap(
                            System.Drawing.Drawing2D.LineCap.Round,
                            System.Drawing.Drawing2D.LineCap.Round,
                            System.Drawing.Drawing2D.DashCap.Round
                            );

                        for (int i = 0; i < solution.Count - 1; i++)
                        {
                            Vec2i pt1 = solution[i];
                            Vec2i pt2 = solution[i + 1];

                            int posX_1 = pt1.x * cell_size + cell_size / 2;
                            int posY_1 = pt1.y * cell_size + cell_size / 2;
                            Point p1 = new Point(posX_1, posY_1);

                            int posX_2 = pt2.x * cell_size + cell_size / 2;
                            int posY_2 = pt2.y * cell_size + cell_size / 2;
                            Point p2 = new Point(posX_2, posY_2);

                            buffer_graphics.DrawLine(solution_pen, p1, p2);
                        }
                    }
                }
            }

            return image;
        }

        private static void Draw_Cell_Walls(
            Graphics graphics,
            Pen wall_pen,
            PM_Maze maze,
            Vec2i cell_coordinates,
            int cell_size,
            int wall_thickness
            )
        {

            int posX = cell_coordinates.x * cell_size;
            int posY = cell_coordinates.y * cell_size;

            Point top_left = new Point(
                posX + wall_thickness / 2,
                posY + wall_thickness / 2
                );

            Point top_right = new Point(
                posX + cell_size + wall_thickness / 2,
                posY + wall_thickness / 2
                );

            Point bottom_left = new Point(
                posX + wall_thickness / 2,
                posY + cell_size + wall_thickness / 2
                );

            Point bottom_right = new Point(
                posX + cell_size + wall_thickness / 2,
                posY + cell_size + wall_thickness / 2
                );

            var cell_directions = maze.Q_Cell_Directions(cell_coordinates);

            if (cell_directions.HasFlag(Directions_Ortho_2D.U) == false)
            {
                graphics.DrawLine(wall_pen, bottom_left, bottom_right);
            }

            if (cell_directions.HasFlag(Directions_Ortho_2D.D) == false)
            {
                graphics.DrawLine(wall_pen, top_left, top_right);
            }

            if (cell_directions.HasFlag(Directions_Ortho_2D.L) == false)
            {
                graphics.DrawLine(wall_pen, top_left, bottom_left);
            }

            if (cell_directions.HasFlag(Directions_Ortho_2D.R) == false)
            {
                graphics.DrawLine(wall_pen, top_right, bottom_right);
            }
        }


    }
}
