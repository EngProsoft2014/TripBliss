namespace TripBliss.Models;

public record CarModelResponse
(
     int Id,
     string ModelName,
     string CarYear,
     int CarBrandId,
     string CarBrandName,
     bool Active
);
