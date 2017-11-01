using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Firebase.Xamarin.Database.Query;
using Firebase.Xamarin.Database;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class ElementosDentroCategoriasPage : ContentPage
	{
		IList<Elemento> elementosCollection = new ObservableCollection<Elemento>{};
        private FirebaseClient firebase;
        private string rubricaUid;
        private string categoriaUid;

        public ElementosDentroCategoriasPage(string rubricaUid, string categoriaUid)
		{
            this.rubricaUid = rubricaUid;
            this.categoriaUid = categoriaUid;
            firebase = Utils.FIREBASE;
            BindingContext = elementosCollection;
			InitializeComponent();
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{
            var nextPage = new ElementoCreateUpdatePage(rubricaUid, categoriaUid, true)
            {
                BindingContext = new Elemento()
            };
            await Navigation.PushAsync(nextPage);
		}

		void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}
			((ListView)sender).SelectedItem = null; // unselect item
		}

		async public void OnEdit(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Elemento elemento = menuItem.CommandParameter as Elemento;

			var nextPage = new ElementoCreateUpdatePage(rubricaUid, categoriaUid, false);
			nextPage.BindingContext = elemento;
			await Navigation.PushAsync(nextPage);
		}

		async public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Elemento elemento = menuItem.CommandParameter as Elemento;
			
            await firebase
				.Child(Utils.FireBase_Entity.RUBRICAS)
				.Child(rubricaUid)
				.Child(Utils.FireBase_Entity.CATEGORIAS)
				.Child(categoriaUid)
                .Child(Utils.FireBase_Entity.ELEMENTOS)
                .Child(elemento.Uid)
				.DeleteAsync();

			await getFireElementosForCategoria();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await getFireElementosForCategoria();
		}

		public async Task<int> getFireElementosForCategoria()
		{
			var list = (await firebase
				.Child(Utils.FireBase_Entity.RUBRICAS)
				.Child(rubricaUid)
				.Child(Utils.FireBase_Entity.CATEGORIAS)
                .Child(categoriaUid)
                .Child(Utils.FireBase_Entity.ELEMENTOS)
                .OnceAsync<Elemento>());

			elementosCollection.Clear();

			foreach (var item in list)
			{
                Elemento elemento = item.Object as Elemento;
				elemento.Uid = item.Key;
				elementosCollection.Add(elemento);
			}
			return 0;
		}
	}

}
