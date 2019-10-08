using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//LIBRERIA PARA DARLE MOVIMIENTO A LAVENTANA HAY DOS FORMAS ESTA ES UNA
using System.Runtime.InteropServices;
//LIBRERIAS PARA LA COEXION
using System.Data.SqlClient;
//LIBRERIA PARA RETRASAR UNOS MILISEGUNDS LA EJECUCION DEL PROGRAMA
using System.Threading;
//LIBRERIA PARA CARACTERES ESPECIALES
using System.Text.RegularExpressions;

namespace SoftOne
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //REFERRENCIAS PARA MOVER EL FORM NO FUNCIONO SE USO EL OTRO METODO ESTA ABAJO
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "sendMessage")]
        private extern static void sendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        //--FIN

        SqlConnection con = Conexion.ObtenerConexion();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public void Validar()
        {
            try
            {
                cmd.CommandText = "SELECT LoginName, password FROM USUARIOS WHERE LoginName ='" + txtuser.Text + "' AND password = '" + txtpass.Text + "'";
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblVacioMessage.Visible = false;
                    lblErrorMessage.Visible = false;
                    msgCorrecto("Datos Correctos");
                    //MILISEGUNDOS DE RETRASO
                    Thread.Sleep(100);

                    if (btnlogin.Capture == true)
                    {
                        Menu obj = new Menu();

                        obj.Show();

                    }

                    //  MessageBox.Show("Bimbenido: " + txtuser.Text, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else if (txtuser.Text.Trim() == "" && txtpass.Text.Trim() == "")
                {
                    // MessageBox.Show("Campos vacios");
                    lblErrorMessage.Visible = false;
                    lblCorrectoMssage.Visible = false;
                    msgVacio("Campos Vacios");

                }
                else
                {
                    lblVacioMessage.Visible = false;
                    lblCorrectoMssage.Visible = false;
                    msgError("Datos Inorrectos");
                    /*MessageBox.Show("Datos Incorrectos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);*/
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //MENSAJE DE ERROR EN EL LOGIN 
        public void msgError(string msg)
        {
            lblErrorMessage.ForeColor = Color.Red;
            lblErrorMessage.Text = "     " + msg;
            lblErrorMessage.Visible = true;
        }
        //MENSAJE DE AUTENTICACION CORECTA EN EL LOGIN
        public void msgCorrecto(string msg)
        {
            lblCorrectoMssage.ForeColor = Color.Green;
            lblCorrectoMssage.Text = "     " + msg;
            lblCorrectoMssage.Visible = true;
        }
        //MENSAJE DE ERROR POR CAPOS VACIOS
        public void msgVacio(string msg)
        {
            lblVacioMessage.ForeColor = Color.Red;
            lblVacioMessage.Text = "     " + msg;
            lblVacioMessage.Visible = true;
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
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void Txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "CONTRASEÑA";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        private void PbtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PbtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Codigo Para Mover Ventana
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        //Evento MouseUP
        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            // ReleaseCapture();
            // sendMessage(this.Handle, 0x112, 0xf012, 0);

            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void PictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void PictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void Btnlogin_Click(object sender, EventArgs e)
        {
            Validar();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Registro obj = new Registro();

            obj.Show();
            this.Hide();



        }

        //EVENTO KEY PRESS PARA IMPEDIR QUE INGRESEN CARACTERES ESPECIALES Y NUMEROS
        private void Txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
        }
        //EVENTO KEY PRESS PARA IMPEDIR QUE INGRESEN CARACTERES ESPECIALES
        private void Txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            //LOD DOS METODOS FUNCIONAN PERO ESTE NO DEJA INGRESAR ESPACIOS Y LO DE ARRIVA

            e.Handled = Char.IsPunctuation(e.KeyChar) ||
                     Char.IsSeparator(e.KeyChar) ||
                     Char.IsSymbol(e.KeyChar);
            //e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
    }
}
