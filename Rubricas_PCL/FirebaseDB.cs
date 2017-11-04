using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace Rubricas_PCL
{
    public static class FirebaseDB
    {
        private static FirebaseClient FIREBASE = Utils.FIREBASE;

        public static async Task<IList<Estudiante>> getEstudiantesForAsignatura(string asignaturaUid, IList<Estudiante> estudiantesCollection = null)
		{
            if (estudiantesCollection == null) estudiantesCollection = new ObservableCollection<Estudiante> { };

			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.ASIGNATURAS)
						.Child(asignaturaUid)
						.Child(Utils.FireBase_Entity.ESTUDIANTES)
						.OnceAsync<Estudiante>());

            estudiantesCollection.Clear();

			foreach (var item in list)
			{
				Estudiante estudiante = item.Object as Estudiante;
				estudiante.Uid = item.Key;
				estudiantesCollection.Add(estudiante);
			}
			return estudiantesCollection;
		}

		public static async Task<Estudiante> getEstudianteForId(string asignaturaUid, string estudianteUid)
		{
			var list = (await FIREBASE
                        .Child(Utils.FireBase_Entity.ASIGNATURAS)
                        .Child(asignaturaUid)
                        .Child(Utils.FireBase_Entity.ESTUDIANTES)
						.OnceAsync<Estudiante>());

			foreach (var item in list)
			{
				Estudiante estudiante = item.Object as Estudiante;
                if (item.Key == estudianteUid) return estudiante;
			}
			return null;
		}

        public static async Task<IList<Rubrica>> getRubricas(IList<Rubrica> rubricasCollection = null)
		{
            if (rubricasCollection == null) rubricasCollection = new ObservableCollection<Rubrica> { };

			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.RUBRICAS)
						.OnceAsync<Rubrica>());

			rubricasCollection.Clear();

			foreach (var item in list)
			{
				Rubrica rubrica = item.Object as Rubrica;
				rubrica.Uid = item.Key;
				rubricasCollection.Add(rubrica);
			}
			return rubricasCollection;
		}

        public static async Task<Rubrica> getRubricaForId(string rubricaUid)
		{
			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.RUBRICAS)
						.OnceAsync<Rubrica>());

			foreach (var item in list)
			{
				Rubrica rubrica = item.Object as Rubrica;
                if (item.Key == rubricaUid) return rubrica;
			}
            return null;
		}

        public static async Task<List<Categoria>> getCategoriasForRubrica(string rubricaUid)
		{
            var categorias = new List<Categoria>();
			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.RUBRICAS)
                        .Child(rubricaUid)
                        .Child(Utils.FireBase_Entity.CATEGORIAS)
                        .OnceAsync<Categoria>());

			foreach (var item in list)
			{
                Categoria categoria = item.Object as Categoria;
                categoria.Uid = item.Key;
                categorias.Add(categoria);
			}
			return categorias;
		}

        public static async Task<List<Elemento>> getElementosForCategoria(string rubricaUid, string categoriaUid)
		{
            var elementos = new List<Elemento>();
			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.RUBRICAS)
						.Child(rubricaUid)
						.Child(Utils.FireBase_Entity.CATEGORIAS)
                        .Child(categoriaUid)
                        .Child(Utils.FireBase_Entity.ELEMENTOS)
                        .OnceAsync<Elemento>());

			foreach (var item in list)
			{
                Elemento elemento = item.Object as Elemento;
				elemento.Uid = item.Key;
				elementos.Add(elemento);
			}
			return elementos;
		}

        public static async Task<IList<CalificacionEvaluacion>> getCalificacionesForEvaluacion(string asignaturaUid, string evaluacionUid, IList<CalificacionEvaluacion> calificacionCollection = null)
		{
            if(calificacionCollection == null) calificacionCollection = new ObservableCollection<CalificacionEvaluacion> { };
			
            var list = (await FIREBASE
                        .Child(Utils.FireBase_Entity.ASIGNATURAS)
						.Child(asignaturaUid)
                        .Child(Utils.FireBase_Entity.EVALUACIONES)
						.Child(evaluacionUid)
                        .Child(Utils.FireBase_Entity.CALIFICACION)
                        .OnceAsync<CalificacionEvaluacion>());

            calificacionCollection.Clear();

			foreach (var item in list)
			{
                CalificacionEvaluacion calificacion = item.Object as CalificacionEvaluacion;
				calificacion.Uid = item.Key;

                Estudiante estudiante = await FirebaseDB.getEstudianteForId(asignaturaUid, calificacion.EstudianteUid);
                calificacion.EstudianteNombre = estudiante.Name;
                calificacion.EstudianteApellido = estudiante.Apellido;
				calificacionCollection.Add(calificacion);
			}
			return calificacionCollection;
		}

        public static async Task<List<CalificacionCategoria>> getCategoriasForCalificacion(string asignaturaUid, string evaluacionUid, string calificacionUid)
		{
            var calificacionCategorias = new List<CalificacionCategoria>();
			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.ASIGNATURAS)
						.Child(asignaturaUid)
						.Child(Utils.FireBase_Entity.EVALUACIONES)
						.Child(evaluacionUid)
						.Child(Utils.FireBase_Entity.CALIFICACION)
                        .Child(calificacionUid)
                        .Child(Utils.FireBase_Entity.CATEGORIAS)
						.OnceAsync<CalificacionCategoria>());

			foreach (var item in list)
			{
				CalificacionCategoria calificacionCategoria = item.Object as CalificacionCategoria;
                calificacionCategoria.Uid = item.Key;
				calificacionCategorias.Add(calificacionCategoria);
			}
			return calificacionCategorias;
		}

		public static async Task<List<CalificacionElemento>> getElementForCalificacion(string asignaturaUid, string evaluacionUid, string calificacionUid, string categoriaUid)
		{
			var calificacionElementos = new List<CalificacionElemento>();
			var list = (await FIREBASE
						.Child(Utils.FireBase_Entity.ASIGNATURAS)
						.Child(asignaturaUid)
						.Child(Utils.FireBase_Entity.EVALUACIONES)
						.Child(evaluacionUid)
						.Child(Utils.FireBase_Entity.CALIFICACION)
						.Child(calificacionUid)
						.Child(Utils.FireBase_Entity.CATEGORIAS)
                        .Child(categoriaUid)
                        .Child(Utils.FireBase_Entity.ELEMENTOS)
                        .OnceAsync<CalificacionElemento>());

			foreach (var item in list)
			{
                CalificacionElemento calificacionElemento = item.Object as CalificacionElemento;
				calificacionElementos.Add(calificacionElemento);
			}
			return calificacionElementos;
		}
    }
}
