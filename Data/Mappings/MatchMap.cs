using MarcaTento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarcaTento.Data.Mappings
{
    public class MatchMap : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            // Tabela
            builder.ToTable("Match");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Property
            builder.Property(x => x.NameTeamOne)
                .IsRequired()
                .HasColumnName("NameTeamOne")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.NameTeamTwo)
                .IsRequired()
                .HasColumnName("NameTeamTwo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);
            
            builder.Property(x => x.Slug)
                .HasColumnName("Slug")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.ImageTeamOne)
                .HasColumnName("ImageTeamOne")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.ImageTeamTwo)
                .HasColumnName("ImageTeamTwo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.ScoreTotal)
                .HasColumnName("ScoreTotal")
                .HasColumnType("INT")
                .HasMaxLength(10);

            builder.Property(x => x.ScoreTeamOne)
                .HasColumnName("ScoreTeamOne")
                .HasColumnType("INT")
                .HasMaxLength(10);

            builder.Property(x => x.ScoreTeamTwo)
                .HasColumnName("ScoreTeamTwo")
                .HasColumnType("INT")
                .HasMaxLength(10);

            builder.Property(x => x.MatchDate)
                .HasColumnName("MatchDate")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            // Indices
            builder
                .HasIndex(x => x.Slug, "IX_Match_Slug")
                .IsUnique();
             

            // Relacionamentos
            builder
                .HasOne(x => x.User) // tem um usuário
                .WithMany(x => x.Matches) // várias partidas
                .HasConstraintName("FK_User_Matches") // cria a foreign key
                .OnDelete(DeleteBehavior.Cascade); // exclui as partidas em cacada com a exclusão do usuário
        }
    }
}
