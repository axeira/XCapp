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
            //+++Observable collection avoids this????
            XCQuery = ((XCAPIClass.XCAPISearch)this.BindingContext);
            //+++Remove down lines from here?
            //XCQuery.Name = ((XCAPIClass.XCAPISearch)this.BindingContext).Name;
            //XCQuery.Gen = ((XCAPIClass.XCAPISearch)this.BindingContext).Gen;
            //XCQuery.Rec = ((XCAPIClass.XCAPISearch)this.BindingContext).Rec;
            //XCQuery.Cnt = ((XCAPIClass.XCAPISearch)this.BindingContext).Cnt;
            //XCQuery.Cnt = ((XCAPIClass.XCAPISearch)this.BindingContext).Cnt;
            //XCQuery.CntIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).CntIndex;
            //XCQuery.Loc = ((XCAPIClass.XCAPISearch)this.BindingContext).Loc;
            //XCQuery.Rmk = ((XCAPIClass.XCAPISearch)this.BindingContext).Rmk;
            //XCQuery.Type = ((XCAPIClass.XCAPISearch)this.BindingContext).Type;
            //XCQuery.TypeIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).TypeIndex;
            //XCQuery.Nr = ((XCAPIClass.XCAPISearch)this.BindingContext).Nr;
            //XCQuery.Lic = ((XCAPIClass.XCAPISearch)this.BindingContext).Lic;
            //XCQuery.Q = ((XCAPIClass.XCAPISearch)this.BindingContext).Q;
            //XCQuery.QIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).QIndex;
            //XCQuery.Area = ((XCAPIClass.XCAPISearch)this.BindingContext).Area;
            //XCQuery.AreaIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).AreaIndex;

            XCAPIClass.QueryRequest = XCAPIClass.BuildTheQuery(XCQuery);
            System.Diagnostics.Debug.WriteLine("=============== " + XCAPIClass.QueryRequest);

            //Move the next window
            await Navigation.PushAsync(new ListViewPage(), true);
        }

}
}