﻿namespace webTest.View
{
    partial class JsonViewForm
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
            this.jsonViewer1 = new EPocalipse.Json.Viewer.JsonViewer();
            this.SuspendLayout();
            // 
            // jsonViewer1
            // 
            this.jsonViewer1.AutoSize = true;
            this.jsonViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonViewer1.Json = null;
            this.jsonViewer1.Location = new System.Drawing.Point(0, 0);
            this.jsonViewer1.Name = "jsonViewer1";
            this.jsonViewer1.Size = new System.Drawing.Size(820, 591);
            this.jsonViewer1.TabIndex = 0;
            // 
            // JsonViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 591);
            this.Controls.Add(this.jsonViewer1);
            this.Name = "JsonViewForm";
            this.Text = "JSON Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EPocalipse.Json.Viewer.JsonViewer jsonViewer1;
    }
}