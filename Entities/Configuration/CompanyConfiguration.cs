

using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CompanyConfiguration:IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company(Guid.NewGuid())
                {
                    Id = Guid.NewGuid(),
                    Name = "FPT Software",
                    Address = "Thu Duc, HCM, VN",
                    Country = "Viet Nam"
                },
                new Company(Guid.NewGuid())
                {
                    Id = Guid.NewGuid(),
                    Name = "VinGroup",
                    Address = "Ha Noi, VN",
                    Country = "Viet Nam"
                }
         );
        }

    }
}
