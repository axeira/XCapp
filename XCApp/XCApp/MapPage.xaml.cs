using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace XCApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage (double x, double y,int km)
		{
            InitializeComponent();

            var position = new Position(x, y); // Latitude, Longitude

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    position, Distance.FromKilometers(km)));
           
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Localization",
                Address = ""
            };
            MyMap.Pins.Add(pin);

		}

        async void OnTappedSearch(object sender, EventArgs args)
        {
            //Move the next window
            await Navigation.PushAsync(new ListViewPage(), true);
        }
    }
}