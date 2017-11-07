using System;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Categoria : INotifyPropertyChanged
	{
		private string uid;
        private string name;
		private int peso;

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

		public int Peso
		{
			set
			{
				if (peso != value)
				{
					peso = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Peso"));
					}
				}
			}
			get => peso;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
