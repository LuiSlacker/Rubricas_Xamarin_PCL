using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class AsignaturasCreateUpdatePage : ContentPage
	{
        
		private bool isCreateMode;
        private FirebaseClient firebase;

        public AsignaturasCreateUpdatePage(bool isCreateMode)
		{
			this.isCreateMode = isCreateMode;

			InitializeComponent();
            firebase = Utils.FIREBASE;
			this.Title = isCreateMode ? "Añadir asignatura" : "Editar asignatura";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";
		}

		async void onBtnClicked(object sender, EventArgs e)
		{
            var newAsignatura = (Asignatura)BindingContext;
            if (isCreateMode) {
                //Asignatura newAsignatura =  new Asignatura { Name = name.Text, Number = number.Text };
				var item = await firebase
                    .Child(Utils.FireBase_Entity.ASIGNATURAS)
                      .PostAsync(newAsignatura);
            } else {
				await firebase
                  .Child(Utils.FireBase_Entity.ASIGNATURAS)
                  .Child(newAsignatura.Uid)
                  .PutAsync(newAsignatura);
            }

			await Navigation.PopAsync();
		}
	}
}
