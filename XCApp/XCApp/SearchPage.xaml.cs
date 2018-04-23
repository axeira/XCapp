using Plugin.Settings;
using Plugin.Settings.Abstractions;
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

        public void LoadSearchSettings()
        {
            int i; 

            TextName.Text = SearchSettings.Name;
            TextGen.Text = SearchSettings.Gen;
            TextRec.Text = SearchSettings.Rec;
            TextLoc.Text = SearchSettings.Loc;
            TextRmk.Text = SearchSettings.Rmk;
            TextNr.Text = SearchSettings.Nr;
            TextAlso.Text = SearchSettings.Also;

            //if i<=0 dont load setting to picker
            i = SearchSettings.CntIndex;
            if (i > 0) PickerCountry.SelectedIndex = i;
            i = SearchSettings.TypeIndex;
            if (i > 0) PickerSongType.SelectedIndex = i;
            i = SearchSettings.LicIndex;
            if (i > 0) PickerLicense.SelectedIndex = i;
            i = SearchSettings.QIndex;
            if (i > 0) PickerQuality.SelectedIndex = i;
            i = SearchSettings.QOIndex;
            if (i > 0) PickerQualityOperator.SelectedIndex = i;
            i = SearchSettings.AreaIndex;
            if (i > 0) PickerArea.SelectedIndex = i;
        }

        public void SaveSearchSettings()
        {
            SearchSettings.Name = TextName.Text;
            SearchSettings.Gen = TextGen.Text;
            SearchSettings.Rec = TextRec.Text;
            SearchSettings.CntIndex = PickerCountry.SelectedIndex;
            SearchSettings.Loc = TextLoc.Text;
            SearchSettings.Rmk = TextRmk.Text;
            SearchSettings.TypeIndex = PickerSongType.SelectedIndex;
            SearchSettings.Nr = TextNr.Text;
            SearchSettings.Also = TextAlso.Text;
            SearchSettings.LicIndex = PickerLicense.SelectedIndex;
            SearchSettings.QIndex = PickerQuality.SelectedIndex;
            SearchSettings.QOIndex = PickerQualityOperator.SelectedIndex;
            SearchSettings.AreaIndex = PickerArea.SelectedIndex;
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

        void TextNameClear_OnTapped(object sender, EventArgs args)
        {
            TextName.Text = "";
        }

        void PickerTypeClear_OnTapped(object sender, EventArgs args)
        {
            PickerSongType.Items.Clear();
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



        public static class SearchSettings
        {

            private static ISettings AppSettings =>
                CrossSettings.Current;

            public static string Name
            {
                get => AppSettings.GetValueOrDefault(nameof(Name), null);
                set => AppSettings.AddOrUpdateValue(nameof(Name), value);
            }
            public static string Gen
            {
                get => AppSettings.GetValueOrDefault(nameof(Gen), null);
                set => AppSettings.AddOrUpdateValue(nameof(Gen), value);
            }
            public static string Rec
            {
                get => AppSettings.GetValueOrDefault(nameof(Rec), null);
                set => AppSettings.AddOrUpdateValue(nameof(Rec), value);
            }
            public static int CntIndex
            {
                //get
                //{
                //    CntIndex = AppSettings.GetValueOrDefault(nameof(CntIndex), -1);
                //    if (CntIndex <= 0  || !AppSettings.Contains(nameof(CntIndex)))
                //        return -1; 
                //    else
                //        return CntIndex;
                //}


                get => AppSettings.GetValueOrDefault(nameof(CntIndex), -1);
                set => AppSettings.AddOrUpdateValue(nameof(CntIndex), value);
            }
            public static string Loc
            {
                get => AppSettings.GetValueOrDefault(nameof(Loc), null);
                set => AppSettings.AddOrUpdateValue(nameof(Loc), value);
            }
            public static string Rmk
            {
                get => AppSettings.GetValueOrDefault(nameof(Rmk), null);
                set => AppSettings.AddOrUpdateValue(nameof(Rmk), value);
            }
            public static int TypeIndex
            {
                get => AppSettings.GetValueOrDefault(nameof(TypeIndex), 0);
                set => AppSettings.AddOrUpdateValue(nameof(TypeIndex), value);
            }
            public static string Nr
            {
                get => AppSettings.GetValueOrDefault(nameof(Nr), null);
                set => AppSettings.AddOrUpdateValue(nameof(Nr), value);
            }
            public static string Also
            {
                get => AppSettings.GetValueOrDefault(nameof(Also), null);
                set => AppSettings.AddOrUpdateValue(nameof(Also), value);
            }
            public static int LicIndex
            {
                get => AppSettings.GetValueOrDefault(nameof(LicIndex), 0);
                set => AppSettings.AddOrUpdateValue(nameof(LicIndex), value);
            }
            public static int QIndex
            {
                get => AppSettings.GetValueOrDefault(nameof(QIndex), 0);
                set => AppSettings.AddOrUpdateValue(nameof(QIndex), value);
            }
            public static int QOIndex
            {
                get => AppSettings.GetValueOrDefault(nameof(QOIndex), 0);
                set => AppSettings.AddOrUpdateValue(nameof(QOIndex), value);
            }
            public static int AreaIndex
            {
                get => AppSettings.GetValueOrDefault(nameof(AreaIndex), 0);
                set => AppSettings.AddOrUpdateValue(nameof(AreaIndex), value);
            }
        }


    }
}