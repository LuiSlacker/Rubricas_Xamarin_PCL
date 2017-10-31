﻿using System;
using System.ComponentModel;

namespace Rubricas_PCL
{
    public class Asignatura : INotifyPropertyChanged
    {
    	private string uid;
    	private string name;
    	private string number;

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

    	public event PropertyChangedEventHandler PropertyChanged;
    }
}