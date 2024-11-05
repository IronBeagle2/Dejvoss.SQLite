using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dejvoss.SQLite
{
    public class Database
    {
        public String dbName = String.Empty;

        public Database(String name)
        {
            dbName = name;

            if(!File.Exists("sqlite\\sqlite3.exe"))
            {
                Console.WriteLine("sqlite3.exe not found, make sure it is in sqlite\\sqlite3.exe");
                throw new FileNotFoundException();
            }
        }

        public void test()
        {
            runCommand("create table abc (col0 int)");
        }

        /// <summary>
        /// Creates a new table
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="tableColumns">Columns of the database it will create; key = name, value = type</param>
        public void createTable(String tableName, Dictionary<String, String> tableColumns)
        {
            String cmdText = $"CREATE TABLE IF NOT EXISTS {tableName} (";

            for (int i = 0; i < tableColumns.Count; i++)
            {
                cmdText += $"{tableColumns.ElementAt(i).Key} {tableColumns.ElementAt(i).Value}";
                if (i != tableColumns.Count - 1)
                {
                    cmdText += ", ";
                }
            }

            cmdText += ");";

            runCommand(cmdText);
        }

        /// <summary>
        /// Deletes a table
        /// </summary>
        /// <param name="tableName">Name of the table to delete</param>
        public void deleteTable(String tableName)
        {
            runCommand($"DROP TABLE IF EXISTS {tableName};");
        }

        //TODO CLEAR ENTIRE TABLE
        public void clearTable(String tableName)
        {
            runCommand($"DELTE FROM {tableName};");
        }

        //TODO ADD SINGLE COLUMN TO TABLE
        public void insertIntoTable(String tableName, List<String> items)
        {
            //String cmdText = 
            //foreach(String i in items)


            //command = connection.CreateCommand();
            //command.CommandText = $"INSERT INTO {tableName} ('ID','SUBJECT','TEACHER','ROOM','GRUPPE','COLOR') values ({id},'{subject}','{teacher}', '{room}', '{group}', '{color}');";
            //command.ExecuteNonQuery();
            //command.Dispose();

            runCommand($"INSERT INTO {tableName} VALUES (\'perro\', \'sample\', \'asdasd\'), (\'rico\', \'test text\', \'asdasd\'), (\'setterano\', \'what a load of bullshit\', \'asdasd\');");
        }

        //TODO REMOVE COLUMN(s) FROM TABLE

        //TODO ADD ROW TO TABLE

        //TODO REMOVE ROW FROM TABLE

        /// <summary>
        /// Runs the sqlite exe with a command
        /// </summary>
        /// <param name="cmd">The command</param>
        internal void runCommand(String cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "sqlite\\sqlite3.exe";
            process.StartInfo.Arguments = $"test.db \"{cmd}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(output);
        }
    }
}
