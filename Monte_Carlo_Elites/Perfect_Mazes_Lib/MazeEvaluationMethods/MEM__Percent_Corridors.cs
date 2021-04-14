using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class MEM__Percent_Corridors : PM__Maze_Evaluation_Method
    {
        public override double Evaluate(PM_Maze maze)
        {
            int numNodes = maze.CellsPositions_All_Set().Count;
            int numCorridors = maze.Num_Cells_Corridors();

            double percentage = (double)numCorridors / (double)numNodes;

            return percentage;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
