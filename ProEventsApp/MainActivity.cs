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
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            btnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            btnSignIn.Click += async (object sender, EventArgs args) =>
            {
                //Pull up dialog
                Toast.MakeText(this, "Welcome to Login", ToastLength.Short).Show();
                StartActivity(typeof(Dialog_SignIn));
            };

            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            btnSignUp.Click += (object sender, EventArgs args) =>
            {

                //Pull up dialog
                //FragmentTransaction transaction = FragmentManager.BeginTransaction();
                //Dialog_SignUp signUpDialog = new Dialog_SignUp();
                //signUpDialog.Show(transaction, "Dialog Fragment");
                StartActivity(typeof(Dialog_SignUp));
                //signUpDialog.ZOnSignUpComplete += SignUpDialog_zOnSignUpComplete;
            };

        }

       /* private void SignUpDialog_zOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            Thread thead = new Thread(ActLikeARequest);
            thead.Start();
        }

        private void ActLikeARequest()
        {
            Thread.Sleep(3000);
        }*/
    }
}

