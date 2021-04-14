using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_MazeExtensions_MazeFeatureTables_MFT
    {

        private static double MaxNumCells_Reachable_Within_Steps(
            int maxNumSteps,
            int currentStep,
            double currentValue,
            double increment,
            double valueToAdd
            )
        {
            if (currentStep > maxNumSteps)
            {
                return currentValue;
            }

            currentValue += valueToAdd;

            return MaxNumCells_Reachable_Within_Steps(
                maxNumSteps,
                currentStep + 1,
                currentValue,
                increment,
                valueToAdd + increment
                );
        }
    }
}
