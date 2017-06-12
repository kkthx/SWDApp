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
    [Activity(Label = "CalculatedRank")]
    public class CalculatedRank : Activity
    {
        AHP ahp;

        TextView textView1;
        Button button1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CalculatedRank);

            ahp = MyApp.ahp;

            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            button1 = FindViewById<Button>(Resource.Id.button1);

            textView1.Text = string.Format(
"{0:0.00}   {1:0.00}   {2:0.00}   {3:0.00}\n" +
"{4:0.00}   {5:0.00}   {6:0.00}   {7:0.00}\n" +
"{8:0.00}   {9:0.00}   {10:0.00}   {11:0.00}\n" +
"{12:0.00}   {13:0.00}   {14:0.00}   {15:0.00}\n",
ahp.criterionRank(0, 0),
ahp.criterionRank(0, 1),
ahp.criterionRank(0, 2),
ahp.criterionRank(0, 3),
ahp.criterionRank(1, 0),
ahp.criterionRank(1, 1),
ahp.criterionRank(1, 2),
ahp.criterionRank(1, 3),
ahp.criterionRank(2, 0),
ahp.criterionRank(2, 1),
ahp.criterionRank(2, 2),
ahp.criterionRank(2, 3),
ahp.criterionRank(3, 0),
ahp.criterionRank(3, 1),
ahp.criterionRank(3, 2),
ahp.criterionRank(3, 3)

);


            button1.Click += (object sender, EventArgs e) =>
            {
                this.FinishAffinity();
            };
        }
    }
}