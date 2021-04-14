using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class MEM__Symmetry_Over_X_Axis : PM__Maze_Evaluation_Method
    {
        public override double Evaluate(PM_Maze maze)
        {
            double verticalSymmetry = maze.Symmetry_Over_X_Axis();

            return verticalSymmetry;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
