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
        private Label sliderValuelabel;

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
                        StyleClass = new ObservableCollection<string>() {categoria.Uid, elemento.Uid }, // misuse StyleClass to set values to each picker
                        ItemsSource = new List<string>() {elemento.Nivel1Name, elemento.Nivel2Name, elemento.Nivel3Name, elemento.Nivel4Name },
						
                    };
                    elementoPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
                    elementoPicker.SelectedIndex = elemento.Nivel;

                    float min = 0,max = 0,val=0;
                    int index = elementoPicker.SelectedIndex;

                    switch (index)
                    {
                        case 0:
                            min = (float)elemento.DeNivel1;
                            max = (float)elemento.HastaNivel1;
                            val = (float)(min + max) / 2;
                            break;
                        case 1:
                            min = (float)elemento.DeNivel2;
                            max = (float)elemento.HastaNivel2;
                            val = (float)(min + max) / 2;
                            break;
                        case 2:
                            min = (float)elemento.DeNivel3;
                            max = (float)elemento.HastaNivel3;
                            val = (float)(min + max) / 2;
                            break;
                        case 3:
                            min = (float)elemento.DeNivel4;
                            max = (float)elemento.HastaNivel4;
                            val = (float)(min + max) / 2;
                            break;
                        default:
                            min = (float)elemento.DeNivel1;
                            max = (float)elemento.HastaNivel1;
                            val = 0;
                            break;
                    }

                    var rangeLabel = new Label
                    {
                        Text = String.Concat(min.ToString(), " <--> ", max.ToString())
                    };

                    var gradeInput = new Entry { Placeholder = String.Concat(min.ToString(), " <--> ", max.ToString()) };

                    gradeInput.Completed += Entry_Completed;

                    var elementoSlider = new Slider
					{
                        Minimum = (float) elemento.DeNivel1,
                        Maximum = (float) elemento.HastaNivel1,
                        Value = (float) elemento.DeNivel1,
						VerticalOptions = LayoutOptions.FillAndExpand,
                        StyleId=elemento.Uid // bind elementoUid to slider for identification in ValueChanged callback
					};
                    elementoSlider.ValueChanged += OnSliderValueChanged;

                    sliderValuelabel = new Label
                    {
                        Text = elemento.DeNivel1.ToString()
                    };

                    elementLayout.Children.Add(elementoLabel);
                    elementLayout.Children.Add(elementoPicker);
                    elementLayout.Children.Add(rangeLabel);
                    //elementLayout.Children.Add(gradeInput);
                    //elementLayout.Children.Add(elementoSlider);
					//elementLayout.Children.Add(sliderValuelabel);

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

        async void Entry_Completed(object sender, EventArgs e)
        {
            //await DisplayAlert("Hi!", "I'm on Entry_Completed.", "Ok"); //Just some debug stuff.
            float rng = float.Parse(((Entry)sender).Text);
            if ((rng < 0) || (rng > 5))
            {
                await DisplayAlert("Error", "Nota debe estar entre 0 y 5.", "Ok");
                ((Entry)sender).Text = "";
            }
            else
            {
                var text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
            }
            
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

            //OnAppearing();
		}

		void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
			sliderValuelabel.Text = String.Format("{0:F1}", e.NewValue);
		}

        async Task<float> calculateNewGrade()
        {
            float notaAverage = 0.0f;
            foreach (CalificacionCategoria calificacionCategoria in calificacionCategorias)
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
