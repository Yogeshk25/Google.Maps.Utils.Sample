using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Foundation;
using GMCluster;
using Google.Maps;
using GoogleMapsUtilsDemo.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(GoogleMapsUtilsDemo.iOS.Renderers.CustomMapRenderer))]
namespace GoogleMapsUtilsDemo.iOS.Renderers
{
    public class CustomMapRenderer : ViewRenderer<CustomMap, MapView>, IGMUClusterRendererDelegate, IGMUClusterManagerDelegate, IMapViewDelegate
    {
        protected MapView nativeMap => (MapView)Control;
        protected CustomMap Map => (CustomMap)Element;

        GMUClusterManager clusterManager;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomMap> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new MapView(RectangleF.Empty));
                UpdateAllPins();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(CustomMap.PinsProperty.PropertyName))
            {
                UpdateAllPins();
            }
        }

        [Export("clusterManager:didTapCluster:")]
        public void DidTapCluster(GMUClusterManager clusterManager, IGMUCluster cluster)
        {
            IList objs = new List<POIItem>();
            foreach (var item in cluster.Items)
            {
                objs.Add((item as POIItem));
            }
            Map.ClusterClicked(objs);
        }

        [Export("clusterManager:didTapClusterItem:")]
        public void DidTapClusterItem(GMUClusterManager clusterManager, IGMUClusterItem clusterItem)
        {
            POIItem poitem = clusterItem as POIItem;
            Map.ClusterItemClicked(poitem);
        }

        [Export("renderer:willRenderMarker:")]
        public void WillRenderMarker(GMUClusterRenderer renderer, Overlay marker)
        {
            Google.Maps.Marker gmsMarker = marker as Marker;
            if (gmsMarker.UserData.GetType() == typeof(GMUStaticCluster))
            {
                GMUStaticCluster cluster = gmsMarker.UserData as GMUStaticCluster;
                // set a custom icon or view here
            }
            else
            {
                POIItem clustItem = gmsMarker.UserData as POIItem;
                // set a custom icon or view here
            }
        }

        void UpdateAllPins()
        {
            var googleMapView = nativeMap;
            var iconGenerator = new GMUDefaultClusterIconGenerator();
            var algorithm = new GMUNonHierarchicalDistanceBasedAlgorithm();
            GMUDefaultClusterRenderer renderer = null;
            try
            {
                renderer = new GMUDefaultClusterRenderer(googleMapView, iconGenerator);

                renderer.WeakDelegate = this;
                clusterManager = new GMUClusterManager(googleMapView, algorithm, renderer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            for (var i = 0; i < Map.Pins.Count; i++)
            {
                Pin pin = (Map.Pins[i] as Pin);
                var lat = pin.Position.Latitude;

                var lng = pin.Position.Longitude;

                var name = pin.Label;

                IGMUClusterItem item = new POIItem(lat, lng, name);

                clusterManager.AddItem(item);
            }

            clusterManager.Cluster();

            clusterManager.SetDelegate(this, this);
        }
    }
}
