﻿using TripBliss.Pages;
namespace TripBliss
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}