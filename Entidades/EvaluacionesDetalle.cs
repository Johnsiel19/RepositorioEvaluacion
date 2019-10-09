using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class EvaluacionesDetalle
    {
        [Key]
        public int id { get; set; }
        public int EvaluacionId { get; set; }
        public int CategoriaId { get; set; }
        public decimal Valor { get; set; }
        public decimal Logrados { get; set; }
        public decimal Perdidos { get; set; }

        public EvaluacionesDetalle()
        {
            this.id = 0;
            EvaluacionId = 0;
            CategoriaId = 0;
            Valor = 0;
            Logrados = 0;
            Perdidos = 0;
        }

        public EvaluacionesDetalle(int id, int evaluacionId, int categoriaId, decimal valor, decimal logrados, decimal perdidos)
        {
            this.id = id;
            EvaluacionId = evaluacionId;
            CategoriaId = categoriaId;
            Valor = valor;
            Logrados = logrados;
            Perdidos = perdidos;
        }
    }
}
