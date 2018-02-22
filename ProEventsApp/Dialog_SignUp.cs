using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;


namespace ProEventsApp
{
    public class OnSignUpEventArgs : EventArgs
    {
        public int zID;
        public string zUsername;
        public string zPassword;
        public string zConfirmPassword;

        public OnSignUpEventArgs(string username, string password, string confirmPassword ) : base()
        {
            zUsername = username;
            zPassword = password;
            zConfirmPassword = confirmPassword;
        }
    }
    [Activity(Label = "Dialog_SignUp")]
    public class Dialog_SignUp : Activity
    {
        public static MobileServiceClient Client =
        new MobileServiceClient("https://proevents.azurewebsites.net");

        // public event EventHandler<OnSignUpEventArgs> ZOnSignUpComplete;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dialog_Sign_Up);

            var zBtnSignUp = FindViewById<Button>(Resource.Id.btnDialogSignup);
            zBtnSignUp.Click += ZBtnSignUp_Click;
            EditText zTxtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            zTxtUsername.TextChanged += ValidateInput;
            EditText zTxtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            zTxtPassword.TextChanged += ValidateInput;
            EditText zTxtConfirmPassword = FindViewById<EditText>(Resource.Id.txtConfirmPassword);
            zTxtConfirmPassword.TextChanged += ValidateInput;
        }

        private async void ZBtnSignUp_Click(object sender, EventArgs e)
        {
            EditText Username = FindViewById<EditText>(Resource.Id.txtUsername);
            EditText Password = FindViewById<EditText>(Resource.Id.txtPassword);
            EditText ConfirmPassword = FindViewById<EditText>(Resource.Id.txtConfirmPassword);

            if (Password.Text == ConfirmPassword.Text)
            {
                string hashedPassword = PasswordStorage.CreateHash(Password.Text);
                string userName = Username.Text.Trim();
                users newU = new users { username = userName, password = hashedPassword };
                List<users> allUsers = await Client.GetTable<users>().ToListAsync();
                users u = allUsers.FirstOrDefault(x => x.username == newU.username);
                if (u == null)
                {
                    DBHelper.InsertNewUser(newU);
                    Toast.MakeText(this, "User " + newU.username + " created! You can now log in!", ToastLength.Short).Show();
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    Toast.MakeText(this, "User " + u.username + " already exists!", ToastLength.Short).Show();
                }
            }
            else
            {
                string message = "Passwords don't match.";
                Toast.MakeText(this, message, ToastLength.Short).Show();
            }
         }

        private void ValidateInput(object sender, TextChangedEventArgs e)
        {
            EditText input = (EditText)sender;
            string pattern = "[^a-zA-Z0-9#?]";
            if (Regex.IsMatch(input.Text, pattern))
            {
                string message = "Input must only contain alphabetic characters, numbers, ? and #";
                Toast.MakeText(this, message, ToastLength.Long).Show();
                input.Text = "";
            }
        }
    }
}