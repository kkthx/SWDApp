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
    [Activity(Label = "CriterionSelectActivity")]
    public class CriterionSelectActivity : Activity
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

            title.Text = "Wybór kryteriów";
            description.Text = "Zdecyduj, które kryterium jest ważniejsze";

            criterion1.Text = ahp.firstCriterion();
            criterionEqual.Text = "Jednakowa ważność obu kryteriów";
            criterion2.Text = ahp.secondCriterion();

            criterionText.Text = string.Format("Kryterium \"{0}\" \n jest tak samo preferowane względem\nkryterium \"{1}\"", ahp.firstCriterion(), ahp.secondCriterion());
            criterionText2.Text = "";

            radioGroup.CheckedChange += (object sender, RadioGroup.CheckedChangeEventArgs e) =>
            {
                if (criterionEqual.Checked)
                {
                    seekBar1.Enabled = false;
                    criterionText2.Text = "";
                    criterionText.Text = string.Format("Kryterium \"{0}\"\n", ahp.firstCriterion());
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
                    Toast.MakeText(this, "Go to decision comparision matrix", ToastLength.Short).Show();
                    StartActivity(typeof(DecisionSelectActivity));
                }
            };
        }

        void criterionTextAssign(int progress)
        {
            if (criterion1.Checked)
                criterionText.Text = string.Format("Kryterium \"{0}\"\n", ahp.firstCriterion());
            else
                criterionText.Text = string.Format("Kryterium \"{0}\"\n", ahp.secondCriterion());

            switch (progress)
            {
                case 0:
                    criterionText.Text += string.Format("jest nieznacznie bardziej preferowane względem\nkryterium \"{0}\"", criterion1.Checked ? ahp.secondCriterion() : ahp.firstCriterion());
                    break;
                case 1:
                    criterionText.Text += string.Format("jest silniej preferowane względem\nkryterium \"{0}\"", criterion1.Checked ? ahp.secondCriterion() : ahp.firstCriterion());
                    break;
                case 2:
                    criterionText.Text += string.Format("jest bardzo silnie preferowane względem\nkryterium \"{0}\"", criterion1.Checked ? ahp.secondCriterion() : ahp.firstCriterion());
                    break;
                case 3:
                    criterionText.Text += string.Format("jest wyłącznie preferowane względem\nkryterium \"{0}\"", criterion1.Checked ? ahp.secondCriterion() : ahp.firstCriterion());
                    break;
                default:
                    criterionText.Text += string.Format("jest tak samo preferowane względem\nkryterium \"{0}\"", criterion1.Checked ? ahp.secondCriterion() : ahp.firstCriterion());
                    break;
            }
        }
    }
}