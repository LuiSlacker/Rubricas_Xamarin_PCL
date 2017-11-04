using System;
namespace Rubricas_PCL
{
    public class CalificacionElemento
    {
		public int nivel { get; set; }
		public string elementoUid { get; set; }

        public CalificacionElemento(int _nivel, string _elementoUid)
		{
			nivel = _nivel;
            elementoUid = _elementoUid;
		}
    }
}
