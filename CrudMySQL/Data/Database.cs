using MySql.Data.MySqlClient;
using System;

namespace CrudMySQL.Data
{
    public static class Database
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=CrudDemo;Uid=root;Pwd=suasenha;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
