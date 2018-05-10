using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace XCApp.Droid
{
    [Activity(Label = "XCApp", Icon = "@drawable/favicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            //=======================
            //Ensure the application resumes whatever task the user was performing the last time
            //they opened the app from the launcher. It would be preferable to configure this
            //behavior in  AndroidMananifest.xml activity settings, but those settings cause drastic
            //undesirable changes to the way the app opens: singleTask closes ALL other activities
            //in the task every time and alwaysRetainTaskState doesn't cover this case, incredibly.
            //
            //The problem happens when the user first installs and opens the app from
            //the play store or sideloaded apk (not via ADB). On this first run, if the user opens
            //activity B from activity A, presses 'home' and then navigates back to the app via the
            //launcher, they'd expect to see activity B. Instead they're shown activity A.
            //
            //The best solution is to close this activity if it isn't the task root.
            //
            //
            if (!IsTaskRoot)
            {
                Finish();
                return;
            }
            //=======================


            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //only needed in 1.1 this is 1.04 
            //Rg.Plugins.Popup.Popup.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());

        }
    }
}

