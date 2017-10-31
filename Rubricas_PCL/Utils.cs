using System;
using Firebase.Xamarin.Database;
namespace Rubricas_PCL
{
    public static class Utils
    {
        public static FirebaseClient FIREBASE = new FirebaseClient("https://rubricas-70761.firebaseio.com/");

        public static class Entity {
            public static string FIRE_ASIGNATURAS = "asignaturas";
        } 
        
    }
}
