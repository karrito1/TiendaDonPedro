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
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Se pregunta si esta editando o no
            if (TxtIDUsuario.Text.Trim().Length == 0)
            {
                // Inserta nomalmente
                if (TxtUsuario.Text.Trim().Length > 0 && TxtNombres.Text.Trim().Length > 0 && CbxRol.Text.Trim().Length > 0 &&
                TxtContraseña.Text.Trim().Length > 0 && TxtApellidos.Text.Trim().Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt = funcionesUsuario.PA_Usuario_Seleccionar_Uno(TxtUsuario.Text);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Usuario existente, ingrese uno nuevo ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MD5 md5 = MD5.Create();
                        string passhash = funcionesUsuario.GetMd5Hash(md5, TxtContraseña.Text);
                        if (funcionesUsuario.PA_Usuario_Insertar(TxtUsuario.Text, TxtNombres.Text, TxtApellidos.Text, passhash, CbxRol.Text) == true)
                        {
                            MessageBox.Show($"Usuario {TxtNombres.Text} registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Esta editando
                MD5 md5 = MD5.Create();
                string contraseña_actual = TxtContraseñaBD.Text;
                string passhash;
                if (TxtContraseña.Text != TxtContraseñaBD.Text)
                {
                    passhash = funcionesUsuario.GetMd5Hash(md5, TxtContraseña.Text);
                }
                else
                {
                    passhash = contraseña_actual;
                }
                if (funcionesUsuario.PA_Usuario_Editar(Convert.ToInt64(TxtIDUsuario.Text), TxtUsuario.Text, TxtNombres.Text, TxtApellidos.Text, passhash, CbxRol.Text) == true)
                {
                    MessageBox.Show("Usuario Editado", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(funcionesUsuario.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
