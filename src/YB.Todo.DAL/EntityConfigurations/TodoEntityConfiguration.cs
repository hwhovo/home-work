using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YB.Todo.Core.Entities;

namespace YB.Todo.DAL.EntityConfigurations
{
    public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
    {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.ToTable("Todos", schema: "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description).HasColumnType("varchar(max)");
            builder.Property(x => x.CreatedDate).HasColumnType("datetimeoffset");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetimeoffset");
        }
    }
}
