using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TestApp
{
    public class MainActivity : Activity
    {
        int count = 1;
        private string url = "http://www.simxtech.com/gavriel/ma.stt";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            EditText emailForm = FindViewById<EditText>(Resource.Id.emailForm);
            EditText passwordForm = FindViewById<EditText>(Resource.Id.passwordForm);

            loginButton.Click += (object sender, EventArgs e) =>
            {
                if (emailForm.Text == null || passwordForm.Text == null)
                {
                    var missingInput = new AlertDialog.Builder(this)
                        .SetTitle("Missing Input")
                        .SetMessage("You must input both an email and a password.")
                        .SetNeutralButton("OK", delegate
                    {
                        Finish();
                    })
                        .Show();
                    //missingInput.GetButton((int)DialogButtonType.Positive).KeyPress += (object sender, EventArgs e) => { };
                    return;
                }
                string urlParam = "?task=login&email=" + emailForm.Text + "&password=" + "passwordForm.Text";
                AlertDialog debug = new AlertDialog.Builder(this)
                    .SetMessage(url + urlParam)
                    .SetPositiveButton("OK", delegate
                    {
                        Finish();
                    })
                    .Show();

            };

            callButton.Enabled = false;
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) => {
                translatedNumber = Core.PhonewordTranslator.toNumber(phoneNumberText.Text);
                if(string.IsNullOrWhiteSpace(translatedNumber)){
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                } else {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            callButton.Click += (object sender, EventArgs e) => {
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", delegate {
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    StartActivity(callIntent);
                });
                callDialog.SetNegativeButton("Cancel", delegate {});
                callDialog.Show();
            };

        }
    }
}

