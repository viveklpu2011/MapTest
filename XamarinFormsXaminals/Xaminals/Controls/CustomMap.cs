using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
namespace Xaminals.Controls
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
        public List<Position> RouteCoordinates { get; set; }

        public CustomMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
    public class CustomPin
    {
        public Pin Pin { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public bool startPin = false;
    }
}
