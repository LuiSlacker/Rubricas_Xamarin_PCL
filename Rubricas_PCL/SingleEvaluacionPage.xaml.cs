using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public class SingleEvaluacion : INotifyPropertyChanged
	{
		private string name;
		private string nota;

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

		public string Nota
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

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public partial class SingleEvaluacionPage : ContentPage
	{
		IList<SingleEvaluacion> categoriasCollection = new ObservableCollection<SingleEvaluacion>{
			new SingleEvaluacion{Name="Ludwig Goohsen", Nota="4.5"},
			new SingleEvaluacion{Name="Jon Doe", Nota="3.5"},
		};

		public SingleEvaluacionPage()
		{
			BindingContext = categoriasCollection;
			InitializeComponent();
			this.Title = "Evaluacion";
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			//if (e.SelectedItem == null)
			//{
			//	return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			//}

			//await Navigation.PushAsync(new EvaluacionUIPage());
			//((ListView)sender).SelectedItem = null; // unselect item
		}
	}

}
