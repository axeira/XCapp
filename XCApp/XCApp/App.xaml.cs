﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Xamarin.Forms;

namespace XCApp
{

    public partial class App : Application
	{
        

        public App ()
		{

        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
        Constants.YearsFill();
        Constants.MonthsFill();

        }

        protected override void OnStart ()
		{
			//+++ Handle when your app starts
		}

		protected override void OnSleep ()
		{
			//+++ Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			//+++ Handle when your app resumes
		}


    }
}
