using System;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Elemento : INotifyPropertyChanged
	{
        private string uid;
        private string name;
		private string peso;
		private string nivel1;
		private string nivel2;
		private string nivel3;
		private string nivel4;

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

		public string Peso
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

		public string Nivel1
		{
			set
			{
				if (nivel1 != value)
				{
					nivel1 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Nivel1"));
					}
				}
			}
			get => nivel1;
		}

		public string Nivel2
		{
			set
			{
				if (nivel2 != value)
				{
					nivel2 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Nivel2"));
					}
				}
			}
			get => nivel2;
		}

		public string Nivel3
		{
			set
			{
				if (nivel3 != value)
				{
					nivel3 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Nivel3"));
					}
				}
			}
			get => nivel3;
		}

		public string Nivel4
		{
			set
			{
				if (nivel4 != value)
				{
					nivel4 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Nivel4"));
					}
				}
			}
			get => nivel4;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
