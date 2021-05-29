using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Controls
{
    public class NullableDatePicker : DatePicker
    {
        public NullableDatePicker() : base()
        {


        }

        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty FontProperty =
            BindableProperty.Create("Font", typeof(Font), typeof(NullableDatePicker), new Font());

        /// <summary>
        /// The NullableDate property
        /// </summary>
        public static readonly BindableProperty NullableDateProperty =
            BindableProperty.Create("NullableDate", typeof(DateTime?), typeof(NullableDatePicker), null, BindingMode.TwoWay);

        /// <summary>
        /// The XAlign property
        /// </summary>
        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(NullableDatePicker),
            TextAlignment.Start);

        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(NullableDatePicker), true);

        /// <summary>
        /// The Placeholder property
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(NullableDatePicker), string.Empty, BindingMode.OneWay);

        /// <summary>
        /// The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(NullableDatePicker), Color.Default);


        /// <summary>
        /// Gets or sets the Font
        /// </summary>
        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        /// <summary>
        /// Get or sets the NullableDate
        /// </summary>
        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set
            {
                if (value != NullableDate)
                {
                    SetValue(NullableDateProperty, value);
                    UpdateDate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the X alignment of the text
        /// </summary>
        public TextAlignment XAlign
        {
            get { return (TextAlignment)GetValue(XAlignProperty); }
            set { SetValue(XAlignProperty, value); }
        }


        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        /// <summary>
        /// Get or sets the PlaceHolder
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Sets color for placeholder text
        /// </summary>
        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == DateProperty.PropertyName)
            {
                NullableDate = Date;
            }

            if (propertyName == NullableDateProperty.PropertyName)
            {
                if (NullableDate.HasValue)
                {
                    Date = NullableDate.Value;
                    OnPropertyChanged(DateProperty.PropertyName);
                }
            }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                Date = NullableDate.Value;
            }
            else
            {
                Date = DateTime.Today;
            }
        }
    }
}
