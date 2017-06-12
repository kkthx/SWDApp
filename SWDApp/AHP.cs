using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SWDApp
{

    class ComparisionMatrix
    {
        // Factorial of number
        static Func<int, int> Factorial = x => x < 0 ? -1 : x == 1 || x == 0 ? 1 : x * Factorial(x - 1);

        // list of values to compare (strings)
        List<string> comparisionNames;
        List<int[]> comparisionPermutations;
        List<int[]> comparisionValuesMatrix;

        int currentComparision;


        public int Count { get { return comparisionNames.Count; } }


        public ComparisionMatrix(List<string> values)
        {
            comparisionNames = new List<string>(values);

            comparisionPermutations = new List<int[]>();
            comparisionValuesMatrix = new List<int[]>();

            currentComparision = 0;
            calculateComparisions();
        }

        void calculateComparisions()
        {
            int a = 1, b = 2;
            for (int i = 0; i < Factorial(comparisionNames.Count - 1); i++)
            {
                comparisionPermutations.Add(new int[]{a-1, b-1});

                if (b >= comparisionNames.Count)
                {
                    b = a + 1;
                    a++;
                }
                b++;
            }
        }

        public bool nextCriterion()
        {
            if (currentComparision < comparisionPermutations.Count-1)
            {
                ++currentComparision;
                return true;
            }
            else
                return false;
        }

        public string firstCriterion()
        {
            return comparisionNames[comparisionPermutations[currentComparision][0]];
        }

        public string secondCriterion()
        {
            return comparisionNames[comparisionPermutations[currentComparision][1]];
        }

        public bool saveRank(int rank)
        {
            return true;
            if (rank < 1 || rank > 9)
            return false;
        }
        public int G()
        {
            return currentComparision;
        }
    }

    public class AHP
    {
        ComparisionMatrix criterionMatrix;
        List<ComparisionMatrix> decisionMatrix;
        public AHP()
        {
            criterionMatrix = new ComparisionMatrix(new List<string>(new string[] {"Wygoda", "Waga", "Wodoodporność", "Budowa"}));
            
        }

        public string firstCriterion()
        {
            return criterionMatrix.firstCriterion();
        }

        public string secondCriterion()
        {
            return criterionMatrix.secondCriterion();
        }

        public bool nextCriterion()
        {
            return criterionMatrix.nextCriterion();
        }

        public int getC()
        {
            return criterionMatrix.G();
        }

    }
}