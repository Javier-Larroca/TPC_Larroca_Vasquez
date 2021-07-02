﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MedicoNegocio
    {
        private AccesoDatos conexion = new AccesoDatos();
        public List<Medico> listarMedicos()
        {
            List<Medico> listaDeMedicos = new List<Medico>();
            try
            {
                conexion.setearConsulta("SELECT ID, NOMBRE, APELLIDO, CONTACTO, MATRICULA, FECHA_ALTA FROM MEDICOS");
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Medico backup = new Medico();

                    //Cargamos objeto utilizando Medico backup
                    backup.Id = (int)conexion.Lector["ID"];
                    backup.Nombre = (String)conexion.Lector["NOMBRE"];
                    backup.Apellido = (String)conexion.Lector["APELLIDO"];
                    backup.Mail = (String)conexion.Lector["CONTACTO"];
                    backup.Matricula = (int)conexion.Lector["MATRICULA"];
                    backup.Alta = (DateTime)conexion.Lector["FECHA_ALTA"];

                    listaDeMedicos.Add(backup);
                }
                conexion.cerrarConexion();

                foreach (Medico medico in listaDeMedicos){
                    medico.Especialidades = especialidadPorMedico(medico.Id);
                }

                return listaDeMedicos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public List<Especialidad> especialidadPorMedico(int id)
        {
            List<Especialidad> EspecialidadesMedico = new List<Especialidad>();
            try
            {
                //Agrego esta forma de ejecutar un procedimiento almacenado que nos devuelve
                //datos, en caso de usarlo en algún bucle usar en Finally conexion.limpiarParametros() ya que acumula.

                //conexion.setearProcedimientoAlmacenado("pEspecialidadesPorMedico");
                //conexion.agregarParametro("@idMedico", id);
                //conexion.ejecutarProcedimientoAlmacenado(true);

                //Una forma de setear una consulta rapida, aca consulto a la vista creada.
                conexion.setearConsulta(string.Format("SELECT DESCRIPCION FROM vEspecialidadesPorMedico WHERE IDMEDICO = {0}", id));
                conexion.ejecutarConsultaLectura();

                while (conexion.Lector.Read())
                {
                    Especialidad backup = new Especialidad((String)conexion.Lector["DESCRIPCION"]);
                    EspecialidadesMedico.Add(backup);
                }

                return EspecialidadesMedico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
    }
}
