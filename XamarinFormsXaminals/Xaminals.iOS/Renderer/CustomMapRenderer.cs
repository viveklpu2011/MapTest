using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using CoreLocation;

using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

using ObjCRuntime;

using Xaminals.iOS.Renderer;
using System.Threading.Tasks;
using Xaminals.Controls;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Xaminals.iOS.Renderer
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        public static List<CustomPin> customPins;
        public static MKMapView nativeMap;
        public static MKPolylineRenderer polylineRenderer;
        public static IMKOverlay routeOverlay;






        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                try
                {


                    var nativeMap = Control as MKMapView;
                    if (nativeMap != null)
                    {
                        nativeMap.RemoveAnnotations(nativeMap.Annotations);
                        nativeMap.GetViewForAnnotation = null;
                        nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                        nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                        nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
            if (e.OldElement != null)
            {
                try
                {


                    var nativeMap = Control as MKMapView;
                    if (nativeMap != null)
                    {
                        nativeMap.RemoveOverlays(nativeMap.Overlays);
                        nativeMap.OverlayRenderer = null;
                        polylineRenderer = null;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MKMapView;

                nativeMap.OverlayRenderer = GetOverlayRenderer;

                CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.RouteCoordinates.Count];

                int index = 0;
                foreach (var position in formsMap.RouteCoordinates)
                {
                    coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
                    index++;
                }

                routeOverlay = MKPolyline.FromCoordinates(coords);
                nativeMap.AddOverlay(routeOverlay);


            }
            //Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            //{

            //    update();

            //    return true;
            //});
        }

        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var anno = annotation as MKPointAnnotation;
            var customPin = GetCustomPin(anno);
            if (customPin == null)
            {
                return null;
            }



            annotationView = mapView.DequeueReusableAnnotation(customPin.Id);
            if (annotationView == null)
            {



                annotationView = new CustomMKAnnotationView(annotation, customPin.Id)
                {

                    Image = UIImage.FromFile("pin.png"),


                    CalloutOffset = new CGPoint(0, 0),
                    Draggable = true,
                    LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("")),
                    RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure),

                };
                ((CustomMKAnnotationView)annotationView).Id = customPin.Id;


            }
            annotationView.CanShowCallout = true;
            return annotationView;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            if (!string.IsNullOrWhiteSpace(customView.Url))
            {
                UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
            }
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            try
            {


                var customView = e.View as CustomMKAnnotationView;
                customPinView = new UIView();
                if (customView.Id == "Xamarin")
                {
                    customPinView.Frame = new CGRect(0, 0, 200, 84);
                    var image = new UIImageView(new CGRect(0, 0, 200, 84));
                    image.Image = UIImage.FromFile("");
                    customPinView.AddSubview(image);
                    customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
                    e.View.AddSubview(customPinView);
                }
            }
            catch (Exception ex)
            {

            }
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }



        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            try
            {



                CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[((CustomMap)Element).RouteCoordinates.Count];

                int index = 0;
                foreach (var position in ((CustomMap)Element).RouteCoordinates)
                {
                    coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
                    index++;
                }

                routeOverlay = MKPolyline.FromCoordinates(coords);
                nativeMap.AddOverlay(routeOverlay);

                if (annotation == null)
                {

                    foreach (var pin in ((CustomMap)Element).CustomPins)
                    {
                        var position = new Position(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude);
                        if (pin.Pin.Position == position)
                        {
                            return pin;
                        }
                    }
                    return null;
                }
                else
                {
                    var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);

                    foreach (var pin in ((CustomMap)Element).CustomPins)
                    {
                        if (pin.Pin.Position == position)
                        {
                            return pin;
                        }
                    }



                    return null;

                }



            }
            catch (Exception ex)
            {
                return null;
            }



        }





        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (polylineRenderer == null && !Equals(overlayWrapper, null))
            {
                var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline)
                {
                    FillColor = UIColor.Blue,
                    StrokeColor = UIColor.Red,
                    LineWidth = 3,
                    Alpha = 0.4f
                };
            }
            return polylineRenderer;
        }



        public async void update()
        {



            try
            {


                var nativeMap = Control as MKMapView;

                var pin = ((CustomMap)Element).CustomPins.FirstOrDefault(p => p.Id == "Driver");

                foreach (var item in nativeMap.Annotations.Where(x => x.Coordinate.Latitude == pin.Pin.Position.Latitude && x.Coordinate.Longitude == pin.Pin.Position.Longitude))
                {


                    nativeMap.RemoveAnnotations(item);
                    item.SetCoordinate(new CLLocationCoordinate2D { Latitude = pin.Pin.Position.Latitude, Longitude = pin.Pin.Position.Longitude });


                }

            }
            catch (Exception ex)
            {

            }









        }

    }

}