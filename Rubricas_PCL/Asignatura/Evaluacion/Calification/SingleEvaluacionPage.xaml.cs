using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class SingleEvaluacionPage : ContentPage
	{
        IList<CalificacionEvaluacion> calificacionCollection = new ObservableCollection<CalificacionEvaluacion>{};
        private string asignaturaUid;
        private Evaluacion evaluacion;

        public SingleEvaluacionPage(Evaluacion evaluacion, string asignaturaUid)
		{
			BindingContext = calificacionCollection;
			InitializeComponent();
            this.Title = evaluacion.Name;
            this.asignaturaUid = asignaturaUid;
            this.evaluacion = evaluacion;
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

            CalificacionEvaluacion calificacionEvaluacion = e.SelectedItem as CalificacionEvaluacion;
            await Navigation.PushAsync(new EvaluacionUIPage(asignaturaUid, evaluacion, calificacionEvaluacion.Uid));
			((ListView)sender).SelectedItem = null; // unselect item
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            await FirebaseDB.getCalificacionesForEvaluacion(asignaturaUid, evaluacion.Uid, calificacionCollection);
		}
	}

}
