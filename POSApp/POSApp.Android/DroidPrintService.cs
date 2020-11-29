using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Print;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Util;
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

        public async void PrintText(string print)
        {            
            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                if (bluetoothAdapter == null)
                {
                    throw new Exception("No default adapter");
                    //return;
                }

                if (!bluetoothAdapter.IsEnabled)
                {
                    throw new Exception("Bluetooth not enabled");
                    //Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                    //StartActivityForResult(enableIntent, REQUEST_ENABLE_BT);
                    // Otherwise, setup the chat session
                }

                BluetoothDevice device = bluetoothAdapter.BondedDevices.FirstOrDefault();
                if (device == null)
                    throw new Exception("no device not found.");

                try
                {
                    using (BluetoothSocket _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                    {
                        await _socket.ConnectAsync();

                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(print);
                        await Task.Delay(3000);
                        // Write data to the device
                        await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        _socket.Close();
                    }
                }
                catch (Exception exp)
                {

                    throw exp;
                }

            }
        }
    }
}