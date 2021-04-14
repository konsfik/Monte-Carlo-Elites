using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class MEM__Symmetry_Over_X_And_Y_Axes:PM__Maze_Evaluation_Method
    {
        public override double Evaluate(PM_Maze maze)
        {

            double verticalSymmetry = maze.Symmetry_Over_X_Axis();
            double horizontalSymmetry = maze.Symmetry_Over_Y_Axis();

            return (verticalSymmetry + horizontalSymmetry) / 2.0;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
