using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudMySQL.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace CrudMySQL.Report
{
    public class PdfGenerator
    {
        public static void GenerateClientesPdf(List<Cliente> clientes, string filePath)
        {
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    document.Add(new Paragraph("Relatório de Clientes")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(20));

                    document.Add(new Paragraph("\n"));
                    //tabela de dados
                    Table table = new Table(5, true);

                    //Cabeçalho
                    table.AddHeaderCell("ID");
                    table.AddHeaderCell("Nome");
                    table.AddHeaderCell("E-Mail");
                    table.AddHeaderCell("Telefone");
                    table.AddHeaderCell("Data Cadastro");

                    //Dados
                    foreach (Cliente cliente in clientes)
                    {
                        table.AddCell(cliente.Id.ToString());
                        table.AddCell(cliente.Nome.ToString());
                        table.AddCell(cliente.Email ?? "-");
                        table.AddCell(cliente.Telefone ?? "-");
                        table.AddCell(cliente.DataCadastro.ToString("dd/MM/yyyy HH:mm"));
                    }
                    document.Add(table);

                    //rodapé
                    document.Add(new Paragraph($"Total de clientes: {clientes.Count} "))
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                        .SetFontSize(10);
                }
            }

        }
    }
}
