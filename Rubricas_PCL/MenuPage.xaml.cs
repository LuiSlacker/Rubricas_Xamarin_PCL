using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Rubricas_PCL
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage()
		{
			InitializeComponent();
			BackgroundColor = Color.Gray.WithLuminosity(0.9);
			Title = "Home";
			Icon = "ic_menu.png";
		}

		void onAsignaturasBtnClicked(object sender, EventArgs e)
		{
			App.MasterDetailPage.Detail = new NavigationPage(new AsignaturasPage());
			App.MasterDetailPage.IsPresented = false;
		}

		void onRubricasBtnClicked(object sender, EventArgs e)
		{
			//App.MasterDetailPage.Detail = new NavigationPage(new RubricasPage());
			//App.MasterDetailPage.IsPresented = false;
		}
	}
}
