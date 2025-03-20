


namespace Demo.DataAccess.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department> 
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
