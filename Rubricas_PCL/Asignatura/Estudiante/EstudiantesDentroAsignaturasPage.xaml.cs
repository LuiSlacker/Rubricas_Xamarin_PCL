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
	public partial class EstudiantesDentroAsignaturasPage : ContentPage
	{
		IList<Estudiante> estudiantesCollection = new ObservableCollection<Estudiante>{};
        private FirebaseClient firebase;
        private string asignaturaUid;

        public EstudiantesDentroAsignaturasPage(string asignaturaUid)
		{
			this.asignaturaUid = asignaturaUid;

            BindingContext = estudiantesCollection;
            InitializeComponent();
            firebase = Utils.FIREBASE;
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{

            Estudiante newEstudiante = new Estudiante();
			var nextPage = new EstudiantesCreateUpdatePage(asignaturaUid, true);
            nextPage.BindingContext = newEstudiante;
			await Navigation.PushAsync(nextPage);
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
			var menuItem = ((MenuItem)sender);
			Estudiante estudiante = menuItem.CommandParameter as Estudiante;

			var nextPage = new EstudiantesCreateUpdatePage(asignaturaUid, false);
			nextPage.BindingContext = estudiante;
			await Navigation.PushAsync(nextPage);
		}

		async public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Estudiante estudiante = menuItem.CommandParameter as Estudiante;

			await firebase
                .Child(Utils.FireBase_Entity.ASIGNATURAS)
				.Child(asignaturaUid)
                .Child(Utils.FireBase_Entity.ESTUDIANTES)
                .Child(estudiante.Uid)
				.DeleteAsync();

			await FirebaseDB.getEstudiantesForAsignatura(this.asignaturaUid, estudiantesCollection);
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            await FirebaseDB.getEstudiantesForAsignatura(this.asignaturaUid, estudiantesCollection);
		}
		
	}
}
