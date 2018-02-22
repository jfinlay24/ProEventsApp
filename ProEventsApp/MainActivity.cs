using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using Android.Content;

namespace ProEventsApp
{
    [Activity(Label = "ProEventsApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button btnSignIn;
        private Button btnSignUp;

        static ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
        ISharedPreferencesEditor edit = pref.Edit();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            btnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            btnSignIn.Click += async (object sender, EventArgs args) =>
            {
                Toast.MakeText(this, "Welcome to Login", ToastLength.Short).Show();
                StartActivity(typeof(Dialog_SignIn));
            };

            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            btnSignUp.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(Dialog_SignUp));
            };
        }
    }
}

