using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Perfect_Mazes_Lib;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib.Tests
{
    [TestClass()]
    public class PM_Maze_Tests
    {

        [TestMethod()]
        public void Maze_Dimensions()
        {
            PM_Maze maze = new PM_Maze(10, 5);

            Assert.IsTrue(maze.Q_Width() == 10);
            Assert.IsTrue(maze.Q_Height() == 5);

            Directions_Ortho_2D[,] dir = new Directions_Ortho_2D[10, 5];

            maze = new PM_Maze(dir);
            Assert.IsTrue(maze.Q_Width() == 10);
            Assert.IsTrue(maze.Q_Height() == 5);
        }


        [TestMethod()]
        public void Serialize_Deserialize_Test_1() {
            int maze_width = 16;
            int maze_height = 16;
            int num_mazes = 1000;

            Random rand = new Random();
            MGM__Random_DFS generator = new MGM__Random_DFS();

            // serialize and deserialize 1000 mazes...
            for (int i = 0; i < num_mazes; i++) {
                PM_Maze maze = generator.Generate_Maze(rand, maze_width, maze_height);
                string serialized_maze = maze.Serialize_ToJson_String(Formatting.None, TypeNameHandling.None);
                //PM_Maze deserialized_maze = 
            }
        }


        [TestMethod()]
        public void Maze_Quality_Test_1()
        {
            Random rand = new Random();

            PM_Maze maze = new PM_Maze(2, 2);

            Assert.IsTrue(maze.NumCells__All() == 4);
            Assert.IsTrue(maze.NumCells__OfSpecificDirections(Directions_Ortho_2D.None) == 4);

            Assert.IsTrue(maze.IsFullyExpanded() == false);
            Assert.IsTrue(maze.IsFullyConnected() == false);
            Assert.IsTrue(maze.IsCyclic() == false);
            Assert.IsTrue(maze.Islands(rand).Count == 4);
            Assert.IsTrue(maze.All_ActiveEdges_List().Count == 0);
            Assert.IsTrue(maze.All_ActiveEdges_Set().Count == 0);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_List().Count == 4);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_Set().Count == 4);
            //Assert.IsTrue(maze.All_ExpansionEdges_List().Count == 0);

            Vec2i bottom_left = new Vec2i(0,0);
            Vec2i bottom_right = new Vec2i(1,0);
            Vec2i top_left = new Vec2i(0,1);
            Vec2i top_right = new Vec2i(1,1);

            maze.OP_AddEdge(new UEdge2i(bottom_left, bottom_right));
            Assert.IsTrue(maze.IsFullyExpanded() == false);
            Assert.IsTrue(maze.IsFullyConnected() == false);
            Assert.IsTrue(maze.IsCyclic() == false);
            Assert.IsTrue(maze.Islands(rand).Count == 3);
            Assert.IsTrue(maze.All_ActiveEdges_List().Count == 1);
            Assert.IsTrue(maze.All_ActiveEdges_Set().Count == 1);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_List().Count == 3);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_Set().Count == 3);

            maze.OP_AddEdge(new UEdge2i(top_left, top_right));
            Assert.IsTrue(maze.IsFullyExpanded() == true);
            Assert.IsTrue(maze.IsFullyConnected() == false);
            Assert.IsTrue(maze.IsCyclic() == false);
            Assert.IsTrue(maze.Islands(rand).Count == 2);
            Assert.IsTrue(maze.All_ActiveEdges_List().Count == 2);
            Assert.IsTrue(maze.All_ActiveEdges_Set().Count == 2);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_List().Count == 2);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_Set().Count == 2);

            maze.OP_AddEdge(new UEdge2i(bottom_left, top_left));
            Assert.IsTrue(maze.IsFullyExpanded() == true);
            Assert.IsTrue(maze.IsFullyConnected() == true);
            Assert.IsTrue(maze.IsCyclic() == false);
            Assert.IsTrue(maze.Islands(rand).Count == 1);
            Assert.IsTrue(maze.All_ActiveEdges_List().Count == 3);
            Assert.IsTrue(maze.All_ActiveEdges_Set().Count == 3);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_List().Count == 1);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_Set().Count == 1);

            maze.OP_AddEdge(new UEdge2i(bottom_right, top_right));
            Assert.IsTrue(maze.IsFullyExpanded() == true);
            Assert.IsTrue(maze.IsFullyConnected() == true);
            Assert.IsTrue(maze.IsCyclic() == true);
            Assert.IsTrue(maze.Islands(rand).Count == 1);
            Assert.IsTrue(maze.All_ActiveEdges_List().Count == 4);
            Assert.IsTrue(maze.All_ActiveEdges_Set().Count == 4);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_List().Count == 0);
            Assert.IsTrue(maze.All_InactiveEdges_InBounds_Set().Count == 0);
        }


        [TestMethod()]
        public void MainTests()
        {

            PM_Maze maze = new PM_Maze(3, 3);

            //Assert.IsFalse(maze.IsCyclic());

            maze.OP_AddEdge(new UEdge2i(new Vec2i(0, 0), new Vec2i(0, 1)));

            Assert.IsFalse(maze.IsCyclic());

            maze.OP_AddEdge(new UEdge2i(new Vec2i(0, 1), new Vec2i(1, 1)));
            maze.OP_AddEdge(new UEdge2i(new Vec2i(1, 1), new Vec2i(1, 0)));

            Assert.IsFalse(maze.IsCyclic());

            maze.OP_AddEdge(new UEdge2i(new Vec2i(1, 0), new Vec2i(0, 0)));

            Assert.IsTrue(maze.IsCyclic());

            //Assert.IsTrue(1==1);



            //Assert.IsFalse(maze.IsCyclic());

            //maze.AddEdge(new PM_UEdge(new PM_V2(1, 0), new PM_V2(0, 0)));

            //Assert.IsTrue(maze.IsCyclic() == true);
        }

        [TestMethod()]
        public void Rect_Tests()
        {
            Rect2i rect = new Rect2i(
                new Vec2i(0, 0),
                new Vec2i(1, 1)
                );

            Assert.IsTrue(rect.Width() == 1);
            Assert.IsTrue(rect.Height() == 1);
            Assert.IsTrue(rect.X_Dots() == 2);
            Assert.IsTrue(rect.Y_Dots() == 2);
        }
    }
}
