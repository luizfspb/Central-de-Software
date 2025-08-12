using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

public class UserControlTools : UserControl
{
    private bool _modoTecnico = false;
    public bool ModoTecnico
    {
        get => _modoTecnico;
        set
        {
            if (_modoTecnico != value)
            {
                _modoTecnico = value;
                RebuildOptionsList();
            }
        }
    }

    private ListView listViewOptions;
    private Button btnExecutar;
    private Button btnLimparSelecao;
    private ProgressBar progressBar;
    private RichTextBox richTextBoxLog;

    // �ndices das op��es administrativas
    private readonly int[] opcoesAdmin = new[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 };

    private readonly (string Nome, string Descricao)[] opcoes = new[]
    {
        ("Criar ponto de restaura��o", "Cria um ponto de restaura��o do sistema para revers�o segura."),
        ("Limpar arquivos tempor�rios", "Remove arquivos tempor�rios do Windows e libera espa�o em disco."),
        ("Desativar Cortana", "Desativa o assistente virtual Cortana."),
        ("Desativar Telemetria", "Desativa coleta de dados de diagn�stico e telemetria."),
        ("Plano de energia: Alto Desempenho", "Configura o plano de energia para m�ximo desempenho."),
        ("Remover apps pr�-instalados (bloatware)", "Remove aplicativos padr�o desnecess�rios do Windows."),
        ("Desabilitar servi�os desnecess�rios", "Desativa servi�os do Windows que n�o s�o essenciais."),
        ("Windows Update manual", "Configura o Windows Update para modo manual."),
        ("Efeitos visuais para desempenho", "Ajusta efeitos visuais para priorizar desempenho."),
        ("Verificar arquivos do sistema (SFC) e disco (CHKDSK)", "Executa SFC e CHKDSK para corrigir erros do sistema e disco."),
        ("Limpar fila de impress�o (Spooler)", "Limpa a fila de impress�o e reinicia o servi�o Spooler."),
        ("Solucionar problemas de rede", "Renova IP, limpa cache DNS e reseta configura��es de rede."),
        ("Limpar atualiza��es antigas (DISM)", "Remove arquivos de atualiza��es antigas do Windows."),
        ("Restaurar Mapeamentos de Rede", "Refaz os mapeamentos de unidades de rede padr�o."),
        ("Limpar Cache de �cones", "Recria o banco de dados de �cones do Windows."),
        ("Limpar Cache de Navegadores", "Limpa cache, cookies e hist�rico dos navegadores (Chrome, Edge, Firefox).")
    };

    private readonly string senhaBase64 = "dmNhcmR1QDI1MDk="; // "vcardu@2509" em Base64

    public UserControlTools()
    {
        this.Width = 700;
        this.Height = 660;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.listViewOptions = new ListView
        {
            Top = 10,
            Left = 10,
            Width = 670,
            Height = 260,
            View = View.Details,
            CheckBoxes = true,
            FullRowSelect = true
        };
        listViewOptions.Columns.Add("Otimiza��o", 220);
        listViewOptions.Columns.Add("Descri��o", 430);
        RebuildOptionsList();

        this.btnExecutar = new Button { Name = "btnExecutar", Text = "Executar Selecionados", Top = 280, Left = 10, Width = 180 };
        this.btnExecutar.Click += btnExecutar_Click;
        this.btnLimparSelecao = new Button { Name = "btnLimparSelecao", Text = "Limpar Sele��o", Top = 280, Left = 200, Width = 140 };
        this.btnLimparSelecao.Click += btnLimparSelecao_Click;
        this.progressBar = new ProgressBar { Top = 320, Left = 10, Width = 670, Height = 20 };

        var panelTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 350
        };
        panelTop.Controls.Add(listViewOptions);
        panelTop.Controls.Add(btnExecutar);
        panelTop.Controls.Add(btnLimparSelecao);
        panelTop.Controls.Add(progressBar);

        this.richTextBoxLog = new RichTextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true
        };

        this.Controls.Add(richTextBoxLog);
        this.Controls.Add(panelTop);
        this.Dock = DockStyle.Fill;
    }

    private void RebuildOptionsList()
    {
        if (listViewOptions == null) return;
        listViewOptions.Items.Clear();
        for (int i = 0; i < opcoes.Length; i++)
        {
            if (!ModoTecnico && opcoesAdmin.Contains(i))
                continue;
            var item = new ListViewItem(opcoes[i].Nome);
            item.SubItems.Add(opcoes[i].Descricao);
            listViewOptions.Items.Add(item);
        }
    }

    private void btnLimparSelecao_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem item in listViewOptions.Items)
            item.Checked = false;
    }

    private async void btnExecutar_Click(object sender, EventArgs e)
    {
        btnExecutar.Enabled = false;
        btnLimparSelecao.Enabled = false;
        richTextBoxLog.Clear();
        progressBar.Value = 0;

        var checkedItems = listViewOptions.CheckedItems.Cast<ListViewItem>().ToList();
        progressBar.Maximum = checkedItems.Count > 0 ? checkedItems.Count : 1;

        foreach (var item in checkedItems)
        {
            string nomeOpcao = item.Text;
            await ExecutarOpcao(nomeOpcao);
            progressBar.Value++;
        }

        btnExecutar.Enabled = true;
        btnLimparSelecao.Enabled = true;
        AppendToLog("Otimiza��o conclu�da.");
        MessageBox.Show("Otimiza��o conclu�da!", "Central de Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task ExecutarOpcao(string nomeOpcao)
    {
        switch (nomeOpcao)
        {
            case "Criar ponto de restaura��o": await CriarPontoRestauracao(); break;
            case "Limpar arquivos tempor�rios": await LimparArquivosTemporarios(); break;
            case "Desativar Cortana": await DesativarCortana(); break;
            case "Desativar Telemetria": await DesativarTelemetria(); break;
            case "Plano de energia: Alto Desempenho": await PlanoAltoDesempenho(); break;
            case "Remover apps pr�-instalados (bloatware)": await RemoverBloatware(); break;
            case "Desabilitar servi�os desnecess�rios": await DesabilitarServicos(); break;
            case "Windows Update manual": await WindowsUpdateManual(); break;
            case "Efeitos visuais para desempenho": await EfeitosVisuaisDesempenho(); break;
            case "Verificar arquivos do sistema (SFC) e disco (CHKDSK)": await SFCeCHKDSK(); break;
            case "Limpar fila de impress�o (Spooler)": await LimparSpooler(); break;
            case "Solucionar problemas de rede": await SolucionarRede(); break;
            case "Limpar atualiza��es antigas (DISM)": await LimparAtualizacoesAntigas(); break;
            case "Restaurar Mapeamentos de Rede": await RestaurarMapeamentosRede(); break;
            case "Limpar Cache de �cones": await LimparCacheIcones(); break;
            case "Limpar Cache de Navegadores": await LimparCacheNavegadores(); break;
        }
    }

    // Implementa��o das novas fun��es
    private async Task RestaurarMapeamentosRede()
    {
        AppendToLog("Restaurando mapeamentos de rede existentes...");
        try
        {
            // 1. Obter lista de mapeamentos atuais
            var psi = new ProcessStartInfo("cmd.exe", "/c net use")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = Process.Start(psi);
            string output = await process.StandardOutput.ReadToEndAsync();
            await Task.Run(() => process.WaitForExit());

            // 2. Parsear as linhas de mapeamento
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var mappings = lines
                .Where(l => l.Trim().StartsWith("\\"))
                .Select(l =>
                {
                    var parts = l.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2 && parts[1].StartsWith("\\"))
                        return (Drive: parts[0], Path: parts[1]);
                    return (Drive: "", Path: "");
                })
                .Where(m => !string.IsNullOrEmpty(m.Drive) && !string.IsNullOrEmpty(m.Path))
                .ToList();

            if (mappings.Count == 0)
            {
                AppendToLog("Nenhum mapeamento de rede encontrado.");
                return;
            }

            // 3. Desconectar todos
            foreach (var map in mappings)
            {
                AppendToLog($"Desconectando {map.Drive} ({map.Path})...");
                await RunCommandAsync("net", $"use {map.Drive} /delete /y");
            }

            // 4. Reconectar todos
            foreach (var map in mappings)
            {
                AppendToLog($"Reconectando {map.Drive} -> {map.Path}...");
                await RunCommandAsync("net", $"use {map.Drive} {map.Path} /persistent:yes");
            }

            AppendToLog("Mapeamentos de rede restaurados com sucesso.");
        }
        catch (Exception ex)
        {
            AppendToLog($"Erro ao restaurar mapeamentos: {ex.Message}");
        }
    }

    private async Task LimparCacheIcones()
    {
        AppendToLog("Limpando cache de �cones...");
        try
        {
            // Fecha todos os explorers
            await RunCommandAsync("taskkill", "/f /im explorer.exe");

            // Diret�rio do cache de �cones
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string iconCacheDir = Path.Combine(localAppData, "Microsoft", "Windows", "Explorer");

            // Apaga todos os arquivos IconCache*.* em %LocalAppData%
            var iconCacheFiles = Directory.GetFiles(localAppData, "IconCache*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in iconCacheFiles)
            {
                try { File.Delete(file); AppendToLog($"Removido: {file}"); }
                catch (Exception ex) { AppendToLog($"Erro ao remover {file}: {ex.Message}"); }
            }

            // Apaga todos os arquivos IconCache*.* na pasta Explorer
            if (Directory.Exists(iconCacheDir))
            {
                var explorerCacheFiles = Directory.GetFiles(iconCacheDir, "IconCache*.*", SearchOption.TopDirectoryOnly);
                foreach (var file in explorerCacheFiles)
                {
                    try { File.Delete(file); AppendToLog($"Removido: {file}"); }
                    catch (Exception ex) { AppendToLog($"Erro ao remover {file}: {ex.Message}"); }
                }
            }

            AppendToLog("Cache de �cones removido.");
        }
        catch (Exception ex)
        {
            AppendToLog($"Erro ao limpar cache de �cones: {ex.Message}");
        }
        finally
        {
            AppendToLog("ATEN��O: Para restaurar a barra de tarefas e os �cones, reinicie o computador.");
            MessageBox.Show(
                "A limpeza do cache de �cones foi conclu�da.\n\n" +
                "Para restaurar a barra de tarefas e os �cones corretamente, � necess�rio REINICIAR o computador.",
                "Reinicie o computador",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }

    private async Task LimparCacheNavegadores()
    {
        AppendToLog("Limpando cache dos navegadores...");

        try
        {
            // Chrome
            string chromeCache = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"Google\Chrome\User Data\Default\Cache");
            
            // Edge
            string edgeCache = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"Microsoft\Edge\User Data\Default\Cache");
            
            // Firefox
            string firefoxCache = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"Mozilla\Firefox\Profiles");

            // Fecha os navegadores
            await RunCommandAsync("taskkill", "/F /IM chrome.exe /T");
            await RunCommandAsync("taskkill", "/F /IM msedge.exe /T");
            await RunCommandAsync("taskkill", "/F /IM firefox.exe /T");

            // Limpa os caches
            if (Directory.Exists(chromeCache))
            {
                Directory.Delete(chromeCache, true);
                AppendToLog("Cache do Chrome limpo.");
            }

            if (Directory.Exists(edgeCache))
            {
                Directory.Delete(edgeCache, true);
                AppendToLog("Cache do Edge limpo.");
            }

            if (Directory.Exists(firefoxCache))
            {
                foreach (var profile in Directory.GetDirectories(firefoxCache))
                {
                    var cache = Path.Combine(profile, "cache2");
                    if (Directory.Exists(cache))
                    {
                        Directory.Delete(cache, true);
                    }
                }
                AppendToLog("Cache do Firefox limpo.");
            }

            AppendToLog("Limpeza de cache dos navegadores conclu�da.");
        }
        catch (Exception ex)
        {
            AppendToLog($"Erro ao limpar cache dos navegadores: {ex.Message}");
        }
    }

    private void AbrirVisualizadorEventos()
    {
        AppendToLog("Abrindo Visualizador de Eventos...");
        Process.Start("eventvwr.msc");
    }

    private void AbrirGerenciadorDispositivos()
    {
        AppendToLog("Abrindo Gerenciador de Dispositivos...");
        Process.Start("devmgmt.msc");
    }

    private void AbrirConfiguracoesVideo()
    {
        AppendToLog("Abrindo Configura��es de V�deo...");
        Process.Start("ms-settings:display");
    }

    private void AbrirWindowsUpdate()
    {
        AppendToLog("Abrindo Windows Update...");
        Process.Start("ms-settings:windowsupdate");
    }

    private async Task ForcarReinicio()
    {
        var result = MessageBox.Show(
            "Escolha o tempo para reiniciar:\n\n" +
            "Sim = 5 minutos\n" +
            "N�o = 10 minutos\n" +
            "Cancelar = Cancelar rein�cio",
            "Agendar Rein�cio",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Cancel)
        {
            AppendToLog("Rein�cio cancelado pelo usu�rio.");
            return;
        }

        int minutos = result == DialogResult.Yes ? 5 : 10;
        AppendToLog($"Agendando rein�cio para {minutos} minutos...");

        // Cancela qualquer shutdown agendado anteriormente
        await RunCommandAsync("shutdown", "/a");

        // Agenda o novo shutdown
        await RunCommandAsync("shutdown", $"/r /t {minutos * 60}");

        MessageBox.Show(
            $"O computador ser� reiniciado em {minutos} minutos.\n" +
            "Salve seus trabalhos e feche todos os programas.",
            "Rein�cio Agendado",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );

        AppendToLog($"Rein�cio agendado para {minutos} minutos.");
    }

    private async Task CriarPontoRestauracao()
    {
        AppendToLog("Criando ponto de restaura��o...");
        await RunCommandAsync("powershell.exe", "-Command \"Checkpoint-Computer -Description 'Ponto de Restauracao - Otimizacao' -RestorePointType 'MODIFY_SETTINGS'\"");
        AppendToLog("Ponto de restaura��o criado.");
    }
    private async Task LimparArquivosTemporarios()
    {
        var result = MessageBox.Show(
            "ATEN��O!\n\n" +
            "Para limpar os arquivos tempor�rios com seguran�a, � necess�rio:\n\n" +
            "1. Fechar todos os programas abertos\n" +
            "2. Salvar todos os trabalhos em andamento\n" +
            "3. Fechar todos os navegadores\n\n" +
            "Alguns arquivos podem estar em uso e n�o ser�o exclu�dos se os programas " +
            "continuarem abertos.\n\n" +
            "Deseja continuar?",
            "Aviso - Limpeza de Arquivos Tempor�rios",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );

        if (result == DialogResult.Yes)
        {
            AppendToLog("Limpando arquivos tempor�rios...");
            
            // Lista de diret�rios tempor�rios comuns
            string[] tempPaths = {
                Environment.GetEnvironmentVariable("TEMP"),
                Environment.GetEnvironmentVariable("TMP"),
                @"C:\Windows\Temp",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp")
            };

            foreach (string path in tempPaths)
            {
                if (Directory.Exists(path))
                {
                    AppendToLog($"Limpando: {path}");
                    try
                    {
                        await RunCommandAsync("cmd.exe", $"/c del /f /s /q \"{path}\\*.*\" > nul 2>&1");
                    }
                    catch (Exception ex)
                    {
                        AppendToLog($"Erro ao limpar {path}: {ex.Message}");
                    }
                }
            }

            // Limpar tamb�m a Lixeira
            try
            {
                AppendToLog("Esvaziando a Lixeira...");
                await RunCommandAsync("cmd.exe", "/c rd /s /q %systemdrive%\\$Recycle.bin");
            }
            catch (Exception ex)
            {
                AppendToLog($"Erro ao esvaziar a Lixeira: {ex.Message}");
            }

            AppendToLog("Limpeza de arquivos tempor�rios conclu�da.");
            MessageBox.Show(
                "Limpeza de arquivos tempor�rios conclu�da!\n\n" +
                "Se alguns arquivos n�o puderam ser exclu�dos, isso significa que eles " +
                "ainda est�o em uso por algum programa.",
                "Limpeza Conclu�da",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        else
        {
            AppendToLog("Limpeza de arquivos tempor�rios cancelada pelo usu�rio.");
        }
    }
    private async Task DesativarCortana()
    {
        AppendToLog("Desativando Cortana...");
        await RunCommandAsync("reg.exe", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search\" /v AllowCortana /t REG_DWORD /d 0 /f");
        AppendToLog("Cortana desativada.");
    }
    private async Task DesativarTelemetria()
    {
        AppendToLog("Desativando Telemetria...");
        await RunCommandAsync("sc.exe", "stop DiagTrack");
        await RunCommandAsync("sc.exe", "config DiagTrack start= disabled");
        await RunCommandAsync("sc.exe", "stop dmwappushservice");
        await RunCommandAsync("sc.exe", "config dmwappushservice start= disabled");
        AppendToLog("Telemetria desativada.");
    }
    private async Task PlanoAltoDesempenho()
    {
        AppendToLog("Configurando plano de energia para Alto Desempenho...");
        await RunCommandAsync("powercfg.exe", "-setactive SCHEME_MIN");
        AppendToLog("Plano de energia configurado.");
    }
    private async Task RemoverBloatware()
    {
        AppendToLog("Removendo apps pr�-instalados...");
        string[] packages = { "xboxapp", "xboxgamingoverlay", "solitairecollection", "zunevideo", "zune", "music", "skypeapp", "bingweather", "peopleofficehub", "getstarted", "feedbackhub" };
        foreach (var pkg in packages)
        {
            await RunCommandAsync("powershell.exe", $"-Command \"Get-AppxPackage *{pkg}* | ForEach-Object {{ Remove-AppxPackage -Package $_.PackageFullName -ErrorAction SilentlyContinue }}\"");
        }
        AppendToLog("Apps pr�-instalados removidos.");
    }
    private async Task DesabilitarServicos()
    {
        AppendToLog("Desabilitando servi�os desnecess�rios...");
        await RunCommandAsync("sc.exe", "config Fax start= disabled");
        await RunCommandAsync("sc.exe", "stop Fax");
        await RunCommandAsync("sc.exe", "config MapsBroker start= disabled");
        await RunCommandAsync("sc.exe", "stop MapsBroker");
        AppendToLog("Servi�os desnecess�rios desabilitados.");
    }
    private async Task WindowsUpdateManual()
    {
        AppendToLog("Ajustando Windows Update para manual...");
        await RunCommandAsync("sc.exe", "config wuauserv start= demand");
        await RunCommandAsync("net.exe", "stop wuauserv");
        AppendToLog("Windows Update ajustado para manual.");
    }
    private async Task EfeitosVisuaisDesempenho()
    {
        AppendToLog("Ajustando efeitos visuais para desempenho...");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VisualEffects\" /v VisualFXSetting /t REG_DWORD /d 2 /f");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v ListviewAlphaSelect /t REG_DWORD /d 0 /f");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v TaskbarAnimations /t REG_DWORD /d 0 /f");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Control Panel\\Desktop\" /v DragFullWindows /t REG_SZ /d 0 /f");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Control Panel\\Desktop\" /v MenuShowDelay /t REG_SZ /d 0 /f");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Control Panel\\Desktop\" /v FontSmoothing /t REG_SZ /d 2 /f");
        await RunCommandAsync("reg.exe", "add \"HKCU\\Control Panel\\Desktop\" /v UserPreferencesMask /t REG_BINARY /d 9012038010000000 /f");
        AppendToLog("Efeitos visuais ajustados.");
    }
    private async Task SFCeCHKDSK()
    {
        AppendToLog("Executando SFC /scannow...");
        await RunCommandAsync("sfc.exe", "/scannow");
        AppendToLog("Executando CHKDSK C: /F /R...");
        await RunCommandAsync("cmd.exe", "/c chkdsk C: /F /R");
        AppendToLog("Verifica��o de disco agendada.");
    }
    private async Task LimparSpooler()
    {
        AppendToLog("Limpando fila de impress�o (Spooler)...");
        await RunCommandAsync("net.exe", "stop spooler");
        await RunCommandAsync("cmd.exe", "/c del /q /f /s \"C:\\Windows\\System32\\spool\\PRINTERS\\*.*\"");
        await RunCommandAsync("net.exe", "start spooler");
        AppendToLog("Fila de impress�o limpa.");
    }
    private async Task SolucionarRede()
    {
        AppendToLog("Solucionando problemas de conectividade de rede...");
        await RunCommandAsync("ipconfig.exe", "/release");
        await RunCommandAsync("ipconfig.exe", "/renew");
        await RunCommandAsync("ipconfig.exe", "/flushdns");
        await RunCommandAsync("netsh.exe", "winsock reset");
        AppendToLog("Configura��es de rede renovadas e cache DNS limpo.");
    }
    private async Task LimparAtualizacoesAntigas()
    {
        AppendToLog("Limpando atualiza��es antigas do Windows...");
        await RunCommandAsync("dism.exe", "/Online /Cleanup-Image /StartComponentCleanup /ResetBase");
        AppendToLog("Atualiza��es antigas removidas.");
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

    private async Task RunCommandAsync(string fileName, string arguments)
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            UserName = "administrador",
            Password = ConvertToSecureString(DecodeBase64(senhaBase64)),
            Domain = "scarduaca.local"
        };

        using (var process = new Process { StartInfo = psi })
        {
            process.OutputDataReceived += (s, e) => { if (e.Data != null) AppendToLog(e.Data); };
            process.ErrorDataReceived += (s, e) => { if (e.Data != null) AppendToLog(e.Data); };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await Task.Run(() => process.WaitForExit());
        }
    }

    private void AppendToLog(string text)
    {
        if (richTextBoxLog.InvokeRequired)
            richTextBoxLog.Invoke(new Action<string>(AppendToLog), text);
        else
        {
            richTextBoxLog.AppendText(text + Environment.NewLine);
            richTextBoxLog.ScrollToCaret();
        }
    }
}
