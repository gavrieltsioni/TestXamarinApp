using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

            SetContentView(Resource.Layout.login);

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            EditText emailForm = FindViewById<EditText>(Resource.Id.emailForm);
            EditText passwordForm = FindViewById<EditText>(Resource.Id.passwordForm);
          
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
                    //missingInput.GetButton((int)DialogButtonType.Positive).KeyPress += (object sender, EventArgs e) => { };
                    return;
                }
                
                string urlParam = "?task=login&email=" + emailForm.Text + "&password=" + passwordForm.Text;

                JsonValue json = await get(urlParam);

                AlertDialog debug = new AlertDialog.Builder(this)
                    .SetMessage(json.ToString())
                    .SetPositiveButton("OK", delegate
                        {
                            Finish();
                        })
                    .Show();
                
            };

        }
        private async Task<JsonValue> get(string urlParam)
        {

            HttpWebRequest request;
            try
            {
                var targetUri = new Uri(url + urlParam);
                request = (HttpWebRequest)HttpWebRequest.Create(targetUri);
                request.ContentType = "application/json";
                request.Method = "GET";
            }
            catch (Exception e)
            {
                return e.Message;
            }


            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    return jsonDoc;
                }
            }
        }            
    }
}
