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
        ImageView bestDecision;
        Button button1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CalculatedRank);

            ahp = MyApp.ahp;

            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            bestDecision = FindViewById<ImageView>(Resource.Id.bestDecision);
            button1 = FindViewById<Button>(Resource.Id.button1);

            /*
            textView1.Text = string.Format("CriterionRank:\n" +
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

            textView1.Text += string.Format("\nCriterion matrix:\n" +
"sum  {0:0.00}   {1:0.00}   {2:0.00}   {3:0.00}\n" +
"avg {4:0.00}   {5:0.00}   {6:0.00}   {7:0.00}\n" +
"ci {8:0.00}",
ahp.sum(0), ahp.sum(1), ahp.sum(2), ahp.sum(3),
ahp.avg(0), ahp.avg(1), ahp.avg(2), ahp.avg(3),
ahp.ci);

            int jjjjj;

            jjjjj = 0;
            textView1.Text += string.Format("\ndecision matrix[0]=\n" +
"sum  {0:0.00}   {1:0.00}   {2:0.00}\n" +
"avg {3:0.00}   {4:0.00}   {5:0.00}\n" +
"ci {6:0.00}",
ahp.dsum(jjjjj, 0), ahp.dsum(jjjjj, 1), ahp.dsum(jjjjj, 2),
ahp.davg(jjjjj, 0), ahp.davg(jjjjj, 1), ahp.davg(jjjjj, 2),
ahp.dci(jjjjj));

            jjjjj = 1;
            textView1.Text += string.Format("\ndecision matrix[1]=\n" +
   "sum  {0:0.00}   {1:0.00}   {2:0.00}\n" +
   "avg {3:0.00}   {4:0.00}   {5:0.00}\n" +
   "ci {6:0.00}",
   ahp.dsum(jjjjj, 0), ahp.dsum(jjjjj, 1), ahp.dsum(jjjjj, 2),
   ahp.davg(jjjjj, 0), ahp.davg(jjjjj, 1), ahp.davg(jjjjj, 2),
   ahp.dci(jjjjj));

            jjjjj = 2;
            textView1.Text += string.Format("\ndecision matrix[2]=\n" +
   "sum  {0:0.00}   {1:0.00}   {2:0.00}\n" +
   "avg {3:0.00}   {4:0.00}   {5:0.00}\n" +
   "ci {6:0.00}",
   ahp.dsum(jjjjj, 0), ahp.dsum(jjjjj, 1), ahp.dsum(jjjjj, 2),
   ahp.davg(jjjjj, 0), ahp.davg(jjjjj, 1), ahp.davg(jjjjj, 2),
   ahp.dci(jjjjj));

            jjjjj = 3;
            textView1.Text += string.Format("\ndecision matrix[3]=\n" +
   "sum  {0:0.00}   {1:0.00}   {2:0.00}\n" +
   "avg {3:0.00}   {4:0.00}   {5:0.00}\n" +
   "ci {6:0.00}",
   ahp.dsum(jjjjj, 0), ahp.dsum(jjjjj, 1), ahp.dsum(jjjjj, 2),
   ahp.davg(jjjjj, 0), ahp.davg(jjjjj, 1), ahp.davg(jjjjj, 2),
   ahp.dci(jjjjj));


            textView1.Text += string.Format("\n rank:\n" +
   "0 {0:0.0000}   1 {1:0.0000}   2 {2:0.0000}\n",
   ahp.rank(0), ahp.rank(1), ahp.rank(2)
   );
   */
            List<decimal> findMax = new List<decimal>();

            for (int i=0; i<ahp.decisionCount;i++)
            {
                findMax.Add(ahp.rank(i));
            }
            //ahp.crit
            // w zaleznosci od ilosci kryteriowwwwwww
            textView1.Text = string.Format("Ranking: deccnt={0} critcnt={1}\n", ahp.decisionCount, ahp.criterionCount);
            for (int i = 0; i < ahp.decisionCount; i++)
            {
                textView1.Text += string.Format("{0}: {1:0.000}%\n", ahp.decisionName(i), ahp.rank(i) * 100);
            }


            textView1.Text += string.Format("\nNajlepszy wybór:\n{0}\n{1:0.000}%\n", ahp.decisionName(findMax.IndexOf(findMax.Max())), findMax.Max() * 100);

            switch(findMax.IndexOf(findMax.Max()))
            {
                case 0:
                    bestDecision.SetImageResource(Resource.Drawable.tawoche);
                    break;
                case 1:
                    bestDecision.SetImageResource(Resource.Drawable.glide);
                    break;
                case 2:
                    bestDecision.SetImageResource(Resource.Drawable.variant);
                    break;
                default:
                    bestDecision.SetImageResource(Resource.Drawable.Icon);
                    break;
            }
            
            button1.Click += (object sender, EventArgs e) =>
            {
                this.FinishAffinity();
            };
        }
    }
}