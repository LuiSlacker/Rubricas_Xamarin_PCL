using System;
using System.Collections.Generic;
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
			if (isCreateMode)
			{
				var item = await firebase
					.Child(Utils.FireBase_Entity.ASIGNATURAS)
					.Child(asignaturaUid)
                    .Child(Utils.FireBase_Entity.EVALUACIONES)
					.PostAsync(newEvaluacion);
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
	}
}
