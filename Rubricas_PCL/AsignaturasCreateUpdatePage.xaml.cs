using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class AsignaturasCreateUpdatePage : ContentPage
	{
		private IList<Asignatura> asignaturasCollection;
		private bool isCreateMode;
        private FirebaseClient firebase;

        public AsignaturasCreateUpdatePage(IList<Asignatura> asignaturasCollection, bool isCreateMode)
		{
			this.asignaturasCollection = asignaturasCollection;
			this.isCreateMode = isCreateMode;

			InitializeComponent();
            firebase = new FirebaseClient("https://rubricas-70761.firebaseio.com/");
			this.Title = isCreateMode ? "Añadir asignatura" : "Editar asignatura";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";
		}

		async void onBtnClicked(object sender, EventArgs e)
		{
            if (isCreateMode) {
                Asignatura newAsignatura =  new Asignatura { Name = name.Text, Number = number.Text };
				var item = await firebase
                      .Child("asignaturas")
                      //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                      .PostAsync(newAsignatura);
            }

            //asignaturasCollection.Add(new Asignatura { Name = name.Text, Number = number.Text });
			await Navigation.PopAsync();
		}
	}
}
