using System;
using Xamarin.Forms;
using System.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace Rubricas_PCL
{
    public partial class MasterDetailClassPage : MasterDetailPage
    {
        public MasterDetailClassPage()
        {
            InitializeComponent();

			this.Master = new MenuPage();
			this.Detail = new NavigationPage(new AsignaturasPage());

			checkConnectivity(CrossConnectivity.Current.IsConnected);
			CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

		void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
            checkConnectivity(e.IsConnected);
		}

        async public void checkConnectivity(bool sw)
		{
			if (sw)
			{
				if (CrossConnectivity.Current != null && CrossConnectivity.Current.ConnectionTypes != null)
				{
					var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                    await DisplayAlert("", "Back online", "Ok");
				}
			}
			else
			{
                await DisplayAlert("Offline", "Verifique su estado de internet y vuelva a intentarlo", "Ok");
			}
		}
    }
}
