using System;
using Xamarin.Forms;

namespace TravelCommunity.Custom
{
	public class ImageCircle : Image
	{
		#region Bindable properties
		/// <summary>
        /// Thickness property of border
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty =
          BindableProperty.Create(propertyName: nameof(BorderThickness),
              returnType: typeof(float),
			                      declaringType: typeof(ImageCircle),
              defaultValue: 0F);

        /// <summary>
        /// Border thickness of circle image
        /// </summary>
        public float BorderThickness
        {
            get { return (float)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        /// <summary>
        /// Color property of border
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(propertyName: nameof(BorderColor),
              returnType: typeof(Color),
			                        declaringType: typeof(ImageCircle),
              defaultValue: Color.White);


        /// <summary>
        /// Border Color of circle image
        /// </summary>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        /// <summary>
        /// Color property of fill
        /// </summary>
        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(propertyName: nameof(FillColor),
              returnType: typeof(Color),
			                        declaringType: typeof(ImageCircle),
              defaultValue: Color.Transparent);

        /// <summary>
        /// Fill color of circle image
        /// </summary>
        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

		/// <summary>
        /// Bool property of corner radius
        /// </summary>
        public static readonly BindableProperty IsRoundedProperty =
			BindableProperty.Create(propertyName: nameof(IsRounded),
			                        returnType: typeof(bool),
                                    declaringType: typeof(ImageCircle),
			                        defaultValue: false);

        /// <summary>
        /// Corner radius of circle image
        /// </summary>
		public bool IsRounded
        {
			get { return (bool)GetValue(IsRoundedProperty); }
			set { SetValue(IsRoundedProperty, value); }
        }
        #endregion
	}
}
