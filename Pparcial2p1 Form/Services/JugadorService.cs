using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;

namespace Pparcial2p1
{
    public class JugadorService
    {
        private readonly DatabaseManager _dbManager;

        public JugadorService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        public void Crear(Jugador jugador)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jugador.Nombre))
                {
                    throw new ArgumentException("El nombre del jugador no puede estar vacío.");
                }

                if (jugador.Nivel <= 0)
                {
                    throw new ArgumentException("El nivel del jugador debe ser mayor a 0.");
                }

                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Jugadores (Nombre, Nivel) VALUES (@Nombre, @Nivel); SELECT SCOPE_IDENTITY();",
                    connection);
                command.Parameters.AddWithValue("@Nombre", jugador.Nombre);
                command.Parameters.AddWithValue("@Nivel", jugador.Nivel);

                jugador.Id = Convert.ToInt32(command.ExecuteScalar());
                Console.WriteLine($"¡Jugador registrado con ID: {jugador.Id}!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error de validación: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear jugador: {ex.Message}");
                throw;
            }
        }

        public List<Jugador> ObtenerTodos()
        {
            var jugadores = new List<Jugador>();
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "SELECT Id, Nombre, Nivel, FechaCreacion FROM Jugadores",
                    connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    jugadores.Add(new Jugador
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Nivel = reader.GetInt32(2),
                        FechaCreacion = reader.GetDateTime(3)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener jugadores: {ex.Message}");
            }
            return jugadores;
        }

        public Jugador ObtenerPorId(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "SELECT Id, Nombre, Nivel, FechaCreacion FROM Jugadores WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Jugador
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Nivel = reader.GetInt32(2),
                        FechaCreacion = reader.GetDateTime(3)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener jugador: {ex.Message}");
            }
            return null;
        }

        public void Actualizar(Jugador jugador)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Jugadores SET Nombre = @Nombre, Nivel = @Nivel WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", jugador.Id);
                command.Parameters.AddWithValue("@Nombre", jugador.Nombre);
                command.Parameters.AddWithValue("@Nivel", jugador.Nivel);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("¡Jugador actualizado con éxito!");
                else
                    Console.WriteLine("No se encontró el jugador para actualizar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar jugador: {ex.Message}");
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var checkCommand = new SqlCommand(
                    "SELECT COUNT(*) FROM Inventario WHERE JugadorId = @Id",
                    connection);
                checkCommand.Parameters.AddWithValue("@Id", id);
                int inventoryCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (inventoryCount > 0)
                {
                    Console.WriteLine("No se puede eliminar el jugador porque tiene elementos en su inventario.");
                    Console.WriteLine($"Elimina primero los {inventoryCount} elementos del inventario asociados a este jugador.");
                    return;
                }

                var deleteCommand = new SqlCommand(
                    "DELETE FROM Jugadores WHERE Id = @Id",
                    connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);

                int rowsAffected = deleteCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("¡Jugador eliminado con éxito!");
                else
                    Console.WriteLine("No se encontró el jugador para eliminar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar jugador: {ex.Message}");
            }
        }

        public void ExportarAJugadoresCSV(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentException("La ruta del archivo no puede estar vacía.");
                }

                var jugadores = ObtenerTodos();
                using var writer = new StreamWriter(filePath);
                // Escribir encabezado
                writer.WriteLine("Id,Nombre,Nivel,FechaCreacion");

                foreach (var jugador in jugadores)
                {
                    // Escapar comillas y manejar comas en los datos
                    string nombre = $"\"{jugador.Nombre.Replace("\"", "\"\"")}\"";
                    string fechaCreacion = $"\"{jugador.FechaCreacion:yyyy-MM-dd}\"";
                    writer.WriteLine($"{jugador.Id},{nombre},{jugador.Nivel},{fechaCreacion}");
                }

                Console.WriteLine($"Datos exportados correctamente a {filePath}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error de validación: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar datos: {ex.Message}");
            }
        }
    }

}