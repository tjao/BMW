﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;

namespace Database
{
    class Database
    {
         // for table UserFinder
         String user="Zijian";
        // for table bandpower
         Queue<double> alpha = new Queue<double>();
         Queue<double> beta = new Queue<double>();
        // for table EEG
         Queue<double> AF3 = new Queue<double>();
         Queue<double> F7 = new Queue<double>();
         Queue<double> F3 = new Queue<double>();
         Queue<double> FC5 = new Queue<double>();
         Queue<double> T7 = new Queue<double>();
         Queue<double> P7 = new Queue<double>();
         Queue<double> O1 = new Queue<double>();
         Queue<double> O2 = new Queue<double>();
         Queue<double> P8 = new Queue<double>();
         Queue<double> T8 = new Queue<double>();
         Queue<double> FC6 = new Queue<double>();
         Queue<double> F4 = new Queue<double>();
         Queue<double> F8 = new Queue<double>();
         Queue<double> AF4 = new Queue<double>();
         Queue<double> TimeStamp = new Queue<double>();
    
         Queue<int> section = new Queue<int>();
         Queue<String> ComputerTime = new Queue<String>();
        void CreateUser()
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=True;");
            // open connecttion to database
            sqlite_conn.Open();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            // Let the SQLiteCommand object know our SQL-Query:

            // need to find the user in the databse table userFinder
            // TODO  create table userFinder
            sqlite_cmd.CommandText = "SELECT * FROM UserFinder where UserName='"+user+"';";
           
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                Boolean data = true;
                try
                {
                    data = sqlite_datareader.Read();
                    if (data)
                    {
                        Console.WriteLine("User " + user + " Existed");
                        Console.WriteLine("User" + sqlite_datareader["UserName"]);
                    }
                }catch (SQLiteException e)
                {
                 Console.WriteLine("User " + user +" Not Existed");
                 data = false;
                }
             sqlite_conn.Close();

            if (!data)
            {
                // We use these three SQLite objects:
               

                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=True;");
                // open connecttion to database
                sqlite_conn.Open();
                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "CREATE TABLE " + user + "_EEG(UserName varchar(20) ,AF3 double , F7 double , F3 double,FC5 double, T7 double,P7 double,O1 double, O2 double,P8 double ,T8 double,FC6 double,F4 double,F8 double,AF4 double,TimeStamp double,section integer, ComputerTime varchar);";
                    sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Creatring Table " + user + "_EEG\n");
                sqlite_cmd.CommandText = "CREATE TABLE " + user + "_BandPower" + "(UserName varchar(20) ,Alpha double, Beta double,TimeStamp double,section integer, ComputerTime varchar);";
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Creatring Table " + user + "_BandPower\n");
                sqlite_cmd.CommandText = "INSERT INTO UserFinder (UserName) VALUES ('" + user+"');";
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Inserting New User " + user + " To UserFinder\n");
            }
            sqlite_conn.Close();
        }

        void CreateUserFinder()
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            // open connecttion to database
            sqlite_conn.Open();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE UserFinder (UserName varchar(50) );";
            try
            {
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("UserFinder Created");
            }catch(SQLiteException e)
            {
                Console.WriteLine("UserFinder Exist");
            }
            sqlite_conn.Close();
        }


        void EEG_Insert(String UserName, double AF3,  double F7 ,  double F3, double FC5,  double T7, double P7, double O1,  double O2, double P8 , double T8, double FC6, double F4, double F8, double AF4, double TimeStamp, int section,  String ComputerTime )
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=True;");
            // open connecttion to database
            sqlite_conn.Open();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO "+user + "_EEG (UserName ,AF3 , F7 , F3,FC5 , T7 ,P7,O1, O2 ,P8  ,T8 ,FC6 ,F4 ,F8 ,AF4 ,TimeStamp ,section , ComputerTime ) VALUES('"  +UserName  +"',"+   AF3  +","  +F7+  ","+F3+","+FC5+","+T7+","+P7+","+O1+","+O2+","+P8+","+FC6+","+F4+","+F8+","+T8+","+AF4+","+TimeStamp+","+section+",'"+ComputerTime+"');" ;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

         void BandPowerInsert(String UserName,double Alpha , double Beta, double TimeStamp, int section, String ComputerTime)
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=True;");
            // open connecttion to database
            sqlite_conn.Open();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO " + user + "_BandPower (UserName  ,Alpha , Beta ,TimeStamp ,section , ComputerTime ) VALUES('"+user+"',"+Alpha+","+Beta+","+TimeStamp+","+section+",'"+ComputerTime+"');";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        
        void Loa_EEGData(String User )
        {
            Console.WriteLine("Start Loading EEG Data");
           // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=True;");
            // open connecttion to database
            sqlite_conn.Open();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM "+ User+"_EEG";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                AF3.Enqueue((double)sqlite_datareader["AF3"]);
                F7.Enqueue((double)sqlite_datareader["F7"]);
                F3.Enqueue((double)sqlite_datareader["F3"]);
                FC5.Enqueue((double)sqlite_datareader["FC5"]);
                T7.Enqueue((double)sqlite_datareader["T7"]); 
                P7.Enqueue((double)sqlite_datareader["P7"]);
                O1.Enqueue((double)sqlite_datareader["O1"]);
                O2.Enqueue((double)sqlite_datareader["O2"]);
                P8.Enqueue((double)sqlite_datareader["P8"]);
                T8.Enqueue((double)sqlite_datareader["T8"]);
                FC6.Enqueue((double)sqlite_datareader["FC6"]);
                F4.Enqueue((double)sqlite_datareader["F4"]);
                F8.Enqueue((double)sqlite_datareader["F8"]);
                AF4.Enqueue((double)sqlite_datareader["AF4"]);
                TimeStamp.Enqueue((double)sqlite_datareader["TimeStamp"]);
            }
            sqlite_conn.Close();
            Console.WriteLine("Finish Loading EEG Data");
        }

        void Load_BandPowerData(String UserName)
        {
            Console.WriteLine("Start Loading BandPower Data");
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
            // open connecttion to database
            sqlite_conn.Open();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM " + UserName + "_BandPower";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                alpha.Enqueue((double)sqlite_datareader["Alpha"]);
                beta.Enqueue((double)sqlite_datareader["Beta"]);
            }
             sqlite_conn.Close();
             Console.WriteLine("Finish Loading BandPower Data\n");
        }

        Database()
        {
           // CreateUserFinder();
        }



        void SetUserName(String username)
        {
            user = username;
        }

     

        static void Main(string[] args)
        {
            Database db = new Database();
         //   db.CreateUserFinder();
            db.CreateUser();
            db.SetUserName("Chen");
            db.CreateUser();
            db.SetUserName("Lion");
            db.CreateUser();
            db.SetUserName("Lion");
            db.CreateUser();
            db.SetUserName("Chen");
            db.CreateUser();
            db.SetUserName("Zijian");
            db.CreateUser();
            db.BandPowerInsert("Zijian",53.3 , 90.2, 12.34, 3, "11:11;11");
           db.BandPowerInsert("Zijian",59.3 , 92.2, 12.34, 3, "11:11;11");
            db.BandPowerInsert("Zijian",70.3 , 90.2, 12.34, 3, "11:11;11");
            db.BandPowerInsert("Zijian",90.3 , 90.2, 12.34, 3, "11:11;11");
            db.BandPowerInsert("Zijian",99.3 , 90.2, 12.34, 3, "11:11;11");
            db.Load_BandPowerData("Zijian");
           foreach(double q  in db.alpha)
               Console.WriteLine(q);
           db.EEG_Insert("Zijian", 10.3F,  10.8F ,   10.3F,  10.3F,  10.3F,  10.3F,  10.3F,   10.3F,  10.3F ,  10.3F,  10.3F,  10.3F,  10.3F,  10.3F,  10.3F, 2,  "11:11:11");
           db.Loa_EEGData("Zijian");
          Console.WriteLine( db.AF3.Peek());
            Console.ReadKey();
        }
    }
}
