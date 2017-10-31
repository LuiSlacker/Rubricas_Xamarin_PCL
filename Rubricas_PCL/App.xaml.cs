using Xamarin.Forms;

namespace Rubricas_PCL
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetailPage;

        public App()
        {
            InitializeComponent();

			MasterDetailPage = new MasterDetailPage
			{
				Master = new MenuPage(),
				Detail = new NavigationPage(new AsignaturasPage()),
			};
			MainPage = MasterDetailPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
