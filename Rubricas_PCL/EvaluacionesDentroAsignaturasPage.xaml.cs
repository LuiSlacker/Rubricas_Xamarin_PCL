using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class EvaluacionesDentroAsignaturasPage : ContentPage
	{
		IList<Evaluacion> evaluacionesCollection = new ObservableCollection<Evaluacion>{
			new Evaluacion{Name="parcial 1"},
		};

		public EvaluacionesDentroAsignaturasPage()
		{
			BindingContext = evaluacionesCollection;
			InitializeComponent();
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{

			//var secondPage = new EvaluacionesCreateUpdatePage(evaluacionesCollection, true);
			//await Navigation.PushAsync(secondPage);
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			//if (e.SelectedItem == null)
			//{
			//	return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			//}

			//await Navigation.PushAsync(new SingleEvaluacionPage());
			//((ListView)sender).SelectedItem = null; // unselect item
		}

		async public void OnEdit(object sender, EventArgs e)
		{
			//var menuItem = ((MenuItem)sender);
			//Evaluacion evaluacion = menuItem.CommandParameter as Evaluacion;

			//var nextPage = new EvaluacionesCreateUpdatePage(evaluacionesCollection, false);
			//nextPage.BindingContext = evaluacion;
			//await Navigation.PushAsync(nextPage);
		}

		public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Evaluacion evaluacion = menuItem.CommandParameter as Evaluacion;
			evaluacionesCollection.Remove(evaluacion);
		}
	}

}
