using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;
using Perfect_Mazes_Lib;

namespace Testbed_3__Mazes
{
    public class MEL_Mazes__Individual_Evaluation_Method 
        : MEL__Individual_Evaluation_Method<MEL__Maze_Individual>
    {
        public readonly PM__Maze_Evaluation_Method maze_evaluation_method;

        public MEL_Mazes__Individual_Evaluation_Method(PM__Maze_Evaluation_Method maze_evaluation_method)
        {
            this.maze_evaluation_method = maze_evaluation_method;
        }

        public override double Calculate_Fitness(MEL__Maze_Individual individual)
        {
            MEL__Maze_Individual maze_individual = (MEL__Maze_Individual)individual;

            return maze_evaluation_method.Evaluate(maze_individual.maze);

        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
