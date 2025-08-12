using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

public class UserControl3uTools : UserControl
{
    public UserControl3uTools()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = Color.White;

        var btnAbrir = new Button
        {
            Text = "Abrir 3uTools",
            Width = 220,
            Height = 60,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            BackColor = Color.White,
            ForeColor = ColorTranslator.FromHtml("#1A377B"),
            FlatStyle = FlatStyle.Flat,
            Location = new Point(40, 40)
        };
        btnAbrir.FlatAppearance.BorderSize = 0;
        btnAbrir.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#F15A22");
        btnAbrir.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#1A377B");
        btnAbrir.Click += async (s, e) => await Abrir3uTools();
        this.Controls.Add(btnAbrir);
    }

    private async Task Abrir3uTools()
    {
        string caminho = @"C:\Program Files\3uToolsV3\3uTools.exe";
        string senhaBase64 = "dmNhcmR1QDI1MDk=";
        try
        {
            if (!System.IO.File.Exists(caminho))
            {
                MessageBox.Show($"Arquivo não encontrado: {caminho}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var psi = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = caminho,
                UseShellExecute = false,
                UserName = "administrador",
                Password = ConvertToSecureString(DecodeBase64(senhaBase64)),
                Domain = "scarduaca.local"
            };
            System.Diagnostics.Process.Start(psi);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir 3uTools: {ex.Message}\nCaminho: {caminho}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string DecodeBase64(string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    private System.Security.SecureString ConvertToSecureString(string senha)
    {
        var secure = new System.Security.SecureString();
        foreach (char c in senha)
            secure.AppendChar(c);
        secure.MakeReadOnly();
        return secure;
    }
}
