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

    class Criterions
    {
        // Factorial of number
        static Func<int, int> Factorial = x => x < 0 ? -1 : x == 1 || x == 0 ? 1 : x * Factorial(x - 1);

        

        // list of  criterions (strings)
        List<string> criterionNames;
        List<int[]> criterionPermutations;
        List<int[]> criterionValuesMatrix;

        int currentCriterion;
        int firstCriterionValue;
        int secondCriterionValue;

        public Criterions()
        {
            criterionNames = new List<string>();
            criterionNames.Add("Wygoda");
            criterionNames.Add("Waga");
            criterionNames.Add("Wodoodporność");
            criterionNames.Add("Budowa");

            criterionPermutations = new List<int[]>();
            criterionValuesMatrix = new List<int[]>();

            currentCriterion = 0;
            calculateCriterions();
        }

        void calculateCriterions()
        {
            int a = 1, b = 2;
            for (int i = 0; i < Factorial(criterionNames.Count - 1); i++)
            {
                criterionPermutations.Add(new int[]{a-1, b-1});

                if (b >= criterionNames.Count)
                {
                    b = a + 1;
                    a++;
                }
                b++;
            }
        }

        public bool nextCriterion()
        {
            if (currentCriterion <= criterionPermutations.Count)
            {
                ++currentCriterion;
                return true;
            }
            else
                return false;
        }

        public string firstCriterion()
        {
            return criterionNames[criterionPermutations[currentCriterion][0]];
        }

        public string secondCriterion()
        {
            return criterionNames[criterionPermutations[currentCriterion][1]];
        }

        public bool saveRank(int rank)
        {
            return true;
            if (rank < 1 || rank > 9)
            return false;
        }
        public int G()
        {
            return currentCriterion;
        }
    }

    public class AHP
    {
        Criterions criterions;
        public AHP()
        {
            criterions = new Criterions();
        }

        public string firstCriterion()
        {
            return criterions.firstCriterion();
        }

        public string secondCriterion()
        {
            return criterions.secondCriterion();
        }

        public bool nextCriterion()
        {
            return criterions.nextCriterion();
        }

        public int getC()
        {
            return criterions.G();
        }

    }
}