using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Evaluaciones
    {
        [Key]

        public int EvaluacionId { get; set; }
        public int EstudianteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPerdido { get; set; }
        public virtual List< EvaluacionesDetalle> Detalles { get; set; }

        public Evaluaciones()
        {
            EvaluacionId = 0;
            EstudianteId = 0;
            Fecha = DateTime.Now;
            TotalPerdido = 0;
            Detalles = new List<EvaluacionesDetalle>();
        }
    }
}
