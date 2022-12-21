using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using School_Labb3.Data;
using School_Labb3.Models;

namespace School_Labb3
{
    public class Menu
    {
        public void SchoolMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the School");
            Console.WriteLine();
            Console.WriteLine("[1] SQL-query\n" +
                              "[2] Entity Framework\n" +
                              "[3] Extra");
            var schoolInput = Console.ReadLine();
            switch (schoolInput)
            {
                case "1":
                    SqlMenu();
                    break;
                case "2":
                    EntityMeny();
                    break;
                    case "3":
                    ExtraChallenges();
                    break;
                default:
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Make a choice between 1-3");
                    Console.ResetColor();
                    Console.ReadLine();
                    SchoolMenu();
                    break;
            }
        }
        public void SqlMenu()
        {
            Console.Clear();
            Employee staff = new Employee();
            GradeCourse gc = new GradeCourse();
            Pupil pupil = new Pupil();
            Console.WriteLine("Sql-query");
            Console.WriteLine();
            Console.WriteLine("[1] Personell list\n" +
                              "[2] Choose position for staff\n" +
                              "[3] Grade last month\n" +
                              "[4] Courses, avg,-highest and lowest grade\n" +
                              "[5] Add new student\n" +
                              "[6] Back to school entrance\n" +
                              "[7] Go home");
            var sqlInput = Console.ReadLine();
            switch (sqlInput)
            {
                case "1":
                    staff.GetAllStaff();
                    break;
                case "2":
                    staff.GetPositions();
                    staff.OtherPosition();
                    break;
                case "3":
                    gc.GradeLastMonth();
                    break;
                case"4":
                    gc.CourseAvg();
                    break;
                case "5":
                    pupil.AddNewStudent();
                    break;
                case "6":
                    SchoolMenu();
                    break;
                case "7":
                    Console.WriteLine("It´s been nice showing you around ");
                    break;
                default:
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Make a choice between 1-7");
                    Console.ResetColor();   
                    Console.ReadLine();
                    SqlMenu();
                    break;
            }
        }
        public void EntityMeny()
        {
            Console.Clear();
            Pupil pupil = new Pupil();
            PupilClass  pupilClass = new PupilClass();
            Employee staff= new Employee();
            Console.WriteLine("Entity framework");
            Console.WriteLine();
            Console.WriteLine("[1] Get all student\n" +
                              "[2] Choose class to see students\n" +
                              "[3] Add new staff\n" +
                              "[4] Back to school entrance\n" +
                              "[5] Go home");
                var staffInput = Console.ReadLine();
                switch (staffInput)
                {
                    case "1":
                        pupil.GetAllStudents();
                        break;
                    case "2":
                    pupilClass.DownlodingClasses();
                    pupilClass.GetStudentByClass();
                        break;
                    case "3":
                    staff.AddStaff();
                    break;
                    case "4":
                    SchoolMenu();
                        break;
                    case "5":
                    Console.WriteLine("It´s been nice showing you around ");
                    Environment.Exit(0);    
                        break;
                    default:
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Make a choice between 1-5");
                    Console.ResetColor();
                    Console.ReadLine();
                    EntityMeny();
                   break;
                }
        }
        public void ExtraChallenges()
        {
            GradeCourse gradeCourse = new GradeCourse();
            Console.Clear();
            Console.WriteLine("This is extra challenges");
            Console.WriteLine();
            Console.WriteLine("[1] Avg grade/age group \n" +
                              "[2] Grades last month(VIEW SQL]\n" +
                              "[3] Back to school entrance\n" +
                              "[4] Go home");
            var schoolInput = Console.ReadLine();
            switch (schoolInput)
            {
                case "1":
                    gradeCourse.pickingUp();
                    gradeCourse.AvgGradeGenderYear();
                    break;
                case "2":
                    gradeCourse.ViewGradesLastMonth();
                        break;
                case "3":
                    SchoolMenu();
                    break;
                case "4":
                    Console.WriteLine("Thanks for today. Looking forward to the next visist.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Make a choice between 1-2");
                    Console.ResetColor();
                    Console.ReadLine();
                    SchoolMenu();
                    break;
            }
        }
    }
}
