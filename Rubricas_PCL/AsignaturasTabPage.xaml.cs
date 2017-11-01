using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Rubricas_PCL
{
    public partial class AsignaturasTabPage : TabbedPage
    {

        public AsignaturasTabPage(Asignatura asignatura)
        {
            Children.Add(new EstudiantesDentroAsignaturasPage(asignatura.Uid));
            Children.Add(new EvaluacionesDentroAsignaturasPage(asignatura.Uid));
        }
    }
}
