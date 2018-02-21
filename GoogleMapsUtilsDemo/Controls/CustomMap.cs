using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GoogleMapsUtilsDemo.Controls
{
    public class ClusterClickedEventArgs : EventArgs
    {
        public IList Items { get; set; }
    }

    public class ClusterItemClickedEventArgs : EventArgs
    {
        public object Item { get; set; }
    }

    public class CustomMap : View
    {
        public event EventHandler<ClusterClickedEventArgs> ClusterClickedEvent;
        public event EventHandler<ClusterItemClickedEventArgs> ClusterItemClickedEvent;

        public IList Pins
        {
            get
            {
                return (IList)GetValue(PinsProperty);
            }
            set
            {
                SetValue(PinsProperty, value);
            }
        }

        public static readonly BindableProperty PinsProperty =
                BindableProperty.CreateAttached(
                nameof(Pins),
                typeof(IList),
                typeof(CustomMap),
                default(IList),
                propertyChanged: null);

        public void ClusterClicked(IList data)
        {
            if (ClusterClickedEvent != null)
            {
                ClusterClickedEvent(this, new ClusterClickedEventArgs() { Items = data });
            }
        }

        public void ClusterItemClicked(object data)
        {
            if (ClusterItemClickedEvent != null)
            {
                ClusterItemClickedEvent(this, new ClusterItemClickedEventArgs() { Item = data });
            }
        }

        public CustomMap()
        {
        }
    }
}
