using CRM.Domain.Entities.DocumentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.DocumentManagement
{
    public class DocumentFolderConfiguration : IEntityTypeConfiguration<DocumentFolder>
    {
        public void Configure(EntityTypeBuilder<DocumentFolder> builder)
        {
            builder.ToTable("DocumentFolders");

            builder.HasKey(df => df.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(df => df.Name)
                .IsRequired()
                .HasMaxLength(100);


                



            builder.Property(df => df.Description)
                .HasMaxLength(500);

            builder.Property(df => df.Path)
                .IsRequired();

            builder.HasOne(df => df.ParentFolder)
               .WithMany(pf => pf.SubFolders)
               .HasForeignKey(df => df.ParentFolderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(df => df.SubFolders)
                .WithOne(sf => sf.ParentFolder)
                .HasForeignKey(sf => sf.ParentFolderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(df => df.Documents)
                .WithOne(d => d.Folder)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.HasIndex(df => df.Name);
            builder.HasIndex(df => df.ParentFolderId);
            builder.HasIndex(df => df.Path);
        }
    }
}
