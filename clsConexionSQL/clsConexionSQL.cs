using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;//Retornar de system de windows el nombre de la maquina
using System.Data.SqlClient;//para los accesos a la base de datos(IMEC)

namespace Modelo
{
    public class clsConexionSQL
    {
        //area de declaracion de variable
        #region 
        private string codigo;
        private string clave;
        private string perfil;
        private string baseDatos;

        private SqlConnection conexion ; //Guardar la cadena de conexion del usuario con la BD
        private SqlCommand comando;//permite ejecutar los IMEC

        #endregion

        //Establecemos el metodo inicial
        #region Constructores
        public clsConexionSQL()
        {
            this.codigo = "";
            this.clave = "";
            this.perfil = "";
            this.baseDatos = "BDEstudiantes";
        }

        #endregion

        //Propiedades de escritura y lectura
        #region GetySet
        public void setCodigo(string codigo)
        {
            this.codigo = codigo.Trim();
        }
        public string getCodigo()
        {
            return this.codigo;
        }

        public void setClave(string clave)
        {
            this.clave = clave.Trim();
        }
        public string getClave()
        {
            return this.clave;
        }

        public void setPerfil(string perfil)
        {
            this.perfil = perfil.Trim();
        }
        public string getPerfil()
        {
            return this.perfil;
        }

        #endregion

        //Metodos para la conexion con la base de datos
        #region Metodos
        //este metodo permitira ejecutar los select
        public SqlDataReader mSeleccionar(string strSentencia, clsConexionSQL cone)
        {
            try
            {
                if (mConectar(cone))
                {
                    comando = new SqlCommand(strSentencia, conexion);
                    comando.CommandType = System.Data.CommandType.Text;
                    return comando.ExecuteReader();//el executeReader ejecuta solo select
                }
                else
                    return null;
            } catch
            {
                return null;
            }
        }//fin de mSeleccionar


        //este metodo permitira ejecutar los insert, update y delete
        public Boolean mEjecutar(string strSentencia,clsConexionSQL cone)
        {
            try
            {
                if (mConectar(cone))
                {
                    comando = new SqlCommand(strSentencia, conexion);
                    comando.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            } catch
            {
                return false;
            }
        }//fin de mEjecutar

        //este metodo nos permite abrir y conectarnos con la base de datos
        public Boolean mConectar(clsConexionSQL cone)
        {
            try
            {
                conexion = new SqlConnection();
                conexion.ConnectionString = "user id='" + cone.getCodigo() + "'; password='" + cone.getClave() + "'; Data Source='" + mNomServidor() + "'; Initial Catalog='" + this.baseDatos + "'";
                conexion.Open();
                return true;
            } catch
            {
                return false;
            }

        }//fin mConectar

        //metodo para obtener el nombre de la maquina
        public string mNomServidor()
        {
            return Dns.GetHostName();
        }//fin mNomServidor


        #endregion

    }//fin clase clsConexionSQL
}//fin namespace
