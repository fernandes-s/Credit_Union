using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BIZ
{
    public class Account
    {
        AddToDataBase addToDB = new AddToDataBase();

        public string username { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string accType { get; set; }
        public int accountNumber { get; set; }
        public int sortCode { get; set; }
        public decimal initialBalance { get; set; }
        public decimal overdraftLimit { get; set; }

        public Account(string user, string fn, string sn, string email, string phone, string add1, string add2, string city, string county, string accType, int accountNumber, int sortCode, decimal initBalance, decimal overdraft)
        {
            username = user;
            firstname = fn;
            surname = sn;
            this.email = email;
            this.phone = phone;
            address1 = add1;
            address2 = add2;
            this.city = city;
            this.county = county;
            this.sortCode = sortCode;
            this.accType = accType;
            this.accountNumber = accountNumber;
            initialBalance = initBalance;
            overdraftLimit = overdraft;
        }

        public void CreateAccount()
        {

            addToDB.CreateAccount(username, firstname, surname, email, phone,
                address1, address2, city, county, accType, accountNumber, sortCode, initialBalance,
                overdraftLimit);
        }
    }
}
