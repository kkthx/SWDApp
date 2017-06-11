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

        // key - value
        Dictionary<int, int> criterionValues;

        // list of  criterions (strings)
        List<string> criterion;
        List<int[]> criterionList;

        int currentCriterion;
        int firstCriterionValue;
        int secondCriterionValue;

        public Criterions()
        {
            criterion = new List<string>();
            criterion.Add("Wygoda");
            criterion.Add("Waga");
            criterion.Add("Wodoodporność");
            criterion.Add("Budowa");

            criterionList = new List<int[]>();

            currentCriterion = 0;
            calculateCriterions();
        }

        void calculateCriterions()
        {
            int a = 1, b = 2;
            for (int i = 0; i < Factorial(criterion.Count - 1); i++)
            {
                criterionList.Add(new int[]{a-1, b-1});

                if (b >= criterion.Count)
                {
                    b = a + 1;
                    a++;
                }
                b++;
            }
        }

        public bool nextCriterion()
        {
            if (currentCriterion < Factorial(criterion.Count - 1))
            {
                ++currentCriterion;
                return true;
            }
            else
                return false;
        }

        public string firstCriterion()
        {
            return criterion[criterionList[currentCriterion][0]];
        }

        public string secondCriterion()
        {
            return criterion[criterionList[currentCriterion][1]];
        }
    }

    class AHP
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

    }
}