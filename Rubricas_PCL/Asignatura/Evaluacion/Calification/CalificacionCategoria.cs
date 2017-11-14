using System;
using System.Collections.Generic;

namespace Rubricas_PCL
{
    public class CalificacionCategoria
    {
		public string CategoriaUid { get; set; }
        public string CategoriaName { get; set; }
        public string Uid { get; set; }
        public int Peso { get; set; }

        private Dictionary<string, CalificacionElemento> _elementos = new Dictionary<string, CalificacionElemento>();

        public Dictionary<string, CalificacionElemento> elementos
		{
			set => _elementos = value;
			get { return _elementos; }
		}

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
