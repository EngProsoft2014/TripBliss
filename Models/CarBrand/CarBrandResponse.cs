namespace TripBliss.Models;

public record CarBrandResponse
(
    int Id,
    string BrandName,
    int CarTypeId,
    string CarTypename,
    bool Active
);
