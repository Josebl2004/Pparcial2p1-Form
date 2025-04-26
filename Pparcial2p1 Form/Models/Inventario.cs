namespace Pparcial2p1
{
    public class Inventario
    {
        public int Id { get; set; }
        public int JugadorId { get; set; }
        public int BloqueId { get; set; }
        public int Cantidad { get; set; }
        public string NombreJugador { get; set; }
        public string NombreBloque { get; set; }
        public string Tipo { get; internal set; }
        public string Rareza { get; internal set; }

        public override string ToString()
        {
            return $"ID: {Id}, Jugador: {NombreJugador}, Bloque: {NombreBloque}, Cantidad: {Cantidad}";
        }
    }
}