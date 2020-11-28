using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Print;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using POSApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(DroidPrintService))]
namespace POSApp.Droid
{
    public class DroidPrintService : IPrintService
    {
        public DroidPrintService()
        {
        }
 
        public void Print(Xamarin.Forms.WebView viewToPrint)
        {
            var droidViewToPrint = Platform.CreateRenderer(viewToPrint).ViewGroup.GetChildAt(0) as Android.Webkit.WebView;
 
            if (droidViewToPrint != null)
            {
                // Only valid for API 19+
                var version = Android.OS.Build.VERSION.SdkInt;
 
                if (version >= Android.OS.BuildVersionCodes.Kitkat)
                {
                    var printMgr = (PrintManager)Forms.Context.GetSystemService(Context.PrintService);
                    printMgr.Print("Forms-EZ-Print", droidViewToPrint.CreatePrintDocumentAdapter(), null);
                }
            }
        }
    }
}