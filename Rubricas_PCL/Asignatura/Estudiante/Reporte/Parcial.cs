using System;

namespace Rubricas_PCL
{
	public class Parcial
	{
		public double Nota { get; set; }
		public string Name { get; set; }

        public Parcial(string name, double nota)
        {
            this.Name = name;
            this.Nota = nota;

        }
	}
}
