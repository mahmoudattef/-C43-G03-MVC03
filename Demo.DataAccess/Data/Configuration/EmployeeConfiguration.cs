﻿using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configuration
{
    internal class EmployeeConfiguration : BaseEntityConfiguration<Employee>,IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("VarChar(50)");
            builder.Property(E => E.Address).HasColumnType("VarChar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");

            builder.Property(E => E.Gender).HasConversion((EmpGender) => EmpGender.ToString(),
                (_gender) =>(Gender) Enum.Parse(typeof(Gender), _gender));

            builder.Property(E => E.EmployeeType).HasConversion((EmpType) => EmpType.ToString(),
               (_Type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _Type));
            base.Configure(builder);


        } 
    }
}
