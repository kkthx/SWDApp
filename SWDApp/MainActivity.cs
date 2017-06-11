using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;
using Android.Content;

namespace SWDApp
{
    [Activity(Label = "SWDApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            TextView t1 = FindViewById<TextView>(Resource.Id.textView1);
            Button b1 = FindViewById<Button>(Resource.Id.mainNext);
            AHP ahp = new AHP();

            b1.Click += (object sender, EventArgs e) =>
            {
                Intent intent = new Intent(this, typeof(CriterionSelectActivity));
                intent.PutExtra("ahp", JsonConvert.SerializeObject(ahp));

                StartActivity(intent);
            };


        }
    }
}

