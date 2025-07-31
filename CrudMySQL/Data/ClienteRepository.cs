using CrudMySQL.Models;
using MySql.Data.MySqlClient;
using System.Data;


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

        public Cliente GetById(int id)
        {
            Cliente cliente = null;
            string query = "SELECT Id, Nome, Email, Telefone, DataCadastro FROM Clientes WHERE Id = @Id";

            using (MySqlConnection connection = Database.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Cliente
                                {
                                    Id = reader.GetInt32("Id"),
                                    Nome = reader.GetString("Nome"),
                                    Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                                    Telefone = reader.IsDBNull("Telefone") ? null : reader.GetString("Telefone"),
                                    DataCadastro = reader.GetDateTime("DataCadastro")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao obter cliente: " + ex.Message);
                }
            }

            return cliente;
        }

        public int Add(Cliente cliente)
        {
            int newId = 0;
            string query = "INSERT INTO Clientes (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone); SELECT LAST_INSERT_ID();";

            using (MySqlConnection connection = Database.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);

                        newId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao adicionar cliente: " + ex.Message);
                }
            }

            return newId;
        }

        public bool Update(Cliente cliente)
        {
            int rowsAffected = 0;
            string query = "UPDATE Clientes SET Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE Id = @Id";

            using (MySqlConnection connection = Database.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Id", cliente.Id);

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar cliente: " + ex.Message);
                }
            }

            return rowsAffected > 0;
        }

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Clientes WHERE Id = @Id";

            using (MySqlConnection connection = Database.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao excluir cliente: " + ex.Message);
                }
            }

            return rowsAffected > 0;
        }
    }
}
