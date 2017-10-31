using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public class Estudiante : INotifyPropertyChanged
	{
		private string name;
		private string apellido;

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


	public partial class EstudiantesDentroAsignaturasPage : ContentPage
	{
		IList<Estudiante> estudiantesCollection = new ObservableCollection<Estudiante>{
			new Estudiante{Name="Luis", Apellido="Goohsen"},
		};

		public EstudiantesDentroAsignaturasPage()
		{
			BindingContext = estudiantesCollection;
			InitializeComponent();
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{

			//var secondPage = new EstudiantesCreateUpdatePage(estudiantesCollection, true);
			//await Navigation.PushAsync(secondPage);
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			//if (e.SelectedItem == null)
			//{
			//	return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			//}

			//await Navigation.PushAsync(new EstudiantesReportPage());
			//((ListView)sender).SelectedItem = null; // unselect item
		}

		async public void OnEdit(object sender, EventArgs e)
		{
			//var menuItem = ((MenuItem)sender);
			//Estudiante estudiante = menuItem.CommandParameter as Estudiante;

			//var nextPage = new EstudiantesCreateUpdatePage(estudiantesCollection, false);
			//nextPage.BindingContext = estudiante;
			//await Navigation.PushAsync(nextPage);
		}

		public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Estudiante estudiante = menuItem.CommandParameter as Estudiante;
			estudiantesCollection.Remove(estudiante);
		}
	}
}
