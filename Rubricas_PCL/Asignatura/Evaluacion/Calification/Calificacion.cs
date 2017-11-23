using System;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Calificacion : INotifyPropertyChanged
	{
		private string uid;
		private double nota;
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

		public double Nota
		{
			set
			{
				if (nota != value)
				{
					nota = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Nota"));
					}
				}
			}
			get => nota;
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
