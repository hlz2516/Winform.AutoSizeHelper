namespace Demo.Adapt_SunnyUI
{
    partial class FAsideHeaderMainFooter
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
            this.Footer.Location = new System.Drawing.Point(230, 470);
            this.Footer.Size = new System.Drawing.Size(621, 56);
            this.Footer.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Header
            // 
            this.Header.FillColor = System.Drawing.Color.Cyan;
            this.Header.Location = new System.Drawing.Point(230, 35);
            this.Header.Size = new System.Drawing.Size(621, 57);
            this.Header.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Aside
            // 
            this.Aside.Size = new System.Drawing.Size(230, 491);
            // 
            // FAsideHeaderMainFooter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(851, 526);
            this.Name = "FAsideHeaderMainFooter";
            this.Text = "FAsideHeaderMainFooter";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.ResumeLayout(false);

        }

        #endregion
    }
}