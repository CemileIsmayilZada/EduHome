using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Configurations
{
    public class CatagoryConfiguration:IEntityTypeConfiguration<Catagory>
    {
        

        public void Configure(EntityTypeBuilder<Catagory> builder)
        {
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasMany(c => c.Courses).WithOne(c => c.Catagory);         

        }
    }
}
