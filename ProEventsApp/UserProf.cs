﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProEventsApp
{
    public class UserProf
    {
        public string ID { get; set; }
        public string userID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Profession { get; set; }
        public string County { get; set; }
        public string Description { get; set; }
    }
}