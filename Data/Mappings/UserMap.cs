using MarcaTento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarcaTento.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabela
            builder.ToTable("User");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);
            // .HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(x => x.Username)
                .IsRequired()
                .HasColumnName("Username")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Image)
                .IsRequired()
                .HasColumnName("Image")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200);

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique();

            builder
                .HasIndex(x => x.Username, "IX_User_Name")
                .IsUnique();

            builder
                .HasIndex(x => x.Email, "IX_User_Email")
                .IsUnique();


           
        }
    }
}
