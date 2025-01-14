using Android.Gms.Maps;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Platforms.Android
{
    class MapCallbackHandler : Java.Lang.Object, IOnMapReadyCallback
    {
        private readonly CustomMapHandler mapHandler;

        public MapCallbackHandler(CustomMapHandler mapHandler)
        {
            this.mapHandler = mapHandler;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mapHandler.UpdateValue(nameof(Microsoft.Maui.Maps.IMap.Pins));
            //mapHandler.Map?.SetOnMarkerClickListener(new CustomMarkerClickListener(mapHandler));
            //mapHandler.Map?.SetOnInfoWindowClickListener(new CustomInfoWindowClickListener(mapHandler));
        }
    }
}
