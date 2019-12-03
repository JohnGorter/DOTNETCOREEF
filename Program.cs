using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Person p = new Person() { Name="john"};
            using (PersonDatabase database = new PersonDatabase()){
                database.Database.EnsureCreated ();
                database.persons.Add(p); 
                database.SaveChanges();
            }
            Console.WriteLine("Done.");
            using (PersonDatabase database = new PersonDatabase()){
                IEnumerable<Person> johns = database.persons.Where(p2 => p2.Name.StartsWith("j")).ToList();

                foreach(var person in johns){
                    Console.WriteLine("person " + person.Name);
                }
            }
        }
    }

    // code first
    class Person {
        public int id { get; set; }
        public String Name { get; set; }  
    }

    class PersonDatabase : DbContext {
        public DbSet<Person> persons { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // Specify the path of the database here
            optionsBuilder.UseSqlite("Filename=./persons.sqlite");
        }

    }
}
