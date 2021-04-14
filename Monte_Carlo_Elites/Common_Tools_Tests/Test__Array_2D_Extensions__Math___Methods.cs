using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common_Tools.Utilities;
using Common_Tools.Elements;

namespace Common_Tools_Tests
{
    [TestClass]
    public class Test__Array_2D_Extensions__Math___Methods
    {
        [TestMethod]
        public void Float_Assertions()
        {
            Assert.IsTrue(float.IsNaN(float.PositiveInfinity + float.NegativeInfinity));
        }

        [TestMethod]
        public void Test__Array2D__Boolean__Sum()
        {
            bool[,] array_2d = new bool[,] {
                { true, false, true },
                { true, false, true }
            };

            Assert.IsTrue(array_2d.Sum() == 4);

            array_2d = new bool[,] {
                { true, false, false },
                { true, false, false }
            };

            Assert.IsTrue(array_2d.Sum() == 2);

            array_2d = new bool[,] {
                { false, false, false },
                { false, false, false }
            };

            Assert.IsTrue(array_2d.Sum() == 0);

            array_2d = new bool[,] { };

            Assert.IsTrue(array_2d.Sum() == 0);
        }

        [TestMethod]
        public void Test__Array2D__Integer__Sum()
        {
            int[,] array_2d = new int[,] {
                { 0,1,2 },
                { 0,1,2 }
            };

            Assert.IsTrue(array_2d.Sum() == 6);

            array_2d = new int[,] {
                { 0,1,1 },
                { 0,1,1 }
            };

            Assert.IsTrue(array_2d.Sum() == 4);

            array_2d = new int[,] {
                { 0,1 },
                { 0,1 }
            };

            Assert.IsTrue(array_2d.Sum() == 2);

            array_2d = new int[,] {
                { 0,0 },
                { 0,0 }
            };

            Assert.IsTrue(array_2d.Sum() == 0);

            array_2d = new int[,] { };

            Assert.IsTrue(array_2d.Sum() == 0);

            array_2d = new int[,] {
                { int.MaxValue,0 },
                { 0,0 }
            };

            Assert.IsTrue(array_2d.Sum() == int.MaxValue);

            array_2d = new int[,] {
                { int.MaxValue,1 },
                { 0,0 }
            };

            // int.MaxValue + 1 = int.MinValue (overflow occurs)
            Assert.IsTrue(array_2d.Sum() == int.MinValue);

            array_2d = new int[,] {
                { int.MinValue,-1 },
                { 0,0 }
            };

            // int.MinValue - 1 = int.MaxValue (overflow occurs)
            Assert.IsTrue(array_2d.Sum() == int.MaxValue);
        }

        [TestMethod]
        public void Test__Array2D__Float__Sum()
        {
            float[,] array_2d = new float[,] {
                { 0.0f,1.0f,2.0f },
                { 0.0f,1.0f,2.0f }
            };

            Assert.IsTrue(array_2d.Sum() == 6.0f);

            array_2d = new float[,] {
                { 0.0f,1.0f,1.0f },
                { 0.0f,1.0f,1.0f }
            };

            Assert.IsTrue(array_2d.Sum() == 4.0f);

            array_2d = new float[,] {
                { 0.0f,1.0f },
                { 0.0f,1.0f }
            };

            Assert.IsTrue(array_2d.Sum() == 2.0f);

            array_2d = new float[,] {
                { 0.0f,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(array_2d.Sum() == 0.0f);

            array_2d = new float[,] { };

            Assert.IsTrue(array_2d.Sum() == 0.0f);

            array_2d = new float[,] {
                { float.MaxValue,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(array_2d.Sum() == float.MaxValue);

            array_2d = new float[,] {
                { -float.MaxValue,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(array_2d.Sum() == -float.MaxValue);

            array_2d = new float[,] {
                { float.MinValue,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(array_2d.Sum() == float.MinValue);

            array_2d = new float[,] {
                { float.NaN,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(float.IsNaN(array_2d.Sum()));

            array_2d = new float[,] {
                { float.PositiveInfinity,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(array_2d.Sum() == float.PositiveInfinity);

            array_2d = new float[,] {
                { float.NegativeInfinity,0.0f },
                { 0.0f,0.0f }
            };

            Assert.IsTrue(array_2d.Sum() == float.NegativeInfinity);

            array_2d = new float[,] {
                { float.NegativeInfinity,0.0f },
                { float.PositiveInfinity,0.0f }
            };

            Assert.IsTrue(float.IsNaN(array_2d.Sum()));

        }

        [TestMethod]
        public void Test__Array2D__Double__Sum()
        {
            double[,] array_2d = new double[,] {
                { 0.0,1.0,2.0 },
                { 0.0,1.0,2.0 }
            };

            Assert.IsTrue(array_2d.Sum() == 6.0);

            array_2d = new double[,] {
                { 0.0,1.0,1.0 },
                { 0.0,1.0,1.0 }
            };

            Assert.IsTrue(array_2d.Sum() == 4.0);

            array_2d = new double[,] {
                { 0.0,1.0 },
                { 0.0,1.0 }
            };

            Assert.IsTrue(array_2d.Sum() == 2.0);

            array_2d = new double[,] {
                { 0.0,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(array_2d.Sum() == 0.0);

            array_2d = new double[,] { };

            Assert.IsTrue(array_2d.Sum() == 0.0);

            array_2d = new double[,] {
                { double.MaxValue,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(array_2d.Sum() == double.MaxValue);

            array_2d = new double[,] {
                { -double.MaxValue,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(array_2d.Sum() == -double.MaxValue);

            array_2d = new double[,] {
                { double.MinValue,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(array_2d.Sum() == double.MinValue);

            array_2d = new double[,] {
                { double.NaN,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(double.IsNaN(array_2d.Sum()));

            array_2d = new double[,] {
                { double.PositiveInfinity,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(array_2d.Sum() == double.PositiveInfinity);

            array_2d = new double[,] {
                { double.NegativeInfinity,0.0 },
                { 0.0,0.0 }
            };

            Assert.IsTrue(array_2d.Sum() == double.NegativeInfinity);

            array_2d = new double[,] {
                { double.NegativeInfinity,0.0 },
                { double.PositiveInfinity,0.0 }
            };

            Assert.IsTrue(double.IsNaN(array_2d.Sum()));

        }

        [TestMethod]
        public void Test__Min_Max_Methods__Array2D_Double()
        {
            double[,] array_2d = new double[,] {
                { double.NaN, 0.0, 1.0 },
                { double.NaN, 0.0, 1.0 }
            };

            Assert.IsTrue(array_2d.Max() == 1.0);
            Assert.IsTrue(array_2d.Min() == 0.0);

            array_2d = new double[,] {
                { double.NaN, double.NaN},
                { double.NaN, double.NaN}
            };

            Assert.IsTrue(double.IsNaN(array_2d.Max()));
            Assert.IsTrue(double.IsNaN(array_2d.Min()));

            array_2d = new double[,] {
                { double.NegativeInfinity, double.NaN, double.PositiveInfinity },
                { double.NegativeInfinity, double.NaN, double.PositiveInfinity }
            };

            Assert.IsTrue(array_2d.Max() == double.PositiveInfinity);
            Assert.IsTrue(array_2d.Min() == double.NegativeInfinity);

            array_2d = new double[,] {
                { double.NaN, double.PositiveInfinity },
                { double.NaN, double.PositiveInfinity }
            };

            Assert.IsTrue(array_2d.Max() == double.PositiveInfinity);
            Assert.IsTrue(array_2d.Min() == double.PositiveInfinity);

            array_2d = new double[,] {
                { double.NegativeInfinity, double.NaN},
                { double.NegativeInfinity, double.NaN}
            };

            Assert.IsTrue(array_2d.Max() == double.NegativeInfinity);
            Assert.IsTrue(array_2d.Min() == double.NegativeInfinity);

            array_2d = new double[,] {
                { double.NegativeInfinity, double.MinValue, double.MaxValue, double.PositiveInfinity },
                { double.NegativeInfinity, double.MinValue, double.MaxValue, double.PositiveInfinity }
            };

            Assert.IsTrue(array_2d.Max() == double.PositiveInfinity);
            Assert.IsTrue(array_2d.Min() == double.NegativeInfinity);

            array_2d = new double[,] {
                { double.MinValue, double.NaN, double.MaxValue },
                { double.MinValue, double.NaN, double.MaxValue }
            };

            Assert.IsTrue(array_2d.Max() == double.MaxValue);
            Assert.IsTrue(array_2d.Min() == double.MinValue);
        }

        [TestMethod]
        public void Test__Min_Max_Methods__Array2D__Float()
        {
            float[,] array_2d = new float[,] {
                { float.NaN, 0.0f, 1.0f },
                { float.NaN, 0.0f, 1.0f },
                { float.NaN, 0.0f, 1.0f }
            };

            Assert.IsTrue(array_2d.Max() == 1.0);
            Assert.IsTrue(array_2d.Min() == 0.0);

            array_2d = new float[,] {
                { float.NaN, float.NaN},
                { float.NaN, float.NaN}
            };

            Assert.IsTrue(float.IsNaN(array_2d.Max()));
            Assert.IsTrue(float.IsNaN(array_2d.Min()));

            array_2d = new float[,] {
                { float.NegativeInfinity, float.NaN, float.PositiveInfinity },
                { float.NegativeInfinity, float.NaN, float.PositiveInfinity }
            };

            Assert.IsTrue(array_2d.Max() == float.PositiveInfinity);
            Assert.IsTrue(array_2d.Min() == float.NegativeInfinity);

            array_2d = new float[,] {
                { float.NaN, float.PositiveInfinity },
                { float.NaN, float.PositiveInfinity }
            };

            Assert.IsTrue(array_2d.Max() == float.PositiveInfinity);
            Assert.IsTrue(array_2d.Min() == float.PositiveInfinity);

            array_2d = new float[,] {
                { float.NegativeInfinity, float.NaN},
                { float.NegativeInfinity, float.NaN}
            };

            Assert.IsTrue(array_2d.Max() == float.NegativeInfinity);
            Assert.IsTrue(array_2d.Min() == float.NegativeInfinity);

            array_2d = new float[,] {
                { float.NegativeInfinity, float.MinValue, float.MaxValue, float.PositiveInfinity },
                { float.NegativeInfinity, float.MinValue, float.MaxValue, float.PositiveInfinity }
            };

            Assert.IsTrue(array_2d.Max() == float.PositiveInfinity);
            Assert.IsTrue(array_2d.Min() == float.NegativeInfinity);

            array_2d = new float[,] {
                { float.MinValue, float.NaN, float.MaxValue },
                { float.MinValue, float.NaN, float.MaxValue }
            };

            Assert.IsTrue(array_2d.Max() == float.MaxValue);
            Assert.IsTrue(array_2d.Min() == float.MinValue);
        }




        [TestMethod]
        public void Test__Map_Value_Double__Usage()
        {
            double num, from_min, from_max, to_min, to_max;
            double remapped;

            from_min = 0.0;
            from_max = 1.0;
            to_min = 0.0;
            to_max = 10.0;
            List<double> nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            List<double> results = new List<double>()
            {
                -10.0, -5.0, 0.0, 5.0, 10.0, 15.0, 20.0, 25.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0;
            from_max = 1.0;
            to_min = 10.0;
            to_max = 0.0;
            nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            results = new List<double>()
            {
                20.0, 15.0, 10.0, 5.0, 0.0, -5.0, -10.0, -15.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            num = 0.0;
            remapped = num.Map_Value(0.0, 0.0, 0.0, 0.0);
            Assert.IsTrue(Double.IsNaN(remapped));
            Assert.IsTrue(remapped.Equals(Double.NaN));

            num = 0.0;
            remapped = num.Map_Value(0.0, 0.0, 0.0, 1.0);
            Assert.IsTrue(Double.IsNaN(remapped));

            num = 0.0;
            remapped = num.Map_Value(0.0, 1.0, 0.0, 0.0);
            Assert.IsTrue(remapped == 0.0);

        }


        [TestMethod]
        public void Test__Map_Value_Float__Usage()
        {
            float num, from_min, from_max, to_min, to_max;
            float remapped;

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 0.0f;
            to_max = 10.0f;
            List<float> nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            List<float> results = new List<float>()
            {
                -10.0f, -5.0f, 0.0f, 5.0f, 10.0f, 15.0f, 20.0f, 25.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 10.0f;
            to_max = 0.0f;
            nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            results = new List<float>()
            {
                20.0f, 15.0f, 10.0f, 5.0f, 0.0f, -5.0f, -10.0f, -15.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            num = 0.0f;
            remapped = num.Map_Value(0.0f, 0.0f, 0.0f, 0.0f);
            Assert.IsTrue(float.IsNaN(remapped));
            Assert.IsTrue(remapped.Equals(float.NaN));

            num = 0.0f;
            remapped = num.Map_Value(0.0f, 0.0f, 0.0f, 1.0f);
            Assert.IsTrue(float.IsNaN(remapped));

            num = 0.0f;
            remapped = num.Map_Value(0.0f, 1.0f, 0.0f, 0.0f);
            Assert.IsTrue(remapped == 0.0f);
        }


        [TestMethod]
        public void Test__Map_Value_Double_Constrained__Usage()
        {
            double from_min, from_max, to_min, to_max;
            double remapped;

            from_min = 0.0;
            from_max = 1.0;
            to_min = 0.0;
            to_max = 10.0;
            List<double> nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            List<double> results = new List<double>()
            {
                0.0, 0.0, 0.0, 5.0, 10.0, 10.0, 10.0, 10.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0;
            from_max = 1.0;
            to_min = 10.0;
            to_max = 0.0;
            nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            results = new List<double>()
            {
                10.0, 10.0, 10.0, 5.0, 0.0, 0.0, 0.0, 0.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }
        }


        [TestMethod]
        public void Test__Map_Value_Float_Constrained__Usage()
        {
            float from_min, from_max, to_min, to_max;
            float remapped;

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 0.0f;
            to_max = 10.0f;
            List<float> nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            List<float> results = new List<float>()
            {
                0.0f, 0.0f, 0.0f, 5.0f, 10.0f, 10.0f, 10.0f, 10.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 10.0f;
            to_max = 0.0f;
            nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            results = new List<float>()
            {
                10.0f, 10.0f, 10.0f, 5.0f, 0.0f, 0.0f, 0.0f, 0.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Map_Value_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }
        }
    }
}
