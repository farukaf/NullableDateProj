using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using App1.Droid.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace App1.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {

        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //Remove a linha embaixo dos entrys
            //Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);

            if (e.NewElement is Entry entry && entry.Keyboard == Keyboard.Numeric)
            {
                var edt = Control as EditText;
                edt.InputType = InputTypes.ClassNumber;
                edt.InputType |= InputTypes.NumberFlagSigned;
                edt.InputType |= InputTypes.NumberFlagDecimal;
                edt.KeyListener = DigitsKeyListener.GetInstance(string.Format("1234567890{0}", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
        }
    }
}