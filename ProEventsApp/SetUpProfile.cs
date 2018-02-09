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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Android.Text;
using Microsoft.WindowsAzure.MobileServices;

namespace ProEventsApp
{
    [Activity(Label = "SetUpProfile")]
    public class SetUpProfile : Activity
    {
        public static MobileServiceClient Client =
        new MobileServiceClient("https://proevents.azurewebsites.net");

        static ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
        ISharedPreferencesEditor edit = pref.Edit();

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Profile_Set_Up);
            #region setUpDropdowns
            Spinner countySelect = FindViewById<Spinner>(Resource.Id.county);
            var countyAdapter = ArrayAdapter.CreateFromResource(
            this, Resource.Array.county, Android.Resource.Layout.SimpleSpinnerItem);

            countyAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            countySelect.Adapter = countyAdapter;
            #endregion

            var saveButton = FindViewById(Resource.Id.btnSave);
            saveButton.Click += ValidateForm;
            //Create your application here
        }
        private async void TrySave()
        {
            CurrentPlatform.Init();
            EditText firstName = (EditText)FindViewById(Resource.Id.txtFirstName);
            EditText lastName = (EditText)FindViewById(Resource.Id.txtSecondName);
            EditText profession = (EditText)FindViewById(Resource.Id.txtProfession);
            Spinner county = (Spinner)FindViewById(Resource.Id.county);
            EditText description = (EditText)FindViewById(Resource.Id.txtDescripton);
            UserProf newUserInfo = new UserProf
            {
                userID = pref.GetString("UserID","NULL"),
                Firstname = firstName.Text,
                Lastname = lastName.Text,
                Profession = profession.Text,
                Description = description.Text,
                County = county.SelectedItem.ToString()
            };
            await Client.GetTable<UserProf>().InsertAsync(newUserInfo);
            Toast.MakeText(ApplicationContext, "User " + pref.GetString("UserName", "NULL") + " info created!", ToastLength.Short).Show();
        }
        private void ValidateForm(object sender, EventArgs e)
        {
            EditText firstName = (EditText)FindViewById(Resource.Id.txtFirstName);
            EditText lastName = (EditText)FindViewById(Resource.Id.txtSecondName);
            EditText profession = (EditText)FindViewById(Resource.Id.txtProfession);
            Spinner county = (Spinner)FindViewById(Resource.Id.county);
            EditText descripton = (EditText)FindViewById(Resource.Id.txtDescripton);
            string namePattern = "[^a-zA-Z]";
            bool validInput = false;
            #region fieldValidation
            if (firstName.Text == "")
            {
                firstName.Error = "Cannot be Empty";
                firstName.RequestFocus();
            }
            else if (lastName.Text == "")
            {
                lastName.Error = "Cannot be Empty";
                lastName.RequestFocus();
            }
            else if (profession.Text == "")
            {
                profession.Error = "Cannot be Empty";
                profession.RequestFocus();
            }
            else if (descripton.Text == "")
            {
                descripton.Error = "Cannot be Empty";
                descripton.RequestFocus();
            }
            else if (Regex.IsMatch(firstName.Text, namePattern))
            {
                firstName.Error = "Invalid characters in name";
                firstName.RequestFocus();
            }
            else if (Regex.IsMatch(lastName.Text, namePattern))
            {
                lastName.Error = "Invalid characters in name";
                lastName.RequestFocus();
            }
            else
            {
                validInput = true;
            }
            #endregion
            if (validInput)
            {
                TrySave();
                Toast.MakeText(this, "Welcome to your Profile", ToastLength.Short).Show();
                StartActivity(typeof(MainProfile));
            }
        }
    }
}