using System;
using System.Drawing;
using System.Windows.Forms;

namespace Central_de_Software
{
    public class FormTecnico : Form
    {
        private Panel panelMenuSuperior;
        private Button btnVoltar;
        private Button btnTools;
        private Button btnAlterarSenha;
        private Panel panelLateral;
        private Panel panelCentral;
        private UserControlTools userControlTools;
        private Color corAzul = ColorTranslator.FromHtml("#1A377B");
        private Color corLaranja = ColorTranslator.FromHtml("#F15A22");
        private Color corBranco = Color.White;

        public FormTecnico()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Painel Técnico";
            this.Width = 950;
            this.Height = 700;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = new Size(950, 700);
            this.MinimumSize = new Size(950, 700);
            this.BackColor = Color.FromArgb(30, 30, 30);

            // Menu superior
            panelMenuSuperior = new Panel { Height = 80, Dock = DockStyle.Top, BackColor = Color.FromArgb(40, 40, 40) };
            
            // Botão Voltar
            btnVoltar = new Button
            {
                Text = "  Voltar",
                Width = 180,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(20, 10),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                TabStop = false
            };
            btnVoltar.FlatAppearance.BorderSize = 0;
            btnVoltar.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            btnVoltar.Click += (s, e) => this.Hide();
            panelMenuSuperior.Controls.Add(btnVoltar);

            // Botão Tools
            btnTools = new Button
            {
                Text = "  Tools",
                Width = 180,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(220, 10),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                TabStop = false
            };
            btnTools.FlatAppearance.BorderSize = 0;
            btnTools.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            btnTools.Click += (s, e) => ShowCentralContentTools();
            panelMenuSuperior.Controls.Add(btnTools);

            // Botão Alterar Senha
            btnAlterarSenha = new Button
            {
                Text = "  Alterar Senha",
                Width = 180,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(420, 10),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                TabStop = false
            };
            btnAlterarSenha.FlatAppearance.BorderSize = 0;
            btnAlterarSenha.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            btnAlterarSenha.Click += (s, e) => AlterarSenhaTecnico();
            panelMenuSuperior.Controls.Add(btnAlterarSenha);

            // Painel lateral
            panelLateral = new Panel { Width = 220, Dock = DockStyle.Left, BackColor = Color.FromArgb(40, 40, 40) };
            var btnToolsMenu = new Button
            {
                Text = "  Otimização do Windows",
                Width = 200,
                Height = 50,
                Top = 30,
                Left = 10,
                Font = new Font("Segoe UI", 10F),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                FlatStyle = FlatStyle.Flat,
                BackColor = corBranco,
                ForeColor = corAzul,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            btnToolsMenu.FlatAppearance.BorderSize = 0;
            btnToolsMenu.FlatAppearance.MouseOverBackColor = corLaranja;
            btnToolsMenu.FlatAppearance.MouseDownBackColor = corAzul;
            btnToolsMenu.Click += (s, e) => ShowCentralContentTools();
            panelLateral.Controls.Add(btnToolsMenu);

            // Painel central
            panelCentral = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 30) };

            this.Controls.Add(panelCentral);
            this.Controls.Add(panelLateral);
            this.Controls.Add(panelMenuSuperior);

            ShowCentralContentTools();
        }

        private void AlterarSenhaTecnico()
        {
            using (var form = new Form
            {
                Width = 350,
                Height = 220,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Text = "Alterar Senha do Modo Técnico",
                BackColor = Color.FromArgb(30, 30, 30)
            })
            {
                var txtSenhaAtual = new TextBox
                {
                    Left = 20,
                    Top = 40,
                    Width = 290,
                    UseSystemPasswordChar = true,
                    BackColor = Color.FromArgb(45, 45, 45),
                    ForeColor = Color.White
                };

                var txtNovaSenha = new TextBox
                {
                    Left = 20,
                    Top = 90,
                    Width = 290,
                    UseSystemPasswordChar = true,
                    BackColor = Color.FromArgb(45, 45, 45),
                    ForeColor = Color.White
                };

                var btnConfirmar = new Button
                {
                    Text = "Confirmar",
                    Left = 125,
                    Top = 130,
                    Width = 100,
                    DialogResult = DialogResult.OK,
                    BackColor = Color.FromArgb(60, 60, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnConfirmar.FlatAppearance.BorderSize = 0;

                var lblSenhaAtual = new Label
                {
                    Text = "Senha atual:",
                    Left = 20,
                    Top = 20,
                    AutoSize = true,
                    ForeColor = Color.White
                };

                var lblNovaSenha = new Label
                {
                    Text = "Nova senha:",
                    Left = 20,
                    Top = 70,
                    AutoSize = true,
                    ForeColor = Color.White
                };

                form.Controls.AddRange(new Control[] { txtSenhaAtual, txtNovaSenha, btnConfirmar, lblSenhaAtual, lblNovaSenha });
                form.AcceptButton = btnConfirmar;
                txtSenhaAtual.Focus();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (txtSenhaAtual.Text != FormMain.senhaTecnico)
                    {
                        MessageBox.Show("Senha atual incorreta!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtNovaSenha.Text))
                    {
                        MessageBox.Show("A nova senha não pode estar em branco!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormMain.senhaTecnico = txtNovaSenha.Text;
                    MessageBox.Show("Senha alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowCentralContentTools()
        {
            if (userControlTools == null)
                userControlTools = new UserControlTools();
            userControlTools.ModoTecnico = true;
            panelCentral.Controls.Clear();
            userControlTools.Dock = DockStyle.Fill;
            panelCentral.Controls.Add(userControlTools);
            SetToolsButtonsWhite();
        }

        private void SetToolsButtonsWhite()
        {
            if (userControlTools == null) return;
            var btnExecutar = userControlTools.Controls.Find("btnExecutar", true);
            var btnLimparSelecao = userControlTools.Controls.Find("btnLimparSelecao", true);
            foreach (Control c in btnExecutar)
            {
                c.BackColor = corBranco;
                c.ForeColor = corAzul;
            }
            foreach (Control c in btnLimparSelecao)
            {
                c.BackColor = corBranco;
                c.ForeColor = corAzul;
            }
        }
    }
}
