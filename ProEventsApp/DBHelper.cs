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
    public static class DBHelper
    {
        public static MobileServiceClient MobileService =
        new MobileServiceClient("https://proevents.azurewebsites.net");

        public static async void InsertNewUser(users u)
        {
            CurrentPlatform.Init();
            await MobileService.GetTable<users>().InsertAsync(u);

        }
        public static async void InsertNewUser(string userName, string password)
        {
            CurrentPlatform.Init();
            string hashedPassword = PasswordStorage.CreateHash(password);
            //CurrentPlatform.Init();
            users u = new users { username = userName, password = hashedPassword };
            await MobileService.GetTable<users>().InsertAsync(u);
        }
        public static async Task<users> GetUser(string userName)
        {
            CurrentPlatform.Init();
            List<users> ls = await MobileService.GetTable<users>().ToListAsync();
            users u = ls.FirstOrDefault(x => x.username == userName);
            return u;
        }
        public static async Task<List<users>> GetAllUsers()
        {
            CurrentPlatform.Init();
            List<users> ls = await MobileService.GetTable<users>().ToListAsync();
            return ls;
        }
        public static async Task<bool> DoesUserExist(string userName)
        {
            CurrentPlatform.Init();
            List<users> ls = await MobileService.GetTable<users>().ToListAsync();
            users u = ls.FirstOrDefault(x => x.username == userName);
            if (u != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void InsertUserProf(UserProf up)
        {
            CurrentPlatform.Init();
            MobileService.GetTable<UserProf>().InsertAsync(up);
        }
        public static void InsertUserProf(string username, string firstName, string lastName, string profession, string county, string description)
        {
            CurrentPlatform.Init();
            UserProf up = new UserProf
            {
                Username = username,
                Firstname = firstName,
                Lastname = lastName,
                Profession = profession,
                County = county,
                Description = description
            };
            MobileService.GetTable<UserProf>().InsertAsync(up);
        }
        public static async Task<UserProf> GetUserProfile(string zUsername)
        {
            CurrentPlatform.Init();
            List<UserProf> ls = await MobileService.GetTable<UserProf>().ToListAsync();
            UserProf u = ls.FirstOrDefault(x => x.Username == zUsername);
            return u;
        }
        public static async Task<bool> IsUserProfileCreated(string zUsername)
        {
            CurrentPlatform.Init();
            List<UserProf> ls = await MobileService.GetTable<UserProf>().ToListAsync();
            UserProf u = ls.FirstOrDefault(x => x.Username == zUsername);
            if (u != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}