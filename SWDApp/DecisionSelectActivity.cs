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
    [Activity(Label = "DecisionSelectActivity")]
    public class DecisionSelectActivity : Activity
    {
        AHP ahp;

        SeekBar seekBar1;

        TextView title;
        TextView description;

        TextView criterionText;
        TextView criterionText2;
        TextView textView1;

        RadioGroup radioGroup;
        RadioButton checkedCriterion;
        RadioButton criterion1;
        RadioButton criterion2;
        RadioButton criterionEqual;

        Button next;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Selector);

            ahp = MyApp.ahp;

            seekBar1 = FindViewById<SeekBar>(Resource.Id.seekBar1);

            title = FindViewById<TextView>(Resource.Id.title);
            description = FindViewById<TextView>(Resource.Id.description);

            criterionText = FindViewById<TextView>(Resource.Id.criterionText);
            criterionText2 = FindViewById<TextView>(Resource.Id.criterionText2);
            textView1 = FindViewById<TextView>(Resource.Id.textView1);

            radioGroup = FindViewById<RadioGroup>(Resource.Id.criterionGroup);
            checkedCriterion = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
            criterion1 = FindViewById<RadioButton>(Resource.Id.criterion1);
            criterion2 = FindViewById<RadioButton>(Resource.Id.criterion2);
            criterionEqual = FindViewById<RadioButton>(Resource.Id.criterionEqual);

            next = FindViewById<Button>(Resource.Id.next);

            seekBar1.Enabled = false;
            textView1.Text = "";

            title.Text = "Wybór wariantów";
            description.Text = string.Format("Zdecyduj, który wariant jest lepszy pod względem decyzji \"{0}\"", ahp.currentDecisionName);

            criterion1.Text = ahp.firstDecisionComparision();
            criterionEqual.Text = "Jednakowa ważność obu decyzji";
            criterion2.Text = ahp.secondDecisionComparision();

            criterionText.Text = string.Format("Wariant \"{0}\"\njest tak samo preferowany względem\nwariantu \"{1}\"", ahp.firstDecisionComparision(), ahp.secondDecisionComparision());
            criterionText2.Text = "";

            radioGroup.CheckedChange += (object sender, RadioGroup.CheckedChangeEventArgs e) =>
            {
                if (criterionEqual.Checked)
                {
                    seekBar1.Enabled = false;
                    criterionText.Text = string.Format("Wariant \"{0}\"\njest tak samo preferowany względem\nwariantu \"{1}\"", ahp.firstDecisionComparision(), ahp.secondDecisionComparision());
                }
                else
                {
                    seekBar1.Enabled = true;
                    criterionTextAssign(seekBar1.Progress);
                }
            };

            seekBar1.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
                criterionTextAssign(e.Progress);

            next.Click += (object sender, EventArgs e) =>
            {
                if (criterionEqual.Checked)
                    ahp.saveDecisionComparision(1);
                else
                {

                    switch (seekBar1.Progress)
                    {
                        case 0: //nieznacznie bardziej preferowane 
                            ahp.saveDecisionComparision(criterion1.Checked ? 3 : (decimal)1 / 3);
                            break;
                        case 1: //silniej preferowane
                            ahp.saveDecisionComparision(criterion1.Checked ? 5 : (decimal)1 / 5);
                            break;
                        case 2: //bardzo silnie preferowane
                            ahp.saveDecisionComparision(criterion1.Checked ? 5 : (decimal)1 / 5);
                            break;
                        case 3: //wyłącznie preferowane
                            ahp.saveDecisionComparision(criterion1.Checked ? 9 : (decimal)1 / 9);
                            break;
                        default:
                            break;
                    }
                }

                if (ahp.nextDecisionComparision())
                {
                    StartActivity(typeof(DecisionSelectActivity));
                }
                else if (ahp.nextDecision())
                {
                    Toast.MakeText(this, "Następny wariant...", ToastLength.Short).Show();
                    StartActivity(typeof(DecisionSelectActivity));
                }
                else
                {
                    StartActivity(typeof(CalculatedRank));
                }
            };
        }

        void criterionTextAssign(int progress)
        {
            if (criterion1.Checked)
                criterionText.Text = string.Format("Wariant \"{0}\"\njest ", ahp.firstDecisionComparision());
            else
                criterionText.Text = string.Format("Wariant \"{0}\"\njest ", ahp.secondDecisionComparision());


            switch (progress)
            {
                case 0:
                    criterionText.Text += "nieznacznie bardziej preferowany";
                    break;
                case 1:
                    criterionText.Text += "silniej preferowany";
                    break;
                case 2:
                    criterionText.Text += "bardzo silnie preferowany";
                    break;
                case 3:
                    criterionText.Text += "wyłącznie preferowany";
                    break;
                default:
                    criterionText.Text += "tak samo preferowany";
                    break;
            }

            if (criterion1.Checked)
                criterionText.Text += string.Format(" niż\nwariant \"{0}\"", ahp.secondDecisionComparision());
            else
                criterionText.Text += string.Format(" niż\nwariant \"{0}\"", ahp.firstDecisionComparision());

        }
    }
}