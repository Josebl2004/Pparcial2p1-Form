using System;

namespace Pparcial2p1
{
    public class MenuPrincipal
    {
        private readonly MenuJugador _menuJugador;
        private readonly MenuBloque _menuBloque;
        private readonly MenuInventario _menuInventario;

        public MenuPrincipal(MenuJugador menuJugador, MenuBloque menuBloque, MenuInventario menuInventario)
        {
            _menuJugador = menuJugador;
            _menuBloque = menuBloque;
            _menuInventario = menuInventario;
        }

        public void MostrarMenuPrincipal()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MostrarEncabezado("SISTEMA DE GESTIÓN DE MINECRAFT");

                Console.WriteLine("\nMENÚ PRINCIPAL:");
                Console.WriteLine("1. Gestionar Jugadores");
                Console.WriteLine("2. Gestionar Bloques");
                Console.WriteLine("3. Gestionar Inventario");
                Console.WriteLine("4. Salir");

                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        _menuJugador.MostrarMenu();
                        break;
                    case "2":
                        _menuBloque.MostrarMenu();
                        break;
                    case "3":
                        _menuInventario.MostrarMenu();
                        break;
                    case "4":
                        salir = true;
                        Console.WriteLine("\n¡Gracias por usar el Sistema de Gestión de Minecraft!");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MostrarEncabezado(string titulo)
        {
            string borde = new string('=', titulo.Length + 10);
            Console.WriteLine(borde);
            Console.WriteLine($"    {titulo}    ");
            Console.WriteLine(borde);
        }
    }
}
