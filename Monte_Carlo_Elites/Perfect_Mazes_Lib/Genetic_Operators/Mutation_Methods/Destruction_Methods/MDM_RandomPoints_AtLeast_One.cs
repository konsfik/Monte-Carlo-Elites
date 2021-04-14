using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class MDM_RandomPoints_AtLeast_One : PM__Maze_Destruction_Method
    {
        public double DestructionRate { get; private set; }

        public MDM_RandomPoints_AtLeast_One(double destructionRate)
        {
            DestructionRate = destructionRate;
        }

        public override void DestroyMaze(
            Random rand,
            PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            bool atleast_one_destroyed = false;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double diceRoll = rand.NextDouble();
                    if (diceRoll < DestructionRate)
                    {
                        maze.OP_DeleteCell(x, y);
                        atleast_one_destroyed = true;
                    }
                }
            }

            if (atleast_one_destroyed == false) {
                // destroy one...
                int random_x_index = rand.Next(0, width - 1);
                int random_y_index = rand.Next(0, height - 1);
                maze.OP_DeleteCell(random_x_index, random_y_index);
            }
        }

        public override string ToString()
        {
            string param = String.Format("{0:0.000}", DestructionRate);
            param = param.Replace('.', '_');
            return this.GetType().Name.ToString() + "__DestructionRate_" + param;
        }
    }
}
