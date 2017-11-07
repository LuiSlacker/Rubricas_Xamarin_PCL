using System;
using System.Collections.Generic;

namespace Rubricas_PCL
{
    public class CalificacionEvaluacionPlain
    {
		public string EstudianteUid { get; set; }
        public float Nota { get; set; }
        private Dictionary<string, CalificacionCategoria> _categorias = new Dictionary<string, CalificacionCategoria>();

		public CalificacionEvaluacionPlain()
		{
		}

        public Dictionary<string, CalificacionCategoria> categorias
        {
            set => _categorias = value;
            get { return _categorias; }
        }

    }
}
