namespace Pparcial2p1_Form
{
    partial class Form1
    {
        // Renamed one of the conflicting fields to resolve ambiguity
        private System.Windows.Forms.DataGridView dgvInventarioControl;
        private System.Windows.Forms.ComboBox cmbTipoBloqueControl; // Renamed to resolve ambiguity
        private System.Windows.Forms.ComboBox cmbRarezaBloqueControl; // Renamed to resolve ambiguity

        private void InitializeComponent()
        {
            dgvInventarioControl = new DataGridView();
            cmbTipoBloqueControl = new ComboBox();
            cmbRarezaBloqueControl = new ComboBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvInventarioControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            SuspendLayout();
            // 
            // dgvInventarioControl
            // 
            dgvInventarioControl.ColumnHeadersHeight = 29;
            dgvInventarioControl.Location = new Point(20, 150);
            dgvInventarioControl.Name = "dgvInventarioControl";
            dgvInventarioControl.RowHeadersWidth = 51;
            dgvInventarioControl.Size = new Size(950, 300);
            dgvInventarioControl.TabIndex = 0;
            // 
            // cmbTipoBloqueControl
            // 
            cmbTipoBloqueControl.Location = new Point(150, 470);
            cmbTipoBloqueControl.Name = "cmbTipoBloqueControl";
            cmbTipoBloqueControl.Size = new Size(200, 28);
            cmbTipoBloqueControl.TabIndex = 1;
            // 
            // cmbRarezaBloqueControl
            // 
            cmbRarezaBloqueControl.Location = new Point(150, 510);
            cmbRarezaBloqueControl.Name = "cmbRarezaBloqueControl";
            cmbRarezaBloqueControl.Size = new Size(200, 28);
            cmbRarezaBloqueControl.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonShadow;
            pictureBox1.Image = Properties.Resources.Arena;
            pictureBox1.Location = new Point(592, 470);
            pictureBox1.MaximumSize = new Size(200, 200);
            pictureBox1.MinimumSize = new Size(100, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(149, 153);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Terracota;
            pictureBox2.Location = new Point(821, 470);
            pictureBox2.MaximumSize = new Size(200, 200);
            pictureBox2.MinimumSize = new Size(100, 100);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(149, 153);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox1_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Piedra;
            pictureBox3.Location = new Point(1020, 428);
            pictureBox3.MaximumSize = new Size(200, 200);
            pictureBox3.MinimumSize = new Size(100, 100);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(149, 153);
            pictureBox3.TabIndex = 12;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox1_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImage = Properties.Resources.Fondos_para_Zoom;
            pictureBox4.BorderStyle = BorderStyle.Fixed3D;
            pictureBox4.Image = Properties.Resources.Mesa_de_creacion;
            pictureBox4.Location = new Point(1020, 242);
            pictureBox4.MaximumSize = new Size(200, 200);
            pictureBox4.MinimumSize = new Size(100, 100);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(149, 153);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 12;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox1_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Tierra;
            pictureBox5.Location = new Point(1020, 57);
            pictureBox5.MaximumSize = new Size(200, 200);
            pictureBox5.MinimumSize = new Size(100, 100);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(149, 153);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 12;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox1_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Magma_Block;
            pictureBox6.Location = new Point(821, 0);
            pictureBox6.MaximumSize = new Size(200, 200);
            pictureBox6.MinimumSize = new Size(100, 100);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(149, 153);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 12;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox1_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = Properties.Resources.Hielo;
            pictureBox7.Location = new Point(592, 0);
            pictureBox7.MaximumSize = new Size(200, 200);
            pictureBox7.MinimumSize = new Size(100, 100);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(149, 153);
            pictureBox7.TabIndex = 12;
            pictureBox7.TabStop = false;
            pictureBox7.Click += pictureBox1_Click;
            // 
            // Form1
            // 
            BackgroundImage = Properties.Resources.Fondos_para_Zoom;
            ClientSize = new Size(1555, 676);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(dgvInventarioControl);
            Controls.Add(cmbTipoBloqueControl);
            Controls.Add(cmbRarezaBloqueControl);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvInventarioControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ResumeLayout(false);
        }
        internal PictureBox pictureBox1;
        internal PictureBox pictureBox2;
        internal PictureBox pictureBox3;
        internal PictureBox pictureBox4;
        internal PictureBox pictureBox5;
        internal PictureBox pictureBox6;
        internal PictureBox pictureBox7;
    }
}
