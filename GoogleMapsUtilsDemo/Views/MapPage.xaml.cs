using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GoogleMapsUtilsDemo.Views
{
    public partial class MapPage : ContentPage
    {
        IList pins;
        public MapPage()
        {
            InitializeComponent();
            pins = new List<Pin>();
            //37.68455 –97.34110

            int range = 37;
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                Pin pin = new Pin();
                pin.Label = i.ToString();
                double rLat = rnd.NextDouble() * range * rnd.Next(-1, 1);
                double rLng = rnd.NextDouble() * range * rnd.Next(-1, 1);
                pin.Position = new Position(rLat, rLng);
                pins.Add(pin);
            }
            map.Pins = pins;
        }
    }
}
