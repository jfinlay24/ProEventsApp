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
using Microsoft.WindowsAzure.MobileServices;

namespace ProEventsApp
{
    [Activity(Label = "Dialog_SignIn")]
    public class Dialog_SignIn : Activity
    {

        public static MobileServiceClient Client =
        new MobileServiceClient("https://proevents.azurewebsites.net");

        static ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
        ISharedPreferencesEditor edit = pref.Edit();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Set our view from the "sign in" layout resource
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dialog_Sign_In);
            EditText username = (EditText)FindViewById(Resource.Id.txtUsername);
            EditText password = (EditText)FindViewById(Resource.Id.txtPassword);
            username.Text = pref.GetString("UserName", "");
            password.Text = pref.GetString("Password", "");
            var login = FindViewById(Resource.Id.btnDialogSignin);
            login.Click += TryLoginAsync;
        }

        private async void TryLoginAsync(object sender, EventArgs e)
        {
            EditText txtUsername = (EditText)FindViewById(Resource.Id.txtUsername);
            EditText txtPassword = (EditText)FindViewById(Resource.Id.txtPassword);
            string user = txtUsername.Text.Trim();
            string zPassword = txtPassword.Text;

            if (!await DBHelper.DoesUserExist(user))
            {
                Toast.MakeText(this, "Invalid Login", ToastLength.Short).Show();
            }
            else
            {
                users u = await DBHelper.GetUser(user);
                if (PasswordStorage.VerifyPassword(zPassword, u.password))
                {
                    edit.PutString("UserID", u.ID);
                    edit.PutString("UserName", u.username);
                    edit.PutString("LoggedIn", "true");
                    edit.PutString("Password", u.password);
                    edit.Commit();
                    Toast.MakeText(ApplicationContext, "Success", ToastLength.Short).Show();
                    if (await DBHelper.IsUserProfileCreated(u.ID))
                    {
                        //go to main menu
                        Toast.MakeText(this, "Logged In and user made", ToastLength.Short).Show();
                        StartActivity(typeof(MainProfile));
                    }
                    else
                    {
                        Toast.MakeText(this, "Profile", ToastLength.Short).Show();
                        StartActivity(typeof(SetUpProfile));
                    }

                }
                else
                {
                    Toast.MakeText(this, "Invalid Login", ToastLength.Short).Show();
                }
            }
        }

        private void GoToMain(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}