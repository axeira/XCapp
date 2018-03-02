using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
        public MainPage()
		{
			InitializeComponent();
            Title = "Main Page";

        }

        async void OnTapRecognizerTappedHelp(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            // Do something
            await Navigation.PushAsync(new HelpPage());
        }


        async void OnTapRecognizerTappedSearch1(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            // Do something
            //+++DisplayAlert("Alert", "Tap gesture recoganised", "OK");
            //+++Navigation.PushModalAsync(new SearchPage());
            await Navigation.PushAsync(new SearchPage());

        }
        async void OnTapRecognizerTappedSearch2(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            // Do something
            await Navigation.PushAsync(new SearchPage());

        }
    }
}
