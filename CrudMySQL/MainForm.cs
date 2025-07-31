using CrudMySQL.Data;
using CrudMySQL.Models;
using CrudMySQL.Report;
using System.Diagnostics.Eventing.Reader;

namespace CrudMySQL
{
    public partial class MainForm : Form
    {
        private ClienteRepository _clienteRepository;
        private int _clienteId = 0;
        public MainForm()
        {
            InitializeComponent();
            _clienteRepository = new ClienteRepository();
            CarregarCliente();
        }

        private void CarregarCliente()
        {
            try
            {
                List<Cliente> clientes = _clienteRepository.GetAll();
                dgvClientes.DataSource = clientes;
                if (dgvClientes.Columns.Contains("DataCadastro"))
                {
                    dgvClientes.Columns["DataCadastro"].DefaultCellStyle.Format = "dd/MM/yyyyHH:mm";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes" + ex.Message + MessageBoxButtons.OK + MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    Cliente novoCliente = new Cliente
                    {
                        Nome = txtNome.Text,
                        Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text,
                        Telefone = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text
                    };
                    int newId = _clienteRepository.Add(novoCliente);
                    MessageBox.Show($"Cliente adicionado! ID:{newId}", "SUCESSO!!" + MessageBoxButtons.OK + MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarCliente();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar cliente: " + ex.Message + MessageBoxButtons.OK + MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo nome é obrigatório", "Validação" + MessageBoxButtons.OK + MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }
            return true;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                _clienteId = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["Id"].Value);
                Cliente cliente = _clienteRepository.GetById(_clienteId);

                if (cliente != null)
                {
                    txtNome.Text = cliente.Nome;
                    txtEmail.Text = cliente.Email;
                    txtTelefone.Text = cliente.Telefone;
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para editar", "Aviso" +
                    MessageBoxButtons.OK + MessageBoxIcon.Warning);
            }

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() && _clienteId > 0)
            {
                try
                {
                    Cliente clienteAtualizado = new Cliente
                    {
                        Id = _clienteId,
                        Nome = txtNome.Text,
                        Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text,
                        Telefone = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text
                    };
                    bool sucess = _clienteRepository.Update(clienteAtualizado);
                    if (sucess)
                    {
                        MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso" + MessageBoxButtons.OK
                            + MessageBoxIcon.Information);
                        LimparCampos();
                        CarregarCliente();
                        _clienteId = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar cliente: " + ex.Message, "Erro" +
                        MessageBoxButtons.OK + MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para atualizar", "Aviso" + MessageBoxButtons.OK
                    + MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["Id"].Value);

                if (MessageBox.Show($"Tem certeza que deseja excluir este cliente {id}?", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        bool sucesso = _clienteRepository.Delete(id);
                        if (sucesso)
                        {
                            MessageBox.Show("Cliente excluído com sucesso!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregarCliente();
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o cliente.", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir cliente: " + ex.Message, "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (.pdf)|pdf",
                DefaultExt = "pdf",
                FileName = $"Clientes.pdf"
            };
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<Cliente> clientes = _clienteRepository.GetAll();
                    PdfGenerator.GenerateClientesPdf(clientes, saveFileDialog.FileName);

                    MessageBox.Show("PDF gerado com sucesso!", "Sucesso", MessageBoxButtons.OK ,
                        MessageBoxIcon.Information);

                    //Abrir pdf após gerar (opcional)
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao gerar PDF: {ex.Message} ", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
