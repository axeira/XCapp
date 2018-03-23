using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace XCApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage (double x, double y,int km,string label, string address)
		{
            InitializeComponent();

            var position = new Position(x, y); // Latitude, Longitude

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    position, Distance.FromKilometers(km)));

            //+++
            var positionUpper = new Position();
            var positionLower = new Position();
            CalculateBoundingCoordinates(MyMap.LastMoveToRegion, out positionUpper, out positionLower);
            //+++

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = label,
                Address = address
            };
            MyMap.Pins.Add(pin);


        }

        static void CalculateBoundingCoordinates(MapSpan region, out Position positionUpper, out Position positionLower )
        {
            //+++ Region might be double size in UWP
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            positionUpper= new Position(top, left);
            positionLower = new Position(bottom, right);


            // Adjust for Internation Date Line (+/- 180 degrees longitude)
            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;
            // I don't wrap around north or south; I don't think the map control allows this anyway

            Console.WriteLine("Bounding box:");
            Console.WriteLine("                    " + top);
            Console.WriteLine("  " + left + "                " + right);
            Console.WriteLine("                    " + bottom);
        }

        async void OnTappedSearch(object sender, EventArgs args)
        {
            //+++
            var positionCenter = new Position(MyMap.VisibleRegion.Center.Latitude, MyMap.VisibleRegion.Center.Longitude);
            var positionUpper = new Position();
            var positionLower = new Position();
            if (MyMap.VisibleRegion==null)
                CalculateBoundingCoordinates(MyMap.LastMoveToRegion, out positionUpper, out positionLower); 
            else
                CalculateBoundingCoordinates(MyMap.VisibleRegion, out positionUpper, out positionLower);

            //Move the next window
            await Navigation.PushAsync(new ListViewPage(), true);
        }
    }
}