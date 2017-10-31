using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class RubricaCreateUpdatePage : ContentPage
	{
		private bool isCreateMode;
        private FirebaseClient firebase;

        public RubricaCreateUpdatePage(bool isCreateMode)
		{
			this.isCreateMode = isCreateMode;
            firebase = Utils.FIREBASE;

			InitializeComponent();
			this.Title = isCreateMode ? "Añadir rúbrica" : "Editar rúbrica";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";
		}

		async void onBtnClicked(object sender, EventArgs e)
		{
            var newRubrica = (Rubrica)BindingContext;
			if (isCreateMode)
			{
				var item = await firebase
                    .Child(Utils.FireBase_Entity.RUBRICAS)
        			.PostAsync(newRubrica);
			}
			else
			{
				await firebase
                    .Child(Utils.FireBase_Entity.RUBRICAS)
    			    .Child(newRubrica.Uid)
				    .PutAsync(newRubrica);
			}

			await Navigation.PopAsync();
		}
	}
}
