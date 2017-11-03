﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class EvaluacionesCreateUpdatePage : ContentPage
	{
		private bool isCreateMode;
        private FirebaseClient firebase;
        private string asignaturaUid;
        private List<Rubrica> rubricas = new List<Rubrica>();

        public EvaluacionesCreateUpdatePage(string asignaturaUid, bool isCreateMode)
		{
			this.asignaturaUid = asignaturaUid;
			this.isCreateMode = isCreateMode;

			InitializeComponent();
            firebase = Utils.FIREBASE;
			this.Title = isCreateMode ? "Añadir evaluacion" : "Editar evaluacion";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";

		}

		async void onBtnClicked(object sender, EventArgs e)
		{
            var newEvaluacion = (Evaluacion)BindingContext;
            newEvaluacion.RubricaUid = rubricas[picker.SelectedIndex].Uid;
			if (isCreateMode)
			{
				var item = await firebase
					.Child(Utils.FireBase_Entity.ASIGNATURAS)
					.Child(asignaturaUid)
                    .Child(Utils.FireBase_Entity.EVALUACIONES)
					.PostAsync(newEvaluacion);
                
                populateCalificacionCollection(newEvaluacion.RubricaUid, item.Key);
			}
			else
			{
				await firebase
					.Child(Utils.FireBase_Entity.ASIGNATURAS)
					.Child(asignaturaUid)
                    .Child(Utils.FireBase_Entity.EVALUACIONES)
					.Child(newEvaluacion.Uid)
					.PutAsync(newEvaluacion);
			}

			await Navigation.PopAsync();
		}

        async private void populateCalificacionCollection(string rubricaUid, string evaluacionUid) {
            IList<Estudiante> estudiantes = await FirebaseDB.getEstudiantesForAsignatura(asignaturaUid);
            Rubrica rubrica = await FirebaseDB.getRubricaForId(rubricaUid);

			foreach (var estudiante in estudiantes)
			{
                System.Diagnostics.Debug.WriteLine(estudiante.Name);
                CalificacionEvaluacion calificacionEvaluacion = new CalificacionEvaluacion(estudiante);
				var item = await firebase
					.Child(Utils.FireBase_Entity.ASIGNATURAS)
					.Child(asignaturaUid)
					.Child(Utils.FireBase_Entity.EVALUACIONES)
                    .Child(evaluacionUid)
                    .Child(Utils.FireBase_Entity.CALIFICACION)
					.PostAsync(calificacionEvaluacion);


			}
        }

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await getFireRubricas();
		}

		public async Task<int> getFireRubricas()
        {
            List<string> rubricasStrings = new List<string>();
			var list = (await firebase
						.Child(Utils.FireBase_Entity.RUBRICAS)
						.OnceAsync<Rubrica>());

			foreach (var item in list)
			{
				Rubrica rubrica = item.Object as Rubrica;
                rubrica.Uid = item.Key;
				rubricas.Add(rubrica);
                rubricasStrings.Add(rubrica.Name);
			}
            picker.ItemsSource = rubricasStrings;

			return 0;
		}

	}
}
