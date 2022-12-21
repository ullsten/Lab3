using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using School_Labb3.Data;
using School_Labb3.Models;

namespace School_Labb3
{
    public class GradeCourse
    {
        public void GradeLastMonth()
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True");
            //Query bestämmer vilken data vill vi ha
            SqlDataAdapter sqlDat = new SqlDataAdapter("SELECT CONCAT(Student.FirstName,' ', Student.LastName) AS Student, CourseName AS [Course], GradeName AS [Grade], DateOfGrade AS [Date of grade]" +
                " From Exam " +
                "Join Student ON FK_StudentId=StudentId " +
                "Join Course ON FK_CourseId=CourseId " +
                "Join Grade ON FK_GradeId=GradeId " +
                "WHERE DateOfGrade >= DATEADD(MONTH,-1,GETDATE()) " +
                "Order by DateOfGrade DESC", sqlcon);

            //Tomt data table
            DataTable dtTbl = new DataTable();
            //Fyller data table
            sqlDat.Fill(dtTbl);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 64));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("| {0, -18} | {1, -8} | {2, -5} | {3, -20} |", "Student", "Course", "Grade", "Date of grade");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 64));
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (DataRow dr in dtTbl.Rows)
            {
                Console.WriteLine("| {0, -18} | {1, -8} | {2, -5} | {3, 20} |", dr["Student"], dr["Course"], dr["Grade"], dr["Date of grade"]);
            }
            Console.WriteLine(new string('-', 64));
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter to see menu again");
            Console.ResetColor();
            Console.ReadLine();
            Menu menu = new Menu();
            menu.SqlMenu();
        }
        public void CourseAvg()
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True");
            //Query bestämmer vilken data vill vi ha
            SqlDataAdapter sqlDat = new SqlDataAdapter("SELECT CourseName AS Course, ROUND(AVG(CAST(GradeName AS FLOAT)),2) AS [Avg grade], Max(GradeName) AS [Max grade], Min(GradeName) AS [Lowest grade] " +
                "FROM Exam " +
                "Join Course ON FK_CourseId=CourseId " +
                "Join Grade ON FK_GradeId=GradeId " +
                "Group BY CourseName", sqlcon);

            //Tomt data table
            DataTable dtTbl = new DataTable();
            //Fyller data table
            sqlDat.Fill(dtTbl);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 33));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("| {0, -7} | {1, -7} | {2, -3} | {3, -3} |", "Course", "Average", "Max", "Min");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 33));
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (DataRow dr in dtTbl.Rows)
            {
                Console.WriteLine("| {0, -7} | {1, -7} | {2, -3} | {3, -3} |", dr["Course"], dr["Avg grade"], dr["Max grade"], dr["Lowest grade"]);
            }
            Console.WriteLine(new string('-', 33));
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("Enter to see menu again");
            Console.ResetColor();
            Console.ReadLine();
            Menu menu = new Menu();
            menu.SqlMenu();
        }
        public void AvgGradeGenderYear()
        {
            using (var context = new SchoolContext())
            {
                ShowAgeGroup();
                int getYear;
                bool check;
                do
                {
                    check= false;
                    Console.WriteLine("Select age group: [4-digits]");
                    var ageGroup = Console.ReadLine();
                    if (int.TryParse(ageGroup, out getYear) && getYear == 1982 || getYear == 1999 || getYear == 2005 || getYear == 2010)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Opp´s! Try again and select age group.4-digits");
                    }
                }
                while (!check);

                string setGender;
                bool checkGender;
                do
                {
                    checkGender = false;
                    Console.WriteLine("Select gender: (M)ale (F)emale");
                   setGender = Console.ReadLine();
                    if (setGender.ToLower() == "m" || setGender.ToLower() == "male")
                    {
                        setGender = "Male";
                        break;
                    }
                    else if (setGender.ToLower() == "f" || setGender.ToLower() == "female")
                    {
                        setGender= "Female";
                        break;
                    }
                }
                while (!checkGender);
                var avgGradeGender = (from e in context.Exams
                                          //join co in context.Courses on e.FkCourseId equals co.CourseId
                                      join g in context.Grades on e.FkGradeId equals g.GradeId
                                      join s in context.Students on e.FkStudentId equals s.StudentId
                                      join c in context.Classes on s.FkClassId equals c.ClassId
                                      where c.YearOfClass == getYear //input from user
                                      where s.Gender == setGender //Input from user
                                      select g.GradeName).DefaultIfEmpty().Average();
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine($"Average grade for {setGender} in age group {getYear} is: {avgGradeGender} ");
                Console.ResetColor();
                AgeGroupAgain();
            }
        }
        public void ShowAgeGroup()
        {
            using (var context = new SchoolContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 13));
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("| {0, -9} |", "Age group");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 13));
                Console.ForegroundColor = ConsoleColor.Green;
                var groupYear = context.Classes.OrderBy(c => c.YearOfClass);
                foreach (var gYear in groupYear)
                {
                    Console.WriteLine("| {0, -9} |", gYear.YearOfClass);
                }
                Console.WriteLine(new string('-', 13));
                Console.ResetColor();
            }      
        }
        public void AgeGroupAgain()
        {
            Menu menu = new Menu();
            Console.WriteLine("See another age group? [Y/N]");
            var moreAdd = Console.ReadLine();
            if (moreAdd.ToLower() == "y")
            {
                Console.Clear();
                AvgGradeGenderYear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("You´ll find out for yourself?");
                Console.ResetColor();
                menu.SqlMenu();
            }
        }
        public void pickingUp()
        {
            
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("Picking up year groups at school");
            Thread.Sleep(400);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("almost there.");
            Thread.Sleep(400);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("just a little more..");
            Thread.Sleep(400);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Oh no! Nothing to worry about");
            Thread.Sleep(400);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Finally! Here you have!");
            Console.ResetColor();
        }
        public void ViewGradesLastMonth()
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True");
            //Query bestämmer vilken data vill vi ha
            SqlDataAdapter sqlDat = new SqlDataAdapter("SELECT * FROM [Grades last month]", sqlcon);
            //Tomt data table
            DataTable dtTbl = new DataTable();
            //Fyller data table
            sqlDat.Fill(dtTbl);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 63));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("| {0, -18} | {1, -7} | {2, -5} | {3, -20} |", "Student", "Course", "Grade", "Date of grade");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 63));
            foreach (DataRow dr in dtTbl.Rows)
            {
               //Console.WriteLine($"{dr["Student"]} { dr["Course"]}");

                Console.WriteLine("| {0, -18} | {1, -7} | {2, -5} | {3, -20} |",
                dr["Student"], dr["Course"], dr["Grade"], dr["Date of grade"]);
            }
            Console.WriteLine(new string('-', 63));
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter to see menu again");
            Console.ResetColor();
            Console.ReadLine();
            Menu menu = new Menu();
            menu.ExtraChallenges();
        }
    }
   
}
