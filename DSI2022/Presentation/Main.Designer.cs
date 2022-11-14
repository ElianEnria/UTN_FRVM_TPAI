namespace DSI2022 {
	partial class Main {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnReservarTurno = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnReservarTurno
			// 
			this.btnReservarTurno.Location = new System.Drawing.Point(12, 12);
			this.btnReservarTurno.Name = "btnReservarTurno";
			this.btnReservarTurno.Size = new System.Drawing.Size(329, 176);
			this.btnReservarTurno.TabIndex = 0;
			this.btnReservarTurno.Text = "Reservar Turno";
			this.btnReservarTurno.UseVisualStyleBackColor = true;
			this.btnReservarTurno.Click += new System.EventHandler(this.btnReservarTurno_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(353, 200);
			this.Controls.Add(this.btnReservarTurno);
			this.Name = "Main";
			this.Text = "UTN FRVM 2022 ENTREGA FINAL";
			this.ResumeLayout(false);

		}

		#endregion

		private Button btnReservarTurno;
	}
}