using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using School_Labb3.Data;
using School_Labb3.Models;

namespace School_Labb3
{
    public class PupilClass
    {
        public void GetStudentByClass() //User select class to show students from 
        {
            Menu menu= new Menu();
            using (var context = new SchoolContext())
            {
                //Print classes to choose from
                var myClass = from c in context.Classes
                              select c;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 18));
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0, -2} | {1, -4} | {2, -4} |", "Id", "Name", "Year");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 18));
                foreach (var c in myClass)
                {
                    Console.WriteLine("{0, -2} | {1, -4} | {2, -4} |", 
                        c.ClassId, c.ClassName, c.YearOfClass);   
                }
                Console.WriteLine(new string('-', 18));
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Select 1, 2 or 3.");
                bool IdContinue = true;
                while (IdContinue)
                {
                    var classinput = Console.ReadLine(); //reads input from user
                    int getClassId = 0;
                    if (Int32.TryParse(classinput, out getClassId) && getClassId >= 1 && getClassId <= 3)
                    {
                        IdContinue = false;
                        //Gets student by class
                        var myStudent = from s in context.Students
                                        join c in context.Classes on s.FkClassId equals c.ClassId
                                        where c.ClassId == getClassId
                                        select new { s.FirstName, s.LastName, c.ClassName };
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(new string('-', 36));
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("| {0, -10} | {1, -12} | {2, -4}|", "First name", "Last name", "Class");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(new string('-', 36));
                        foreach (var s in myStudent)
                        {
                            Console.WriteLine("| {0, -10} | {1, -12} | {2, -4} |", s.FirstName, s.LastName, s.ClassName);
                           
                        }
                        Console.WriteLine(new string('-', 36));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("That´s wrong input! Select class 1-3");
                    }
                }
                Console.WriteLine();
                SeeOtherClass();
            }
        }
        public void SeeOtherClass()
        {
            Menu menu = new Menu();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("See another class? [Y/N]");
            Console.ResetColor();
            var input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                Console.Clear();
                GetStudentByClass();
            }
            else
            {
                menu.EntityMeny();
            }
        }
        public void DownlodingClasses()
        {
            Console.WriteLine("Downloading school classes");
            Thread.Sleep(400);
            Console.WriteLine("-");
            Thread.Sleep(400);
            Console.WriteLine(" -");
            Thread.Sleep(400);
            Console.WriteLine("  -");
            Thread.Sleep(400);
            Console.WriteLine("Done");
        }
    }
}
