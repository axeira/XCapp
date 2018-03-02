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
        public string QueryRequest;

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
            XCQuery.Name = ((XCAPIClass.XCAPISearch)this.BindingContext).Name;
            XCQuery.Gen = ((XCAPIClass.XCAPISearch)this.BindingContext).Gen;
            XCQuery.Rec = ((XCAPIClass.XCAPISearch)this.BindingContext).Rec;
            XCQuery.Cnt = ((XCAPIClass.XCAPISearch)this.BindingContext).Cnt;
            XCQuery.Cnt = ((XCAPIClass.XCAPISearch)this.BindingContext).Cnt;
            XCQuery.CntIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).CntIndex;
            XCQuery.Loc = ((XCAPIClass.XCAPISearch)this.BindingContext).Loc;
            XCQuery.Rmk = ((XCAPIClass.XCAPISearch)this.BindingContext).Rmk;
            XCQuery.Type = ((XCAPIClass.XCAPISearch)this.BindingContext).Type;
            XCQuery.TypeIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).TypeIndex;
            XCQuery.Nr = ((XCAPIClass.XCAPISearch)this.BindingContext).Nr;
            XCQuery.Lic = ((XCAPIClass.XCAPISearch)this.BindingContext).Lic;
            XCQuery.Q = ((XCAPIClass.XCAPISearch)this.BindingContext).Q;
            XCQuery.QIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).QIndex;
            XCQuery.Area = ((XCAPIClass.XCAPISearch)this.BindingContext).Area;
            XCQuery.AreaIndex = ((XCAPIClass.XCAPISearch)this.BindingContext).AreaIndex;

            QueryRequest = BuildTheQuery();
            System.Diagnostics.Debug.WriteLine(QueryRequest);

            //Move the next window
            await Navigation.PushAsync(new ListViewPage(QueryRequest), true);
        }

    private string BuildTheQuery()
    {
        //Build the query
        QueryRequest = "";
        if (!String.IsNullOrEmpty(XCQuery.Name))
        {
            QueryRequest = XCQuery.Name + " ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Gen))
        {
            QueryRequest = QueryRequest + "gen:\"" + XCQuery.Gen + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Rec))
        {
            QueryRequest = QueryRequest + "rec:\"" + XCQuery.Rec + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Cnt))
        {
            if (XCQuery.Cnt.ToUpper() != ConstantsClass.NoneStr)
                QueryRequest = QueryRequest + "cnt:\"" + XCQuery.Cnt + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Loc))
        {
            QueryRequest = QueryRequest + "loc:\"" + XCQuery.Loc + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Rmk))
        {
            QueryRequest = QueryRequest + "rmk:\"" + XCQuery.Rmk + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Type))
        {
            if (XCQuery.Type.ToUpper() != ConstantsClass.NoneStr)
                QueryRequest = QueryRequest + "type:\"" + XCQuery.Type + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Rec))
        {
            QueryRequest = QueryRequest + "rec:\"" + XCQuery.Rec + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Nr))
        {
            QueryRequest = QueryRequest + "nr:" + XCQuery.Nr + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Lic))
        {
            QueryRequest = QueryRequest + "lic:\"" + XCQuery.Lic + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Q))
        {
            if (XCQuery.Q.ToUpper() != ConstantsClass.NoneStr)
                QueryRequest = QueryRequest + "q:\"" + XCQuery.Rec + "\" ";
        }
        if (!String.IsNullOrEmpty(XCQuery.Area))
        {
            if (XCQuery.Area.ToUpper() != ConstantsClass.NoneStr)
                QueryRequest = QueryRequest + "area:\"" + XCQuery.Area + "\" ";
        }


        //Form query
        QueryRequest = (ConstantsClass.XCAPIUrl + QueryRequest).TrimEnd().ToLower();
        //Clean double spaces
        while (QueryRequest.Contains("  "))
                QueryRequest = QueryRequest.Replace("  ", " ");

        //+++
            QueryRequest = ConstantsClass.XCAPIUrl + "nr:404086"; // with ssp
            //QueryRequest = ConstantsClass.XCAPIUrl + "nr:134880";
            //QueryRequest = ConstantsClass.XCAPIUrl + "passer iagoensis";
            //QueryRequest = ConstantsClass.XCAPIUrl + "passer domesticus";
            //https://www.xeno-canto.org/134880

            return QueryRequest;
    }


}
}