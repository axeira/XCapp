using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace XCApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        private bool Playing=false;
        private bool FirstTimePlaying = true;



        public DetailPage ()
		{
            InitializeComponent ();
            

            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBarSlider.Maximum = 100;
                    ProgressBarSlider.Minimum = 0;
                    ProgressBarSlider.Value= e.Progress;
                    //e.Progress=percentage 0-100
                    //e.Position=seconds
                    //e.Duration=seconds
                    Duration.Text = e.Progress.ToString() + " " + e.Position + " " + e.Duration;
                    Duration.Text = XCAPIClass.SecondsToString(e.Position.TotalSeconds, true)+"/"+ XCAPIClass.SecondsToString(e.Duration.TotalSeconds,true);
                });
            };

            //+++MediaManager.PlaybackController.SeekTo(TimeSpan.FromMilliseconds(5000));
            //+++MediaManager.PlaybackController.Play();

            //events
            CrossMediaManager.Current.MediaFinished += Current_MediaFinished;
            //+++CrossMediaManager.Current.MediaFailed
            //Throw Error and stop playing


            //Creat gesture in LabelUrl to link Url
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                //Get Url from the ViewModel
                //this has to be done here, because in the main DetailPage() returns null, the constructor not yet done
                var XCAPIRecordingTapped = BindingContext as XCAPIClass.XCAPIRecordings;
                string url = XCAPIRecordingTapped.Url;
                Device.OpenUri(new Uri(url));
            };
            LabelUrl.GestureRecognizers.Add(tapGestureRecognizer);

            //Choose image to CC field
            CCImage.SetBinding(Image.SourceProperty, new Binding("Lic", converter: new CCImageConverter()));
            //Choose license text to show
            LabelLicense.SetBinding(Label.TextProperty, new Binding("Lic", converter: new LabelLicenseConverter()));
        }

        private void CCImage_OnTapped(object sender, EventArgs e) //***
        {
            var XCAPIRecordingTapped = BindingContext as XCAPIClass.XCAPIRecordings;
            string lic = "https:"+XCAPIRecordingTapped.Lic;
            Device.OpenUri(new Uri(lic));
        }

        async void OnTappedMap(object sender, EventArgs args)
        {
            //Show in Map window
            var XCAPIRecordingTapped = BindingContext as XCAPIClass.XCAPIRecordings;
            double x, y;

            if (!string.IsNullOrEmpty(XCAPIRecordingTapped.Lat))
                x = Convert.ToDouble(XCAPIRecordingTapped.Lat);
            else
                x = 0;

            if (!string.IsNullOrEmpty(XCAPIRecordingTapped.Lng))
                y = Convert.ToDouble(XCAPIRecordingTapped.Lng);
            else
                y = 0;

            await Navigation.PushAsync(new MapPage(x,y,25), true);
        }


        private void Current_MediaFinished(object sender, Plugin.MediaManager.Abstractions.EventArguments.MediaFinishedEventArgs e)
        {
            //CrossMediaManager.Current.MediaFinished
            FirstTimePlaying = true;
            ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
            Playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }

        private async void PlayAudio_OnTapped(object sender, EventArgs e)
        {
            string url;
            if (FirstTimePlaying)
            {
                //url = ((XCAPIClass.XCAPIRecordings)this.BindingContext).File;
                url = "https://www.xeno-canto.org/sounds/uploaded/OJMFAOUBDU/XC134880-JMJ-20130525-091612-1068-USA-MN-GreyCloudDunes-RBGR.mp3";
                //+++url = "https://www.xeno-canto.org/sounds/uploaded/PJVICFDZGZ/XC298756-160108_0508%20Beaudette%20Park_Trump%20Swan%203%20XC.mp3";
                var mediaFile = new MediaFile
                {
                    Type = MediaFileType.Audio,
                    Availability = ResourceAvailability.Remote,
                    Url = url
                };

                await CrossMediaManager.Current.Play(mediaFile);
                FirstTimePlaying = false;
                ButtonPlay.Source = "ic_pause_white_48dp.png";
                Playing = true;
            }
            else
            {
                if (Playing)
                {
                    await CrossMediaManager.Current.Pause();
                    ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
                    Playing = false;
                }
                else
                {
                    await CrossMediaManager.Current.Play();
                    ButtonPlay.Source = "ic_pause_white_48dp.png";
                    Playing = true;
                }
            }

        }

        private async void StopAudio_OnTapped(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
            ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
            FirstTimePlaying = true;
            Playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }

        protected async override void OnDisappearing()
        {
            //+++ consulidate with StopAudio_OnTapped in only one
            await CrossMediaManager.Current.Stop();
            ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
            FirstTimePlaying = true;
            Playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }


        private class CCImageConverter : Xamarin.Forms.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var stringValue = (String)value;

                if (stringValue.IndexOf(ConstantsClass.Short_byncsa) >= 0)
                    return "byncsa.png";
                else if (stringValue.IndexOf(ConstantsClass.Short_byncnd) >= 0)
                    return "byncnd.png";
                else if (stringValue.IndexOf(ConstantsClass.Short_bysa) >= 0)
                    return "bysa.png";
                //else if (stringValue.IndexOf("/by/") >= 0)
                //    return "by.png";
                //else if (stringValue.IndexOf("/by-nc.eu/") >= 0)
                //    return "bynceu.png";
                //else if (stringValue.IndexOf("/by-nc/") >= 0)
                //    return "bync.png";
                //else if (stringValue.IndexOf("/by-nc-sa.eu/") >= 0)
                //    return "byncsaeu.png";
                //else if (stringValue.IndexOf("/by-nd/") >= 0)
                //    return "bynd.png";
                else
                    return "";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException("Not implemented.");
            }
        }

        private class LabelLicenseConverter : Xamarin.Forms.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var stringValue = (String)value;

                if (stringValue.IndexOf(ConstantsClass.Short_byncsa) >= 0)
                    return ConstantsClass.Long_byncsa;
                else if (stringValue.IndexOf(ConstantsClass.Short_byncnd) >= 0)
                    return ConstantsClass.Long_byncnd;
                else if (stringValue.IndexOf(ConstantsClass.Short_bysa) >= 0)
                    return ConstantsClass.Long_bysa;
                else
                    return "";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException("Not implemented.");
            }
        }

        
    }
}