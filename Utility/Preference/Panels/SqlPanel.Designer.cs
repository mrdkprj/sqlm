namespace MasudaManager.Utility.Preference.Setting
{
    partial class SqlPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.flayRun = new System.Windows.Forms.FlowLayoutPanel();
            this.chkDisplayProgress = new System.Windows.Forms.CheckBox();
            this.chkAutoCommit = new System.Windows.Forms.CheckBox();
            this.chkRunAfterSemiColon = new System.Windows.Forms.CheckBox();
            this.chkIgnoreError = new System.Windows.Forms.CheckBox();
            this.grpCommandTimeout = new System.Windows.Forms.GroupBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblTimeoutSecond = new System.Windows.Forms.Label();
            this.flayRun.SuspendLayout();
            this.grpCommandTimeout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // flayRun
            // 
            this.flayRun.Controls.Add(this.chkDisplayProgress);
            this.flayRun.Controls.Add(this.chkAutoCommit);
            this.flayRun.Controls.Add(this.chkRunAfterSemiColon);
            this.flayRun.Controls.Add(this.chkIgnoreError);
            this.flayRun.Controls.Add(this.grpCommandTimeout);
            this.flayRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flayRun.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flayRun.Location = new System.Drawing.Point(0, 0);
            this.flayRun.Name = "flayRun";
            this.flayRun.Padding = new System.Windows.Forms.Padding(5);
            this.flayRun.Size = new System.Drawing.Size(365, 241);
            this.flayRun.TabIndex = 2;
            this.flayRun.WrapContents = false;
            // 
            // chkDisplayProgress
            // 
            this.chkDisplayProgress.AutoSize = true;
            this.chkDisplayProgress.Location = new System.Drawing.Point(8, 8);
            this.chkDisplayProgress.Name = "chkDisplayProgress";
            this.chkDisplayProgress.Size = new System.Drawing.Size(166, 16);
            this.chkDisplayProgress.TabIndex = 9;
            this.chkDisplayProgress.Text = "Display progress of a query";
            this.chkDisplayProgress.UseVisualStyleBackColor = true;
            // 
            // chkAutoCommit
            // 
            this.chkAutoCommit.AutoSize = true;
            this.chkAutoCommit.Location = new System.Drawing.Point(8, 30);
            this.chkAutoCommit.Name = "chkAutoCommit";
            this.chkAutoCommit.Size = new System.Drawing.Size(135, 16);
            this.chkAutoCommit.TabIndex = 1;
            this.chkAutoCommit.Text = "Automatically commit";
            this.chkAutoCommit.UseVisualStyleBackColor = true;
            // 
            // chkRunAfterSemiColon
            // 
            this.chkRunAfterSemiColon.AutoSize = true;
            this.chkRunAfterSemiColon.Location = new System.Drawing.Point(8, 52);
            this.chkRunAfterSemiColon.Name = "chkRunAfterSemiColon";
            this.chkRunAfterSemiColon.Size = new System.Drawing.Size(152, 16);
            this.chkRunAfterSemiColon.TabIndex = 0;
            this.chkRunAfterSemiColon.Text = "Run SQL after semicolon";
            this.chkRunAfterSemiColon.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreError
            // 
            this.chkIgnoreError.AutoSize = true;
            this.chkIgnoreError.Location = new System.Drawing.Point(8, 74);
            this.chkIgnoreError.Name = "chkIgnoreError";
            this.chkIgnoreError.Size = new System.Drawing.Size(125, 16);
            this.chkIgnoreError.TabIndex = 2;
            this.chkIgnoreError.Text = "Continue after error";
            this.chkIgnoreError.UseVisualStyleBackColor = true;
            // 
            // grpCommandTimeout
            // 
            this.grpCommandTimeout.Controls.Add(this.numTimeout);
            this.grpCommandTimeout.Controls.Add(this.lblTimeoutSecond);
            this.grpCommandTimeout.Location = new System.Drawing.Point(8, 96);
            this.grpCommandTimeout.Name = "grpCommandTimeout";
            this.grpCommandTimeout.Size = new System.Drawing.Size(152, 51);
            this.grpCommandTimeout.TabIndex = 10;
            this.grpCommandTimeout.TabStop = false;
            this.grpCommandTimeout.Text = "Command timeout";
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(15, 19);
            this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(47, 19);
            this.numTimeout.TabIndex = 0;
            this.numTimeout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblTimeoutSecond
            // 
            this.lblTimeoutSecond.AutoSize = true;
            this.lblTimeoutSecond.Location = new System.Drawing.Point(68, 21);
            this.lblTimeoutSecond.Name = "lblTimeoutSecond";
            this.lblTimeoutSecond.Size = new System.Drawing.Size(25, 12);
            this.lblTimeoutSecond.TabIndex = 11;
            this.lblTimeoutSecond.Text = "sec.";
            // 
            // SqlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayRun);
            this.Name = "SqlPanel";
            this.flayRun.ResumeLayout(false);
            this.flayRun.PerformLayout();
            this.grpCommandTimeout.ResumeLayout(false);
            this.grpCommandTimeout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flayRun;
        private System.Windows.Forms.CheckBox chkAutoCommit;
        private System.Windows.Forms.CheckBox chkRunAfterSemiColon;
        private System.Windows.Forms.CheckBox chkIgnoreError;
        private System.Windows.Forms.CheckBox chkDisplayProgress;
        private System.Windows.Forms.GroupBox grpCommandTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblTimeoutSecond;

    }
}
