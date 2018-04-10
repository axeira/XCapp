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
using System.Net;

namespace XCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        private bool Playing = false;
        private bool FirstTimePlaying = true;
        private XCAPIClass.XCAPIGetUris Uris = new XCAPIClass.XCAPIGetUris();
        private XCAPIClass.XCAPIRecordings recordingTapped;

        public DetailPage(XCAPIClass.XCAPIRecordings RTapped)
        {
            InitializeComponent();

            recordingTapped = RTapped;
            BindingContext = recordingTapped;

            Uris.GetUris(ConstantsClass.UrlScheme + recordingTapped.File, recordingTapped.Id);

            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBarSlider.Maximum = 100;
                    ProgressBarSlider.Minimum = 0;
                    ProgressBarSlider.Value = e.Progress;
                    //e.Progress=percentage 0-100
                    //e.Position=seconds
                    //e.Duration=seconds
                    Duration.Text = e.Progress.ToString() + " " + e.Position + " " + e.Duration;
                    Duration.Text = XCAPIClass.SecondsToString(e.Position.TotalSeconds, true) + "/" + XCAPIClass.SecondsToString(e.Duration.TotalSeconds, true);

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
                string url = recordingTapped.Url;
                Device.OpenUri(new Uri(url));
            };
            LabelUrl.GestureRecognizers.Add(tapGestureRecognizer);

            //Choose image to CC field
            CCImage.SetBinding(Image.SourceProperty, new Binding("Lic", converter: new CCImageConverter()));
            //Choose license text to show
            LabelLicense.SetBinding(Label.TextProperty, new Binding("Lic", converter: new LabelLicenseConverter()));
            //Show Spectrogram
            Spectogram.Source = Uris.FFTSLargeImageUri;

        }



        private void CCImage_OnTapped(object sender, EventArgs e)
        {
            string lic = ConstantsClass.UrlScheme + recordingTapped.Lic;
            Device.OpenUri(new Uri(lic));
        }

        async void OnTappedMap(object sender, EventArgs args)
        {
            //Show in Map window
            double x, y;

            if (!string.IsNullOrEmpty(recordingTapped.Lat))
                x = Convert.ToDouble(recordingTapped.Lat);
            else
                x = 0;

            if (!string.IsNullOrEmpty(recordingTapped.Lng))
                y = Convert.ToDouble(recordingTapped.Lng);
            else
                y = 0;

            await Navigation.PushAsync(new MapPage(x,y,25, recordingTapped.FullSName + " by " +recordingTapped.Rec, recordingTapped.Loc+", "+recordingTapped.Cnt ), true);
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
            //+++string recordingUrl;
            //+++string recordingId;
            if (FirstTimePlaying)
            {
                try
                {
                    if (Uris.Error=="OK")
                    {
                        var mediaFile = new MediaFile
                        {
                            Type = MediaFileType.Audio,
                            Availability = ResourceAvailability.Remote,
                            Url = Uris.AudioUri
                        };
                        try
                        {
                            await CrossMediaManager.Current.Play(mediaFile);
                            FirstTimePlaying = false;
                            ButtonPlay.Source = "ic_pause_white_48dp.png";
                            Playing = true;
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                            Duration.Text = "Error playing file...!";
                            //+++ Show error to the user
                        }
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    Duration.Text = "Error playing file...!";
                    //+++ Show error to the user
                }
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
            //+++ consolidate with StopAudio_OnTapped in only one
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