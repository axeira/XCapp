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
        //sliderchange private bool sliderProgramChanged = false;
        private double recDuration; //in seconds
        private double recPosition; //in seconds
        private bool playing = false;
        private bool firstTimePlaying = true;
        private XCAPIClass.XCAPIGetUris uris = new XCAPIClass.XCAPIGetUris();
        private XCAPIClass.XCAPIRecordings recordingTapped;

        public DetailPage(XCAPIClass.XCAPIRecordings RTapped)
        {
            InitializeComponent();

            recordingTapped = RTapped;
            BindingContext = recordingTapped;

            //Decode the Urls for this tapped recording
            uris.GetUris(Constants.UrlScheme + recordingTapped.File, recordingTapped.Id);

            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBarSlider.Maximum = 100;
                    ProgressBarSlider.Minimum = 0;
                    //sliderchange sliderProgramChanged = true;
                    ProgressBarSlider.Value = e.Progress;
                    //sliderchange sliderProgramChanged = false;
                    //e.Progress=percentage 0-100
                    //e.Position=seconds
                    recPosition = e.Position.TotalSeconds;
                    //e.Duration=seconds
                    recDuration = e.Duration.TotalSeconds;
                    Position.Text = XCAPIClass.SecondsToString(recPosition, false);
                    Duration.Text = XCAPIClass.SecondsToString(recDuration, false);

                });
            };

            //sliderchange Event when slider changes
            //sliderchange ProgressBarSlider.ValueChanged += ProgressBarSlider_ValueChanged;

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
            Spectogram.Source = uris.FFTSLargeImageUri;

        }

        //sliderchange when move slider get error E/MediaPlayer(14143): Attempt to perform seekTo in wrong state: mPlayer=0x90d8b2c0, mCurrentState=2
        //private async void ProgressBarSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    //if user moved the slider
        //    if (!sliderProgramChanged)
        //    {
        //        //jump to new position
        //        recPosition = recDuration * e.NewValue / 100;
        //        await CrossMediaManager.Current.Stop();
        //        await CrossMediaManager.Current.PlaybackController.SeekTo(recPosition);
        //        await CrossMediaManager.Current.Play();
        //        Position.Text = XCAPIClass.SecondsToString(recPosition, false);
        //        Duration.Text = XCAPIClass.SecondsToString(recDuration, false);
        //    }
        //}

        private void CCImage_OnTapped(object sender, EventArgs e)
        {
            string lic = Constants.UrlScheme + recordingTapped.Lic;
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
            firstTimePlaying = true;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ButtonPlay.Source = "ic_play_arrow_white_48pt.png";
                    break;
                case Device.Android:
                    ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
                    break;
                case Device.UWP:
                    ButtonPlay.Source = "Images/ic_play_arrow_white_48dp.png";
                    break;
                default:
                    break;
            }
            playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }

        private async void PlayAudio_OnTapped(object sender, EventArgs e)
        {
            //+++string recordingUrl;
            //+++string recordingId;
            if (firstTimePlaying)
            {
                try
                {
                    if (uris.Error=="OK")
                    {
                        var mediaFile = new MediaFile
                        {
                            Type = MediaFileType.Audio,
                            Availability = ResourceAvailability.Remote,
                            Url = uris.AudioUri
                        };
                        try
                        {
                            await CrossMediaManager.Current.Play(mediaFile);
                            firstTimePlaying = false;
                            switch (Device.RuntimePlatform)
                            {
                                case Device.iOS:
                                    ButtonPlay.Source = "ic_pause_white_48pt.png";
                                    break;
                                case Device.Android:
                                    ButtonPlay.Source = "ic_pause_white_48dp.png";
                                    break;
                                case Device.WPF:
                                    ButtonPlay.Source = "Images/ic_pause_white_48dp.png";
                                    break;
                                default:
                                    break;
                            }
                            playing = true;
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
                if (playing)
                {
                    await CrossMediaManager.Current.Pause();
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            ButtonPlay.Source = "ic_play_arrow_white_48pt.png";
                            break;
                        case Device.Android:
                            ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
                            break;
                        case Device.WPF:
                            ButtonPlay.Source = "Images/ic_play_arrow_white_48dp.png";
                            break;
                        default:
                            break;
                    }
                    playing = false;
                }
                else
                {
                    await CrossMediaManager.Current.Play();
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            ButtonPlay.Source = "ic_pause_white_48pt.png";
                            break;
                        case Device.Android:
                            ButtonPlay.Source = "ic_pause_white_48dp.png";
                            break;
                        case Device.WPF:
                            ButtonPlay.Source = "Images/ic_pause_white_48dp.png";
                            break;
                        default:
                            break;
                    }
                    playing = true;
                }
            }

        }

        private async void StopAudio_OnTapped(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ButtonPlay.Source = "ic_play_arrow_white_48pt.png";
                    break;
                case Device.Android:
                    ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
                    break;
                case Device.WPF:
                    ButtonPlay.Source = "Images/ic_play_arrow_white_48dp.png";
                    break;
                default:
                    break;
            }
            firstTimePlaying = true;
            playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }

        protected async override void OnDisappearing()
        {
            //+++ consolidate with StopAudio_OnTapped in only one
            await CrossMediaManager.Current.Stop();
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ButtonPlay.Source = "ic_play_arrow_white_48pt.png";
                    break;
                case Device.Android:
                    ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
                    break;
                case Device.WPF:
                    ButtonPlay.Source = "Images/ic_play_arrow_white_48dp.png";
                    break;
                default:
                    break;
            }
            firstTimePlaying = true;
            playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }


        private class CCImageConverter : Xamarin.Forms.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var stringValue = (String)value;

                if (stringValue.IndexOf(Constants.Short_byncsa) >= 0)
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            return "byncsa.png";
                        case Device.Android:
                            return "byncsa.png";
                        case Device.WPF:
                            return "Images/byncsa.png";
                        default:
                            return "";
                    }
                else if (stringValue.IndexOf(Constants.Short_byncnd) >= 0)
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            return "byncnd.png";
                        case Device.Android:
                            return "byncnd.png";
                        case Device.WPF:
                            return "Images/byncnd.png";
                        default:
                            return "";
                    }
                else if (stringValue.IndexOf(Constants.Short_bysa) >= 0)
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            return "bysa.png";
                        case Device.Android:
                            return "bysa.png";
                        case Device.WPF:
                            return "Images/bysa.png";
                        default:
                            return "";
                    }
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

                if (stringValue.IndexOf(Constants.Short_byncsa) >= 0)
                    return Constants.Long_byncsa;
                else if (stringValue.IndexOf(Constants.Short_byncnd) >= 0)
                    return Constants.Long_byncnd;
                else if (stringValue.IndexOf(Constants.Short_bysa) >= 0)
                    return Constants.Long_bysa;
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