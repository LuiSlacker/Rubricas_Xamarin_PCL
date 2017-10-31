using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Diagnostics;

namespace Rubricas_PCL
{

	public class Asignatura : INotifyPropertyChanged
	{
        private string uid;
		private string name;
		private string number;

		public string Uid
		{
			set
			{
				if (uid != value)
				{
					uid = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Uid"));
					}
				}
			}
			get
			{
				return uid;
			}
		}

		public string Name
		{
			set
			{
				if (name != value)
				{
					name = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Name"));
					}
				}
			}
			get => name;
		}

		public string Number
		{
			set
			{
				if (number != value)
				{
					number = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Number"));
					}
				}
			}
			get => number;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}


	public partial class AsignaturasPage : ContentPage
	{
		IList<Asignatura> asignaturasCollection = new ObservableCollection<Asignatura>{
			new Asignatura{Name="matemáticas", Number="RF1001"},
		};

        FirebaseClient firebase;

		public AsignaturasPage()
		{
			InitializeComponent();
            firebase = Utils.FIREBASE;

			BindingContext = this;
			asignaturas_List.ItemsSource = asignaturasCollection;
		}

		async void onAddBtnClicked(object sender, EventArgs e)
		{
            Asignatura newAsignatura = new Asignatura();
            var nextPage = new AsignaturasCreateUpdatePage(true);
            nextPage.BindingContext = newAsignatura;
			await Navigation.PushAsync(nextPage);
		}

		async void onSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

			await Navigation.PushAsync(new AsignaturasTabPage());
			((ListView)sender).SelectedItem = null; // unselect item
		}

		async public void OnEdit(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Asignatura asignatura = menuItem.CommandParameter as Asignatura;

            var nextPage = new AsignaturasCreateUpdatePage(false);
			nextPage.BindingContext = asignatura;
			await Navigation.PushAsync(nextPage);
		}

		public void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			Asignatura asignatura = menuItem.CommandParameter as Asignatura;
			asignaturasCollection.Remove(asignatura);
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await getFireAsignaturas();
		}

		public async Task<int> getFireAsignaturas()
		{
            var list = (await firebase
                        .Child(Utils.Entity.FIRE_ASIGNATURAS)
                        .OnceAsync<Asignatura>());

            asignaturasCollection.Clear();
            Debug.WriteLine("Número de entradas en firebase " + list.Count);

            foreach(var item in list) {
                Asignatura asignatura = item.Object as Asignatura;
                asignatura.Uid = item.Key;
                asignaturasCollection.Add(asignatura);
            }
            return 0;
		}
	}


}
