using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rubricas_PCL
{
    public class CalificacionEvaluacion : INotifyPropertyChanged
    {
        private string uid;
        private double nota;


        public string EstudianteUid { get; set; }
        public string EstudianteNombre { get; set; }
        public string EstudianteApellido { get; set; }

        private Dictionary<string, CalificacionCategoria> _categorias = new Dictionary<string, CalificacionCategoria>();

		public CalificacionEvaluacion()
		{
		}
		
		public CalificacionEvaluacion(Estudiante estudiante)
		{
			EstudianteUid = estudiante.Uid;
		}

		public Dictionary<string, CalificacionCategoria> categorias
		{
			set => _categorias = value;
			get { return _categorias; }
		}

        public string Uid
        {
            set
            {
                if (uid != value)
                {
                    uid = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Uid"));
                    }
                }
            }
            get
            {
                return uid;
            }
        }

        public double Nota
        {
            set
            {
                if (nota != value)
                {
                    nota = Math.Round(value, 2);

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Nota"));
                    }
                }
            }
            get
            {
                return nota;
            }
        }

		public event PropertyChangedEventHandler PropertyChanged;
    }
}
