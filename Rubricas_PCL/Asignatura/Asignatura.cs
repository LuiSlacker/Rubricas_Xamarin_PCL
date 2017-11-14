using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rubricas_PCL
{
    public class Asignatura : INotifyPropertyChanged
    {
    	private string uid;
    	private string name;
    	private string number;
        private Dictionary<string, Estudiante> _estudiantes = new Dictionary<string, Estudiante>();
        private Dictionary<string, Evaluacion> _evaluaciones = new Dictionary<string, Evaluacion>();

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

    	public string Number
    	{
    		set
    		{
    			if (number != value)
    			{
    				number = value;
    				if (PropertyChanged != null)
    				{
    					PropertyChanged(this, new PropertyChangedEventArgs("Number"));
    				}
    			}
    		}
    		get => number;
    	}

        public Dictionary<string, Estudiante> estudiantes
		{
            set => _estudiantes = value;
            get { return _estudiantes; }
		}

        public Dictionary<string, Evaluacion> evaluaciones
		{
            set => _evaluaciones = value;
            get { return _evaluaciones; }
		}

    	public event PropertyChangedEventHandler PropertyChanged;
    }
}