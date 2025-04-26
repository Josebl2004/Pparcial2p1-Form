using System;
using System.Windows.Forms;

namespace Pparcial2p1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar servicios
            var dbManager = new DatabaseManager();
            var jugadorService = new JugadorService(dbManager);
            var bloqueService = new BloqueService(dbManager);
            var inventarioService = new InventarioService(dbManager, jugadorService, bloqueService);

            // Iniciar el formulario principal con todas las dependencias necesarias
            Application.Run(new Pparcial2p1_Form.Form1(jugadorService, inventarioService, bloqueService));
        }
    }
}