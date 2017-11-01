using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class ElementoCreateUpdatePage : ContentPage
	{
		private bool isCreateMode;
        private FirebaseClient firebase;
        private string rubricaUid;
        private string categoriaUid;

        public ElementoCreateUpdatePage(string rubricaUid, string categoriaUid, bool isCreateMode)
		{
			this.rubricaUid = rubricaUid;
            this.categoriaUid = categoriaUid;
			this.isCreateMode = isCreateMode;
            firebase = Utils.FIREBASE;

			InitializeComponent();
			this.Title = isCreateMode ? "Añadir elemento" : "Editar elemento";
			btnLabel.Text = isCreateMode ? "Guardar" : "Actualizar";
		}

		async void onBtnClicked(object sender, EventArgs e)
		{
			var newElemento = (Elemento)BindingContext;
			if (isCreateMode)
			{
				var item = await firebase
					.Child(Utils.FireBase_Entity.RUBRICAS)
					.Child(rubricaUid)
					.Child(Utils.FireBase_Entity.CATEGORIAS)
                    .Child(categoriaUid)
                    .Child(Utils.FireBase_Entity.ELEMENTOS)
					.PostAsync(newElemento);
			}
			else
			{
				await firebase
					.Child(Utils.FireBase_Entity.RUBRICAS)
					.Child(rubricaUid)
					.Child(Utils.FireBase_Entity.CATEGORIAS)
					.Child(categoriaUid)
					.Child(Utils.FireBase_Entity.ELEMENTOS)
					.Child(newElemento.Uid)
					.PutAsync(newElemento);
			}

			await Navigation.PopAsync();
		}
	}
}
