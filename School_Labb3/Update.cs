using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace School_Labb3
{
    public class Update
    {
        public void AgeUpdate()
        {
            //Update Age for last added student
            string conUpdateAge = "Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True";
            using (SqlConnection updateAge = new SqlConnection(conUpdateAge))
            {
                SqlCommand cmdAge = new SqlCommand("UPDATE Student\r\nSET Age = DATEDIFF(year,DayOfBirth, GETDATE())" +
                    "Where StudentId = IDENT_CURRENT('Student')", updateAge);
                //Open connection
                updateAge.Open();
                SqlDataReader sdr2 = cmdAge.ExecuteReader();
            }
        }
        public void GenderUpdate()
        {
            //Update gender for last added student
            string conUpdateSsn = "Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True";
            using (SqlConnection updateSsn = new SqlConnection(conUpdateSsn))
            {
                SqlCommand cmd3 = new SqlCommand("UPDATE Student\r\nSET Gender = (CASE WHEN right(rtrim(SecurityNumber),1) IN ('1', '3', '5', '7', '9') THEN 'Male'\r\n" +
                        "WHEN right(rtrim(SecurityNumber), 1) IN ('2', '4', '6', '8', '0') THEN 'Female' END)" +
                        "Where StudentId = IDENT_CURRENT('Student')", updateSsn);
                //open connection
                updateSsn.Open();
                SqlDataReader sdr = cmd3.ExecuteReader();
            }
        }
    }
}
