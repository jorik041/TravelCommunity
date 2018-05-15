using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using TravelCommunity.Custom;
using TravelCommunity.iOS.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCircleRenderer))]
namespace TravelCommunity.iOS.Custom
{
	/// <summary>
    /// ImageCircle Implementation
    /// </summary>
    //[Preserve(AllMembers = true)]
    public class ImageCircleRenderer : ImageRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public async static void Init()
        {
            var temp = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            CreateCircle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
			    e.PropertyName == ImageCircle.BorderColorProperty.PropertyName ||
			    e.PropertyName == ImageCircle.BorderThicknessProperty.PropertyName ||
			    e.PropertyName == ImageCircle.FillColorProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
				var min = Math.Min(Element.Width, Element.Height);
				if(((ImageCircle)Element).IsRounded)
				{
					Control.Layer.CornerRadius = (nfloat)(min / 2.0);
				} else
				{
					Control.Layer.CornerRadius = 0f;
				}
                Control.Layer.MasksToBounds = false;
				Control.BackgroundColor = ((ImageCircle)Element).FillColor.ToUIColor();
                Control.ClipsToBounds = true;

				var borderThickness = ((ImageCircle)Element).BorderThickness;

                //Remove previously added layers
                var tempLayer = Control.Layer.Sublayers?
                                       .Where(p => p.Name == borderName)
                                       .FirstOrDefault();
                tempLayer?.RemoveFromSuperLayer();

                var externalBorder = new CALayer();
                externalBorder.Name = borderName;
                externalBorder.CornerRadius = Control.Layer.CornerRadius;
                externalBorder.Frame = new CGRect(-.5, -.5, min + 1, min + 1);
				externalBorder.BorderColor = ((ImageCircle)Element).BorderColor.ToCGColor();
				externalBorder.BorderWidth = ((ImageCircle)Element).BorderThickness;

                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }

        const string borderName = "borderLayerName";
    }
}