using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infra.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Type)
                .IsRequired()
                .HasColumnName("type")
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Date)
                .IsRequired()
                .HasColumnName("date")
                .HasColumnType("datetime2");

            builder.Property(c => c.Amount)
                .IsRequired()
                .HasColumnName("amount")
                .HasColumnType("decimal(8,2)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("varchar(255)");

            builder.ToTable("transactions");
        }
    }
}
