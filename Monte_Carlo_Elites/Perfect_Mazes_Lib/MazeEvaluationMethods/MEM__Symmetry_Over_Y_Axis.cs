using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class MEM__Symmetry_Over_Y_Axis : PM__Maze_Evaluation_Method
    {
        public override double Evaluate(PM_Maze maze)
        {

            double horizontalSymmetry = maze.Symmetry_Over_Y_Axis();

            return horizontalSymmetry;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
