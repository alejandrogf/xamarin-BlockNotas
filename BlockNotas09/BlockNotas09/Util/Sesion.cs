using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockNotas09.Util
{
    public class Session
    {
        private Dictionary<String, Object> _session = new Dictionary<String, Object>();
        //Se crea un indice para el diccionario, con un indexer
        public object this[String index]
        {
            get { return _session[index]; }

            set { _session[index] = value; }
        }
    }
}
