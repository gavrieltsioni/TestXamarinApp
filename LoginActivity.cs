using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApp
{
    [Activity(Label = "Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        int count = 1;
        private string url = "http://www.simxtech.com/gavriel/ma.stt";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.login);

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            EditText emailForm = FindViewById<EditText>(Resource.Id.emailForm);
            EditText passwordForm = FindViewById<EditText>(Resource.Id.passwordForm);
            
            loginButton.Click += (object sender, EventArgs e) => {
                if(emailForm.Text == null || passwordForm.Text == null){
                    var missingInput = new AlertDialog.Builder(this)
                        .SetTitle("Missing Input")
                        .SetMessage("You must input both an email and a password.")
                        .SetNeutralButton("OK", delegate {
                            Finish();
                        })
                        .Show();
                    //missingInput.GetButton((int)DialogButtonType.Positive).KeyPress += (object sender, EventArgs e) => { };
                    return;
                }
                string urlParam = "?task=login&email=" + emailForm.Text + "&password=" + passwordForm.Text;
                AlertDialog debug = new AlertDialog.Builder(this)
                    .SetMessage(url + urlParam)
                    .SetPositiveButton("OK", delegate
                        {
                            Finish();
                        })
                    .Show();

            };

        }
    }
}