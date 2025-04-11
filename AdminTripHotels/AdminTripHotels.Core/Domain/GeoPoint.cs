namespace AdminTripHotels.Core.Domain;

public readonly struct GeoPoint
{
    public GeoPoint(float latitude, float longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public readonly float Latitude;

    public readonly float Longitude;
}