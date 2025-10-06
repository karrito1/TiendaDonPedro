using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPLETE_FLAT_UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (TxtContraseña.PasswordChar == '*')
            {
                TxtContraseña.PasswordChar = '\0';
            }
            else
            {
                TxtContraseña.PasswordChar = '*';
            }
            TxtContraseña.Focus();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta¡¡", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta¡¡", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            MD5 md5Hash = MD5.Create();
            string hash = funcionesUsuario.GetMd5Hash(md5Hash,TxtContraseña.Text);
            DataTable dt = funcionesUsuario.PA_Usuario_Seleccionar_Uno(TxtUsuario.Text);
            if (dt.Rows.Count > 0)
            {
                if (hash == dt.Rows[0]["PASSWORD_USUARIO"].ToString())
                {
                    this.Hide();
                    FrmMenuPrincipal f = new FrmMenuPrincipal();

                    f.Lbl_Usuario_Nombre.Text = dt.Rows[0]["NOMBRE_USUARIO"].ToString();
                    f.Lbl_Usuario_Apellido.Text = dt.Rows[0]["APELLIDO_USUARIO"].ToString();
                    if (dt.Rows[0]["ROL_USUARIO"].ToString() != "Administrador")
                    {
                        f.BtnUsuarios.Enabled = false;
                    }
                        f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Usuario no existente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            TxtUsuario.Focus();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
