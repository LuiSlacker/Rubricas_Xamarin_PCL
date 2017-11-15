using System;

namespace Rubricas_PCL
{
	public class Parcial
	{
		public float Nota { get; set; }
		public string Name { get; set; }

        public Parcial(string name, float nota)
        {
            this.Name = name;
            this.Nota = nota;

        }
	}
}
