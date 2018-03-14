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
		public MapPage ()
		{
            InitializeComponent();

            var position = new Position(37, -122); // Latitude, Longitude

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    position, Distance.FromKilometers(25)));
           
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "custom pin",
                Address = "custom detail info"
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