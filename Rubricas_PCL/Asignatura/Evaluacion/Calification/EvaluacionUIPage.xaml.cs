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

        private Dictionary<string, Slider> sliderDict = new Dictionary<string, Slider>();
        private Dictionary<string, Label> labelDict = new Dictionary<string, Label>();
        private Dictionary<string, CalificacionElemento> calificacionElementoDict = new Dictionary<string, CalificacionElemento>();
        private Dictionary<string, CalificacionElemento> updatedcalificacionElementoDict = new Dictionary<string, CalificacionElemento>();

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
                    // save calificacion elemento to update slider range later
                    elemento.CalificacionCategoriaUid = categoria.Uid;
                    calificacionElementoDict.Add(elemento.Uid, elemento);

                    var elementLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(20, 0, 0, 0)
                    };

                    var elementoLabel = new Label
                    {
                        Text = elemento.ElementoName
                    };

					// elementoSlider and sliderValuelabel have to be declard before Picker since they are  accessed during initial Picker-index setting
					var elementoSlider = new Slider
					{
						Minimum = (float)elemento.DeNivel1,
						Maximum = (float)elemento.HastaNivel1,
                        Value = (float)elemento.Nota,
						VerticalOptions = LayoutOptions.FillAndExpand,
						StyleId = elemento.Uid // bind elementoUid to slider for identification in ValueChanged callback
					};
					elementoSlider.ValueChanged += OnSliderValueChanged;
					sliderDict.Add(elemento.Uid, elementoSlider);

					sliderValuelabel = new Label
					{
                        Text = elemento.Nota.ToString()
					};

					labelDict.Add(elemento.Uid, sliderValuelabel);

                    var elementoPicker = new Picker
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        StyleClass = new ObservableCollection<string>() {categoria.Uid, elemento.Uid, "true" }, // misuse StyleClass to set values to each picker
                        ItemsSource = new List<string>() {elemento.Nivel1Name, elemento.Nivel2Name, elemento.Nivel3Name, elemento.Nivel4Name },
						
                    };
                    elementoPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
                    elementoPicker.SelectedIndex = elemento.Nivel;

                    elementLayout.Children.Add(elementoLabel);
                    elementLayout.Children.Add(elementoPicker);
                    elementLayout.Children.Add(elementoSlider);
					elementLayout.Children.Add(sliderValuelabel);

                    layout.Children.Add(elementLayout);
                }

            }
			
            var btn = new Button{
                Text = "Guardar"
            };
			btn.Clicked += async (sender, ea) => {
                foreach (KeyValuePair<string, CalificacionElemento> entry in calificacionElementoDict) {
                    CalificacionElemento calificacionElemento = entry.Value;
                    await FirebaseDB.updateCalificacionElemento(asignaturaUid, evaluacionUid, calificacionUid, calificacionElemento.CalificacionCategoriaUid, calificacionElemento.Uid, calificacionElemento);
                }

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
            bool initialPickerChanged = Convert.ToBoolean(picker.StyleClass[2]);

            // update limits for corresponding slider
            CalificacionElemento calificacionElemento = calificacionElementoDict[calificacionElementoUid];
            setSliderLimits(selectedIndex, calificacionElemento, initialPickerChanged);

            if (!initialPickerChanged) {
				// persist Elemento nivel change
				calificacionElemento.Nivel = selectedIndex;
				await FirebaseDB.updateCalificacionElemento(asignaturaUid, evaluacionUid, calificacionUid, calificacionCategoriaUid, calificacionElementoUid, calificacionElemento);

				// update dictionary to reflect the changes
				calificacionElementoDict[calificacionElementoUid] = calificacionElemento;

            }
            // set boolean to false to persist successive onPickerChanged events
			picker.StyleClass = new ObservableCollection<string>() { calificacionCategoriaUid, calificacionElementoUid, "false" };

        }

		void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
            var slider = (Slider)sender;

            bool isInitialSetting = Convert.ToBoolean(slider.StyleClass[0]);
            if (!isInitialSetting) {
                
                // update corresponding label
                string elementoId = slider.StyleId;
                setSliderValueLabelText(elementoId, e.NewValue);

                // update elemento nota and push to updatedDictonary
                CalificacionElemento calificacionElemento = calificacionElementoDict[elementoId];
                calificacionElemento.Nota = (float)Math.Round(e.NewValue, 1);
			}
            slider.StyleClass[0] = "false";
		}

        void setSliderLimits(int selectedIndex, CalificacionElemento elemento, bool initialPickerChanged) {
            double min = 0.0;
            double max = 0.0;

            switch (selectedIndex) {
                case 0:
                    min = elemento.DeNivel1;
                    max = elemento.HastaNivel1;
                    break;
				case 1:
					min = elemento.DeNivel2;
					max = elemento.HastaNivel2;
					break;
				case 2:
					min = elemento.DeNivel3;
					max = elemento.HastaNivel3;
					break;
				case 3:
    				min = elemento.DeNivel4;
    				max = elemento.HastaNivel4;
    				break;
                default:
					min = 0;
					max = 10;
                    break;
            }


            sliderDict[elemento.Uid].StyleClass = new ObservableCollection<string>() { "" + initialPickerChanged } ;
			sliderDict[elemento.Uid].Maximum = max;
            sliderDict[elemento.Uid].Minimum = min;

            if (!initialPickerChanged) {
				double center = min + ((max - min) / 2);
				sliderDict[elemento.Uid].Value = center;

				// update corresponding label
				setSliderValueLabelText(elemento.Uid, center);
            } else {
                sliderDict[elemento.Uid].Value = elemento.Nota;
                setSliderValueLabelText(elemento.Uid, elemento.Nota);
            }

		}

        void setSliderValueLabelText(string elementoId, double newValue) {
            labelDict[elementoId].Text = String.Format("{0:F1}", newValue);
        }

		async Task<double> calculateNewAverage()
		{
			double notaAverage = 0.0f;
            calificacionCategorias = await FirebaseDB.getCategoriasForCalificacion(asignaturaUid, evaluacionUid, calificacionUid);
            foreach(CalificacionCategoria calificacionCategoria in calificacionCategorias)
			{
                List<CalificacionElemento> calificacionElementos = await FirebaseDB.getElementsForCalificacion(asignaturaUid, evaluacionUid, calificacionUid, calificacionCategoria.Uid);
				double categoriaAverage = 0.0f;
				foreach (CalificacionElemento calificacionElemento in calificacionElementos)
				{
                    int elementoPeso = calificacionElemento.Peso;
                    double elementoNota = calificacionElemento.Nota;
					categoriaAverage += elementoPeso * elementoNota / 100.0f;
				}
                int categoriaPeso = calificacionCategoria.Peso;
				notaAverage += categoriaPeso * categoriaAverage / 100.0f;
			}
			return notaAverage;
		}
    }
	
}
