using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace COMPLETE_FLAT_UI
{



    public partial class FrmLista : Form
    {
        private void FrmLista_Load(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                DGVDatos.DataSource = funcionesUsuario.PA_Usuario_Cargar_Todo();
                DGVDatos.Columns["PASSWORD_USUARIO"].Visible = false;
                DGVDatos.Columns["ID_USUARIO"].Visible = false;
            }
        }
        public FrmLista()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                long Id = Convert.ToInt64(DGVDatos.CurrentRow.Cells["ID_USUARIO"].Value);
                string alias = DGVDatos.CurrentRow.Cells["ALIAS_USUARIO"].Value.ToString();
                string nombre = DGVDatos.CurrentRow.Cells["NOMBRE_USUARIO"].Value.ToString();
                string apellido = DGVDatos.CurrentRow.Cells["APELLIDO_USUARIO"].Value.ToString();
                string contra = DGVDatos.CurrentRow.Cells["PASSWORD_USUARIO"].Value.ToString();
                string rol = DGVDatos.CurrentRow.Cells["ROL_USUARIO"].Value.ToString();
                FrmUsuarios f = new FrmUsuarios();
                f.TxtApellidos.Text = apellido;
                f.TxtContraseña.Text = contra;
                f.TxtNombres.Text = nombre;
                f.TxtUsuario.Text = alias;
                f.CbxRol.Text = rol;
                f.TxtIDUsuario.Text = Id.ToString();
                f.TxtContraseñaBD.Text = contra;
                f.ShowDialog();
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                FrmUsuarios f = new FrmUsuarios();
                f.ShowDialog();
            }
            if (LblTitulo.Text == "Lista de Clientes")
            {
                FrmUsuarios f = new FrmUsuarios();
                f.ShowDialog();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                string usuario = DGVDatos.CurrentRow.Cells["ALIAS_USUARIO"].Value.ToString();
                DialogResult resultado = MessageBox.Show($"Desea eliminar el ID: {usuario}?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (resultado == DialogResult.OK)
                {
                    if (funcionesUsuario.PA_Usuario_Eliminar(usuario))
                    {
                        MessageBox.Show($"Usuario con ID {usuario} Eliminado", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else
                    {
                        MessageBox.Show(funcionesUsuario.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DGVDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {


            if (LblTitulo.Text == "Lista de Usuarios")
            {
                DataTable dt = funcionesUsuario.PA_Usuario_Cargar_Todo();
                dt.Columns.Remove("ID_USUARIO");
                dt.Columns.Remove("PASSWORD_USUARIO");

                if (dt.Rows.Count > 0)
                {
                    // Mostrar EXCEL
                    string ruta = Application.StartupPath + "\\" + "UsuariosExcel.xlsx";
                    if (File.Exists(ruta))
                    {
                        File.Delete(ruta);
                    }

                    FileInfo fileinfo = new FileInfo(ruta);
                    var package = new ExcelPackage(fileinfo);
                    var sheet = package.Workbook.Worksheets.Add("Usuarios");

                    // Encabezados
                    var cellStyle = sheet.Cells[1, 1, 1, 4].Style;
                    cellStyle.Font.Bold = true;
                    sheet.Cells["A1"].Value = "Alias";
                    sheet.Cells["B1"].Value = "Nombre";
                    sheet.Cells["C1"].Value = "Apellido";
                    sheet.Cells["D1"].Value = "Rol";

                    // Llenar datos
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sheet.Cells["A" + (i + 2)].Value = dt.Rows[i]["ALIAS_USUARIO"].ToString();
                        sheet.Cells["B" + (i + 2)].Value = dt.Rows[i]["NOMBRE_USUARIO"].ToString();
                        sheet.Cells["C" + (i + 2)].Value = dt.Rows[i]["APELLIDO_USUARIO"].ToString();
                        sheet.Cells["D" + (i + 2)].Value = dt.Rows[i]["ROL_USUARIO"].ToString();
                    }

                    // Ajustar columnas
                    // AutoFit columns 1 to 4
                    sheet.Column(1).AutoFit();
                    sheet.Column(2).AutoFit();
                    sheet.Column(3).AutoFit();
                    sheet.Column(4).AutoFit();

                    ExcelRange modeloTable = sheet.Cells[1, 1, dt.Rows.Count + 1, dt.Columns.Count];



                    modeloTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modeloTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modeloTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modeloTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                    // Negrilla en encabezados
                    sheet.Cells["A1:D1"].Style.Font.Bold = true;

                    package.Save();
                    Process.Start(ruta);
                }
                else
                {
                    MessageBox.Show("No hay usuarios para mostrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
