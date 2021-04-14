using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Perfect_Mazes_Lib;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public class MEM__Medium_Solution_Length : PM__Maze_Evaluation_Method
    {
        
        public override double Evaluate(PM_Maze maze)
        {
            int maze_width = maze.Q_Width();
            int maze_height = maze.Q_Height();

            Vec2i top_left_corner = new Vec2i(0, 0);
            Vec2i bottom_right_corner = new Vec2i(maze_width - 1, maze_height - 1);

            double maximum_possible_distance = maze_width * maze_height - 1;

            double actual_distance = (double)maze.ShortestDistance(top_left_corner, bottom_right_corner);

            double ratio = actual_distance / maximum_possible_distance;

            // maximum value when ratio is 0.5:
            double score = 1.0 - Math.Abs(2 * ratio - 1);

            return score;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
