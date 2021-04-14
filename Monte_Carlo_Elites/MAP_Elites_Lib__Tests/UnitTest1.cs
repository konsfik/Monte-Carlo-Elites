using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using MapElites_Lib;

namespace MAP_Elites_Lib__Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test__Curiosity_Table()
        {
            bool[,] individual_exists_table = new bool[,] {
                {true,false,true},
                {false,false,false},
                {true,false,true}
            };

            int[,] selections_table = new int[,] {
                {4,0,0},
                {0,0,0},
                {0,0,0}
            };

            int[,] success_table = new int[,] {
                {2,0,0},
                {0,0,0},
                {0,0,0}
            };

            double[,] curiosity_table = MEL_PSM__Help_Methods.Curiosity_Score__Table_Calculation(
                individual_exists_table: individual_exists_table,
                selections_table: selections_table,
                success_table: success_table,
                curiosity_reward_value: 1.0,
                curiosity_penalty_value: -0.5,
                out double max_curiosity_value,
                out double min_curiosity_value
                );

            Assert.IsTrue(curiosity_table[0, 0] == 1);
            Assert.IsTrue(max_curiosity_value == 1);
            Assert.IsTrue(min_curiosity_value == 0);
            Assert.IsTrue(Double.IsNaN(curiosity_table[1, 0]));

            individual_exists_table = new bool[,] {
                {true,false,true},
                {false,false,false},
                {true,false,true}
            };

            selections_table = new int[,] {
                {8,0,0},
                {0,0,0},
                {0,0,0}
            };

            success_table = new int[,] {
                {2,0,0},
                {0,0,0},
                {0,0,0}
            };

            curiosity_table = MEL_PSM__Help_Methods.Curiosity_Score__Table_Calculation(
                individual_exists_table: individual_exists_table,
                selections_table: selections_table,
                success_table: success_table,
                curiosity_reward_value: 1.0,
                curiosity_penalty_value: -0.5,
                out max_curiosity_value,
                out min_curiosity_value
                );

            Assert.IsTrue(curiosity_table[0, 0] == -1);
            Assert.IsTrue(max_curiosity_value == 0);
            Assert.IsTrue(min_curiosity_value == -1);
            Assert.IsTrue(Double.IsNaN(curiosity_table[1, 0]));

            individual_exists_table = new bool[,] {
                {true,true,true},
                {true,true,true},
                {true,true,true}
            };

            selections_table = new int[,] {
                {8,5,16},
                {1,0,0},
                {0,0,0}
            };

            success_table = new int[,] {
                {2,3,8},
                {0,0,0},
                {0,0,0}
            };

            curiosity_table = MEL_PSM__Help_Methods.Curiosity_Score__Table_Calculation(
                individual_exists_table: individual_exists_table,
                selections_table: selections_table,
                success_table: success_table,
                curiosity_reward_value: 1.0,
                curiosity_penalty_value: -0.5,
                out max_curiosity_value,
                out min_curiosity_value
                );

            Assert.IsTrue(curiosity_table[0, 0] == -1);
            Assert.IsTrue(curiosity_table[0, 1] == 2);
            Assert.IsTrue(curiosity_table[0, 2] == 4);
            Assert.IsTrue(curiosity_table[1, 0] == -0.5);
            //Assert.IsTrue(max_curiosity_value == 0);
            //Assert.IsTrue(min_curiosity_value == -1);
            //Assert.IsTrue(Double.IsNaN(curiosity_table[1, 0]));
        }
    }
}
