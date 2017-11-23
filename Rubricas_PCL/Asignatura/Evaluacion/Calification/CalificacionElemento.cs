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

		public float DeNivel1 { get; set; }
		public float HastaNivel1 { get; set; }

		public float DeNivel2 { get; set; }
		public float HastaNivel2 { get; set; }

		public float DeNivel3 { get; set; }
		public float HastaNivel3 { get; set; }

		public float DeNivel4 { get; set; }
		public float HastaNivel4 { get; set; }

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

            DeNivel1 = elemento.DeNivel1;
            DeNivel2 = elemento.DeNivel2;
            DeNivel3 = elemento.DeNivel3;
            DeNivel4 = elemento.DeNivel4;

			HastaNivel1 = elemento.HastaNivel1;
			HastaNivel2 = elemento.HastaNivel2;
			HastaNivel3 = elemento.HastaNivel3;
			HastaNivel4 = elemento.HastaNivel4;
		}
    }
}
