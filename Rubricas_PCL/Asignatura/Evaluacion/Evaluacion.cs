using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Evaluacion : INotifyPropertyChanged
	{
		private string uid;
        private string name;
		private string rubrica_uid;

        private Dictionary<string, CalificacionEvaluacion> _calificaciones = new Dictionary<string, CalificacionEvaluacion>();

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
		
        public string Name
		{
			set
			{
				if (name != value)
				{
					name = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Name"));
					}
				}
			}
			get => name;
		}

		public string RubricaUid
		{
			set
			{
				if (rubrica_uid != value)
				{
					rubrica_uid = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("RubricaUid"));
					}
				}
			}
			get
			{
				return rubrica_uid;
			}
		}

        public Dictionary<string, CalificacionEvaluacion> calificaciones
		{
            set => _calificaciones = value;
			get { return _calificaciones; }
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
