namespace DotNetHelper.Serialization.Csv.Tests.Models
{
   public class Employee
   {
       public string FirstName { get; set; }
       public string LastName { get; set; } 

       public Employee()
       {
           FirstName = "Kate";
           LastName = "Blake";
        }
    }
}
