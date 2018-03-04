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
                    if (CrossMediaManager.Current.Duration.TotalSeconds >= 0)
                    {
                        ProgressBarSlider.Maximum = CrossMediaManager.Current.Duration.TotalSeconds;
                    }
                    else
                    {
                        ProgressBarSlider.Maximum = 100;
                    }

                    ProgressBarSlider.Minimum = 0;
                    ProgressBarSlider.Value= e.Progress;//+++anda depressa de mais <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    Duration.Text = XCAPIClass.SecondsToString(e.Progress,true)+"/"+ XCAPIClass.SecondsToString(ProgressBarSlider.Maximum,true);
                });
            };
            //+++quando sai da form desligar o som

            //MediaManager.PlaybackController.SeekTo(streamingPosition);
            //MediaManager.PlaybackController.Play();

            //events
            CrossMediaManager.Current.MediaFinished += Current_MediaFinished;
            //+++CrossMediaManager.Current.MediaFailed
            //Throw Error and stop playing


            //Creat gesture in Label to link Url
            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) => {
                //Get Url from the ViewModel
                //this has to be done here, because in the main DetailPage() returns null, the constructor not yet done
                var XCAPIRecordingTapped = BindingContext as XCAPIClass.XCAPIRecordings;
                string url = XCAPIRecordingTapped.Url;

                Device.OpenUri(new Uri(url));
            };
            LabelUrl.GestureRecognizers.Add(tapGestureRecognizer);
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

        private async void StopAudio_OnClicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
            ButtonPlay.Source = "ic_play_arrow_white_48dp.png";
            FirstTimePlaying = true;
            Playing = false;
            ProgressBarSlider.Value = 0;
            Duration.Text = "";
        }
    

        private async void PlayAudio_OnClicked(object sender, EventArgs e)
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

        protected async override void OnDisappearing()
        {
            await CrossMediaManager.Current.Stop();
        }

    }
}