using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public class MGM__Random_DFS : PM__Maze_Generation_Method
    {
        public override PM_Maze Generate_Maze(
            Random randomness_provider,
            int width,
            int height
            )
        {
            // initialize maze
            PM_Maze maze = new PM_Maze(width, height);

            Vec2i current_position = maze.RandomCell(randomness_provider);
            List<UEdge2i> expansion_edges = maze.Cell_ExpansionEdges_List(current_position);
            expansion_edges.Shuffle(randomness_provider);

            while (expansion_edges.Count > 0)
            {
                UEdge2i selected_edge = expansion_edges.Pop_Last_Item(); // select & remove (draw) the last edge
                current_position = selected_edge.exit;
                if (maze.Q_Is_Cell_Unconnected(current_position))
                {
                    maze.OP_AddEdge(selected_edge);
                    List<UEdge2i> newEdges = maze.Cell_ExpansionEdges_List(current_position);
                    newEdges.Shuffle(randomness_provider);
                    expansion_edges.AddRange(newEdges);
                }
            }
            return maze;
        }

        public override string Description()
        {
            return this.GetType().Name.ToString();
        }

        public override string ToString()
        {
            return Description();
        }
    }
}
