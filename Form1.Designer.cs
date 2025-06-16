
namespace Bus_Logger_Application
{
    partial class Login_Screen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Screen));
            this.BusLoggerIcon = new System.Windows.Forms.PictureBox();
            this.WelcomeText = new System.Windows.Forms.PictureBox();
            this.txtBxUsername = new ZBobb.AlphaBlendTextBox();
            this.Aesthetics = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBxPassword = new ZBobb.AlphaBlendTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCantLogin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BusLoggerIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WelcomeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aesthetics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BusLoggerIcon
            // 
            this.BusLoggerIcon.BackColor = System.Drawing.Color.Transparent;
            this.BusLoggerIcon.Image = ((System.Drawing.Image)(resources.GetObject("BusLoggerIcon.Image")));
            this.BusLoggerIcon.Location = new System.Drawing.Point(109, 12);
            this.BusLoggerIcon.Name = "BusLoggerIcon";
            this.BusLoggerIcon.Size = new System.Drawing.Size(135, 139);
            this.BusLoggerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BusLoggerIcon.TabIndex = 0;
            this.BusLoggerIcon.TabStop = false;
            // 
            // WelcomeText
            // 
            this.WelcomeText.BackColor = System.Drawing.Color.Transparent;
            this.WelcomeText.Image = ((System.Drawing.Image)(resources.GetObject("WelcomeText.Image")));
            this.WelcomeText.Location = new System.Drawing.Point(12, 193);
            this.WelcomeText.Name = "WelcomeText";
            this.WelcomeText.Size = new System.Drawing.Size(120, 20);
            this.WelcomeText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WelcomeText.TabIndex = 1;
            this.WelcomeText.TabStop = false;
            // 
            // txtBxUsername
            // 
            this.txtBxUsername.BackAlpha = 1;
            this.txtBxUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtBxUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBxUsername.Font = new System.Drawing.Font("Rubik", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxUsername.ForeColor = System.Drawing.Color.Transparent;
            this.txtBxUsername.Location = new System.Drawing.Point(12, 244);
            this.txtBxUsername.MaxLength = 20;
            this.txtBxUsername.Multiline = true;
            this.txtBxUsername.Name = "txtBxUsername";
            this.txtBxUsername.Size = new System.Drawing.Size(337, 26);
            this.txtBxUsername.TabIndex = 2;
            this.txtBxUsername.Text = "Username";
            // 
            // Aesthetics
            // 
            this.Aesthetics.BackColor = System.Drawing.Color.Transparent;
            this.Aesthetics.Image = ((System.Drawing.Image)(resources.GetObject("Aesthetics.Image")));
            this.Aesthetics.Location = new System.Drawing.Point(12, 276);
            this.Aesthetics.Name = "Aesthetics";
            this.Aesthetics.Size = new System.Drawing.Size(350, 4);
            this.Aesthetics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Aesthetics.TabIndex = 3;
            this.Aesthetics.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 386);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 4);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtBxPassword
            // 
            this.txtBxPassword.BackAlpha = 1;
            this.txtBxPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtBxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBxPassword.Font = new System.Drawing.Font("Rubik", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxPassword.ForeColor = System.Drawing.Color.Transparent;
            this.txtBxPassword.Location = new System.Drawing.Point(12, 354);
            this.txtBxPassword.MaxLength = 20;
            this.txtBxPassword.Multiline = true;
            this.txtBxPassword.Name = "txtBxPassword";
            this.txtBxPassword.Size = new System.Drawing.Size(337, 26);
            this.txtBxPassword.TabIndex = 4;
            this.txtBxPassword.Text = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnLogin.Location = new System.Drawing.Point(48, 528);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(266, 77);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCantLogin
            // 
            this.btnCantLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnCantLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCantLogin.BackgroundImage")));
            this.btnCantLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCantLogin.FlatAppearance.BorderSize = 0;
            this.btnCantLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCantLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCantLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCantLogin.Location = new System.Drawing.Point(131, 611);
            this.btnCantLogin.Name = "btnCantLogin";
            this.btnCantLogin.Size = new System.Drawing.Size(89, 17);
            this.btnCantLogin.TabIndex = 7;
            this.btnCantLogin.UseVisualStyleBackColor = false;
            this.btnCantLogin.Click += new System.EventHandler(this.btnCantLogin_Click);
            // 
            // Login_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(375, 812);
            this.Controls.Add(this.btnCantLogin);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtBxPassword);
            this.Controls.Add(this.Aesthetics);
            this.Controls.Add(this.txtBxUsername);
            this.Controls.Add(this.WelcomeText);
            this.Controls.Add(this.BusLoggerIcon);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Login_Screen";
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_Screen_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_Screen_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.BusLoggerIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WelcomeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aesthetics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BusLoggerIcon;
        private System.Windows.Forms.PictureBox WelcomeText;
        private ZBobb.AlphaBlendTextBox txtBxUsername;
        private System.Windows.Forms.PictureBox Aesthetics;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ZBobb.AlphaBlendTextBox txtBxPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCantLogin;
    }
}

