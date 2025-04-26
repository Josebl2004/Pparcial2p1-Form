using System;
using System.Collections.Generic;


namespace Pparcial2p1
{
    public class MenuBloque
    {
        private readonly BloqueService _bloqueService;

        public MenuBloque(BloqueService bloqueService)
        {
            _bloqueService = bloqueService;
        }

        public void MostrarMenu()
        {
            bool volver = false;

            while (!volver)
            {
                Console.Clear();
                MostrarEncabezado("GESTIÓN DE BLOQUES");

                Console.WriteLine("\nOPCIONES DISPONIBLES:");
                Console.WriteLine("1. Registrar nuevo bloque");
                Console.WriteLine("2. Listar todos los bloques");
                Console.WriteLine("3. Buscar bloque por ID");
                Console.WriteLine("4. Buscar bloques por tipo");
                Console.WriteLine("5. Buscar bloques por rareza");
                Console.WriteLine("6. Actualizar bloque");
                Console.WriteLine("7. Eliminar bloque");
                Console.WriteLine("8. Exportar bloques a CSV"); // Nueva opción
                Console.WriteLine("9. Volver al menú principal");

                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarBloque();
                        break;
                    case "2":
                        ListarBloques();
                        break;
                    case "3":
                        BuscarBloquePorId();
                        break;
                    case "4":
                        BuscarBloquePorTipo();
                        break;
                    case "5":
                        BuscarBloquePorRareza();
                        break;
                    case "6":
                        ActualizarBloque();
                        break;
                    case "7":
                        EliminarBloque();
                        break;
                    case "8":
                        ExportarBloquesACSV(); // Nueva funcionalidad
                        break;
                    case "9":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void RegistrarBloque()
        {
            Console.Clear();
            MostrarEncabezado("REGISTRAR NUEVO BLOQUE");

            var bloque = new Bloque();

            Console.Write("\nNombre del bloque: ");
            bloque.Nombre = Console.ReadLine();

            Console.Write("Tipo (Mineral, Madera, Piedra, Decoración, etc.): ");
            bloque.Tipo = Console.ReadLine();

            Console.Write("Rareza (Común, Raro, Épico, Legendario, etc.): ");
            bloque.Rareza = Console.ReadLine();

            _bloqueService.Crear(bloque);

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ListarBloques()
        {
            Console.Clear();
            MostrarEncabezado("LISTA DE BLOQUES");

            var bloques = _bloqueService.ObtenerTodos();

            if (bloques.Count == 0)
            {
                Console.WriteLine("\nNo hay bloques registrados.");
            }
            else
            {
                Console.WriteLine("\nBLOQUES REGISTRADOS:");
                foreach (var bloque in bloques)
                {
                    Console.WriteLine(bloque);
                }
                Console.WriteLine($"\nTotal de bloques: {bloques.Count}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarBloquePorId()
        {
            Console.Clear();
            MostrarEncabezado("BUSCAR BLOQUE POR ID");

            Console.Write("\nIngrese el ID del bloque: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var bloque = _bloqueService.ObtenerPorId(id);

                if (bloque != null)
                {
                    Console.WriteLine("\nBloque encontrado:");
                    Console.WriteLine(bloque);
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un bloque con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarBloquePorTipo()
        {
            Console.Clear();
            MostrarEncabezado("BUSCAR BLOQUES POR TIPO");

            Console.Write("\nIngrese el tipo de bloque a buscar: ");
            string tipo = Console.ReadLine();

            var bloques = _bloqueService.BuscarPorTipo(tipo);

            if (bloques.Count == 0)
            {
                Console.WriteLine($"\nNo se encontraron bloques del tipo '{tipo}'.");
            }
            else
            {
                Console.WriteLine($"\nBloques encontrados del tipo '{tipo}':");
                foreach (var bloque in bloques)
                {
                    Console.WriteLine(bloque);
                }
                Console.WriteLine($"\nTotal de bloques encontrados: {bloques.Count}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarBloquePorRareza()
        {
            Console.Clear();
            MostrarEncabezado("BUSCAR BLOQUES POR RAREZA");

            Console.Write("\nIngrese la rareza de bloque a buscar: ");
            string rareza = Console.ReadLine();

            var bloques = _bloqueService.BuscarPorRareza(rareza);

            if (bloques.Count == 0)
            {
                Console.WriteLine($"\nNo se encontraron bloques con rareza '{rareza}'.");
            }
            else
            {
                Console.WriteLine($"\nBloques encontrados con rareza '{rareza}':");
                foreach (var bloque in bloques)
                {
                    Console.WriteLine(bloque);
                }
                Console.WriteLine($"\nTotal de bloques encontrados: {bloques.Count}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarBloque()
        {
            Console.Clear();
            MostrarEncabezado("ACTUALIZAR BLOQUE");

            Console.Write("\nIngrese el ID del bloque a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var bloque = _bloqueService.ObtenerPorId(id);

                if (bloque != null)
                {
                    Console.WriteLine("\nBloque encontrado:");
                    Console.WriteLine(bloque);

                    Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para mantener el valor actual):");

                    Console.Write($"Nombre ({bloque.Nombre}): ");
                    string nombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nombre))
                        bloque.Nombre = nombre;

                    Console.Write($"Tipo ({bloque.Tipo}): ");
                    string tipo = Console.ReadLine();
                    if (!string.IsNullOrEmpty(tipo))
                        bloque.Tipo = tipo;

                    Console.Write($"Rareza ({bloque.Rareza}): ");
                    string rareza = Console.ReadLine();
                    if (!string.IsNullOrEmpty(rareza))
                        bloque.Rareza = rareza;

                    _bloqueService.Actualizar(bloque);
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un bloque con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarBloque()
        {
            Console.Clear();
            MostrarEncabezado("ELIMINAR BLOQUE");

            Console.Write("\nIngrese el ID del bloque a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var bloque = _bloqueService.ObtenerPorId(id);

                if (bloque != null)
                {
                    Console.WriteLine("\nBloque a eliminar:");
                    Console.WriteLine(bloque);

                    Console.Write("\n¿Está seguro de eliminar este bloque? (S/N): ");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        _bloqueService.Eliminar(id);
                    }
                    else
                    {
                        Console.WriteLine("\nOperación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró un bloque con ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ExportarBloquesACSV()
        {
            Console.Clear();
            MostrarEncabezado("EXPORTAR BLOQUES A CSV");

            string filePath = "c:\\Users\\Mario\\OneDrive\\Escritorio\\PROGRAMACION 1\\Pparcial2p1 consola\\Pparcial2p1 consola\\ExportedFiles\\Minecraft.csv";

            _bloqueService.ExportarABloquesCSV(filePath);

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