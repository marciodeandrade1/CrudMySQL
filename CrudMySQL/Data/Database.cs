﻿using MySql.Data.MySqlClient;

namespace CrudMySQL.Data
{
    public static class Database
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=CrudDemo;Uid=root;Pwd=Info@2025;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
