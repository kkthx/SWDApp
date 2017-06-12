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

            //test if all criterions selected
            SetContentView(Resource.Layout.Selector);
            //else go next
            //StartActivity(typeof(DecisionSelectActivity));



            //get data from previous activity, deserializing it to object
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

            title.Text = "Wybór decyzji";
            description.Text = string.Format("Zdecyduj, który wybór jest lepszy pod względem kryterium \"{0}\"", "a");

            criterion1.Text = ahp.firstCriterion();
            criterionEqual.Text = "Jednakowa ważność obu decyzji";
            criterion2.Text = ahp.secondCriterion();

            criterionText.Text = string.Format("\"{0}\" względem \"{1}\" jest", ahp.firstCriterion(), ahp.secondCriterion());
            criterionText2.Text = "tak samo preferowany";


            radioGroup.CheckedChange += (object sender, RadioGroup.CheckedChangeEventArgs e) =>
            {
                if (criterionEqual.Checked)
                {
                    seekBar1.Enabled = false;
                    criterionText2.Text = "tak samo preferowany";
                    criterionText.Text = string.Format("Kryterium \"{0}\" względem \"{1}\" jest", ahp.firstCriterion(), ahp.secondCriterion());
                }
                else
                {
                    seekBar1.Enabled = true;

                    if (criterion1.Checked)
                        criterionText.Text = string.Format("\"{0}\" względem \"{1}\" jest", ahp.firstCriterion(), ahp.secondCriterion());
                    else
                        criterionText.Text = string.Format("\"{0}\" względem \"{1}\" jest", ahp.secondCriterion(), ahp.firstCriterion());

                    criterionTextAssign(seekBar1.Progress);
                }
            };

            seekBar1.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
                criterionTextAssign(e.Progress);

            next.Click += (object sender, EventArgs e) =>
            {
                if (criterionEqual.Checked)
                    ahp.saveCriterion(1);
                else
                {
                    switch (seekBar1.Progress)
                    {
                        case 0: //nieznacznie bardziej preferowane 
                            ahp.saveCriterion(criterion1.Checked ? 3 : (decimal)1 / 3);
                            break;
                        case 1: //silniej preferowane
                            ahp.saveCriterion(criterion1.Checked ? 5 : (decimal)1 / 5);
                            break;
                        case 2: //bardzo silnie preferowane
                            ahp.saveCriterion(criterion1.Checked ? 5 : (decimal)1 / 5);
                            break;
                        case 3: //wyłącznie preferowane
                            ahp.saveCriterion(criterion1.Checked ? 9 : (decimal)1 / 9);
                            break;
                        default:
                            break;
                    }
                }

                if (ahp.nextCriterion())
                    StartActivity(typeof(CriterionSelectActivity));
                else
                {
                    textView1.Text = string.Format(
"sum  {0:0.00}   {1:0.00}   {2:0.00}   {3:0.00}\n" +
"avg {4:0.00}   {5:0.00}   {6:0.00}   {7:0.00}\n" +
"ci {8:0.00}   ",

ahp.sum(0), ahp.sum(1), ahp.sum(2), ahp.sum(3),
ahp.avg(0), ahp.avg(1), ahp.avg(2), ahp.avg(3),
ahp.ci

);

                    Toast.MakeText(this, "Go to decision comparision matrix", ToastLength.Short).Show();
                    //StartActivity(typeof(CriterionSelectActivity));
                }
            };
        }

        void criterionTextAssign(int progress)
        {
            switch (progress)
            {
                case 0:
                    criterionText2.Text = "nieznacznie bardziej preferowany";
                    break;
                case 1:
                    criterionText2.Text = "silniej preferowany";
                    break;
                case 2:
                    criterionText2.Text = "bardzo silnie preferowany";
                    break;
                case 3:
                    criterionText2.Text = "wyłącznie preferowany";
                    break;
                default:
                    criterionText2.Text = "tak samo preferowany";
                    break;
            }
        }
    }
}