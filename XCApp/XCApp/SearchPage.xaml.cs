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
            TextName.Text = SearchSettings.Name;
            TextGen.Text = SearchSettings.Gen;
            TextRec.Text = SearchSettings.Rec;
            PickerCountry.SelectedIndex = SearchSettings.CntIndex;
            TextLoc.Text = SearchSettings.Loc;
            TextRmk.Text = SearchSettings.Rmk;
            TextTypeMix.Text = SearchSettings.TypeMix;
            PickerSongType.SelectedIndex = SearchSettings.TypeIndex;
            TextNr.Text = SearchSettings.Nr;
            TextAlso.Text = SearchSettings.Also;
            PickerLicense.SelectedIndex = SearchSettings.LicIndex;
            PickerQuality.SelectedIndex = SearchSettings.QIndex;
            PickerQualityOperator.SelectedIndex = SearchSettings.QOIndex;
            PickerArea.SelectedIndex = SearchSettings.AreaIndex;
        }

        public void SaveSearchSettings()
        {
            SearchSettings.Name = TextName.Text;
            SearchSettings.Gen = TextGen.Text;
            SearchSettings.Rec = TextRec.Text;
            SearchSettings.CntIndex = PickerCountry.SelectedIndex;
            SearchSettings.Loc = TextLoc.Text;
            SearchSettings.Rmk = TextRmk.Text;
            SearchSettings.TypeMix = TextTypeMix.Text;
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

        
        void TextTypeMixClear_OnTapped(object sender, EventArgs args)
        {
            TextTypeMix.Text = "";
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
                //{
                //    string result;
                //    if (AppSettings.Contains(nameof(Name)))
                //    { result = AppSettings.GetValueOrDefault(nameof(Name), string.Empty); } 
                //    else
                //    { result = null; }
                //    return result;
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
            public static string TypeMix //string with list of types choosen
            {
                get => AppSettings.GetValueOrDefault(nameof(TypeMix), null);
                set => AppSettings.AddOrUpdateValue(nameof(TypeMix), value);
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