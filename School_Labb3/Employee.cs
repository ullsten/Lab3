using System.Data;
using Microsoft.Data.SqlClient;
using School_Labb3.Data;
using School_Labb3.Models;

namespace School_Labb3
{
    public class Employee
    {
        public void GetAllStaff() //Get all staff in school and position
        {
           SqlConnection sqlcon = new SqlConnection(@"Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True");
            //Query bestämmer vilken data vill vi ha
            SqlDataAdapter sqlDat = new SqlDataAdapter("SELECT CONCAT(FirstName,' ', LastName) AS Staff, PositionId, PositionName AS [Position] From StaffAdmin\r\n" +
                "Join Staff ON FK_StaffId=StaffId\r\n" +
                "Join Position ON FK_PositionId=PositionId", sqlcon);
            //Tomt data table
            DataTable dtTbl = new DataTable();
            //Fyller data table
            sqlDat.Fill(dtTbl);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 41));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("| {0, -2} | {1, -20} | {2, -9} |", "ID", "Name", "Position");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 41));
            foreach (DataRow dr in dtTbl.Rows)
            {
                Console.WriteLine("| {0, -2} | {1, -20} | {2, -9} |",
                        dr["PositionId"], dr["Staff"], dr["Position"]);
            }
            Console.WriteLine(new string('-', 41));
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.DarkGray;
            Console.WriteLine("Enter to see menu again");
            Console.ResetColor();
            Console.ReadLine();
            Menu menu = new Menu();
            menu.SqlMenu();
        }
        public void OtherPosition() //Get staff after user select position
        {
            using (var context = new SchoolContext()) //Connection to get position info
            {
                
                var showPosition = from p in context.Positions //Gets positions from database
                                   select p;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 20));
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0, -2} | {1, -13} |", "Id", "Position");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 20));
           
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var p in showPosition) //show position ID and position name
                {
                    Console.WriteLine("{0, -2} | {1, -13} |", p.PositionId, p.PositionName);
                }
                Console.WriteLine(new string('-', 20));
                Console.ResetColor();
            }
            Console.WriteLine();
            int getPosition = 0;
            Console.WriteLine("Select position to view staff from. [1-5]");
            while (true)//Check thats input is INT and between 1-5
            {
                var positioninput = Console.ReadLine();
                if (int.TryParse(positioninput, out getPosition) && getPosition >= 1 && getPosition <= 5)
                {
                    try
                    {
                        string conString = "Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True";
                        using (SqlConnection connection = new SqlConnection(conString))
                        {
                            //SQL-code
                            SqlCommand cmd = new SqlCommand("SELECT CONCAT(FirstName,' ', LastName) AS Staff, PositionName AS Position, StreetAddress AS [Street address], PostCode [Post code], City\r\n" +
                                "FROM StaffAdmin\r\n" +
                                "Join Staff ON FK_StaffId=StaffId\r\n" +
                                "Join Position ON FK_PositionId=PositionId\r\n" +
                                "Join Address ON FK_AddressId=AddressId\r\nWHERE PositionId = @getPosition;", connection);
                            //open connection to base
                            connection.Open();
                            cmd.Parameters.AddWithValue("@getPosition", getPosition); //get input value to sql-query
                            SqlDataReader sdr = cmd.ExecuteReader();
                            while (sdr.Read())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{sdr["Staff"]} {sdr["Position"]}");
                                Console.ResetColor();
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            MoreInput();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("OOPs, Somthing went wrong" + e);
                    }
                }
                else
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Select position 1-5!");
                    Console.ResetColor();
                }
                 //Ask if user want to see other positions
            }         
        }
        public void GetPositions()
        {
            using (var context = new SchoolContext())
            {

                Console.WriteLine("Getting positions");
                Thread.Sleep(200);
                Console.WriteLine("*");
                Thread.Sleep(200);
                Console.WriteLine(" *");
                Thread.Sleep(200);
                Console.WriteLine("  *");
                Thread.Sleep(200);
                Console.WriteLine("Done");
            }
        }
        public void MoreInput( )
        {
            Menu menu = new Menu();
            Console.WriteLine("Do you want to search again? (Y/N)");
            Console.WriteLine();
            Console.ResetColor();
            var again = Console.ReadLine();
            switch (again.ToLower())
            {
                case "y":
                    Console.Clear();
                    OtherPosition();
                    break;
                default:
                    Console.Clear();
                    menu.SqlMenu();
                    break;
            }
        }
        public void AddStaff() //Add new staff
        {
            using (var context = new SchoolContext())
            {
                Console.Write("Enter first name: ");
                var firstname = Console.ReadLine();
                Console.Write("Enter last name: ");
                var lastname = Console.ReadLine();
                Console.Write("Enter start date: ");
                var startdate = Console.ReadLine();

                var newStaff = new staff();
                {
                    newStaff.FirstName = firstname;
                    newStaff.LastName = lastname;
                    newStaff.HireDate = startdate;
                }
                Console.WriteLine();
                Console.Write("Street address: ");
                var street = Console.ReadLine();
                Console.Write("Postal code: ");
                var postalcode = Console.ReadLine();
                Console.Write("City: ");
                var city = Console.ReadLine();
                var Address = new Address
                {
                    StreetAddress = street,
                    PostCode = postalcode,
                    City = city
                };
                //Adds+save to address + staff
                context.Addresses.Add(Address);
                context.staff.Add(newStaff);
                context.SaveChanges();
                Console.WriteLine("New staff added");
                Console.WriteLine("New address added");
                //Find newStaffId for connection table StaffAdmin
                int newSId = 0; //stores latest staffId
                var newStaffId = from ns in context.staff
                                 orderby newStaff.StaffId
                                 select ns;
                foreach (var item in newStaffId)
                {
                    newSId = newStaff.StaffId;
                }
                //Find addressId for connection table StaffAdmin
                int newAId = 0; //Stores latest addressId
                var newAddressId = from na in context.Addresses
                                   orderby Address.AddressId
                                   select na;
                foreach (var item in newAddressId)
                {
                    newAId = Address.AddressId;
                }
                Console.WriteLine();
                SetRelations(newSId, newAId, firstname, lastname);
            }
        }
        public void SetRelations(int newSId, int newAId, string firstname, string lastname) 
        {
            //Sets relations in StaffAdmin table
            using (var context = new SchoolContext())
            {
                //Loop and check for int input
                bool IdContinue = true;
                while (IdContinue)
                {
                    Console.Write("Set position for new staff 1-6: ");
                    var selectedPosition = Console.ReadLine();
                    int setPositionId;
                    if (Int32.TryParse(selectedPosition, out setPositionId) && setPositionId >= 1 || setPositionId <= 6)
                    {
                        IdContinue = false;
                        var staffAdmin = new StaffAdmin
                        {
                            FkStaffId = newSId,
                            FkPositionId = setPositionId,
                            FkAddressId = newAId
                        };
                        //Adds+save to table
                        context.StaffAdmins.Add(staffAdmin);
                        context.SaveChanges();

                        string showPos;
                        var getPosName = from p in context.Positions
                                         where p.PositionId == setPositionId
                                         select p;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (var p in getPosName)
                        {
                            showPos = p.PositionName;
                            Console.WriteLine($"Welcome {firstname} {lastname} as a {showPos} at our mySchool.");
                        }
                        Console.ResetColor();
                        //Console.WriteLine($"Welcome {firstname} {lastname} as a {setPositionId} at our mySchool.");
                    }
                    else
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("Select between 1-6");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                    moreToAdd();
                }
            }
        }
        public void moreToAdd()
        {
            Menu menu = new Menu();
            Console.WriteLine("Are there more employees to be registered today? [Y/N]");
            var moreAdd = Console.ReadLine();
            if (moreAdd.ToLower() == "y")
            {
                AddStaff();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("You find out the way out yourself");
                Console.ResetColor();
                menu.SqlMenu();
            }
        }
        public void RemoveStaff()
        {
            // Removes from database
            var context = new SchoolContext();
            context.staff.Remove(context.staff.Find(11));
            context.SaveChanges();
            Console.WriteLine("Staff removed");
        }
    }
}
