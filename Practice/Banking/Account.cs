using System;

namespace BankApp
{
    class Account
    {
        static int getId;
        int id = 0;
        string name;
        double balance;
        static double interest_rate = 0.07;
        static Account()
        {
            Console.WriteLine("Liberty Bank");
        }
        public Account(string nm,double bal)
        {
            if (id < 5)
            {
                id = ++getId;
                name = nm;
                balance = bal;
            }
            else
            {
                throw new Exception("Cannot add new customers, capacity full!");
            }

        }
        double getBalance()
        {
            return this.balance;
        }
        public void deposit(double amt)
        {
            balance += amt;
            Console.WriteLine("Amount of Rs.{0} deposited into Account id '{1}'\nUpdated Balance: {2}\n", amt, this.id,this.getBalance());
        }
        public void display()
        {
            Console.WriteLine("\nAccount Information:\n1.Customer  ID: {0}\n2.Customer name: {1}\n3.Account balance: {2}",this.id,this.name,this.balance);
        }
        public static double payint(Account ob)
        {
            double int_gained = ob.balance * interest_rate;
            ob.balance += int_gained;
            return int_gained;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Account cust1, cust2, cust3, cust4, cust5, cust6;
           // try
            //{
                cust1 = new Account("Suresh", 1000);
                cust2 = new Account("Ramesh", 1000);
                cust3 = new Account("Rohan", 1000);
                cust4 = new Account("Roshan", 1000);
                cust5 = new Account("Rajesh", 1000);
               // cust6 = new Account("Sahil", 1000);
                //cust6.display();
                cust1.display();
                Console.WriteLine("\n");
                cust2.display();
                Console.WriteLine("\n");
                cust3.display();
                Console.WriteLine("\n\n");
                cust3.deposit(10000);
                cust2.deposit(18000);
                Console.WriteLine("Summary:");
                Console.WriteLine("Interest gained: {0}", Account.payint(cust1));
                cust1.display();
                Console.WriteLine("\n");
                Console.WriteLine("Interest gained: {0}", Account.payint(cust2));
                cust2.display();
                Console.WriteLine("\n");
                Console.WriteLine("Interest gained: {0}", Account.payint(cust3));
                cust3.display();
            //}
            //catch { }
        }
    }
}
