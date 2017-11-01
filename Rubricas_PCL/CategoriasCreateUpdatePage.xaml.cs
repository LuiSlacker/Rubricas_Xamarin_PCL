using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class CategoriasCreateUpdatePage : ContentPage
	{
		private bool isCreateMode;
        private string rubricaUid;
        private FirebaseClient firebase;

        public CategoriasCreateUpdatePage(string rubricaUid, bool isCreateMode)
		{
			this.rubricaUid = rubricaUid;
            this.isCreateMode = isCreateMode;
            firebase = Utils.FIREBASE;

			InitializeComponent();
			this.Title = isCreateMode ? "Añadir categoria" : "Editar categoria";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";
		}

		async void onBtnClicked(object sender, EventArgs e)
		{
            var newCategoria = (Categoria)BindingContext;
			if (isCreateMode)
			{
				var item = await firebase
                    .Child(Utils.FireBase_Entity.RUBRICAS)
					.Child(rubricaUid)
                    .Child(Utils.FireBase_Entity.CATEGORIAS)
					.PostAsync(newCategoria);
			}
			else
			{
				await firebase
                    .Child(Utils.FireBase_Entity.RUBRICAS)
					.Child(rubricaUid)
                    .Child(Utils.FireBase_Entity.CATEGORIAS)
					.Child(newCategoria.Uid)
					.PutAsync(newCategoria);
			}

			await Navigation.PopAsync();
		}
	}
}
