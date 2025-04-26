using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Data.SqlClient;

namespace Pparcial2p1
{
    public class InventarioService
    {
        private readonly DatabaseManager _dbManager;
        private readonly JugadorService _jugadorService;
        private readonly BloqueService _bloqueService;

        public InventarioService(DatabaseManager dbManager, JugadorService jugadorService, BloqueService bloqueService)
        {
            _dbManager = dbManager;
            _jugadorService = jugadorService;
            _bloqueService = bloqueService;
        }

        public void Agregar(Inventario inventario)
        {
            try
            {
                if (inventario.Cantidad <= 0)
                {
                    Console.WriteLine("Error: La cantidad debe ser un valor positivo.");
                    return;
                }

                var jugador = _jugadorService.ObtenerPorId(inventario.JugadorId);
                if (jugador == null)
                {
                    Console.WriteLine($"Error: No existe un jugador con ID {inventario.JugadorId}");
                    return;
                }

                var bloque = _bloqueService.ObtenerPorId(inventario.BloqueId);
                if (bloque == null)
                {
                    Console.WriteLine($"Error: No existe un bloque con ID {inventario.BloqueId}");
                    return;
                }

                using var connection = _dbManager.GetConnection();
                connection.Open();
                var checkCommand = new SqlCommand(
                    "SELECT Id, Cantidad FROM Inventario WHERE JugadorId = @JugadorId AND BloqueId = @BloqueId",
                    connection);
                checkCommand.Parameters.AddWithValue("@JugadorId", inventario.JugadorId);
                checkCommand.Parameters.AddWithValue("@BloqueId", inventario.BloqueId);

                using var reader = checkCommand.ExecuteReader();
                if (reader.Read())
                {
                    int existingId = reader.GetInt32(0);
                    int existingCantidad = reader.GetInt32(1);
                    reader.Close();

                    var updateCommand = new SqlCommand(
                        "UPDATE Inventario SET Cantidad = @Cantidad WHERE Id = @Id",
                        connection);
                    updateCommand.Parameters.AddWithValue("@Id", existingId);
                    updateCommand.Parameters.AddWithValue("@Cantidad", existingCantidad + inventario.Cantidad);
                    updateCommand.ExecuteNonQuery();

                    Console.WriteLine($"¡Se actualizó el inventario! Nuevo total: {existingCantidad + inventario.Cantidad} {bloque.Nombre}");
                }
                else
                {
                    reader.Close();
                    var insertCommand = new SqlCommand(
                        "INSERT INTO Inventario (JugadorId, BloqueId, Cantidad) VALUES (@JugadorId, @BloqueId, @Cantidad); SELECT SCOPE_IDENTITY();",
                        connection);
                    insertCommand.Parameters.AddWithValue("@JugadorId", inventario.JugadorId);
                    insertCommand.Parameters.AddWithValue("@BloqueId", inventario.BloqueId);
                    insertCommand.Parameters.AddWithValue("@Cantidad", inventario.Cantidad);

                    inventario.Id = Convert.ToInt32(insertCommand.ExecuteScalar());
                    Console.WriteLine($"¡Se agregó {inventario.Cantidad} {bloque.Nombre} al inventario de {jugador.Nombre}!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar al inventario: {ex.Message}");
            }
        }

        public List<Inventario> ObtenerTodos()
        {
            var inventarios = new List<Inventario>();
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    @"SELECT i.Id, i.JugadorId, i.BloqueId, i.Cantidad, j.Nombre AS NombreJugador, b.Nombre AS NombreBloque
                FROM Inventario i
                INNER JOIN Jugadores j ON i.JugadorId = j.Id
                INNER JOIN Bloques b ON i.BloqueId = b.Id",
                    connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    inventarios.Add(new Inventario
                    {
                        Id = reader.GetInt32(0),
                        JugadorId = reader.GetInt32(1),
                        BloqueId = reader.GetInt32(2),
                        Cantidad = reader.GetInt32(3),
                        NombreJugador = reader.GetString(4),
                        NombreBloque = reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener inventarios: {ex.Message}");
            }
            return inventarios;
        }

        public List<Inventario> ObtenerPorJugador(int jugadorId)
        {
            var inventarios = new List<Inventario>();
            try
            {
                var jugador = _jugadorService.ObtenerPorId(jugadorId);
                if (jugador == null)
                {
                    Console.WriteLine($"Error: No existe un jugador con ID {jugadorId}");
                    return inventarios;
                }

                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    @"SELECT i.Id, i.JugadorId, i.BloqueId, i.Cantidad, j.Nombre AS NombreJugador, b.Nombre AS NombreBloque
                FROM Inventario i
                INNER JOIN Jugadores j ON i.JugadorId = j.Id
                INNER JOIN Bloques b ON i.BloqueId = b.Id
                WHERE i.JugadorId = @JugadorId",
                    connection);
                command.Parameters.AddWithValue("@JugadorId", jugadorId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    inventarios.Add(new Inventario
                    {
                        Id = reader.GetInt32(0),
                        JugadorId = reader.GetInt32(1),
                        BloqueId = reader.GetInt32(2),
                        Cantidad = reader.GetInt32(3),
                        NombreJugador = reader.GetString(4),
                        NombreBloque = reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener inventario del jugador: {ex.Message}");
            }
            return inventarios;
        }

        public void Actualizar(Inventario inventario)
        {
            try
            {
                if (inventario.Cantidad <= 0)
                {
                    Console.WriteLine("Error: La cantidad debe ser un valor positivo.");
                    return;
                }

                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Inventario SET Cantidad = @Cantidad WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", inventario.Id);
                command.Parameters.AddWithValue("@Cantidad", inventario.Cantidad);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("¡Inventario actualizado con éxito!");
                else
                    Console.WriteLine("No se encontró el registro de inventario para actualizar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar inventario: {ex.Message}");
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "DELETE FROM Inventario WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("¡Elemento del inventario eliminado con éxito!");
                else
                    Console.WriteLine("No se encontró el elemento del inventario para eliminar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar de inventario: {ex.Message}");
            }
        }

        public void ExportarAInventarioCSV(string filePath)
        {
            try
            {
                Console.WriteLine("Exportando inventario a CSV...");
                var inventarios = ObtenerTodos()
                    .OrderBy(i => i.NombreJugador)
                    .ThenBy(i => i.NombreBloque)
                    .ToList();

                using var writer = new StreamWriter("C:\\Users\\Mario\\OneDrive\\Escritorio\\PROGRAMACION 1\\Pparcial2p1 Form\\Pparcial2p1 Form\\Inventario.csv");
                // Escribir encabezado
                writer.WriteLine("Jugador,Bloque,Cantidad");

                foreach (var inventario in inventarios)
                {
                    // Escapar comillas y manejar comas en los datos
                    string nombreJugador = $"\"{inventario.NombreJugador.Replace("\"", "\"\"")}\"";
                    string nombreBloque = $"\"{inventario.NombreBloque.Replace("\"", "\"\"")}\"";
                    writer.WriteLine($"{nombreJugador},{nombreBloque},{inventario.Cantidad}");
                }

                Console.WriteLine($"¡Éxito! Se exportaron {inventarios.Count} registros a {@"C:\\Users\\Mario\\OneDrive\\Escritorio\\PROGRAMACION 1\\Pparcial2p1 Form\\Pparcial2p1 Form\\Inventario.csv"}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar datos: {ex.Message}");
            }
        }
    }
}