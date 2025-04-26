using System;
using System.Collections.Generic;
using System.Linq;

namespace Pparcial2p1
{
    public class MenuJugador
    {
        private readonly JugadorService _jugadorService;
        private readonly InventarioService _inventarioService;

        public MenuJugador(JugadorService jugadorService, InventarioService inventarioService)
        {
            _jugadorService = jugadorService;
            _inventarioService = inventarioService;
        }

        public void MostrarMenu()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                MostrarEncabezado("GESTIÓN DE JUGADORES");

                Console.WriteLine("\nOPCIONES DISPONIBLES:");
                Console.WriteLine("1. Registrar nuevo jugador");
                Console.WriteLine("2. Listar todos los jugadores");
                Console.WriteLine("3. Buscar jugador por ID");
                Console.WriteLine("4. Actualizar jugador");
                Console.WriteLine("5. Eliminar jugador");
                Console.WriteLine("6. Exportar jugadores a CSV"); // Nueva opción
                Console.WriteLine("7. Volver al menú principal");

                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarJugador();
                        break;
                    case "2":
                        ListarJugadores();
                        break;
                    case "3":
                        BuscarJugadorPorId();
                        break;
                    case "4":
                        ActualizarJugador();
                        break;
                    case "5":
                        EliminarJugador();
                        break;
                    case "6":
                        ExportarJugadoresACSV(); // Nueva funcionalidad
                        break;
                    case "7":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void RegistrarJugador()
        {
            Console.Clear();
            MostrarEncabezado("REGISTRAR NUEVO JUGADOR");

            var jugador = new Jugador();

            Console.Write("\nNombre del jugador: ");
            jugador.Nombre = Console.ReadLine();

            Console.Write("Nivel inicial (deje en blanco para nivel 1): ");
            string nivelStr = Console.ReadLine();
            jugador.Nivel = string.IsNullOrEmpty(nivelStr) ? 1 : int.Parse(nivelStr);

            try
            {
                _jugadorService.Crear(jugador);
                Console.WriteLine("\n¡Jugador registrado con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al registrar el jugador: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ListarJugadores()
        {
            Console.Clear();
            MostrarEncabezado("LISTA DE JUGADORES");

            var jugadores = _jugadorService.ObtenerTodos();

            if (jugadores.Count == 0)
            {
                Console.WriteLine("\nNo hay jugadores registrados.");
            }
            else
            {
                Console.WriteLine("\nJUGADORES REGISTRADOS:");
                foreach (var jugador in jugadores)
                {
                    Console.WriteLine(jugador);
                }
                Console.WriteLine($"\nTotal de jugadores: {jugadores.Count}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarJugadorPorId()
        {
            Console.Clear();
            MostrarEncabezado("BUSCAR JUGADOR POR ID");

            Console.Write("\nIngrese el ID del jugador: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var jugador = _jugadorService.ObtenerPorId(id);

                if (jugador != null)
                {
                    Console.WriteLine("\nJugador encontrado:");
                    Console.WriteLine(jugador);

                    var inventario = _inventarioService.ObtenerPorJugador(jugador.Id);
                    if (inventario.Count > 0)
                    {
                        Console.WriteLine("\nInventario del jugador:");
                        foreach (var item in inventario)
                        {
                            Console.WriteLine($"- {item.Cantidad} {item.NombreBloque}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nEste jugador no tiene bloques en su inventario.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un jugador con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarJugador()
        {
            Console.Clear();
            MostrarEncabezado("ACTUALIZAR JUGADOR");

            Console.Write("\nIngrese el ID del jugador a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var jugador = _jugadorService.ObtenerPorId(id);

                if (jugador != null)
                {
                    Console.WriteLine("\nJugador encontrado:");
                    Console.WriteLine(jugador);

                    Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para mantener el valor actual):");

                    Console.Write($"Nombre ({jugador.Nombre}): ");
                    string nombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nombre))
                        jugador.Nombre = nombre;

                    Console.Write($"Nivel ({jugador.Nivel}): ");
                    string nivelStr = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nivelStr) && int.TryParse(nivelStr, out int nivel))
                        jugador.Nivel = nivel;

                    _jugadorService.Actualizar(jugador);
                    Console.WriteLine("\n¡Jugador actualizado con éxito!");
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un jugador con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarJugador()
        {
            Console.Clear();
            MostrarEncabezado("ELIMINAR JUGADOR");

            Console.Write("\nIngrese el ID del jugador a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var jugador = _jugadorService.ObtenerPorId(id);

                if (jugador != null)
                {
                    Console.WriteLine("\nJugador a eliminar:");
                    Console.WriteLine(jugador);

                    Console.Write("\n¿Está seguro de eliminar este jugador? (S/N): ");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        _jugadorService.Eliminar(id);
                    }
                    else
                    {
                        Console.WriteLine("\nOperación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un jugador con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ExportarJugadoresACSV()
        {
            Console.Clear();
            MostrarEncabezado("EXPORTAR JUGADORES A CSV");

            string filePath = "c:\\Users\\Mario\\OneDrive\\Escritorio\\PROGRAMACION 1\\Pparcial2p1 consola\\Pparcial2p1 consola\\ExportedFiles\\Minecraft.csv";

            _jugadorService.ExportarAJugadoresCSV(filePath);

            Console.WriteLine($"\nDatos exportados correctamente a {filePath}");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
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