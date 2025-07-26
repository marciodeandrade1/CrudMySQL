using MySql.Data.MySqlClient;
using System;

namespace CrudMySQL.Data
{
    public static class Database
    {
        private static string connectionString = "Server=localhost;Database=CrudDemo;Uid=root;Pwd=suasenha;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
