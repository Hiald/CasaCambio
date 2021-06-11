using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioTotalED
{
    public class edDivisa
    {
        // i, id, b = int 
        // v, dt = string
        // d = decimal
        public int iddivisa { get; set; }
        public int idusuario { get; set; }
        public int itipobusqueda { get; set; }
        public decimal dmonto { get; set; }
        public decimal dsolesventa { get; set; }
        public decimal dsolescompra { get; set; }
        public decimal ddolaresventa { get; set; }
        public decimal ddolarescompra { get; set; }
        public int itipopromocion { get; set; }
        public string dtfecha { get; set; }
        public string vhora { get; set; }
        public string dtfecharegistro { get; set; }
        public string dtfechamodificacion { get; set; }
        public string horamodificacion { get; set; }



    }
}
