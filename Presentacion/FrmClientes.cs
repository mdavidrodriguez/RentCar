using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Entidades;

namespace Presentacion
{
    public partial class FrmClientes : Form
    {
        private static FrmClientes instacia = null;
        private FrmClientes()
        {
            InitializeComponent();


        }
        public static FrmClientes GetInstancia()
        {
            if (instacia == null || instacia.IsDisposed)
            {
                instacia = new FrmClientes();
            }
            return instacia;
        }

      

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarLista("");
            CargarGrillaClientes("");
 
        }
        void CargarLista(string condicion)
        {
            listaClientes.DataSource = new RepositorioClientes().Todos(condicion);
            listaClientes.DisplayMember = "Nombre";
            listaClientes.ValueMember = "IDC";
            if (listaClientes.Items.Count >= 0)
            {
                listaClientes.SelectedIndex = 0;
                listaClientes.Select();
            }
        }
        void CargarGrillaClientes(string condicion)
        {
            grillaClientes.DataSource = new RepositorioClientes().Todos(condicion);

        }

        private void listaClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = listaClientes.SelectedValue.ToString();
            Buscar(id);    
        }
        void Buscar(string id)
        {
            try
            {
                var cliente = new RepositorioClientes().BuscarID(id);
                ver(cliente);
            }
            catch (Exception)
            {


            }
        }
        void ver(Entidades.Cliente cliente)
        {
            if (cliente == null)
            {
                return;
            }
            txtIdCliente.Text = cliente.IDC;
            txtNombre.Text = cliente.Nombre;
            comboxTipocliente.Text = cliente.TipoCliente;
        }

        private void grillaClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = grillaClientes.Rows[e.RowIndex].Cells[0].Value.ToString();
            Buscar(id);
            listaClientes.SelectedIndex = e.RowIndex;
            this.tabControl1.TabPages[0].Show();
        }

        private void txtCondicion_TextChanged(object sender, EventArgs e)
        {
            string condicion = txtCondicion.Text.Trim();
            CargarGrillaClientes(condicion);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtIdCliente.Text.Length == 0)
            {
                return;
            }
            Eliminar(new RepositorioClientes().BuscarID(txtIdCliente.Text));
        }
        void Eliminar(Cliente cliente)
        {

            MessageBox.Show(new RepositorioClientes().Eliminar(cliente));
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar(txtIdCliente.Text.Trim());

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente(txtIdCliente.Text, txtNombre.Text, comboxTipocliente.Text);
            Actualizar(cliente);
        }
        void Actualizar(Cliente cliente)
        {
            MessageBox.Show(new RepositorioClientes().Actualizar(cliente));
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo(false);
            txtIdCliente.Enabled = true;
            txtIdCliente.Focus();

        }
        void Nuevo(bool soloLectura)
        {
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                    ((TextBox)item).ReadOnly = soloLectura;
                }
            }
        }

        private void txtTipo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente(txtIdCliente.Text, txtNombre.Text, comboxTipocliente.Text);
            Insertar(cliente);
        }

        void Insertar(Cliente cliente)
        {
            MessageBox.Show(new RepositorioClientes().Insertar(cliente));
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (txtIdCliente.Text.Length == 0)
                {
                    return;
                }
                Eliminar(new RepositorioClientes().BuscarID(txtIdCliente.Text));
            }
        }
    }
}