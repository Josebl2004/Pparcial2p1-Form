using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Pparcial2p1
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public DateTime FechaCreacion { get; set; }
        public object Tipo { get; internal set; }
        public object Rareza { get; internal set; }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Nivel: {Nivel}, Creado: {FechaCreacion.ToShortDateString()}";
        }
    }
}
