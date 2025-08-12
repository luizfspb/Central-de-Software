using System;
using System.Drawing;
using System.Windows.Forms;

public class UserControlInfo : UserControl
{
    public UserControlInfo()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.BackColor = Color.White;

        // Título principal
        var lblTitulo = new Label
        {
            Text = "Central de Software",
            Font = new Font("Segoe UI", 24, FontStyle.Bold),
            ForeColor = ColorTranslator.FromHtml("#1A377B"),
            AutoSize = true,
            Location = new Point(40, 40)
        };
        this.Controls.Add(lblTitulo);

        // Versão
        var lblVersao = new Label
        {
            Text = "Versão 1.0.0",
            Font = new Font("Segoe UI", 12),
            ForeColor = ColorTranslator.FromHtml("#1A377B"),
            AutoSize = true,
            Location = new Point(44, 85)
        };
        this.Controls.Add(lblVersao);

        // Separador
        var separator = new Panel
        {
            BackColor = ColorTranslator.FromHtml("#F15A22"),
            Height = 2,
            Width = 600,
            Location = new Point(40, 120)
        };
        this.Controls.Add(separator);

        // Lista de recursos
        var recursos = new[]
        {
            "?? Interface Unificada - Centralização de todas as ferramentas de TI",
            "?? Instaladores Embutidos - Software packages para uso offline",
            "?? Modo Técnico Protegido - Autenticação segura para funções avançadas",
            "?? Diagnóstico Completo - Análise detalhada do sistema",
            "?? Automação de Tarefas - Otimização e manutenção",
            "?? Integração Corporativa - Suporte para Active Directory"
        };

        var top = 140;
        foreach (var recurso in recursos)
        {
            var lblRecurso = new Label
            {
                Text = recurso,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(40, top)
            };
            this.Controls.Add(lblRecurso);
            top += 35;
        }

        // Separador final
        var separatorBottom = new Panel
        {
            BackColor = ColorTranslator.FromHtml("#F15A22"),
            Height = 2,
            Width = 600,
            Location = new Point(40, top + 20)
        };
        this.Controls.Add(separatorBottom);

        // Desenvolvedor
        var lblDesenvolvedor = new Label
        {
            Text = "Desenvolvido por Luiz Filipe",
            Font = new Font("Segoe UI", 10),
            ForeColor = ColorTranslator.FromHtml("#1A377B"),
            AutoSize = true,
            Location = new Point(40, top + 35)
        };
        this.Controls.Add(lblDesenvolvedor);

        // Copyright
        var lblCopyright = new Label
        {
            Text = "© 2024 - Todos os direitos reservados",
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.Gray,
            AutoSize = true,
            Location = new Point(40, top + 60)
        };
        this.Controls.Add(lblCopyright);
    }
}