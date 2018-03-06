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
	public partial class SearchPage : ContentPage
    {
        public XCAPIClass.XCAPISearch XCQuery = new XCAPIClass.XCAPISearch();
        //+++public string QueryRequest;

        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new XCAPIClass.XCAPISearch();

            pickerCnt.ItemsSource = ConstantsClass.Countries;
            pickerArea.ItemsSource = ConstantsClass.Area;
            pickerType.ItemsSource = ConstantsClass.Type;
            pickerQ.ItemsSource = ConstantsClass.Quality;
        }

        async void OnTapGestureRecognizerTappedPlay(object sender, EventArgs args)
        {
            //Store Query parameters
            XCQuery = ((XCAPIClass.XCAPISearch)this.BindingContext);

            XCAPIClass.QueryRequest = XCAPIClass.BuildTheQuery(XCQuery);
            System.Diagnostics.Debug.WriteLine("=============== " + XCAPIClass.QueryRequest);

            //Move the next window
            await Navigation.PushAsync(new ListViewPage(), true);
        }

}
}