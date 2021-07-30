using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioTotalED
{
    public class edTransaccion
    {
        // i, id, b = int 
        // v, dt = string
        // d = decimal
        public int idtransaccion { get; set; }

        public string vdocumento { get; set; }

        public string vcelular { get; set; }
        public int idusuario { get; set; }
        public string vnombres { get; set; }
        public string vapellidos { get; set; }
        public int iddivisa { get; set; }
        public int idpromocion { get; set; }
        public int idcuentabancaria { get; set; }
        public int idusuarioadministrador { get; set; }
        public string dtfecha { get; set; }
        public int itipodivisa { get; set; }
        public decimal ddolaresventa { get; set; }
        public decimal ddolarescompra { get; set; }
        public string vhora { get; set; }
        public string vimagen { get; set; }
        public string vimagenruta { get; set; }
        public int itipoenvio { get; set; }
        public int iestado { get; set; }
        public string voperacion { get; set; }
        public int iorigenfondo { get; set; }
        public decimal denvio { get; set; }
        public decimal drecibo { get; set; }
        public int itipocambio { get; set; }
        public int itipotrasaccion { get; set; }
        public decimal digv { get; set; }
        public string vbancoreceptor { get; set; }
        public string dtfecharegistro { get; set; }



    }
}
