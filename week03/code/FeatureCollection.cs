public class FeatureCollection
{
    public List<EarthquakeFeature> Features {get; set; }
    
}

public class EarthquakeFeature
{
    public EarthquakeProperties Properties { get; set; }
}

public class EarthquakeProperties
{
    public string Place { get; set; }
    public double? Mag { get; set; }
}
