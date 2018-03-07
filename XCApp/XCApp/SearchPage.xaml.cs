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
        //private object textName;

        //+++public string QueryRequest;

        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new XCAPIClass.XCAPISearch();

            PickerCountry.ItemsSource = ConstantsClass.Countries;
            PickerArea.ItemsSource = ConstantsClass.Areas;
            PickerSongType.ItemsSource = ConstantsClass.SongTypes;
            PickerQuality.ItemsSource = ConstantsClass.Qualities;
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
        

        void TextNameClear_OnTapped(object sender, EventArgs args)
        {
            TextName.Text = "";
        }

        void TextGenClear_OnTapped(object sender, EventArgs args)
        {
            TextGen.Text = ""; 
        }

        //void TextSspClear_OnTapped(object sender, EventArgs args)
        //{
        //    TextSsp.Text = "";
        //}

        //void TextLoc_OnTapped(object sender, EventArgs args)
        //{
        //    TextLoc.Text = "";
        //}

        //void TextNr_OnTapped(object sender, EventArgs args)
        //{
        //    TextNr.Text = "";
        //}

        void PickerSongTypeClear_OnTapped(object sender, EventArgs args)
        {

            //+++LabelSelectedSongTypes.Text= "";

            //+++var picker = (Picker)PickerType;
            //int selectedIndex = picker.SelectedIndex;

            //try
            //{
            //    // code causing TargetInvocationException
            //    picker.SelectedIndex = -1; 
            //}
            //catch (Exception e)
            //{
            //    if (e.InnerException != null)
            //    {
            //        string err = e.InnerException.Message;
            //    }
            //}
            
        }


    }
}