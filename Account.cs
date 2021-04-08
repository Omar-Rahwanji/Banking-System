using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arab_Bank
{
    class Account
    {
        private int id;
        private string name;
        private string password;
        private int balance;

        public Account(string n="test", string p="123", int b = 0)
        {
            Name = n;
            Password = p;
            Balance = b;
        }

        public int ID
        {
            private set
            {
                if (value > 0)
                    id = value;
            }
            get
            {
                return id;
            }
        }

        public string Name
        {
            private set
            {
                if (value != "" || value != null)
                    name = value;
            }
            get
            {
                return name;
            }
        }

        public string Password
        {
            set
            {
                if (value != "" || value != null)
                    password = value;
            }
            get
            {
                return password;
            }
        }

        public int Balance
        {
            private set
            {
                if (value > 0)
                    balance = value;
            }
            get
            {
                return balance;
            }
        }
    }
}
