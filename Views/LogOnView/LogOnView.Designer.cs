namespace MasudaManager.Views
{
    partial class LogOnView
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogOnView));
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtID = new MasudaManager.Controls.XTextBox();
            this.txtPassword = new MasudaManager.Controls.XTextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.txtDS = new MasudaManager.Controls.XTextBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(31, 37);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(54, 12);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "User ID : ";
            // 
            // txtID
            // 
            this.txtID.AllowAutoSelectAll = true;
            this.txtID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtID.Location = new System.Drawing.Point(87, 34);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(136, 19);
            this.txtID.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.AllowAutoSelectAll = true;
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassword.Location = new System.Drawing.Point(87, 63);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(136, 19);
            this.txtPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 66);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 12);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password : ";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(87, 127);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(65, 23);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(162, 127);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(65, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(7, 9);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(78, 12);
            this.lblDataSource.TabIndex = 6;
            this.lblDataSource.Text = "Data Source : ";
            // 
            // txtDS
            // 
            this.txtDS.AllowAutoSelectAll = true;
            this.txtDS.Location = new System.Drawing.Point(87, 6);
            this.txtDS.Name = "txtDS";
            this.txtDS.Size = new System.Drawing.Size(135, 19);
            this.txtDS.TabIndex = 1;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(42, 95);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(42, 12);
            this.lblMode.TabIndex = 7;
            this.lblMode.Text = "Mode : ";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(87, 92);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(135, 20);
            this.cmbMode.TabIndex = 8;
            // 
            // LogOnView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 162);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.txtDS);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblUserId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogOnView";
            this.Text = "Connect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lblDataSource;
        private Controls.XTextBox txtID;
        private Controls.XTextBox txtPassword;
        private Controls.XTextBox txtDS;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cmbMode;
    }
}