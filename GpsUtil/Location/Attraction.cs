using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsUtil.Location;

public class Attraction : Locations
{
    public string AttractionName { get; }
    public string City { get; }
    public string State { get; }
    public Guid AttractionId { get; }
    public double LatRad { get; }
    public double LonRad { get; }
    public double SinLat { get; }
    public double CosLat { get; }

    public Attraction(string attractionName, string city, string state, double latitude, double longitude) : base(latitude, longitude)
    {
        AttractionName = attractionName;
        City = city;
        State = state;
        AttractionId = Guid.NewGuid();

        LatRad = Math.PI * Latitude / 180.0;
        LonRad = Math.PI * Longitude / 180.0;

        SinLat = Math.Sin(LatRad);
        CosLat = Math.Cos(LatRad);
    }
}
