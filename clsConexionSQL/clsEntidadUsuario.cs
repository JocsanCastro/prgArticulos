using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class clsEntidadUsuario
    {

        //Region de los atributos
        #region Atributos
        private string codigo;
        private string clave;
        private string perfil;
        private int estado;
        #endregion

        //Region de los set y get
        #region SetyGet
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
        public void setEstado(int estado)
        {
            this.estado = estado;
        }
        public int getEstado()
        {
            return this.estado;
        }
        #endregion

        //region del constructor
        #region Constructor
        public clsEntidadUsuario()
        {
            this.clave = "";
            this.codigo = "";
            this.perfil = "";
            this.estado = 0;
        }        
        #endregion
    }

}
