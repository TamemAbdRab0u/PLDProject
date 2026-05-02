namespace CompilerProject;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        txtInput = new RichTextBox();
        btnStart = new Button();
        dgvLexical = new DataGridView();
        tvSyntax = new TreeView();
        lblStatus = new Label();
        resultsHorizontalSplit = new SplitContainer();
        lblLexical = new Label();
        lblSyntax = new Label();
        mainVerticalSplit = new SplitContainer();
        lblSourceCode = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvLexical).BeginInit();
        ((System.ComponentModel.ISupportInitialize)resultsHorizontalSplit).BeginInit();
        resultsHorizontalSplit.Panel1.SuspendLayout();
        resultsHorizontalSplit.Panel2.SuspendLayout();
        resultsHorizontalSplit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)mainVerticalSplit).BeginInit();
        mainVerticalSplit.Panel1.SuspendLayout();
        mainVerticalSplit.Panel2.SuspendLayout();
        mainVerticalSplit.SuspendLayout();
        SuspendLayout();
        // 
        // txtInput
        // 
        txtInput.BorderStyle = BorderStyle.None;
        txtInput.Dock = DockStyle.Fill;
        txtInput.Font = new Font("Consolas", 10.2F);
        txtInput.Location = new Point(0, 33);
        txtInput.Name = "txtInput";
        txtInput.Size = new Size(776, 197);
        txtInput.TabIndex = 0;
        txtInput.Text = "Start\nx = 5\ny = 5\nswitch (x):\n<~\n    case (1): x = x + 1\n~>\nEnd";
        // 
        // btnStart
        // 
        btnStart.Dock = DockStyle.Top;
        btnStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnStart.Location = new Point(0, 0);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(776, 45);
        btnStart.TabIndex = 1;
        btnStart.Text = "RUN COMPILER";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        // 
        // dgvLexical
        // 
        dgvLexical.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvLexical.Dock = DockStyle.Fill;
        dgvLexical.Location = new Point(0, 33);
        dgvLexical.Name = "dgvLexical";
        dgvLexical.RowHeadersVisible = false;
        dgvLexical.RowHeadersWidth = 51;
        dgvLexical.RowTemplate.Height = 24;
        dgvLexical.Size = new Size(350, 368);
        dgvLexical.TabIndex = 2;
        // 
        // tvSyntax
        // 
        tvSyntax.Dock = DockStyle.Fill;
        tvSyntax.Location = new Point(0, 33);
        tvSyntax.Name = "tvSyntax";
        tvSyntax.Size = new Size(422, 368);
        tvSyntax.TabIndex = 3;
        // 
        // lblStatus
        // 
        lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStatus.Location = new Point(12, 700);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(104, 20);
        lblStatus.TabIndex = 4;
        lblStatus.Text = "Status: Ready";
        // 
        // resultsHorizontalSplit
        // 
        resultsHorizontalSplit.Dock = DockStyle.Fill;
        resultsHorizontalSplit.Location = new Point(0, 45);
        resultsHorizontalSplit.Name = "resultsHorizontalSplit";
        // 
        // resultsHorizontalSplit.Panel1
        // 
        resultsHorizontalSplit.Panel1.Controls.Add(dgvLexical);
        resultsHorizontalSplit.Panel1.Controls.Add(lblLexical);
        // 
        // resultsHorizontalSplit.Panel2
        // 
        resultsHorizontalSplit.Panel2.Controls.Add(tvSyntax);
        resultsHorizontalSplit.Panel2.Controls.Add(lblSyntax);
        resultsHorizontalSplit.Size = new Size(776, 401);
        resultsHorizontalSplit.SplitterDistance = 350;
        resultsHorizontalSplit.TabIndex = 5;
        // 
        // lblLexical
        // 
        lblLexical.AutoSize = true;
        lblLexical.Dock = DockStyle.Top;
        lblLexical.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLexical.Location = new Point(0, 0);
        lblLexical.Name = "lblLexical";
        lblLexical.Padding = new Padding(0, 5, 0, 5);
        lblLexical.Size = new Size(64, 33);
        lblLexical.TabIndex = 7;
        lblLexical.Text = "Tokens";
        // 
        // lblSyntax
        // 
        lblSyntax.AutoSize = true;
        lblSyntax.Dock = DockStyle.Top;
        lblSyntax.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblSyntax.Location = new Point(0, 0);
        lblSyntax.Name = "lblSyntax";
        lblSyntax.Padding = new Padding(0, 5, 0, 5);
        lblSyntax.Size = new Size(91, 33);
        lblSyntax.TabIndex = 8;
        lblSyntax.Text = "Parse Tree";
        // 
        // mainVerticalSplit
        // 
        mainVerticalSplit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        mainVerticalSplit.Location = new Point(12, 12);
        mainVerticalSplit.Name = "mainVerticalSplit";
        mainVerticalSplit.Orientation = Orientation.Horizontal;
        // 
        // mainVerticalSplit.Panel1
        // 
        mainVerticalSplit.Panel1.Controls.Add(txtInput);
        mainVerticalSplit.Panel1.Controls.Add(lblSourceCode);
        // 
        // mainVerticalSplit.Panel2
        // 
        mainVerticalSplit.Panel2.Controls.Add(resultsHorizontalSplit);
        mainVerticalSplit.Panel2.Controls.Add(btnStart);
        mainVerticalSplit.Size = new Size(776, 680);
        mainVerticalSplit.SplitterDistance = 230;
        mainVerticalSplit.TabIndex = 9;
        // 
        // lblSourceCode
        // 
        lblSourceCode.AutoSize = true;
        lblSourceCode.Dock = DockStyle.Top;
        lblSourceCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblSourceCode.Location = new Point(0, 0);
        lblSourceCode.Name = "lblSourceCode";
        lblSourceCode.Padding = new Padding(0, 5, 0, 5);
        lblSourceCode.Size = new Size(110, 33);
        lblSourceCode.TabIndex = 10;
        lblSourceCode.Text = "Source Code";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 730);
        Controls.Add(mainVerticalSplit);
        Controls.Add(lblStatus);
        MinimumSize = new Size(600, 400);
        Name = "Form1";
        Text = "Compiler Project 2026";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dgvLexical).EndInit();
        resultsHorizontalSplit.Panel1.ResumeLayout(false);
        resultsHorizontalSplit.Panel1.PerformLayout();
        resultsHorizontalSplit.Panel2.ResumeLayout(false);
        resultsHorizontalSplit.Panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)resultsHorizontalSplit).EndInit();
        resultsHorizontalSplit.ResumeLayout(false);
        mainVerticalSplit.Panel1.ResumeLayout(false);
        mainVerticalSplit.Panel1.PerformLayout();
        mainVerticalSplit.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)mainVerticalSplit).EndInit();
        mainVerticalSplit.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.RichTextBox txtInput;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.DataGridView dgvLexical;
    private System.Windows.Forms.TreeView tvSyntax;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.SplitContainer resultsHorizontalSplit;
    private System.Windows.Forms.SplitContainer mainVerticalSplit;
    private System.Windows.Forms.Label lblLexical;
    private System.Windows.Forms.Label lblSyntax;
    private System.Windows.Forms.Label lblSourceCode;
}
