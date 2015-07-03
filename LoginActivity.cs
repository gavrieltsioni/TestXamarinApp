using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace TestApp
{
    [Activity(Label = "TestApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        int count = 1;
        private string url = "http://www.simxtech.com/gavriel/ma.stt";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var client = new HttpClient();
            SetContentView(Resource.Layout.login);

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            EditText emailForm = FindViewById<EditText>(Resource.Id.emailForm);
            EditText passwordForm = FindViewById<EditText>(Resource.Id.passwordForm);

            TextView responseText = FindViewById<TextView>(Resource.Id.responseText);
          
            loginButton.Click += async (object sender, EventArgs e) => {
                if (String.IsNullOrEmpty(emailForm.Text) || String.IsNullOrEmpty(passwordForm.Text))
                {
                    var missingInput = new AlertDialog.Builder(this)
                        .SetTitle("Missing Input")
                        .SetMessage("You must input both an email and a password.")
                        .SetNeutralButton("OK", delegate {
                            Finish();
                        })
                        .Show();
                    return;
                }
                
                string urlParam = "?task=login&email=" + emailForm.Text + "&password=" + passwordForm.Text;

                String result = "";
                try
                {
                    result = await client.GetStringAsync(new Uri(url + urlParam));
                }
                catch (Exception er)
                {
                    result = er.Message;
                }

                responseText.Text = result;       
            };
        }      
    }
}
