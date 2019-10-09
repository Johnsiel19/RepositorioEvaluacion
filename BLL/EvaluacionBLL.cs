using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades;
using System.Data.Entity;

namespace BLL
{
    public  class EvaluacionBLL
    {
        public static bool Modificar(Evaluaciones evaluacion)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Estudiantes> dbE = new RepositorioBase<Estudiantes>();


            try
            {
                var anterior = new RepositorioBase<Evaluaciones>().Buscar(evaluacion.EvaluacionId);
                var estudiante = dbE.Buscar(evaluacion.EstudianteId);

                estudiante.PuntosPerdidos = evaluacion.TotalPerdido;

                foreach (var item in anterior.Detalles)
                {
                    if (!evaluacion.Detalles.Any(A => A.id == item.id))
                    {
                        db.Entry(item).State = EntityState.Deleted;

                    }

                }

                foreach (var item in evaluacion.Detalles)
                {
                    if (item.id== 0)
                    {

                        db.Entry(item).State = EntityState.Added;
                    }

                    else
                        db.Entry(item).State = EntityState.Modified;
                }


          
                dbE.Modificar(estudiante);

                db.Entry(evaluacion).State = EntityState.Modified;

                paso = db.SaveChanges() > 0;


            }
            catch (Exception)
            {
                throw;
            }


            return paso;
        }


        public static bool Guardar(Evaluaciones evaluacion)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                RepositorioBase<Estudiantes> dbEst = new RepositorioBase<Estudiantes>(new DAL.Contexto());
                RepositorioBase<Categorias> Cate = new RepositorioBase<Categorias>(new DAL.Contexto());
                RepositorioBase<EvaluacionesDetalle> dCate = new RepositorioBase<EvaluacionesDetalle>(new DAL.Contexto());

                if (db.Evaluaciones.Add(evaluacion) != null)
                {
                    var estudiante = dbEst.Buscar(evaluacion.EstudianteId);

                    
                    estudiante.PuntosPerdidos += evaluacion.TotalPerdido;
                    paso = db.SaveChanges() > 0;
                    dbEst.Modificar(estudiante);
                }
            

            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Estudiantes> dbEst = new RepositorioBase<Estudiantes>(new DAL.Contexto());
            try
            {
                var evaluaciones = db.Evaluaciones.Find(id);
                var estudiante = dbEst.Buscar(evaluaciones.EstudianteId);
                estudiante.PuntosPerdidos =0;
                dbEst.Modificar(estudiante);

                db.Entry(evaluaciones).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }


    }
}
