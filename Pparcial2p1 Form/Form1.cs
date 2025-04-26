using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;
using Pparcial2p1;

namespace Pparcial2p1_Form
{
    public partial class Form1 : Form
    {
        private readonly JugadorService _jugadorService;
        private readonly InventarioService _inventarioService;
        private readonly BloqueService _bloqueService;

        private ComboBox cmbTipoBloque;
        private ComboBox cmbRarezaBloque;
        private DataGridView dgvInventario;

        public Form1(JugadorService jugadorService, InventarioService inventarioService, BloqueService bloqueService)
        {
            _jugadorService = jugadorService;
            _inventarioService = inventarioService;
            _bloqueService = bloqueService;

            InitializeComponent();

            // Asignar controles inicializados en Designer.cs
            cmbTipoBloque = cmbTipoBloqueControl;
            cmbRarezaBloque = cmbRarezaBloqueControl;
            dgvInventario = dgvInventarioControl;

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configuración inicial del formulario
            this.Text = "Gestión de Jugadores e Inventario";
            this.Size = new System.Drawing.Size(1000, 700);

            // Crear controles
            var lblNombre = new Label { Text = "Nombre:", Location = new System.Drawing.Point(20, 20) };
            var txtNombre = new TextBox { Name = "txtNombre", Location = new System.Drawing.Point(140, 20), Width = 200 };

            var lblNivel = new Label { Text = "Nivel:", Location = new System.Drawing.Point(20, 60) };
            var txtNivel = new TextBox { Name = "txtNivel", Location = new System.Drawing.Point(140, 60), Width = 200 };

            var btnRegistrar = new Button { Text = "Registrar", Location = new System.Drawing.Point(20, 100) };
            btnRegistrar.Click += (sender, e) => RegistrarJugador(txtNombre, txtNivel);

            var btnActualizar = new Button { Text = "Actualizar", Location = new System.Drawing.Point(120, 100) };
            btnActualizar.Click += (sender, e) => ActualizarJugador(txtNombre, txtNivel);

            var btnEliminar = new Button { Text = "Eliminar", Location = new System.Drawing.Point(220, 100) };
            btnEliminar.Click += (sender, e) => EliminarJugador();

            cmbTipoBloque.SelectedIndexChanged += (sender, e) => FiltrarBloques();
            cmbRarezaBloque.SelectedIndexChanged += (sender, e) => FiltrarBloques();

            // Agregar controles al formulario
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblNivel);
            this.Controls.Add(txtNivel);
            this.Controls.Add(btnRegistrar);
            this.Controls.Add(btnActualizar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(new Label { Text = "Filtrar por Tipo:", Location = new System.Drawing.Point(20, 470) });
            this.Controls.Add(cmbTipoBloque);
            this.Controls.Add(new Label { Text = "Filtrar por Rareza:", Location = new System.Drawing.Point(20, 510) });
            this.Controls.Add(cmbRarezaBloque);
            this.Controls.Add(dgvInventario);

            // Cargar datos iniciales
            CargarJugadores();
            CargarFiltros();
            CargarInventario();
        }

        private void RegistrarJugador(TextBox txtNombre, TextBox txtNivel)
        {
            try
            {
                var jugador = new Jugador
                {
                    Nombre = txtNombre.Text,
                    Nivel = int.TryParse(txtNivel.Text, out int nivel) ? nivel : 1
                };
                _jugadorService.Crear(jugador);
                MessageBox.Show("Jugador registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarJugadores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarJugador(TextBox txtNombre, TextBox txtNivel)
        {
            try
            {
                if (dgvInventario.SelectedRows.Count > 0)
                {
                    var filaSeleccionada = dgvInventario.SelectedRows[0];
                    var jugador = new Jugador
                    {
                        Id = (int)filaSeleccionada.Cells["Id"].Value,
                        Nombre = txtNombre.Text,
                        Nivel = int.TryParse(txtNivel.Text, out int nivel) ? nivel : 1
                    };
                    _jugadorService.Actualizar(jugador);
                    MessageBox.Show("Jugador actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarJugadores();
                }
                else
                {
                    MessageBox.Show("Seleccione un jugador para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarJugador()
        {
            try
            {
                if (dgvInventario.SelectedRows.Count > 0)
                {
                    var filaSeleccionada = dgvInventario.SelectedRows[0];
                    var idJugador = (int)filaSeleccionada.Cells["Id"].Value;
                    _jugadorService.Eliminar(idJugador);
                    MessageBox.Show("Jugador eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarJugadores();
                }
                else
                {
                    MessageBox.Show("Seleccione un jugador para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarJugadores()
        {
            try
            {
                var jugadores = _jugadorService.ObtenerTodos();
                dgvInventario.DataSource = jugadores;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarFiltros()
        {
            cmbTipoBloque.Items.Clear();
            cmbRarezaBloque.Items.Clear();

            var bloques = _bloqueService.ObtenerTodos();
            var tipos = bloques.Select(b => b.Tipo).Where(tipo => !string.IsNullOrEmpty(tipo)).Distinct().ToList();
            var rarezas = bloques.Select(b => b.Rareza).Where(rareza => !string.IsNullOrEmpty(rareza)).Distinct().ToList();

            cmbTipoBloque.Items.AddRange(tipos.ToArray());
            cmbRarezaBloque.Items.AddRange(rarezas.ToArray());
        }

        private void CargarInventario()
        {
            try
            {
                var inventarios = _inventarioService.ObtenerTodos();
                dgvInventario.DataSource = inventarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarBloques()
        {
            var tipoSeleccionado = cmbTipoBloque.SelectedItem?.ToString();
            var rarezaSeleccionada = cmbRarezaBloque.SelectedItem?.ToString();

            var bloques = _bloqueService.ObtenerTodos();

            if (!string.IsNullOrEmpty(tipoSeleccionado))
            {
                bloques = bloques.Where(b => b.Tipo == tipoSeleccionado).ToList();
            }

            if (!string.IsNullOrEmpty(rarezaSeleccionada))
            {
                bloques = bloques.Where(b => b.Rareza == rarezaSeleccionada).ToList();
            }

            dgvInventario.DataSource = bloques;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
