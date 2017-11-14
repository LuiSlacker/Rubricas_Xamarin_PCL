using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class CategoriasDentroRubricasPage : ContentPage
	{
		IList<Categoria> categoriasCollection = new ObservableCollection<Categoria>{};
        private string rubricaUid;
        private FirebaseClient firebase;

        public CategoriasDentroRubricasPage(string rubricaUid)
		{
			this.rubricaUid = rubricaUid;
            BindingContext = categoriasCollection;
            firebase = Utils.FIREBASE;

            InitializeComponent();
			this.Title = "Catgorias";
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{
            var nextPage = new CategoriasCreateUpdatePage(rubricaUid, true)
            {
                BindingContext = new Categoria()
            };
            await Navigation.PushAsync(nextPage);
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

            Categoria categoria = e.SelectedItem as Categoria;
            await Navigation.PushAsync(new ElementosDentroCategoriasPage(rubricaUid, categoria.Uid));
			((ListView)sender).SelectedItem = null; // unselect item
		}

		async public void OnEdit(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Categoria categoria = menuItem.CommandParameter as Categoria;

            var nextPage = new CategoriasCreateUpdatePage(rubricaUid, false)
            {
                BindingContext = categoria
            };
            await Navigation.PushAsync(nextPage);
		}

		async public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Categoria categoria = menuItem.CommandParameter as Categoria;

			await firebase
                .Child(Utils.FireBase_Entity.RUBRICAS)
                .Child(rubricaUid)
                .Child(Utils.FireBase_Entity.CATEGORIAS)
				.Child(categoria.Uid)
				.DeleteAsync();

			await getFireCategoriasForRubrica();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await getFireCategoriasForRubrica();
		}

		public async Task<int> getFireCategoriasForRubrica()
		{
			var list = (await firebase
                        .Child(Utils.FireBase_Entity.RUBRICAS)
						.Child(this.rubricaUid)
                        .Child(Utils.FireBase_Entity.CATEGORIAS)
                        .OnceAsync<Categoria>());

			categoriasCollection.Clear();

			foreach (var item in list)
			{
                Categoria categoria = item.Object as Categoria;
				categoria.Uid = item.Key;
				categoriasCollection.Add(categoria);
			}
			return 0;
		}
	}

}
