using System;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Elemento : INotifyPropertyChanged
	{
        private string uid;
        private string name;
		private int peso;
		private string nivel1;
		private string nivel2;
		private string nivel3;
		private string nivel4;

        private float deNivel1;
        private float hastaNivel1;

		private float deNivel2;
		private float hastaNivel2;

		private float deNivel3;
		private float hastaNivel3;

		private float deNivel4;
		private float hastaNivel4;


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

		public float DeNivel1
		{
			set
			{
				if (deNivel1 != value)
				{
					deNivel1 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DeNivel1"));
					}
				}
			}
			get => deNivel1;
		}

		public float HastaNivel1
		{
			set
			{
				if (hastaNivel1 != value)
				{
					hastaNivel1 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("HastaNivel1"));
					}
				}
			}
			get => hastaNivel1;
		}

		public float DeNivel2
		{
			set
			{
				if (deNivel2 != value)
				{
					deNivel2 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DeNivel2"));
					}
				}
			}
			get => deNivel2;
		}

		public float HastaNivel2
		{
			set
			{
				if (hastaNivel2 != value)
				{
					hastaNivel2 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("HastaNivel2"));
					}
				}
			}
			get => hastaNivel2;
		}

		public float DeNivel3
		{
			set
			{
				if (deNivel3 != value)
				{
					deNivel3 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DeNivel3"));
					}
				}
			}
			get => deNivel3;
		}

		public float HastaNivel3
		{
			set
			{
				if (hastaNivel3 != value)
				{
					hastaNivel3 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("HastaNivel3"));
					}
				}
			}
			get => hastaNivel3;
		}

		public float DeNivel4
		{
			set
			{
				if (deNivel4 != value)
				{
					deNivel4 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DeNivel4"));
					}
				}
			}
			get => deNivel4;
		}

		public float HastaNivel4
		{
			set
			{
				if (hastaNivel4 != value)
				{
					hastaNivel4 = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("HastaNivel4"));
					}
				}
			}
			get => hastaNivel4;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
