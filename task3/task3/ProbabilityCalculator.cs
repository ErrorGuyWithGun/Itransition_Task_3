using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    internal class ProbabilityCalculator
    {
        public static double CalculateWinProbability(Dice dice1, Dice dice2)
        {
            int wins = 0;
            foreach (int face1 in dice1.Faces)
            {
                foreach (int face2 in dice2.Faces)
                {
                    if (face1 > face2) wins++;
                }
            }
            return (double)wins;
        }
    }
}
