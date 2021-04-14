using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;
using Perfect_Mazes_Lib;

namespace Testbed_3__Mazes
{
    public class MEL_Mazes__Individual_Mutation_Method : MEL__Individual_Mutator<MEL__Maze_Individual>
    {
        public PM__Maze_Destruction_Method MazeDestructionMethod { get; private set; }
        public PM__Maze_Repair_Method MazeRepairMethod { get; private set; }

        public MEL_Mazes__Individual_Mutation_Method(
            PM__Maze_Destruction_Method maze_destruction_method,
            PM__Maze_Repair_Method maze_repair_method
            )
        {
            this.MazeDestructionMethod = maze_destruction_method;
            this.MazeRepairMethod = maze_repair_method;
        }


        public override MEL__Maze_Individual Generate_Offspring(Random randomness_provider, MEL__Maze_Individual parent)
        {
            PM_Maze maze_clone = (PM_Maze)parent.maze.DeepCopy();

            // destroy the maze clone
            MazeDestructionMethod.DestroyMaze(randomness_provider,maze_clone);
            MazeRepairMethod.RefillMaze(randomness_provider,maze_clone);

            MEL__Maze_Individual offspring = new MEL__Maze_Individual(maze_clone);

            return offspring;
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
