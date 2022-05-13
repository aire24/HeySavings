using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;
using System;

namespace HeySavings.SQL_Lite
{
    public class SqlHelper
    {
        static object locker = new object();
        SQLiteConnection database;
        public SqlHelper()
        {
            database = GetConnection();

            // create the tables  
            database.CreateTable<LoginTable>();
            database.CreateTable<Budget>();
            database.CreateTable<MonthlyBudget>();
            database.CreateTable<Spendings>();
        }


        public SQLite.SQLiteConnection GetConnection()
        {
            SQLiteConnection sqlitConnection;
            var sqliteFilename = "heysavings.db";
            IFolder folder = FileSystem.Current.LocalStorage;
            string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
            sqlitConnection = new SQLite.SQLiteConnection(path);
            return sqlitConnection;
        }


        public IEnumerable<LoginTable> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<LoginTable>() select i).ToList();
            }
        }

        public MonthlyBudget getMonthlyBudget(int id)
        {
            DateTime now = DateTime.Now;
            DateTime start = new DateTime(now.Year, now.Month, 1);
            DateTime dtend = start.AddMonths(1).AddDays(-1);
            return database.Table<MonthlyBudget>().FirstOrDefault(x => x.userid == id && x.CreatedDate >= start && x.CreatedDate <= dtend);
        }

        public int updateMonthlyBudget(MonthlyBudget item)
        {
            lock (locker)
            {
                return database.Update(item);

            }
        }

        public MonthlyBudget getPreviousMonthlyBudget(DateTime dtstart)
        {
            DateTime start = new DateTime(dtstart.Year, dtstart.Month, 1);
            DateTime dtend = start.AddMonths(1).AddDays(-1);
            return database.Table<MonthlyBudget>().FirstOrDefault(x => x.userid == App.login.id && x.CreatedDate >= start && x.CreatedDate <= dtend);
        }

        public double getPreviousMonthRemainingBudget()
        {
            DateTime now = DateTime.Now;
            DateTime previousmonth = new DateTime(now.Year, now.Month, 1).AddDays(-1);
            var data = database.Table<MonthlyBudget>().ToList();

            MonthlyBudget budget = data.FirstOrDefault(x => x.userid == App.login.id && x.CreatedDate.Month == previousmonth.Month);

            if (budget == null) return 0;
            List<Spendings> lstSpending = getMonthAllSpendings(App.login.id, budget.id);
            double remaining = (Convert.ToDouble(budget.monthlybudget) - lstSpending.Sum(x => Convert.ToDouble(x.amount)));
            return remaining;
        }

        public int createMonthlyBudget(bool isprevious = false)
        {
            string remainbudget = getPreviousMonthRemainingBudget().ToString();
            MonthlyBudget mb = new MonthlyBudget()
            {
                CreatedDate = DateTime.Now,
                monthlybudget = (Convert.ToDouble(getBudget(App.login.id).budget)+Convert.ToDouble(remainbudget)).ToString(),
                userid = App.login.id,
                lastMonthRemaingBudget  = remainbudget
            };

            if(isprevious)
            mb.CreatedDate = mb.CreatedDate.AddMonths(-1);
            lock (locker)
            {
                return database.Insert(mb);
            }

        }


        



        public LoginTable GetItem(string userName, string Password)
        {
            lock (locker)
            {
                return database.Table<LoginTable>().FirstOrDefault(x => x.email == userName && x.password == Password);
            }
        }


        public int SaveItem(LoginTable item)
        {
            lock (locker)
            {
                if (item.id != 0)
                {
                    //Update Item  
                    database.Update(item);
                    return item.id;
                }
                else
                {
                    //Insert item  
                    return database.Insert(item);
                }
            }
        }


        public Budget GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<Budget>().FirstOrDefault(x => x.id ==id);
            }
        }


        public int SaveBudget(Budget item)
        {
            lock (locker)
            {
                //Insert item  
                return database.Insert(item);
                
            }
        }

        public int UpdateBudget(Budget item)
        {
            var monthlybudget = getMonthlyBudget(App.login.id);
            monthlybudget.monthlybudget = (Convert.ToDouble(monthlybudget.lastMonthRemaingBudget) + Convert.ToDouble(item.budget)).ToString();
            updateMonthlyBudget(monthlybudget);
            lock (locker)
            {
                return database.Update(item);

            }
        }



        public Budget getBudget(int id)
        {
            lock (locker)
            {
                return database.Table<Budget>().FirstOrDefault(x => x.id == id);

            }
        }

        public int AddSpendng(Spendings sp)
        {
            lock (locker)
            {
                return database.Insert(sp);
            }
        }

        public int UpdateSpending(Spendings sp)
        {
            lock (locker)
            {
                return database.Update(sp);
            }
        }


        public int deleteSpending(Spendings sp)
        {
            lock (locker)
            {
                return database.Delete<Spendings>(sp.spendingId);

            }
        }

        public List<Spendings> getAllSpendings(int Userid)
        {
            lock (locker)
            {
                return database.Table<Spendings>().Where(x => x.id == Userid).ToList();

            }
        }

        public List<Spendings> getMonthAllSpendings(int Userid, int monthid)
        {
            lock (locker)
            {
                var data = database.Table<Spendings>().Where(x => x.id == Userid && x.MonthlybudgetId == monthid).ToList();
                return data;
            }
        }


    }
}
