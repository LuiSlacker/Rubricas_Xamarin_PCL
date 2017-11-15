using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
    public partial class EstudianteReportePage : ContentPage
    {
        IList<Parcial> parcialesCollection = new ObservableCollection<Parcial> { };
        private FirebaseClient firebase;
        private string asignaturaUid;
        private string estudianteUid;

        public EstudianteReportePage() { }
        public EstudianteReportePage(string asignaturaUid, string estudianteUid)
        {
            InitializeComponent();
            firebase = Utils.FIREBASE;
            this.asignaturaUid = asignaturaUid;
            this.estudianteUid = estudianteUid;

            BindingContext = parcialesCollection;
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


            parcialesCollection.Clear();
			foreach (var evaluacionItem in list)
			{
				Evaluacion evaluacion = evaluacionItem.Object as Evaluacion;
				evaluacion.Uid = evaluacionItem.Key;

                foreach (var calificacionItem in evaluacion.calificaciones)
                {
                    if (this.estudianteUid == calificacionItem.Value.EstudianteUid)
                    {
						Parcial parcial = new Parcial(evaluacion.Name, calificacionItem.Value.Nota);
                        parcialesCollection.Add(parcial);
                    }
				}
			}
			return 0;
		}
    }
}
