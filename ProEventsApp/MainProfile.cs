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
using System.Threading.Tasks;

namespace ProEventsApp
{
    [Activity(Label = "MainProfile")]
    public class MainProfile : Activity
    {
        public static MobileServiceClient Client =
        new MobileServiceClient("https://proevents.azurewebsites.net");

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main_Profile);
            ISharedPreferences login = Application.Context.GetSharedPreferences("Login", FileCreationMode.Private);

            String username = login.GetString("Username", null);
            String password = login.GetString("Password", null);

            CurrentPlatform.Init();
            List<UserProf> ls = await Client.GetTable<UserProf>().ToListAsync();
            UserProf prof = ls.FirstOrDefault(x => x.Username == username);
            Toast.MakeText(this, "Hello " + prof.Firstname + " " + prof.Lastname, ToastLength.Short).Show();
        }
    }
}