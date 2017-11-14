using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Threading.Tasks;

namespace Rubricas_PCL
{
	public partial class RubricasPage : ContentPage
	{

		IList<Rubrica> rubricasCollection = new ObservableCollection<Rubrica>{};
        private FirebaseClient firebase;

        public RubricasPage()
		{
			BindingContext = rubricasCollection;
            firebase = Utils.FIREBASE;
			InitializeComponent();
			this.Title = "Rúbricas";
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{
            Rubrica newRubrica = new Rubrica();
			var nextPage = new RubricaCreateUpdatePage(true);
            nextPage.BindingContext = newRubrica;

			await Navigation.PushAsync(nextPage);
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

            Rubrica rubrica = e.SelectedItem as Rubrica;

            await Navigation.PushAsync(new CategoriasDentroRubricasPage(rubrica.Uid));
			((ListView)sender).SelectedItem = null; // unselect item
		}

		async public void OnEdit(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Rubrica rubrica = menuItem.CommandParameter as Rubrica;

			var nextPage = new RubricaCreateUpdatePage(false);
			nextPage.BindingContext = rubrica;
			await Navigation.PushAsync(nextPage);
		}

		async public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Rubrica rubrica = menuItem.CommandParameter as Rubrica;

			await firebase
                .Child(Utils.FireBase_Entity.RUBRICAS)
                .Child(rubrica.Uid)
				.DeleteAsync();

			await FirebaseDB.getRubricas(rubricasCollection);
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            await FirebaseDB.getRubricas(rubricasCollection);
		}

	}
}
