using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ques1_Employee
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp1 = new Employee { ID = 1 , Name = "Sunil", Salary = 600000};
            Operations op = new Operations();
            int a = op.insertData(emp1);
            if(a > 0)
            {
                Console.WriteLine("Record {0} Added", emp1.ID);
            }
            Employee emp2 = new Employee { ID = 2, Name = "Ram", Salary = 800000 };
            int a2 = op.insertData(emp2);
            if (a2 > 0)
            {
                Console.WriteLine("Record {0} Added", emp2.ID);
            }
            Employee emp3 = new Employee { ID = 3, Name = "Shyam", Salary = 400000 };
            int a3 = op.insertData(emp3);
            if (a3 > 0)
            {
                Console.WriteLine("Record {0} Added", emp3.ID);
            }

            Console.WriteLine("Searching record with ID: 2");
            Employee e = op.Search(2);
            if(e != null)
            {
                Console.WriteLine("Record Found:");
                Console.WriteLine("ID: {0} \n Name: {1}\n Salary: {2}", e.ID, e.Name, e.Salary);
            }
            else
            {
                Console.WriteLine("Record Not Found!");
            }


            Console.WriteLine("Searching record with Name: Shyam");
            List<Employee> eList = op.Search("Shyam");
            if (eList != null)
            {
                Console.WriteLine("Records(s) found:");
                foreach (var emp in eList)
                {
                    Console.WriteLine("ID: {0} \n Name: {1}\n Salary: {2}", emp.ID, emp.Name, emp.Salary);
                }
            }
            else
            { 
                Console.WriteLine("Record not found!"); 
            }
        }
    }
}
