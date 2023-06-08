namespace Demo.Adapt_SunnyUI
{
    partial class FHeaderMain
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Page1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Page2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Page3");
            this.SuspendLayout();
            // 
            // Header
            // 
            treeNode1.Name = "节点0";
            treeNode1.Text = "Page1";
            treeNode2.Name = "节点1";
            treeNode2.Text = "Page2";
            treeNode3.Name = "节点2";
            treeNode3.Text = "Page3";
            this.Header.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.Header.Size = new System.Drawing.Size(812, 70);
            // 
            // FHeaderMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(812, 476);
            this.Name = "FHeaderMain";
            this.Text = "FHeaderMain";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 812, 476);
            this.ResumeLayout(false);

        }

        #endregion
    }
}