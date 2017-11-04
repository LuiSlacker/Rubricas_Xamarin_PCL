using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Rubricas_PCL
{
    public partial class EvaluacionUIPage : ContentPage
    {
        private string asignaturaUid;
		private string evaluacionUid;
        private string calificacionUid;

        public EvaluacionUIPage(string asignaturaUid, Evaluacion evaluacion, string calificacionUid)
        {
            this.asignaturaUid = asignaturaUid;
            this.evaluacionUid = evaluacion.Uid;
            this.calificacionUid = calificacionUid;
			this.Title = evaluacion.Name;

			InitializeComponent();
        }

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            List<CalificacionCategoria> calificacionCategorias = await FirebaseDB.getCategoriasForCalificacion(asignaturaUid, evaluacionUid, calificacionUid);

            var layout = new StackLayout{
                Margin = new Thickness(20, 20, 20, 20)
            };
            foreach(CalificacionCategoria categoria in calificacionCategorias)
            {
                var categoriaLabel = new Label
                {
                    Text = categoria.CategoriaName
                };
				layout.Children.Add(categoriaLabel);

                List<CalificacionElemento> calificacionElementos = await FirebaseDB.getElementForCalificacion(asignaturaUid, evaluacionUid, calificacionUid, categoria.Uid);
                foreach (CalificacionElemento elemento in calificacionElementos)
                {
                    var elementLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(20, 0, 0, 0)
                    };

                    var elementoLabel = new Label
                    {
                        Text = elemento.ElementoName
                    };

                    var elementoPicker = new Picker{
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    elementLayout.Children.Add(elementoLabel);
                    elementLayout.Children.Add(elementoPicker);

                    layout.Children.Add(elementLayout);
                }

            }
			
            var btn = new Button{
                Text = "Guardar"
            };
            layout.Children.Add(btn);
			Content = layout;

		}
    }
}
