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
    [Activity(Label = "InitialSelection")]
    public class InitialSelection : Activity
    {
        AHP ahp;

        TextView title;
        TextView description;

        Button next;

        CheckBox checkBox1;
        CheckBox checkBox2;
        CheckBox checkBox3;
        CheckBox checkBox4;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InitialSelector);

            title = FindViewById<TextView>(Resource.Id.title);
            description = FindViewById<TextView>(Resource.Id.description);

            next = FindViewById<Button>(Resource.Id.next);

            checkBox1 = FindViewById<CheckBox>(Resource.Id.checkBox1);
            checkBox2 = FindViewById<CheckBox>(Resource.Id.checkBox2);
            checkBox3 = FindViewById<CheckBox>(Resource.Id.checkBox3);
            checkBox4 = FindViewById<CheckBox>(Resource.Id.checkBox4);

            title.Text = "Wybór kryteriów";
            description.Text = "Wybierz kryteria które cię interesują";

            List<string> criterions = new List<string>();
            List<string> decisions = new List<string>
                (new string[] { "HIMOUNTAIN TAWOCHE 35+10", "SALEWA MOUNTAIN GUIDE PRO 38", "OSPREY VARIANT 37" });

            checkBox1.Text = "Wygoda";
            checkBox2.Text = "Waga";
            checkBox3.Text = "Wodoodporność";
            checkBox4.Text = "Budowa";

            next.Click += (object sender, EventArgs e) =>
            {
                criterions.Clear();

                if (checkBox1.Checked)
                    criterions.Add(checkBox1.Text);

                if (checkBox2.Checked)
                    criterions.Add(checkBox2.Text);

                if (checkBox3.Checked)
                    criterions.Add(checkBox3.Text);

                if (checkBox4.Checked)
                    criterions.Add(checkBox4.Text);

                if (criterions.Count < 2)
                {
                    Toast.MakeText(this, "Wybierz co najmniej 2 kryteria...", ToastLength.Short).Show();
                }
                else
                {
                    MyApp.ahp = new AHP(criterions, decisions);
                    StartActivity(typeof(CriterionSelectActivity));
                }
            };
        }
    }
}