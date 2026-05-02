using System.Data;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace CompilerProject;

public partial class Form1 : Form
{
    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    public Form1()
    {
        InitializeComponent();
        SetupDataGridView();
        ApplyModernTheme();
    }

    private void SetupDataGridView()
    {
        dgvLexical.Columns.Add("Token", "Token");
        dgvLexical.Columns.Add("Type", "Token Type");
        dgvLexical.Columns.Add("Position", "Position");
        dgvLexical.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        dgvLexical.Rows.Clear();
        tvSyntax.Nodes.Clear();
        lblStatus.Text = "Status: Running...";
        lblStatus.ForeColor = Color.FromArgb(137, 180, 250); // Accent blue

        string input = txtInput.Text;
        if (string.IsNullOrWhiteSpace(input))
        {
            lblStatus.Text = "Status: Please enter some code.";
            return;
        }

        try
        {
            // 1: Scanning
            var tokens = Scanner.Tokenize(input);
            foreach (var token in tokens)
            {
                if (token.Type != TokenType.EndOfFile)
                {
                    dgvLexical.Rows.Add(token.Value, token.Type, token.Position);
                }
            }

            // 2: Parsing
            var parser = new Parser(tokens);
            var rootNode = parser.Parse();

            if (parser.Errors.Count == 0)
            {
                lblStatus.Text = "Status: Parsing Successful!";
                lblStatus.ForeColor = Color.FromArgb(166, 227, 161); // Green
                PopulateTreeView(rootNode, tvSyntax.Nodes);
                tvSyntax.ExpandAll();
            }
            else
            {
                lblStatus.Text = "Status: Parsing Failed! (Check Parser Space)";
                lblStatus.ForeColor = Color.FromArgb(243, 139, 168); // Red

                TreeNode errorRoot = new TreeNode("Syntax Errors") { ForeColor = Color.Red };
                foreach (var err in parser.Errors)
                {
                    errorRoot.Nodes.Add(new TreeNode(err));
                }
                tvSyntax.Nodes.Add(errorRoot);
                errorRoot.Expand();

                // Still show the tree partially if possible
                PopulateTreeView(rootNode, tvSyntax.Nodes);
                
                // If it's a small tree, expand rootNode as well
                if (tvSyntax.Nodes.Count > 1) tvSyntax.Nodes[1].Expand();
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Status: Fatal Error (Check Parser Space)";
            lblStatus.ForeColor = Color.FromArgb(243, 139, 168); // Red
            
            TreeNode fatalRoot = new TreeNode("Fatal Error") { ForeColor = Color.Red };
            fatalRoot.Nodes.Add(new TreeNode(ex.Message));
            tvSyntax.Nodes.Add(fatalRoot);
            fatalRoot.ExpandAll();
        }
    }

    private void PopulateTreeView(Node node, TreeNodeCollection nodes)
    {
        if (node == null) return;

        TreeNode treeNode = new TreeNode(node.Name);
        nodes.Add(treeNode);

        foreach (var child in node.Children)
        {
            PopulateTreeView(child, treeNode.Nodes);
        }
    }

    private void ApplyModernTheme()
    {
        Color bgDark = Color.FromArgb(26, 26, 46);     // Deep Navy
        Color bgSurface = Color.FromArgb(35, 35, 59);  // Lighter Navy
        Color accent = Color.FromArgb(230, 57, 70);    // Vibrant Red
        Color text = Color.FromArgb(232, 213, 181);    // Cream/Beige
        Color success = Color.FromArgb(166, 227, 161);

        this.BackColor = bgDark;
        this.ForeColor = text;

        // Immersive Dark Mode for Title Bar
        int darkMode = 1;
        DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));

        // txtInput
        txtInput.BackColor = bgSurface;
        txtInput.ForeColor = text;
        txtInput.BorderStyle = BorderStyle.None;
        txtInput.Font = new Font("Consolas", 11F);
        SetWindowTheme(txtInput.Handle, "Explorer", null);

        // btnStart
        btnStart.Text = "▶  RUN COMPILER";
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.BackColor = accent;
        btnStart.ForeColor = bgDark;
        btnStart.FlatAppearance.BorderSize = 0;
        btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStart.Cursor = Cursors.Hand;
        
        btnStart.MouseEnter += (s, e) => btnStart.BackColor = Color.FromArgb(255, 77, 77); // Lighter Red
        btnStart.MouseLeave += (s, e) => btnStart.BackColor = accent;
        
        // Apply rounding
        RoundControl(btnStart, 15);

        // dgvLexical
        dgvLexical.BackgroundColor = bgDark;
        dgvLexical.ForeColor = text;
        dgvLexical.GridColor = bgSurface;
        dgvLexical.BorderStyle = BorderStyle.None;
        dgvLexical.RowHeadersVisible = false;
        dgvLexical.DefaultCellStyle.BackColor = bgDark;
        dgvLexical.DefaultCellStyle.ForeColor = text;
        dgvLexical.DefaultCellStyle.SelectionBackColor = accent;
        dgvLexical.DefaultCellStyle.SelectionForeColor = bgDark;
        dgvLexical.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 52);
        dgvLexical.ColumnHeadersDefaultCellStyle.BackColor = bgSurface;
        dgvLexical.ColumnHeadersDefaultCellStyle.ForeColor = text;
        dgvLexical.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvLexical.EnableHeadersVisualStyles = false;
        SetWindowTheme(dgvLexical.Handle, "Explorer", null);

        // tvSyntax
        tvSyntax.BackColor = bgDark;
        tvSyntax.ForeColor = text;
        tvSyntax.BorderStyle = BorderStyle.None;
        tvSyntax.LineColor = accent;

        // lblStatus
        lblStatus.ForeColor = accent;
        lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);

        // lblLexical, lblSyntax, lblSourceCode
        lblLexical.ForeColor = accent;
        lblSyntax.ForeColor = accent;
        lblSourceCode.ForeColor = accent;
        lblLexical.BackColor = Color.Transparent;
        lblSyntax.BackColor = Color.Transparent;
        lblSourceCode.BackColor = Color.Transparent;

        // splitContainers
        mainVerticalSplit.BackColor = bgDark;
        resultsHorizontalSplit.BackColor = bgDark;
        mainVerticalSplit.Panel1.Padding = new Padding(5);
        mainVerticalSplit.Panel2.Padding = new Padding(5);
        mainVerticalSplit.SplitterWidth = 4;
        resultsHorizontalSplit.SplitterWidth = 4;
    }

    private void RoundControl(Control control, int radius)
    {
        control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius));
        // Also handle resize to re-calculate region
        control.Resize += (s, e) => 
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius));
        };
    }

    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,     // x-coordinate of upper-left corner
        int nTopRect,      // y-coordinate of upper-left corner
        int nRightRect,    // x-coordinate of lower-right corner
        int nBottomRect,   // y-coordinate of lower-right corner
        int nWidthEllipse, // width of ellipse
        int nHeightEllipse // height of ellipse
    );

    private void Form1_Load(object sender, EventArgs e)
    {
    }
}
