using System;
namespace Rubricas_PCL
{
    public class CalificacionElemento
    {
		public string Uid { get; set; }

        public int Nivel { get; set; }
        public int Peso { get; set; }
		public string ElementoUid { get; set; }
        public string ElementoName { get; set; }

        public string Nivel1Name { get; set; }
        public string Nivel2Name { get; set; }
        public string Nivel3Name { get; set; }
        public string Nivel4Name { get; set; }

		public CalificacionElemento()
		{
		}

        public CalificacionElemento(int nivel, Elemento elemento)
		{
			Nivel = nivel;
            Peso = elemento.Peso;
            ElementoUid = elemento.Uid;
            ElementoName = elemento.Name;
            Nivel1Name = elemento.Nivel1;
            Nivel2Name = elemento.Nivel2;
            Nivel3Name = elemento.Nivel3;
            Nivel4Name = elemento.Nivel4;
		}
    }
}
