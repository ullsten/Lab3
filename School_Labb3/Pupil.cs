using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Labb3.Data;
using School_Labb3.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Runtime.CompilerServices;
using ActiveLogin.Identity.Swedish;

namespace School_Labb3
{
    public class Pupil
    {
        public void GetAllStudents()
        {
            Console.Clear();
            Menu menu = new Menu();
            using (var context = new SchoolContext())
            {
                Console.WriteLine("Choose the order you want to see");
                Console.WriteLine();
                Console.WriteLine("(F)irst name and ascendant");
                Console.WriteLine("(L)ast name and descending");
                Console.WriteLine("(E)Entity menu");
                Console.WriteLine("(S)chool entrance");
                Console.WriteLine("(G)o home");
                var orderInput = Console.ReadLine();
                switch (orderInput.ToLower())
                {
                    case "f":
                        GetStudentsByFirstName();
                        break;
                    case "l":
                        GetStudentsByLastName();
                        break;
                    case "e":
                        menu.EntityMeny();
                        break;
                    case "s":
                        menu.SchoolMenu();
                        break;
                    case "g":
                        Console.WriteLine("Thanks for your company.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choose from menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        GetAllStudents();
                        break;
                }
            }
            Console.ReadLine();
        }
        public void GetStudentsByFirstName()
        {
            Console.Clear();
            using (var context = new SchoolContext())
            {
                //Fluent API Syntax
                var students = context.Students.OrderBy(s => s.FirstName); //Ascendant and first name first

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Sorted by first name and Ascendant");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine(new string('-', 70));
                Console.ResetColor();
                Console.ForegroundColor= ConsoleColor.Blue;
                Console.WriteLine("{0, -2} | {1, -10} | {2, -13} | {3, -5} | {4, -6} | {5, -15} |", "Id", "First name", "Last name", "Age", "Gender", "Security number");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 70));
                foreach (var s in students)
                {
                        Console.WriteLine("{0, -2} | {1, -10} | {2, -13} | {3, -5} | {4, -6} | {5, -15} |", 
                       s.StudentId, s.FirstName, s.LastName, s.Age, s.Gender, s.SecurityNumber);
                }
                Console.WriteLine(new string('-', 70));
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.DarkGray;
            Console.WriteLine("Enter to menu");
            Console.ResetColor();
            Console.ReadLine();
            GetAllStudents();

        }
        public void GetStudentsByLastName()
        {
            Console.Clear();
            using (var context = new SchoolContext())
            {
                //Fluent API Syntax
                var students = context.Students.OrderByDescending(s => s.LastName); //Descending and last name first

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Sorted by last name and descending");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 70));
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0, -2} | {1, -10} | {2, -13} | {3, -5} | {4, -6} | {5, -15} |", "Id", "First name", "Last name", "Age", "Gender", "Security number");
                Console.ResetColor();
                Console.WriteLine(new string('-', 70));
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var s in students)
                {
                    Console.WriteLine("{0, -2} | {1, -10} | {2, -13} | {3, -5} | {4, -6} | {5, -15} |",
                       s.StudentId, s.FirstName, s.LastName, s.Age, s.Gender, s.SecurityNumber);
                }
                Console.WriteLine(new string('-', 70));
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter to menu");
            Console.ResetColor();
            Console.ReadLine();
            GetAllStudents();
        }
        public void AddNewStudent()
        {
            Validate validate = new Validate();
            Update update = new Update();
            Console.Write("Enter firstname: ");
            var firstname = Console.ReadLine();
            Console.Write("Enter lastname: ");
            var lastname = Console.ReadLine();
            Console.Write("Birthday: ");
            var birthday = Console.ReadLine();
            Console.Write("Enter Security number: ");
            var ssn = Console.ReadLine();
            //string ssn;
            //validate.ValidateSsn(out ssn);

            int newClassId;
            Console.WriteLine("Select class: (1)A | (2)A | (3)B");
            while (true)
            {
                var classid = Console.ReadLine();
                if (int.TryParse(classid, out newClassId) && newClassId >= 1 && newClassId <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Select class 1-3!");
                }
            }
            Console.WriteLine();
            try
            {
                string conString = "Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    //SQL-code
                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (FirstName, LastName, DayOfBirth, SecurityNumber, FK_ClassId)" +
                        "Values(@firstname, @lastname, @birthday ,@ssn, @classId)", connection);
                    //open connection to base
                    connection.Open();
                    //set input value to sql-query
                    cmd.Parameters.AddWithValue("@firstname", firstname); 
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@birthday", birthday);
                    cmd.Parameters.AddWithValue("@ssn", ssn);
                    cmd.Parameters.AddWithValue("@classId", newClassId);
                    SqlDataReader sdr = cmd.ExecuteReader();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine($"We welcome {firstname} {lastname}");
                    Console.WriteLine($"Hope you enjoy your class and at school.");
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, Somthing went wrong" + e);
            }
            update.AgeUpdate();
            update.GenderUpdate();
            Console.WriteLine();
            AddMoreStudent();
        }
        public void AddMoreStudent()
        {
            Menu menu = new Menu();
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("Are there more students to be enrolled? [Y/N]");
            Console.ResetColor();
            var moreEnrolled = Console.ReadLine();
            if (moreEnrolled.ToLower() == "y")
            {
                Console.Clear();
                AddNewStudent();
            }
            else 
            {
                Console.WriteLine("Have a nice day!");
                menu.SqlMenu();
                
            }
        }
    }
}
