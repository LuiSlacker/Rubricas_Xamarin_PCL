using System;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Estudiante : INotifyPropertyChanged
	{
		private string uid;
        private string name;
		private string apellido;

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

		public string Apellido
		{
			set
			{
				if (apellido != value)
				{
					apellido = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Apellido"));
					}
				}
			}
			get => apellido;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
	
}
