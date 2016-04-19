using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Llamado de las referencias propias del proyecto
using System.Data.SqlClient;
using Modelo;
using Controlador;

namespace prgArticulos
{
    public partial class frmAcceso : Form
    {
        #region Atributos
        clsConexionSQL conexion;
        clsEntidadUsuario pEntidadUsuario;
        clsUsuario usuario;
        SqlDataReader dtrUsuario;//para el retorno de las tuplas
        int intContador = 0;
        #endregion

        //inicializamos los atributos que utilizaremos en toda la ventana
        public frmAcceso()
        {
            conexion = new clsConexionSQL();
            pEntidadUsuario = new clsEntidadUsuario();
            usuario = new clsUsuario();
            InitializeComponent();
        }

        private void frmAcceso_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)(Keys.Enter))
            {
                //El evento focus lo que permite es trasladar el cursos del mouse al objetivo indicado
                this.txtClave.Focus();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (mValidarDatos() == true)
                {
                    this.btnIngresar.Enabled = true;
                }
            }
        }//fin del key press de clave

        #region Metodos
        //este metodo permite verificar la existencia del usuario segun el codigo y la clave
        private Boolean mValidarDatos()
        {
            if (intContador <= 2)
            {
                //llenado de losa atributos de la bd(para conectarme con el servidor)
                conexion.setCodigo("admEstudiante");
                conexion.setClave("123");

                //llenado de los atributos de la clase entidadUsuario(para conectarme con la bd)
                pEntidadUsuario.setCodigo(this.txtCodigo.Text.Trim());
                pEntidadUsuario.setClave(this.txtClave.Text.Trim());

                //consulamos si el usuario existe
                dtrUsuario = usuario.mConsultarUsurio(conexion, pEntidadUsuario);

                //evaluo si retorna tuplas o datos
                if (dtrUsuario != null)
                {
                    if (dtrUsuario.Read())
                    {
                        pEntidadUsuario.setPerfil(dtrUsuario.GetString(2));//para obtener el perfil den la posicion 2 de la tabla en la bd
                        pEntidadUsuario.setEstado(dtrUsuario.GetInt32(3));

                        if (pEntidadUsuario.getEstado() == 0)
                        {
                            this.btnIngresar.Enabled = true;
                            return true;

                        } else {

                            MessageBox.Show("El usuario esta bloqueado", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return false;

                        }//verifica el estado

                    } else {

                        MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }//fin del if del read

                } else {

                    MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }//fin del if del null

            } else {

                MessageBox.Show("Usted digito su usuario 3 veces de forma erronea","Usuario Bloqueado",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;

            }//fin del if del contador
        }//fin del metodo mValidar

        #endregion

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            mdiMenu menu = new mdiMenu(conexion);
            menu.Show();
        }
    }
}
