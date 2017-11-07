using System;
namespace Rubricas_PCL
{
    public class CalificacionCategoria
    {
		public string CategoriaUid { get; set; }
        public string CategoriaName { get; set; }
        public string Uid { get; set; }
        public int Peso { get; set; }

		public CalificacionCategoria()
		{
		}

        public CalificacionCategoria(Categoria categoria)
		{
            CategoriaUid = categoria.Uid;
            CategoriaName = categoria.Name;
            Peso = categoria.Peso;
		}
    }
}
