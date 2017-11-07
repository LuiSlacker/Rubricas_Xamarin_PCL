using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Rubricas_PCL
{
    public partial class EvaluacionUIPage : ContentPage
    {
        private string asignaturaUid;
		private string evaluacionUid;
        private string calificacionUid;
        private List<CalificacionCategoria> calificacionCategorias;

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
            calificacionCategorias = await FirebaseDB.getCategoriasForCalificacion(asignaturaUid, evaluacionUid, calificacionUid);

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

                List<CalificacionElemento> calificacionElementos = await FirebaseDB.getElementsForCalificacion(asignaturaUid, evaluacionUid, calificacionUid, categoria.Uid);
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

                    var elementoPicker = new Picker
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        StyleClass = new ObservableCollection<string>() {categoria.Uid, elemento.Uid },
                        ItemsSource = new List<string>() {elemento.Nivel1Name, elemento.Nivel2Name, elemento.Nivel3Name, elemento.Nivel4Name },
						
                    };
                    elementoPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
                    elementoPicker.SelectedIndex = elemento.Nivel;

                    elementLayout.Children.Add(elementoLabel);
                    elementLayout.Children.Add(elementoPicker);

                    layout.Children.Add(elementLayout);
                }

            }
			
            var btn = new Button{
                Text = "Guardar"
            };
			btn.Clicked += async (sender, ea) => {
                CalificacionEvaluacion calificacion = await FirebaseDB.getCalificacionById(asignaturaUid, evaluacionUid, calificacionUid);
                calificacion.Nota = await calculateNewAverage();
                await FirebaseDB.updateCalificacion(asignaturaUid, evaluacionUid, calificacionUid, calificacion);
                await Navigation.PopAsync();
			};

            layout.Children.Add(btn);
			Content = layout;

		}

		async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
            string calificacionCategoriaUid = picker.StyleClass[0];
            string calificacionElementoUid = picker.StyleClass[1];
            CalificacionElemento calificacionElemento = await FirebaseDB.getCalificacionElementById(asignaturaUid, evaluacionUid, calificacionUid, calificacionCategoriaUid, calificacionElementoUid);
            calificacionElemento.Nivel = selectedIndex;

            await FirebaseDB.updateCalificacionElemento(asignaturaUid, evaluacionUid, calificacionUid, calificacionCategoriaUid, calificacionElementoUid, calificacionElemento);
		}

		async Task<float> calculateNewAverage()
		{
			float notaAverage = 0.0f;
            foreach(CalificacionCategoria calificacionCategoria in calificacionCategorias)
			{
                List<CalificacionElemento> calificacionElementos = await FirebaseDB.getElementsForCalificacion(asignaturaUid, evaluacionUid, calificacionUid, calificacionCategoria.Uid);
				float categoriaAverage = 0.0f;
				foreach (CalificacionElemento calificacionElemento in calificacionElementos)
				{
                    int nivel = calificacionElemento.Nivel + 1;
                    int elementoPeso = calificacionElemento.Peso;
					categoriaAverage += elementoPeso * nivel / 100.0f;
				}
                int categoriaPeso = calificacionCategoria.Peso;
				notaAverage += categoriaPeso * categoriaAverage / 100.0f;
			}
			return notaAverage * 1.25f;
		}
    }
	
}
