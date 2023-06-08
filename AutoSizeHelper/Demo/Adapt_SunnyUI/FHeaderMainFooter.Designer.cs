namespace Demo.Adapt_SunnyUI
{
    partial class FHeaderMainFooter
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
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.FillColor = System.Drawing.Color.Navy;
            this.Footer.Location = new System.Drawing.Point(0, 455);
            this.Footer.Size = new System.Drawing.Size(846, 56);
            this.Footer.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Header
            // 
            this.Header.Size = new System.Drawing.Size(846, 62);
            // 
            // FHeaderMainFooter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(846, 511);
            this.Name = "FHeaderMainFooter";
            this.Text = "FHeaderMainFooter";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.ResumeLayout(false);

        }

        #endregion
    }
}