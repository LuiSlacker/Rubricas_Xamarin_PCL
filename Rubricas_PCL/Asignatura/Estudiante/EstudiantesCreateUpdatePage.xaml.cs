using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class EstudiantesCreateUpdatePage : ContentPage
	{
		private bool isCreateMode;
        private FirebaseClient firebase;
        private string asignaturaUid;

        public EstudiantesCreateUpdatePage(string asignaturaUid, bool isCreateMode)
		{
			this.isCreateMode = isCreateMode;
            this.asignaturaUid = asignaturaUid;

			InitializeComponent();
            firebase = Utils.FIREBASE;
			this.Title = isCreateMode ? "Añadir estudiante" : "Editar estudiante";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";
		}

		async void onBtnClicked(object sender, EventArgs e)
		{
            var newEstudiante = (Estudiante)BindingContext;
			if (isCreateMode)
			{
				var item = await firebase
                    .Child(Utils.FireBase_Entity.ASIGNATURAS)
                    .Child(asignaturaUid)
                    .Child(Utils.FireBase_Entity.ESTUDIANTES)
    			    .PostAsync(newEstudiante);
			}
            else
			{
				await firebase
                    .Child(Utils.FireBase_Entity.ASIGNATURAS)
                    .Child(asignaturaUid)
                    .Child(Utils.FireBase_Entity.ESTUDIANTES)
    			    .Child(newEstudiante.Uid)
    			    .PutAsync(newEstudiante);
			}

			await Navigation.PopAsync();
		}
	}
}
