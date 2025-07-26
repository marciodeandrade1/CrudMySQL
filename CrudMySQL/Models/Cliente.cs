using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudMySQL.Models
{
    public class Cliente
    {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public DateTime DataCadastro { get; set; }

            public Cliente()
            {
                DataCadastro = DateTime.Now;
            }

            public Cliente(string nome, string email, string telefone)
            {
                Nome = nome;
                Email = email;
                Telefone = telefone;
                DataCadastro = DateTime.Now;
            }
        }
}

