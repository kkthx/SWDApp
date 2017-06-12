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
        double[] RandomIndex = new double[] { 0, 0, 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45 };

        // Factorial of number
        static Func<int, int> Factorial = x => x < 0 ? -1 : x == 1 || x == 0 ? 1 : x * Factorial(x - 1);

        // list of values to compare (strings)
        List<string> comparisionNames;
        List<int[]> comparisionPermutations;
        public List<decimal[]> comparisionValuesMatrix { get; }
        public List<decimal[]> comparisionNormalizedMatrix { get; }
        public List<decimal> sumOfColumn;
        public List<decimal> averageOfRow;

        int currentComparision;


        public int Count { get { return comparisionNames.Count; } }
        public int Current { get { return currentComparision; } }

        public ComparisionMatrix(List<string> values)
        {
            comparisionNames = new List<string>(values);

            comparisionPermutations = new List<int[]>();

            comparisionValuesMatrix = new List<decimal[]>();
            for (int i = 0; i < comparisionNames.Count; i++)
            {
                comparisionValuesMatrix.Add(new decimal[comparisionNames.Count]);
                comparisionValuesMatrix[i][i] = 1;
            }

            comparisionNormalizedMatrix = new List<decimal[]>();
            for (int i = 0; i < comparisionNames.Count; i++)
                comparisionNormalizedMatrix.Add(new decimal[comparisionNames.Count]);

            sumOfColumn = new List<decimal>();
            averageOfRow = new List<decimal>();


            currentComparision = 0;
            calculateComparisions();
        }

        void calculateComparisions()
        {
            int a = 1, b = 2;
            for (int i = 0; i < Factorial(comparisionNames.Count - 1); i++)
            {
                comparisionPermutations.Add(new int[] { a - 1, b - 1 });

                if (b >= comparisionNames.Count)
                {
                    b = a + 1;
                    a++;
                }
                b++;
            }
        }

        void normalizeMatrix()
        {
            for (int i = 0; i < comparisionValuesMatrix.Count; i++)
            {
                //calculate sum of column
                decimal sum = (decimal)0;
                foreach (var v in comparisionValuesMatrix)
                    sum += v[i];

                sumOfColumn.Add(sum);
                //divide every element by its sum of column
                for (int j = 0; j < comparisionValuesMatrix.Count; j++)
                    comparisionNormalizedMatrix[j][i] = comparisionValuesMatrix[j][i] / sum;
            }

            //calcuate average rows of normalized matrix
            foreach (var v in comparisionNormalizedMatrix)
                averageOfRow.Add(v.Sum() / v.Length);
        }

        public decimal calculateConsistency()
        {
            int k = comparisionNames.Count;
            decimal randomIndex = (decimal)RandomIndex[k];
            decimal consistencyIndex = 0;
            for (int i = 0; i < k; i++)
                consistencyIndex += sumOfColumn[i] * averageOfRow[i];
            consistencyIndex = (consistencyIndex - k) / (k - 1);

            //return Consistency Ratio CR=CI/RI
            return consistencyIndex / randomIndex;
        }

        public bool isConsistent { get { return calculateConsistency() < (decimal)0.1 ? true : false; } }

        public bool nextCriterion()
        {
            if (currentComparision < comparisionPermutations.Count - 1)
            {
                ++currentComparision;
                return true;
            }
            else
            {
                normalizeMatrix();
                return false;
            }
        }

        public string firstComparisionName()
        {
            return comparisionNames[comparisionPermutations[currentComparision][0]];
        }

        public string secondComparisionName()
        {
            return comparisionNames[comparisionPermutations[currentComparision][1]];
        }

        public bool saveRank(decimal rank)
        {
            comparisionValuesMatrix[comparisionPermutations[currentComparision][0]][comparisionPermutations[currentComparision][1]] = (decimal)rank;
            comparisionValuesMatrix[comparisionPermutations[currentComparision][1]][comparisionPermutations[currentComparision][0]] = 1 / (decimal)rank;
            return true;
        }

    }

    public class AHP
    {
        ComparisionMatrix criterionMatrix;
        List<ComparisionMatrix> decisionMatrix;
        public AHP()
        {
            criterionMatrix = new ComparisionMatrix(new List<string>(new string[] { "Wygoda", "Waga", "Wodoodporność", "Budowa" }));
            decisionMatrix = new List<ComparisionMatrix>();
            for (int i = 0; i < criterionMatrix.Count; i++)
            {
                ComparisionMatrix c = new ComparisionMatrix
                    (new List<string>(new string[] { "HIMOUNTAIN TAWOCHE 35+10", "SALEWA MOUNTAIN GUIDE PRO 38", "OSPREY VARIANT 37" }));
                decisionMatrix.Add(c);
            }
        }

        public string firstCriterion()
        {
            return criterionMatrix.firstComparisionName();
        }

        public string secondCriterion()
        {
            return criterionMatrix.secondComparisionName();
        }

        public bool nextCriterion()
        {
            return criterionMatrix.nextCriterion();
        }

        public int getC()
        {
            return criterionMatrix.Current;
        }

        public decimal ci {  get { return criterionMatrix.calculateConsistency();  } }
        public decimal criterionRank(int i, int j) { return criterionMatrix.comparisionValuesMatrix[i][j]; }
        public decimal sum(int j) { return criterionMatrix.sumOfColumn[j]; }
        public decimal avg(int j) { return criterionMatrix.averageOfRow[j]; }
        public bool saveCriterion(decimal rank)
        {
            return criterionMatrix.saveRank(rank);
        }
    }
}