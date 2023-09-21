using StudentFormMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace StudentFormMVC.DAL
{
    public class StudentDBContext : DbContext
    {

        public StudentDBContext() : base("StudentDBContext")
        {

        }
        //Database r sata connection
        //data access er kaj a use kori kono data binding er kaje use kori na
        //DbSet=Database hota list hesab a nea asba
        public DbSet<Students> Students { get; set; }

    }
}