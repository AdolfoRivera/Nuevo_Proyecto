using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//LIBRERIAS PARA LA COEXION
using System.Data.SqlClient;
//LIBRERIA PARA ATRASAR UN POCO LA EJECUCION
using System.Threading;

namespace SoftOne
{
    public partial class Registro : Form
    {
            
        public Registro()
        {
            InitializeComponent();

            
        }
        SqlConnection con = Conexion.ObtenerConexion();
        SqlCommand cmd = new SqlCommand();

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "USUARIO")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void Txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "USUARIO";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void Txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "CONTRASEÑA")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
            }
        }

        private void Txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "CONTRASEÑA";
                txtpass.ForeColor = Color.DimGray;
            }
        }

        private void TxtAPP_Enter(object sender, EventArgs e)
        {
            if (txtAPP.Text == "APELLIDO PATERNO")
            {
                txtAPP.Text = "";
                txtAPP.ForeColor = Color.LightGray;
            }
        }

        private void TxtAPP_Leave(object sender, EventArgs e)
        {
            if (txtAPP.Text == "")
            {
                txtAPP.Text = "APELLIDO PATERNO";
                txtAPP.ForeColor = Color.DimGray;
            }
        }

        private void TxtAPM_Enter(object sender, EventArgs e)
        {
            if (txtAPM.Text == "APELLIDO MATERNO")
            {
                txtAPM.Text = "";
                txtAPM.ForeColor = Color.LightGray;
            }
        }

        private void TxtAPM_Leave(object sender, EventArgs e)
        {
            if (txtAPM.Text == "")
            {
                txtAPM.Text = "APELLIDO MATERNO";
                txtAPM.ForeColor = Color.DimGray;
            }
        }

        private void Txtpocicion_Enter(object sender, EventArgs e)
        {
            if (txtpocicion.Text == "POSICION")
            {
                txtpocicion.Text = "";
                txtpocicion.ForeColor = Color.LightGray;
            }
        }

        private void Txtpocicion_Leave(object sender, EventArgs e)
        {
            if (txtpocicion.Text == "")
            {
                txtpocicion.Text = "POSICION";
                txtpocicion.ForeColor = Color.DimGray;
            }
        }

        private void Txtemail_Enter(object sender, EventArgs e)
        {
            if (txtemail.Text == "EMAIL")
            {
                txtemail.Text = "";
                txtemail.ForeColor = Color.LightGray;
            }
        }

        private void Txtemail_Leave(object sender, EventArgs e)
        {
            if (txtemail.Text == "")
            {
                txtemail.Text = "EMAIL";
                txtemail.ForeColor = Color.DimGray;
            }
        }
        //INSERTAR A LA BASE DE DATOS NUEVOS USUARIOS 
        public void ins_Usuario(string usuario, string contraseña, string apellido_paterno, string apellido_materno, string pocicion, string email)
        {
        
            //  cnn = new SqlConnection("Data Source=DARCK;Initial Catalog=Tienda;Integrated Security=True");
                con = Conexion.ObtenerConexion();
                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "Ins_User";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoginName", txtuser.Text);
                cmd.Parameters.AddWithValue("@password", txtpass.Text);
                cmd.Parameters.AddWithValue("@FirstName", txtAPP.Text);
                cmd.Parameters.AddWithValue("@LastName", txtAPM.Text);
                cmd.Parameters.AddWithValue("@position", txtpocicion.Text);
                cmd.Parameters.AddWithValue("@Email", txtemail.Text);

                txtuser.Clear();
                txtpass.Clear();
                txtAPP.Clear();
                txtAPM.Clear();
                txtpocicion.Clear();
                txtemail.Clear();

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();              
                    con.Close();
         
                  //MessageBox.Show("Usuario Agregado Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    msgCorrecto("Guardado");
      
                }
                catch (Exception er)
                {
                  //throw;
                    MessageBox.Show(er.Message + MessageBoxIcon.Error);
                }

            }

        private void Btnregistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string cadsql = "SELECT * FROM USUARIOS WHERE LoginName = '" + txtuser.Text + "' ";
                SqlCommand comando = new SqlCommand(cadsql, con);
                con.Open();
                SqlDataReader leer1 = comando.ExecuteReader();

                if (leer1.Read() == true)
                {
                    MessageBox.Show("El Registro ya exciste", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                //VALIDACION CAMBIADA AL FROM
                //else if (textBox1_nombre.Text != " "||textBox_apellido.Text!=" "||textBox_telefono.Text!=" "||textBox_contraseña.Text!=" ")
            else if (txtuser.Text.Trim() == "" || txtpass.Text.Trim() == "" || txtAPP.Text.Trim() == "" || txtAPM.Text.Trim() == "" || txtpocicion.Text.Trim() == "" || txtemail.Text.Trim() == "")

                {
                    MessageBox.Show("No Se Permiten Usuarios En Blanco", "Llenar Registro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    //int telefono = Convert.ToInt32(textBox_telefono.Text);
                    ins_Usuario(txtuser.Text, txtpass.Text, txtAPP.Text, txtAPM.Text, txtpocicion.Text, txtemail.Text);

                    //IF PARA INGRESAR A EL LOGIN SOLO DANDO CLIC EN EL BOTON SIN ENTER
                    if (btnregistrar.Capture == true)
                    {
                        //OCULTAR EL FORM REGISTRO Y AVRIR EL LOGIN
                        this.Hide();
                        Login objLog = new Login();
                        objLog.Show();

                    }                  
                }
                con.Close();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

       
        //MENSAJE DE AUTENTICACION CORECTA EN EL LOGIN
        public void msgCorrecto(string msg)
        {
            lblCorrectoMssage.ForeColor = Color.Green;
            lblCorrectoMssage.Text = "     " + msg;
            lblCorrectoMssage.Visible = true;
        }
      
        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void PbtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PbtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
    }

