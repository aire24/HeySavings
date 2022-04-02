using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;

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


    }
}
