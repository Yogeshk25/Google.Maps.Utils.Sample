using System;
using GoogleMapsUtilsDemo.Views;
using Xamarin.Forms;

namespace GoogleMapsUtilsDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MapPage();
        }
    }
}
