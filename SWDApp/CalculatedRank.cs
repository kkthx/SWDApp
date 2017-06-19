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

            List<decimal> findMax = new List<decimal>();

            for (int i = 0; i < ahp.decisionCount; i++)
            {
                findMax.Add(ahp.rank(i));
            }

            textView1.Text = string.Format("Ranking:\n");

            for (int i = 0; i < ahp.decisionCount; i++)
            {
                textView1.Text += string.Format("{0}: {1:0.000}%\n", ahp.decisionName(i), ahp.rank(i) * 100);
            }


            textView1.Text += string.Format("\nNajlepszy wybór:\n{0}\n{1:0.000}%\n", ahp.decisionName(findMax.IndexOf(findMax.Max())), findMax.Max() * 100);

            switch (findMax.IndexOf(findMax.Max()))
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