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
    }
}
