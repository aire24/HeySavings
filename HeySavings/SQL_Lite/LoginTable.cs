using System;
using HeySavings.Enums;
using SQLite;
namespace HeySavings.SQL_Lite
{
    public class LoginTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int id { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(25)]

        public string email { get; set; }

        [MaxLength(15)]

        public string password { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }
    }


    public class Budget
    {
        [PrimaryKey, Column("_Id")]

        public int id { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public string budget { get; set; }
    }

    public class MonthlyBudget
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }

        public int userid { get; set; }
        public string monthlybudget { get; set; }
        public DateTime CreatedDate { get; set; }
        public string lastMonthRemaingBudget { get; set; }
    }

    public class Spendings
    {
        [PrimaryKey, AutoIncrement]
        public int spendingId { get; set; }
        public int id { get; set; }
        public string spendingName { get; set; }
        public string spendingDescription { get; set; }
        public string amount { get; set; }
        public int MonthlybudgetId { get; set; }
        public SpendingType type { get; set; }
    }
}
