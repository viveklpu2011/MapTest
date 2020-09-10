using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps.Model;
using Xaminals.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Android.Gms.Maps;
using System.ComponentModel;
using Xaminals.Controls;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Xaminals.Droid.Renderer
{


    public class CustomMapRenderer : MapRenderer
    {
        public static Marker MoveAble_Marker;
       
        bool isDrawn;

        public CustomMapRenderer(Context context) : base(context)
        {

        }
       public void update()
        {
            try
            {

                     NativeMap.Clear();
                 

                    var polylineOptions = new PolylineOptions();
                    polylineOptions.InvokeColor(0x66FF0000);
                    foreach (var pin in ((CustomMap)Element).CustomPins)
                    {
                        BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.pin);
                        

                        var marker = new MarkerOptions();
                        marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                        marker.SetTitle(pin.Pin.Label);
                        marker.SetSnippet(pin.Pin.Address);
                        marker.SetIcon(icon);
                        if (pin.Id != "1")
                        {

                            MoveAble_Marker = NativeMap.AddMarker(marker);
                        }
                        else
                        {
                            NativeMap.AddMarker(marker);
                        }

                    }
                    //
                    foreach (var position in ((CustomMap)Element).RouteCoordinates)
                    {
                        polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                    }
                    NativeMap.AddPolyline(polylineOptions);

                   



                
            }
            catch { }
        }
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.OldElement != null)
                {
                    NativeMap.InfoWindowClick -= OnInfoWindowClick;
                }

                 
            }
            catch { }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

               // if (!isDrawn)
                {
                    //NativeMap.Clear();
                    NativeMap.InfoWindowClick += OnInfoWindowClick;

                    var polylineOptions = new PolylineOptions();
                    polylineOptions.InvokeColor(0x66FF0000);
                    foreach (var pin in ((CustomMap)Element).CustomPins)
                    {
                        BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.pin);
                       

                        var marker = new MarkerOptions();
                        marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                        marker.SetTitle(pin.Pin.Label);
                        marker.SetSnippet(pin.Pin.Address);
                        marker.SetIcon(icon);
                        if (pin.Id != "1")
                        {

                            MoveAble_Marker = NativeMap.AddMarker(marker);
                        }
                        else
                        {
                            NativeMap.AddMarker(marker);
                        }

                    }
                    //
                    foreach (var position in ((CustomMap)Element).RouteCoordinates)
                    {
                        polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                    }
                    NativeMap.AddPolyline(polylineOptions);

                    isDrawn = true;

                    //Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                    //{

                    //    update();

                    //    return true;
                    //});

                }
            }
            catch { }

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            try
            {
                base.OnLayout(changed, l, t, r, b);

                if (changed)
                {
                    isDrawn = false;
                }
            }
            catch { }
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            try
            {
                var customPin = GetCustomPin(e.Marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (!string.IsNullOrWhiteSpace(customPin.Url))
                {
                    var url = Android.Net.Uri.Parse(customPin.Url);
                    var intent = new Intent(Intent.ActionView, url);
                    intent.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(intent);
                }
            }
            catch { }
        }



        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
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
}