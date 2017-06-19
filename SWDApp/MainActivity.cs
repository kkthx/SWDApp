using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;
using Android.Content;
using System.Xml.Serialization;
using System.Xml;
using Android.Runtime;

namespace SWDApp
{
    [Application]
    class MyApp : Application
    {
        public static AHP ahp;
        public MyApp(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip) { }

        public override void OnCreate()
        {
            base.OnCreate();

        }
    }

    [Activity(Label = "SWDApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            TextView t1 = FindViewById<TextView>(Resource.Id.textView1);
            Button b1 = FindViewById<Button>(Resource.Id.mainNext);

            b1.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(InitialSelection));
            };


        }
    }
}

