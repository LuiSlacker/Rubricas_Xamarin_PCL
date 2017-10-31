using System;
using Firebase.Xamarin.Database;
namespace Rubricas_PCL
{
    public static class Utils
    {
        public static FirebaseClient FIREBASE = new FirebaseClient("https://rubricas-70761.firebaseio.com/");

        public static class FireBase_Entity {
            public static string ASIGNATURAS = "asignaturas";
            public static string ESTUDIANTES = "estudiantes";
            public static string RUBRICAS = "rubricas";
        } 
        
    }
}
