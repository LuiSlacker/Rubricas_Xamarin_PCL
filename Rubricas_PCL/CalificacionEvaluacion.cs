using System;
namespace Rubricas_PCL
{
    public class CalificacionEvaluacion
    {
        public CalificacionEvaluacion(Estudiante estudiante)
        {
            estudianteUid = estudiante.Uid;
            estudianteNombre = estudiante.Name;
        }

        public float Nota { get; set; }
        public string estudianteUid { get; set; }
        public string estudianteNombre { get; set; }
    }
}
