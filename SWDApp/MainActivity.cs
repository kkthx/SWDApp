using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Text;
using System.Collections.Generic;


using Android.Content;


namespace SWDApp
{
    [Activity(Label = "SWDApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public static readonly List<string> phoneNumbers = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

           
            Button b1 = FindViewById<Button>(Resource.Id.button1);

            b1.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(CriterionSelectActivity));
            };


        }
    }
}

