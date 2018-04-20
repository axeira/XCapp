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

        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new XCAPIClass.XCAPISearch();

            PickerCountry.ItemsSource = ConstantsClass.Countries;
            PickerArea.ItemsSource = ConstantsClass.Areas;
            PickerSongType.ItemsSource = ConstantsClass.SongTypes;
            PickerQualityOperator.ItemsSource = ConstantsClass.QualityOperators;
            PickerQuality.ItemsSource = ConstantsClass.Qualities;
            PickerLicense.ItemsSource = ConstantsClass.Licenses;

            LoadSearchSettings();
        }

        private void LoadSearchSettings()
        {
            if (Application.Current.Properties.ContainsKey("name"))
            {
                TextName.Text = Application.Current.Properties["name"] as string;
            }


        }

        private async void SaveSearchSettings()
        {
            Application.Current.Properties["name"] = TextName.Text;
            await Application.Current.SavePropertiesAsync();
        }




        async void OnTapGestureRecognizerTappedPlaySearch(object sender, EventArgs args)
        {
            
            //Store Query parameters
            XCQuery = ((XCAPIClass.XCAPISearch)this.BindingContext);

            
            SaveSearchSettings();
            XCAPIClass.QueryRequest = XCAPIClass.BuildTheQuery(XCQuery);
            System.Diagnostics.Debug.WriteLine("=============== " + XCAPIClass.QueryRequest);

            //Move the next window
            await Navigation.PushAsync(new ListViewPage(), true);
        }


        public void QGLToggledEvent(object sender, ToggledEventArgs e)
        {

        }

        void TextNameClear_OnTapped(object sender, EventArgs args)
        {
            TextName.Text = "";
        }

        void TextGenClear_OnTapped(object sender, EventArgs args)
        {
            TextGen.Text = ""; 
        }

        void TextAlsoClear_OnTapped(object sender, EventArgs args)
        {
            TextAlso.Text = "";
        }

        //+++Missing Ssp there is no tag in documetation
        //void TextSspClear_OnTapped(object sender, EventArgs args)
        //{
        //    TextSsp.Text = "";
        //}

        void TextLocClear_OnTapped(object sender, EventArgs args)
        {
            TextLoc.Text = "";
        }

        void TextRecClear_OnTapped(object sender, EventArgs args)
        {
            TextRec.Text = "";
        }

        void TextNrClear_OnTapped(object sender, EventArgs args)
        {
            TextNr.Text = "";
        }

        void TextRmkClear_OnTapped(object sender, EventArgs args)
        {
            TextRmk.Text = "";
        }


    }
}