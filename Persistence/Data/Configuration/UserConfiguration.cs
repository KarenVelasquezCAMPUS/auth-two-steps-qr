using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(p => p.UserId);

            builder.Property(p => p.UserId)
            .IsRequired();

            builder.Property(p => p.UserName)
            .IsRequired()
            .HasColumnName("username")
            .HasColumnType("varchar")
            .HasMaxLength(50);

            builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasColumnType("varchar")
            .HasMaxLength(60);

            builder.Property(p => p.TwoStepSecret)
            .IsRequired()
            .HasColumnName("twostepsecret");

            builder.Property(p => p.DateCreated)
            .IsRequired()
            .HasColumnName("datacreated")
            .HasColumnType("varchar")
            .HasMaxLength(36);
        }
    }
}