using System.Data;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using School_Labb3.Data;
using School_Labb3.Models;
using ActiveLogin;
using ActiveLogin.Identity.Swedish;
using ActiveLogin.Identity.Swedish.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace School_Labb3
{
    
    public class Program
    {
        static void Main(string[] args)
        {
            Menu schoolMenu = new Menu();
            schoolMenu.SchoolMenu();
        }
        
    }
    
}
