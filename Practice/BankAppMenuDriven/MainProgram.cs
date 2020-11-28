using System;

namespace BankAccount
{
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public delegate void notifyEvent(string op, int cid, double amt, double bal);
    public class notify
    {
        public void sendEmail(string op, int cid, double amt, double bal)
        {
            if (op.Equals("withdraw"))
            {
                Console.WriteLine("Email to customer: \n Amount of Rs. " + amt + " withdrawn from account id: " + cid + "\n Current balance in Account: Rs. " + bal);
            }
            else if (op.Equals("deposit"))
            {
                Console.WriteLine("Email to customer: \n Amount of Rs. " + amt + " deposited to account id: " + cid + "\n Current balance in Account: Rs. " + bal);
            }
        }
        public void sendSMS(string op, int cid, double amt, double bal)
        {
            if (op.Equals("withdraw"))
            {
                Console.WriteLine("SMS to customer: \n Amount of Rs. " + amt + " withdrawn from account id: " + cid + "\n Current balance in Account: Rs. " + bal);
            }
            else if (op.Equals("deposit"))
            {
                Console.WriteLine("SMS to customer: \n Amount of Rs. " + amt + " deposited to account id: " + cid + "\n Current balance in Account: Rs. " + bal);
            }
        }

    }
    public abstract class Account
    {
        static protected int minBal = 1000;
        string acc_type;
        static int bankID = 1200;
        static int getId = 0;
        protected int id
        { get; }
        string name;

        public event notifyEvent sendNotify;
        public void OnAccountOp(string op, int cid, double amt, double bal)
        {
            if (sendNotify != null)
            {
                sendNotify(op, cid, amt, bal);
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length < 15)
                {
                    this.name = value;
                }
                else
                {
                    throw new Exception("Length cannot be of more than 15 characters");
                }
            }
        }
        protected double balance
        {
            get;
            set;
        }
        static Account()
        {
            Console.WriteLine("Liberty Bank");
        }
        public Account()
        {

        }
        public Account(string nm, double bal, string type)
        {
            if (getId < 5)
            {
                id = bankID + (++getId);
                name = nm;
                balance = bal;
                acc_type = type;
            }
            else
            {
                throw new Exception("Cannot add new customers, capacity full!");
            }

        }


        public abstract void withdraw(double amt);
        public void deposit(double amt)
        {
            if (amt > 0)
            {
                balance += amt;
                OnAccountOp("deposit", this.id, amt, this.balance);
                //Console.WriteLine("\nAmount of Rs.{0} deposited into Account id '{1}'\nUpdated Balance: {2}\n", amt, this.id, this.balance);
            }
            else
            {
                throw new Exception("Deposit amount cannot be less than 0");
            }
        }

        public void display()
        {
            Console.WriteLine("\nAccount Information:\n1.Customer  ID: {0}\n2.Customer name: {1}\n3.Account balance: {2}\n4.Account Type: {3}", this.id, this.name, this.balance, this.acc_type);
        }
    }
    public class savings : Account
    {
        public savings() : base()
        {
        }
        public savings(string nm, double bal) : base(nm, bal, "savings")
        {

        }
        public override void withdraw(double amt)
        {
            if (amt <= (this.balance - minBal))
            {
                this.balance -= amt;
                OnAccountOp("withdraw", this.id, amt, this.balance);
            }
            else
            {
                throw new Exception("Insufficient account balance.");
            }
        }
    }
    public class current : Account
    {
        public current() { }
        public current(string nm, double bal) : base(nm, bal, "current")
        {

        }
        public override void withdraw(double amt)
        {
            
                this.balance -= amt;
                OnAccountOp("withdraw", this.id, amt, this.balance);
            
        }
    }
    class program
    {

        public static Account makeAccount(Account ac)
        {
            Account acc;
            acc = ac;
            return acc;
        }
        
        static void Main(String[] args)
        {
            try
            {

                List<Account> cust = new List<Account>();
                notify ntfy = new notify();
                int ch;
                
                do
                {

                    Console.WriteLine("::Menu::");
                    Console.WriteLine("1.Add new savings account\n2.Add new current account\n3.Deposit\n4.Withdraw\n5.Display all Customers data\n6.Exit\n\nSelect Option:");
                    ch = int.Parse(Console.ReadLine());
                    switch (ch)
                    {
                        case 1:
                            string name; double bal;
                            Console.WriteLine("Enter name:");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter opening balance:");
                            double.TryParse(Console.ReadLine(), out bal);
                            cust.Add(new savings(name, bal));
                            cust[cust.Count - 1].sendNotify += ntfy.sendEmail;
                            cust[cust.Count - 1].sendNotify += ntfy.sendSMS;
                            Console.WriteLine("Saving Account Opened");
                            break;
                        case 2:
                            Console.WriteLine("Enter name:");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter opening balance:");
                            double.TryParse(Console.ReadLine(), out bal);
                            cust.Add(new current(name, bal));
                            cust[cust.Count - 1].sendNotify += ntfy.sendEmail;
                            cust[cust.Count - 1].sendNotify += ntfy.sendSMS;
                            Console.WriteLine("Current Account Opened");
                            break;
                        case 3:
                            Console.WriteLine("Enter Account holder's name:");
                            name = Console.ReadLine();
                            foreach (Account a in cust)
                            {
                                if (a.Name.Equals(name))
                                {
                                    Console.WriteLine("Enter Amount to deposit:");
                                    double amt = double.Parse(Console.ReadLine());
                                    a.deposit(amt);
                                    Console.WriteLine("Rs. {0} deposited.", amt);
                                    break;
                                }

                            }
                            Console.WriteLine("Customer not found");
                            break;
                        case 4:
                            Console.WriteLine("Enter Account holder's name:");
                            name = Console.ReadLine();
                            foreach (Account a in cust)
                            {
                                if (a.Name.Equals(name))
                                {
                                    Console.WriteLine("Enter Amount to withdraw:");
                                    double amt = double.Parse(Console.ReadLine());
                                    a.withdraw(amt);
                                    Console.WriteLine("Rs. {0} deposited.", amt);
                                    break;
                                }

                            }
                            Console.WriteLine("Customer not found");
                            break;
                        case 5:
                            foreach (Account i in cust)
                            {
                                i.display();
                            }
                            break;
                        case 6:
                            
                            break;

                        default:
                            Console.WriteLine("Please select a valid option!");
                            break;

                    }
                } while (ch != 6);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
