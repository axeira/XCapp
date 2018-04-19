#define TimeDiagnostics
//+++#undef TimeDiagnostics

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//#if (TimeDiagnostics)
//    using System.Diagnostics;
//    using System.Threading;
//#endif

namespace XCApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        private XCAPIClass.XCAPIRecordingsMain XCAPIRecordings = new XCAPIClass.XCAPIRecordingsMain();
        private int _currentPage = 0;
        private int _currentPagePrevious = 0;

        public ListViewPage()
        {
            InitializeComponent();
            RunQuery(_currentPage);
            _currentPage = 1;
        }

        private async void RunQuery( int currentPage)
        {
            string serverRequestResult;
            string queryRequest;
            bool listIsEmpty = true;

            #region
            //+++Doesnt work in IOS
//#if (TimeDiagnostics)
//            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(8); // Use only the 8 core 
//            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
//            Thread.CurrentThread.Priority = ThreadPriority.Highest;
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            Debug.WriteLine("========== Start: " + stopwatch.ElapsedTicks + " mS: " + stopwatch.ElapsedMilliseconds);
//#endif
            #endregion

            //+++The List View might not show the last lines, as long as the answer received it is too long?!?!?!
            if (!string.IsNullOrEmpty(XCAPIClass.QueryRequest))
            {
                // Open the PopupPage circle
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                //Manage if tag page is necessary
                if (currentPage == 0)
                {
                    queryRequest = XCAPIClass.QueryRequest;
                    currentPage = 1;
                }
                else
                {
                    queryRequest = XCAPIClass.QueryRequest + "&page=" + currentPage.ToString();
                }

                //Communicate with server<<<<<<<<<<<<<<<<<<<<<<<<
                Task<string> s = XCAPIClass.ServerRequest(queryRequest);
                serverRequestResult = await s;

                #region
//#if (TimeDiagnostics)
//                Debug.WriteLine("========== Received: " + stopwatch.ElapsedTicks + " mS: " + stopwatch.ElapsedMilliseconds);
//#endif
                #endregion
                if (serverRequestResult == HttpStatusCode.OK.ToString())
                {
                    //Deserializing <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //+++catch error
                    //+++Create Task
                    XCAPIRecordings = XCAPIClass.Deserialize_json_data<XCAPIClass.XCAPIRecordingsMain>(XCAPIClass.JsonData);
                    #region
//#if (TimeDiagnostics)
//                    Debug.WriteLine("========== Deserialized: " + stopwatch.ElapsedTicks + " mS: " + stopwatch.ElapsedMilliseconds);
//#endif
                    #endregion
                    //Fill Listview
                    MyListView.ItemsSource = XCAPIClass.XCAPIRecordingsMain.Recordings;
                    #region
//#if (TimeDiagnostics)

//                    Debug.WriteLine("========== ListView filled: " + stopwatch.ElapsedTicks + " mS: " + stopwatch.ElapsedMilliseconds);
//#endif
                    #endregion
                    //+++await DisplayAlert("Alert", XCAPIClass.XCAPIRecordingsMain.NumRecordings + " "  , "OK");
                    if (string.IsNullOrEmpty(XCAPIClass.XCAPIRecordingsMain.NumRecordings))
                    {
                        LabelNoRecordings.Text = "Error found!";
                        FootnoteLabel.Text = "Response was empty!";
                        listIsEmpty = true;
                    }
                    else
                    {
                        if (Convert.ToInt32(XCAPIClass.XCAPIRecordingsMain.NumRecordings) > 0)
                        {
                            //All was good ans some response arrived then move to the top of the List
                            MyListView.ScrollTo(XCAPIClass.XCAPIRecordingsMain.Recordings[0], ScrollToPosition.Start, false);
                            listIsEmpty = false;
                        }
                        else
                        {
                            //See if there is no recordings
                            if (Convert.ToInt32(XCAPIClass.XCAPIRecordingsMain.NumRecordings) == 0) LabelNoRecordings.Text = "No recordings found!";
                            listIsEmpty = true;
                        }
                        //Fill Footnote
                        FootnoteLabel.Text = "Page " + XCAPIClass.XCAPIRecordingsMain.Page + " of " +
                        XCAPIClass.XCAPIRecordingsMain.NumPages + ", # Recordings: " +
                        XCAPIClass.XCAPIRecordingsMain.NumRecordings + ", # Species: " +
                        XCAPIClass.XCAPIRecordingsMain.NumSpecies;
                    }
                }
                else
                {
                    //+++Improve error messaging
                    if (string.IsNullOrEmpty(serverRequestResult))
                    {
                        LabelNoRecordings.Text = "Network unavailable!";
                        listIsEmpty = true;
                        FootnoteLabel.Text = "";
                    }
                    else
                    {
                        LabelNoRecordings.Text = "Error found!";
                        listIsEmpty = true;
                        FootnoteLabel.Text = serverRequestResult.ToString();
                    }

                }
                // Close the PopupPage circle
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else
            {
                LabelNoRecordings.Text = "Search query is empty!";
                listIsEmpty = true;
                FootnoteLabel.Text = "";
            }

            //Hide or show the List is empty Message
            LabelNoRecordings.IsVisible = listIsEmpty;
            MyListView.IsVisible = !listIsEmpty;

            #region 
//#if (TimeDiagnostics)

//            Debug.WriteLine("========== End: " + stopwatch.ElapsedTicks + " mS: " + stopwatch.ElapsedMilliseconds);
//            Debug.WriteLine("==================================\r\n");
//            stopwatch.Stop();
//#endif
            #endregion
        }
        
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var XCAPIRecordingTapped = e.Item as XCAPIClass.XCAPIRecordings;
            await Navigation.PushAsync(new DetailPage(XCAPIRecordingTapped));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void OnTapGestureRecognizerTappedHome(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync(true);
        }

        void OnTapGestureRecognizerTappedPreviousPage(object sender, EventArgs args)
        {
            _currentPagePrevious = _currentPage;
            if (_currentPage > 1)
                _currentPage -= 1;
            if (_currentPage != _currentPagePrevious)
                RunQuery(_currentPage);
        }

        void OnTapGestureRecognizerTappedNextPage(object sender, EventArgs args)
        {
            _currentPagePrevious = _currentPage;
            if (_currentPage < Convert.ToInt32(XCAPIClass.XCAPIRecordingsMain.NumPages))
                _currentPage += 1;
            if (_currentPage != _currentPagePrevious)
                RunQuery(_currentPage);
        }

        void Handle_OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
            RunQuery(_currentPage);
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }

    }



}
