using System;
namespace Rubricas_PCL
{
    public class CalificacionElemento
    {
		public int Nivel { get; set; }
		public string ElementoUid { get; set; }
        public string ElementoName { get; set; }

		public CalificacionElemento()
		{
		}

        public CalificacionElemento(int nivel, Elemento elemento)
		{
			Nivel = nivel;
            ElementoUid = elemento.Uid;
            ElementoName = elemento.Name;
		}
    }
}
