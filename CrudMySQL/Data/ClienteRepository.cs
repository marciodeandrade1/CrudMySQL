using CrudMySQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CrudMySQL.Data
{
    public class ClienteRepository
    {
        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            string query = "SELECT Id, Nome, Email, Telefone, DataCadastro FROM Clientes";

            using (MySqlConnection connection = Database.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new Cliente
                                {
                                    Id = reader.GetInt32("Id"),
                                    Nome = reader.GetString("Nome"),
                                    Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                                    Telefone = reader.IsDBNull("Telefone") ? null : reader.GetString("Telefone"),
                                    DataCadastro = reader.GetDateTime("DataCadastro")
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao obter clientes: " + ex.Message);
                }
            }

            return clientes;
        }
    }
}
