using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClNota
    {
        public virtual long IDNOTA { get; set;}
        public virtual long IDINSCRITO { get; set; }
        public virtual long IDNIVEL { get; set; }
        public virtual long IDTEMA { get; set; }
        public virtual string UNIT_1 { get; set; }
        public virtual string DONE_1 { get; set; }
        public virtual string UNIT_2 { get; set; }
        public virtual string DONE_2 { get; set; }
        public virtual string UNIT_3 { get; set; }
        public virtual string DONE_3 { get; set; }
        public virtual string CHECK_POINT { get; set; }
        public virtual string UNIT_4 { get; set; }
        public virtual string DONE_4 { get; set; }
        public virtual string UNIT_5 { get; set; }
        public virtual string DONE_5 { get; set; }
        public virtual string UNIT_6 { get; set; }
        public virtual string DONE_6 { get; set; }
        public virtual string ESTADO { get; set; }
    }
}
