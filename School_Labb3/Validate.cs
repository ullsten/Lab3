using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLogin.Identity.Swedish;
using ActiveLogin.Identity.Swedish.Extensions;
using School_Labb3.Data;

namespace School_Labb3
{
    
    public class Validate
    {
        public void SsnValidateDatabase() //Validate students social security number
        {
            using (var context = new SchoolContext())
            {
                var studId = 0;
                string firstname = "";
                string lastname = "";
                string ssn = "";
                var ssnValidate = from s in context.Students
                                  select s;

                foreach (var s in ssnValidate)
                {
                    studId = s.StudentId;
                    firstname = s.FirstName;
                    lastname = s.LastName;
                    ssn = s.SecurityNumber;
                }

                if (PersonalIdentityNumber.TryParse(ssn, out var personalIdentityNumber))
                {
                    ssn = personalIdentityNumber.ToString();
                    //return ssn;
                    Console.WriteLine("Valid");
                }
                else
                {
                    Console.WriteLine("Found student with non valid security number!");
                    Console.WriteLine($"{studId} {firstname} {lastname} {ssn}");
                }
            }
        }
        public string ValidateSsn(out string ssn) //Validate social security number for new student
        {
            while (true)
            {
                Console.Write("Enter Security number: ");
                var inputSsn = Console.ReadLine();

                if (PersonalIdentityNumber.TryParse(inputSsn, out var personalIdentityNumber))
                {
                    ssn = personalIdentityNumber.ToString();
                    return ssn;
                }
                else
                {
                    Console.WriteLine("Not a valid ssn! Try again");

                }
            } 
        }
        public void test()
        {
            using (var context = new SchoolContext())
            {
                var studId = 0;
                string firstname = "";
                string lastname = "";
                string ssn = "";
                var ssnValidate = from s in context.Students
                                  select s;

                foreach (var s in ssnValidate)
                {
                    studId = s.StudentId;
                    firstname = s.FirstName;
                    lastname = s.LastName;
                    ssn = s.SecurityNumber;
                }

                if (PersonalIdentityNumber.TryParse(ssn, out var personalIdentityNumber))
                {
                    ssn = personalIdentityNumber.ToString();
                    //return ssn;
                    Console.WriteLine("Valid");
                }
                else
                {
                    Console.WriteLine("Found student with non valid security number!");

                    foreach (var item in ssn)
                    {
                        Console.WriteLine($"{studId} {firstname} {lastname} {ssn}");
                    }
                }
            }
        }
    }
}
