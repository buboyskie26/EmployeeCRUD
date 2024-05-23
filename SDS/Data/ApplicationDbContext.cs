using SDS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SDS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("EmployeeExamConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}