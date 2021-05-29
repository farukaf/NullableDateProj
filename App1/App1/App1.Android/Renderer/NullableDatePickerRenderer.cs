using Android.Views;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using App1.Droid.Renderer;
using Android.Content;
using App1.Controls;
using System;
using Android.App;
using System.Globalization;

[assembly: ExportRenderer(typeof(NullableDatePicker), typeof(NullableDatePickerRenderer))]
namespace App1.Droid.Renderer
{
    public class NullableDatePickerRenderer : DatePickerRenderer
    {
        public NullableDatePickerRenderer(Context context) : base(context)
        {
        }


        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            var dialog = base.CreateDatePickerDialog(year, month, day);
            //capture OK click
            dialog.DateSet += Dialog_DateSet;
            return dialog;
        }

        private void Dialog_DateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var view = (NullableDatePicker)Element;
            view.NullableDate = view.Date;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            //Remove line under input
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);

            var view = Element as NullableDatePicker;

            if (view != null)
            {
                SetFont(view);
                SetTextAlignment(view);
                // SetBorder(view);
                SetNullableText(view);
                SetPlaceholder(view);
                SetPlaceholderTextColor(view);
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (NullableDatePicker)Element;

            if (e.PropertyName == NullableDatePicker.FontProperty.PropertyName)
                SetFont(view);
            else if (e.PropertyName == NullableDatePicker.XAlignProperty.PropertyName)
                SetTextAlignment(view);
            // else if (e.PropertyName == NullableDatePicker.HasBorderProperty.PropertyName)
            //  SetBorder(view);
            else if (e.PropertyName == NullableDatePicker.NullableDateProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == NullableDatePicker.PlaceholderProperty.PropertyName)
                SetPlaceholder(view);
            else if (e.PropertyName == NullableDatePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

        }

        /// <summary>
        /// Sets the text alignment.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetTextAlignment(NullableDatePicker view)
        {
            switch (view.XAlign)
            {
                case Xamarin.Forms.TextAlignment.Center:
                    Control.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    Control.Gravity = GravityFlags.End;
                    break;
                case Xamarin.Forms.TextAlignment.Start:
                    Control.Gravity = GravityFlags.Start;
                    break;
            }
        }

        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetFont(NullableDatePicker view)
        {
            if (view.Font != Font.Default)
            {
                Control.TextSize = view.Font.ToScaledPixel();
                //Control.Typeface = view.Font.ToExtendedTypeface(Context);
            }
        }

        /// <summary>
        /// Set text based on nullable value
        /// </summary>
        /// <param name="view"></param>
        private void SetNullableText(NullableDatePicker view)
        {
            if (view.NullableDate == null)
                Control.Text = string.Empty;
        }

        /// <summary>
        /// Set the placeholder
        /// </summary>
        /// <param name="view"></param>
        private void SetPlaceholder(NullableDatePicker view)
        {
            Control.Hint = view.Placeholder;
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(NullableDatePicker view)
        {
            if (view.PlaceholderTextColor != Color.Default)
            {
                Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}