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
using Newtonsoft.Json;

namespace SWDApp
{
    [Activity(Label = "Activity1")]
    public class CriterionSelectActivity : Activity
    {
        /*
        var criterions = new Dictionary<int, string>
        {
            {1, "jest wyłącznie preferowane" },
            {3, "jest bardzo silnie preferowane względem" },
            {5, "" },
            {7, "" },
            {9, "" }
        };
        */
        private AHP ahp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //test if all criterions selected
            SetContentView(Resource.Layout.CriterionSelect);
            //else go next
            //StartActivity(typeof(DecisionSelectActivity));
            
            ahp = JsonConvert.DeserializeObject<AHP>(Intent.GetStringExtra("ahp"));
            SeekBar s1 = FindViewById<SeekBar>(Resource.Id.seekBar1);
            TextView criterionText = FindViewById<TextView>(Resource.Id.criterionText);
            TextView criterionTextValue = FindViewById<TextView>(Resource.Id.criterionTextValue);
            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.criterionGroup);
            RadioButton checkedCriterion = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
            RadioButton criterion1 = FindViewById<RadioButton>(Resource.Id.criterion1);
            RadioButton criterion2 = FindViewById<RadioButton>(Resource.Id.criterion2);
            RadioButton criterionEqual = FindViewById<RadioButton>(Resource.Id.criterionEqual);

            criterion1.Text = ahp.firstCriterion();
            criterionEqual.Text = string.Format("Kryterium {0} tak samo ważne jak kryterium {1}", ahp.firstCriterion(), ahp.secondCriterion());
            criterion2.Text = ahp.secondCriterion();

            radioGroup.CheckedChange += (object sender, RadioGroup.CheckedChangeEventArgs e) =>
            {
                if (criterionEqual.Checked)
                {
                    s1.Enabled = false;
                    criterionText.Enabled = false;
                    criterionTextValue.Enabled = false;
                }
                else 
                {
                    criterionText.Enabled = true;
                    criterionTextValue.Enabled = true;
                    s1.Enabled = true;

                    if (criterion1.Checked)
                        criterionText.Text = string.Format("Kryterium {0} względem {1} jest", ahp.firstCriterion(), ahp.secondCriterion());
                    else
                        criterionText.Text = string.Format("Kryterium {0} względem {1} jest", ahp.secondCriterion(), ahp.firstCriterion());
                }
            };

            RadioButton radioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);



            s1.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                switch(e.Progress)
                {
                    case 0:
                        criterionTextValue.Text = string.Format("nieznacznie bardziej preferowane {0}", e.Progress);
                        break;
                    case 1:
                        criterionTextValue.Text = string.Format("silniej preferowane {0}", e.Progress);
                        break;
                    case 2:
                        criterionTextValue.Text = string.Format("bardzo silnie preferowane {0}", e.Progress);
                        break;
                    case 3:
                        criterionTextValue.Text = string.Format("wyłącznie preferowane {0}", e.Progress);
                        break;
                    default:
                        criterionTextValue.Text = "";
                        break;
                }
            };
        }
    }
}