using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using HotelListing.Api.Data;

public class HotelListingDbContextFactory : IDesignTimeDbContextFactory<HotelListingDbContext>
{
    public HotelListingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelListingDbContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-6T09G16\\SQLEXPRESS;Database=HotelListingDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Encrypt=false");

        return new HotelListingDbContext(optionsBuilder.Options);
    }
}