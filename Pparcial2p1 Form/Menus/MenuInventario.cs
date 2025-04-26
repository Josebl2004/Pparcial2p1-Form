using System;
using System.Collections.Generic;
using System.Linq;

namespace Pparcial2p1
{
    public class MenuInventario
    {
        private readonly JugadorService _jugadorService;
        private readonly BloqueService _bloqueService;
        private readonly InventarioService _inventarioService;

        public MenuInventario(JugadorService jugadorService, BloqueService bloqueService, InventarioService inventarioService)
        {
            _jugadorService = jugadorService;
            _bloqueService = bloqueService;
            _inventarioService = inventarioService;
        }

        public void MostrarMenu()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                MostrarEncabezado("GESTIÓN DE INVENTARIO");

                Console.WriteLine("\nOPCIONES DISPONIBLES:");
                Console.WriteLine("1. Agregar bloques al inventario");
                Console.WriteLine("2. Listar todo el inventario");
                Console.WriteLine("3. Ver inventario de un jugador");
                Console.WriteLine("4. Actualizar cantidad en inventario");
                Console.WriteLine("5. Eliminar elemento del inventario");
                Console.WriteLine("6. Exportar inventario a CSV"); // Nueva opción
                Console.WriteLine("7. Volver al menú principal");

                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarAInventario();
                        break;
                    case "2":
                        ListarInventario();
                        break;
                    case "3":
                        VerInventarioJugador();
                        break;
                    case "4":
                        ActualizarInventario();
                        break;
                    case "5":
                        EliminarDeInventario();
                        break;
                    case "6":
                        ExportarInventarioACSV(); // Nueva funcionalidad
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

        private void AgregarAInventario()
        {
            Console.Clear();
            MostrarEncabezado("AGREGAR BLOQUES AL INVENTARIO");

            var jugadores = _jugadorService.ObtenerTodos();
            if (jugadores.Count == 0)
            {
                Console.WriteLine("\nNo hay jugadores registrados. Primero debe registrar un jugador.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nJUGADORES DISPONIBLES:");
            foreach (var jugador in jugadores)
            {
                Console.WriteLine($"{jugador.Id}. {jugador.Nombre}");
            }

            Console.Write("\nSeleccione el ID del jugador: ");
            if (!int.TryParse(Console.ReadLine(), out int jugadorId) || _jugadorService.ObtenerPorId(jugadorId) == null)
            {
                Console.WriteLine("\nID de jugador inválido.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var bloques = _bloqueService.ObtenerTodos();
            if (bloques.Count == 0)
            {
                Console.WriteLine("\nNo hay bloques registrados. Primero debe registrar un bloque.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nBLOQUES DISPONIBLES:");
            foreach (var bloque in bloques)
            {
                Console.WriteLine($"{bloque.Id}. {bloque.Nombre} (Tipo: {bloque.Tipo}, Rareza: {bloque.Rareza})");
            }

            Console.Write("\nSeleccione el ID del bloque: ");
            if (!int.TryParse(Console.ReadLine(), out int bloqueId) || _bloqueService.ObtenerPorId(bloqueId) == null)
            {
                Console.WriteLine("\nID de bloque inválido.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("\nCantidad a agregar: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("\nCantidad inválida. Debe ser un número positivo.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var inventario = new Inventario
            {
                JugadorId = jugadorId,
                BloqueId = bloqueId,
                Cantidad = cantidad
            };

            _inventarioService.Agregar(inventario);

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ListarInventario()
        {
            Console.Clear();
            MostrarEncabezado("LISTA COMPLETA DE INVENTARIO");

            var inventarios = _inventarioService.ObtenerTodos();

            if (inventarios.Count == 0)
            {
                Console.WriteLine("\nNo hay elementos en el inventario.");
            }
            else
            {
                Console.WriteLine("\nELEMENTOS EN INVENTARIO:");
                foreach (var inventario in inventarios)
                {
                    Console.WriteLine($"Jugador: {inventario.NombreJugador} - Bloque: {inventario.NombreBloque} - Cantidad: {inventario.Cantidad}");
                }
                Console.WriteLine($"\nTotal de registros de inventario: {inventarios.Count}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void VerInventarioJugador()
        {
            Console.Clear();
            MostrarEncabezado("VER INVENTARIO DE JUGADOR");

            var jugadores = _jugadorService.ObtenerTodos();
            if (jugadores.Count == 0)
            {
                Console.WriteLine("\nNo hay jugadores registrados.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nJUGADORES DISPONIBLES:");
            foreach (var jugador in jugadores)
            {
                Console.WriteLine($"{jugador.Id}. {jugador.Nombre}");
            }

            Console.Write("\nSeleccione el ID del jugador: ");
            if (int.TryParse(Console.ReadLine(), out int jugadorId))
            {
                var jugador = _jugadorService.ObtenerPorId(jugadorId);

                if (jugador != null)
                {
                    Console.WriteLine($"\nInventario de {jugador.Nombre} (Nivel {jugador.Nivel}):");

                    var inventario = _inventarioService.ObtenerPorJugador(jugadorId);

                    if (inventario.Count == 0)
                    {
                        Console.WriteLine("\nEste jugador no tiene bloques en su inventario.");
                    }
                    else
                    {
                        foreach (var item in inventario)
                        {
                            Console.WriteLine($"- {item.Cantidad} {item.NombreBloque}");
                        }

                        int totalBloques = inventario.Sum(i => i.Cantidad);
                        Console.WriteLine($"\nTotal de bloques: {totalBloques}");
                        Console.WriteLine($"Total de tipos de bloques: {inventario.Count}");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un jugador con ID {jugadorId}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarInventario()
        {
            Console.Clear();
            MostrarEncabezado("ACTUALIZAR CANTIDAD EN INVENTARIO");

            var inventarios = _inventarioService.ObtenerTodos();

            if (inventarios.Count == 0)
            {
                Console.WriteLine("\nNo hay elementos en el inventario.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nELEMENTOS EN INVENTARIO:");
            foreach (var inv in inventarios)
            {
                Console.WriteLine($"ID: {inv.Id} - Jugador: {inv.NombreJugador} - Bloque: {inv.NombreBloque} - Cantidad: {inv.Cantidad}");
            }

            Console.Write("\nIngrese el ID del registro a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var inventario = inventarios.FirstOrDefault(i => i.Id == id);

                if (inventario != null)
                {
                    Console.WriteLine($"\nVa a actualizar: {inventario.NombreJugador} - {inventario.NombreBloque} - Cantidad actual: {inventario.Cantidad}");

                    Console.Write("\nNueva cantidad: ");
                    if (int.TryParse(Console.ReadLine(), out int nuevaCantidad) && nuevaCantidad > 0)
                    {
                        inventario.Cantidad = nuevaCantidad;
                        _inventarioService.Actualizar(inventario);
                    }
                    else
                    {
                        Console.WriteLine("\nCantidad inválida. Debe ser un número positivo.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un registro de inventario con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarDeInventario()
        {
            Console.Clear();
            MostrarEncabezado("ELIMINAR ELEMENTO DEL INVENTARIO");

            var inventarios = _inventarioService.ObtenerTodos();

            if (inventarios.Count == 0)
            {
                Console.WriteLine("\nNo hay elementos en el inventario.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nELEMENTOS EN INVENTARIO:");
            foreach (var inv in inventarios)
            {
                Console.WriteLine($"ID: {inv.Id} - Jugador: {inv.NombreJugador} - Bloque: {inv.NombreBloque} - Cantidad: {inv.Cantidad}");
            }

            Console.Write("\nIngrese el ID del registro a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var inventario = inventarios.FirstOrDefault(i => i.Id == id);

                if (inventario != null)
                {
                    Console.WriteLine($"\nVa a eliminar: {inventario.NombreJugador} - {inventario.NombreBloque} - Cantidad: {inventario.Cantidad}");

                    Console.Write("\n¿Está seguro de eliminar este registro del inventario? (S/N): ");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        _inventarioService.Eliminar(id);
                    }
                    else
                    {
                        Console.WriteLine("\nOperación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un registro de inventario con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ExportarInventarioACSV()
        {
            Console.Clear();
            MostrarEncabezado("EXPORTAR INVENTARIO A CSV");

            string filePath = "c:\\Users\\Mario\\OneDrive\\Escritorio\\PROGRAMACION 1\\Pparcial2p1 consola\\Pparcial2p1 consola\\ExportedFiles\\Minecraft.csv";

            _inventarioService.ExportarAInventarioCSV(filePath);

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