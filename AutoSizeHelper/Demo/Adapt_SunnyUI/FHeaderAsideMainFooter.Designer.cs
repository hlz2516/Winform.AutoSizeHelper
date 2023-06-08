namespace Demo.Adapt_SunnyUI
{
    partial class FHeaderAsideMainFooter
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
            this.Footer.Location = new System.Drawing.Point(243, 477);
            this.Footer.Size = new System.Drawing.Size(610, 56);
            this.Footer.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Aside
            // 
            this.Aside.Location = new System.Drawing.Point(0, 104);
            this.Aside.Size = new System.Drawing.Size(243, 429);
            // 
            // Header
            // 
            this.Header.Size = new System.Drawing.Size(853, 69);
            // 
            // FHeaderAsideMainFooter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(853, 533);
            this.Name = "FHeaderAsideMainFooter";
            this.Text = "FHeaderAsideMainFooter";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.ResumeLayout(false);

        }

        #endregion
    }
}