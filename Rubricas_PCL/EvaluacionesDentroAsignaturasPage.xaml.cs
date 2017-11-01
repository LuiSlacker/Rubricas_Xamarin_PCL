using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class EvaluacionesDentroAsignaturasPage : ContentPage
	{
		IList<Evaluacion> evaluacionesCollection = new ObservableCollection<Evaluacion>{};
        private FirebaseClient firebase;
        private string asignaturaUid;

        public EvaluacionesDentroAsignaturasPage(string asignaturaUid)
		{
            this.asignaturaUid = asignaturaUid;
			BindingContext = evaluacionesCollection;
			InitializeComponent();
            firebase = Utils.FIREBASE;
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{

            var nextPage = new EvaluacionesCreateUpdatePage(asignaturaUid, true) {
                BindingContext = new Evaluacion()
            };
			await Navigation.PushAsync(nextPage);
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
			var menuItem = ((MenuItem)sender);
			Evaluacion evaluacion = menuItem.CommandParameter as Evaluacion;

			var nextPage = new EvaluacionesCreateUpdatePage(asignaturaUid, false);
			nextPage.BindingContext = evaluacion;
			await Navigation.PushAsync(nextPage);
		}

		async public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Evaluacion evaluacion = menuItem.CommandParameter as Evaluacion;
			
            await firebase
				.Child(Utils.FireBase_Entity.ASIGNATURAS)
				.Child(asignaturaUid)
                .Child(Utils.FireBase_Entity.EVALUACIONES)
				.Child(evaluacion.Uid)
				.DeleteAsync();

			await getFireEvaluaciones();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await getFireEvaluaciones();
		}

		public async Task<int> getFireEvaluaciones()
		{
			var list = (await firebase
						.Child(Utils.FireBase_Entity.ASIGNATURAS)
						.Child(asignaturaUid)
                        .Child(Utils.FireBase_Entity.EVALUACIONES)
                        .OnceAsync<Evaluacion>());
            
			evaluacionesCollection.Clear();

			foreach (var item in list)
			{
                Evaluacion evaluacion = item.Object as Evaluacion;
				evaluacion.Uid = item.Key;
				evaluacionesCollection.Add(evaluacion);
			}
			return 0;
		}
	}

}
