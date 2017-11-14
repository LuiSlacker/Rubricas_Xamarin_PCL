using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rubricas_PCL
{
	public class Rubrica : INotifyPropertyChanged
	{
		private string uid;
        private string name;
        private Dictionary<string, Categoria> _categorias = new Dictionary<string, Categoria>();

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

        public Dictionary<string, Categoria> categorias
		{
            set => _categorias = value;
            get { return _categorias; }
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
