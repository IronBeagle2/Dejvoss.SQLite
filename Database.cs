using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Deletes (drops) a table; equivalent is deleteTable()
        /// </summary>
        /// <param name="tableName">Name of the table to delete</param>
        public void dropTable(String tableName)
        {
            runCommand($"DROP TABLE IF EXISTS {tableName};");
        }
        /// <summary>
        /// Drops (deletes) a table; equivalent is dropTable()
        /// </summary>
        /// <param name="tableName">Name of the table to delete</param>
        public Action<String> deleteTable => dropTable;

        //TODO CLEAR ENTIRE TABLE
        public void clearTable(String tableName)
        {
            runCommand($"DELTE FROM {tableName};");
        }

        //TODO ADD SINGLE COLUMN TO TABLE
        public void addToTable(String tableName, List<String> items)
        {
            //String cmdText = 
            //foreach(String i in items)


            //command = connection.CreateCommand();
            //command.CommandText = $"INSERT INTO {tableName} ('ID','SUBJECT','TEACHER','ROOM','GRUPPE','COLOR') values ({id},'{subject}','{teacher}', '{room}', '{group}', '{color}');";
            //command.ExecuteNonQuery();
            //command.Dispose();

            runCommand("INSERT INTO table145 (\'test\') VALUES (\'perro\'), (\'rico\'), (\'setterano\');");
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
            process.StartInfo.FileName = Environment.CurrentDirectory + "\\sqlite\\sqlite3.exe";
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
