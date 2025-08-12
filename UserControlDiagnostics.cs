using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Drawing;

namespace Central_de_Software
{
    public class UserControlDiagnostics : UserControl
    {
        protected RichTextBox richTextBoxInfo;
        protected Button btnDiagnostico;
        protected Button btnCopiar;
        protected Button btnSalvar;
        protected SaveFileDialog saveFileDialog;
        protected Label lblAdmin;

        protected Assembly managementAssembly;
        protected Assembly serviceProcessAssembly;
        protected Type managementObjectSearcherType;
        protected Type serviceControllerType;

        public UserControlDiagnostics()
        {
            LoadRequiredAssemblies();
            InitializeComponent();
        }

        private void LoadRequiredAssemblies()
        {
            try
            {
                managementAssembly = Assembly.Load("System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                serviceProcessAssembly = Assembly.Load("System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                
                managementObjectSearcherType = managementAssembly.GetType("System.Management.ManagementObjectSearcher");
                serviceControllerType = serviceProcessAssembly.GetType("System.ServiceProcess.ServiceController");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar assemblies: {ex.Message}\n\nAlgumas funcionalidades de diagn�stico podem n�o estar dispon�veis.", 
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeComponent()
        {
            this.Width = 700;
            this.Height = 660;
            this.Dock = DockStyle.Fill;

            richTextBoxInfo = new RichTextBox
            {
                Top = 50,
                Left = 10,
                Width = 670,
                Height = 550,
                ReadOnly = true,
                Font = new Font("Consolas", 10F),
                BackColor = Color.White
            };

            btnDiagnostico = new Button
            {
                Text = "Executar Diagn�stico Completo",
                Top = 10,
                Left = 10,
                Width = 180,
                Height = 30,
                FlatStyle = FlatStyle.Flat
            };
            btnDiagnostico.FlatAppearance.BorderSize = 0;
            btnDiagnostico.Click += btnDiagnostico_Click;

            btnCopiar = new Button
            {
                Text = "Copiar Resultados",
                Top = 10,
                Left = 200,
                Width = 140,
                Height = 30,
                Enabled = false,
                FlatStyle = FlatStyle.Flat
            };
            btnCopiar.FlatAppearance.BorderSize = 0;
            btnCopiar.Click += btnCopiar_Click;

            btnSalvar = new Button
            {
                Text = "Salvar Relat�rio",
                Top = 10,
                Left = 350,
                Width = 140,
                Height = 30,
                Enabled = false,
                FlatStyle = FlatStyle.Flat
            };
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.Click += btnSalvar_Click;

            saveFileDialog = new SaveFileDialog
            {
                Filter = "Arquivo de texto|*.txt",
                Title = "Salvar relat�rio de diagn�stico"
            };

            this.Controls.Add(richTextBoxInfo);
            this.Controls.Add(btnDiagnostico);
            this.Controls.Add(btnCopiar);
            this.Controls.Add(btnSalvar);
        }

        protected void btnDiagnostico_Click(object sender, EventArgs e)
        {
            try
            {
                btnDiagnostico.Enabled = false;
                richTextBoxInfo.Clear();
                AppendLine("=== Iniciando diagn�stico do sistema ===\n");

                Task.Run(() =>
                {
                    if (managementObjectSearcherType != null)
                    {
                        ColetarInformacoesWindows();
                        ColetarInformacoesCPU();
                        ColetarInformacoesRAM();
                        ColetarInformacoesArmazenamento();
                        ColetarInformacoesRede();
                        ColetarInformacoesAntivirus();
                        if (serviceControllerType != null)
                        {
                            ColetarInformacoesServicos();
                        }
                    }
                }).ContinueWith(_ =>
                {
                    AppendLine("\n=== Diagn�stico conclu�do ===");
                    btnCopiar.Enabled = true;
                    btnSalvar.Enabled = true;
                    btnDiagnostico.Enabled = true;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar diagn�stico: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDiagnostico.Enabled = true;
            }
        }

        protected void AppendLine(string text)
        {
            if (richTextBoxInfo.InvokeRequired)
            {
                richTextBoxInfo.Invoke(new Action<string>(AppendLine), text);
            }
            else
            {
                richTextBoxInfo.AppendText(text + Environment.NewLine);
            }
        }

        protected void ColetarInformacoesWindows()
        {
            AppendLine("--- Informa��es do Windows ---");
            try
            {
                dynamic searcher = Activator.CreateInstance(managementObjectSearcherType, "SELECT * FROM Win32_OperatingSystem");
                var items = searcher.Get();
                foreach (dynamic item in items)
                {
                    AppendLine($"Sistema Operacional: {item["Caption"]}");
                    AppendLine($"Vers�o: {item["Version"]}");
                    AppendLine($"Arquitetura: {item["OSArchitecture"]}");
                    var lastBootTime = item["LastBootUpTime"].ToString();
                    var bootTime = ManagementDateTimeConverter(lastBootTime);
                    AppendLine($"�ltimo Boot: {bootTime}");
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es do Windows: {ex.Message}");
            }
            AppendLine("");
        }

        private DateTime ManagementDateTimeConverter(string dmtfDate)
        {
            var year = int.Parse(dmtfDate.Substring(0, 4));
            var month = int.Parse(dmtfDate.Substring(4, 2));
            var day = int.Parse(dmtfDate.Substring(6, 2));
            var hour = int.Parse(dmtfDate.Substring(8, 2));
            var minute = int.Parse(dmtfDate.Substring(10, 2));
            var second = int.Parse(dmtfDate.Substring(12, 2));
            var microSeconds = int.Parse(dmtfDate.Substring(15, 6));
            return new DateTime(year, month, day, hour, minute, second, microSeconds / 1000);
        }

        protected void ColetarInformacoesCPU()
        {
            AppendLine("--- Informa��es da CPU ---");
            try
            {
                dynamic searcher = Activator.CreateInstance(managementObjectSearcherType, "SELECT * FROM Win32_Processor");
                var items = searcher.Get();
                foreach (dynamic item in items)
                {
                    AppendLine($"Processador: {item["Name"]}");
                    AppendLine($"N�cleos: {item["NumberOfCores"]}");
                    AppendLine($"Threads: {item["NumberOfLogicalProcessors"]}");
                    AppendLine($"Velocidade: {item["MaxClockSpeed"]} MHz");
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es da CPU: {ex.Message}");
            }
            AppendLine("");
        }

        protected void ColetarInformacoesRAM()
        {
            AppendLine("--- Informa��es da Mem�ria RAM ---");
            try
            {
                dynamic searcher = Activator.CreateInstance(managementObjectSearcherType, "SELECT * FROM Win32_ComputerSystem");
                var items = searcher.Get();
                foreach (dynamic item in items)
                {
                    ulong totalMemoryKB = Convert.ToUInt64(item["TotalPhysicalMemory"]) / 1024;
                    ulong totalMemoryMB = totalMemoryKB / 1024;
                    ulong totalMemoryGB = totalMemoryMB / 1024;
                    AppendLine($"Mem�ria Total: {totalMemoryGB} GB");
                }

                searcher = Activator.CreateInstance(managementObjectSearcherType, "SELECT * FROM Win32_OperatingSystem");
                items = searcher.Get();
                foreach (dynamic item in items)
                {
                    ulong freeMemoryKB = Convert.ToUInt64(item["FreePhysicalMemory"]);
                    ulong freeMemoryMB = freeMemoryKB / 1024;
                    ulong freeMemoryGB = freeMemoryMB / 1024;
                    AppendLine($"Mem�ria Livre: {freeMemoryGB} GB");
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es da RAM: {ex.Message}");
            }
            AppendLine("");
        }

        protected void ColetarInformacoesArmazenamento()
        {
            AppendLine("--- Informa��es de Armazenamento ---");
            try
            {
                foreach (var drive in System.IO.DriveInfo.GetDrives().Where(d => d.IsReady))
                {
                    AppendLine($"Unidade: {drive.Name}");
                    AppendLine($"R�tulo: {drive.VolumeLabel}");
                    AppendLine($"Sistema de Arquivos: {drive.DriveFormat}");
                    AppendLine($"Espa�o Total: {drive.TotalSize / 1073741824} GB");
                    AppendLine($"Espa�o Livre: {drive.AvailableFreeSpace / 1073741824} GB");
                    AppendLine("");
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es de armazenamento: {ex.Message}");
            }
            AppendLine("");
        }

        protected void ColetarInformacoesRede()
        {
            AppendLine("--- Informa��es de Rede ---");
            try
            {
                dynamic searcher = Activator.CreateInstance(managementObjectSearcherType, "SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter=True");
                var items = searcher.Get();
                foreach (dynamic item in items)
                {
                    AppendLine($"Adaptador: {item["Name"]}");
                    AppendLine($"Fabricante: {item["Manufacturer"]}");
                    AppendLine($"Status: {(Convert.ToString(item["NetEnabled"]) == "True" ? "Conectado" : "Desconectado")}");
                    AppendLine("");
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es de rede: {ex.Message}");
            }
            AppendLine("");
        }

        protected void ColetarInformacoesAntivirus()
        {
            AppendLine("--- Informa��es do Antiv�rus ---");
            try
            {
                dynamic searcher = Activator.CreateInstance(managementObjectSearcherType, new[] { @"root\SecurityCenter2", "SELECT * FROM AntivirusProduct" });
                var items = searcher.Get();
                bool encontrado = false;
                foreach (dynamic item in items)
                {
                    encontrado = true;
                    AppendLine($"Nome: {item["displayName"]}");
                    AppendLine($"Estado: {(Convert.ToString(item["productState"]) == "397568" ? "Ativo" : "Inativo")}");
                    AppendLine("");
                }
                if (!encontrado)
                {
                    AppendLine("Nenhum antiv�rus detectado");
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es do antiv�rus: {ex.Message}");
            }
            AppendLine("");
        }

        protected void ColetarInformacoesServicos()
        {
            AppendLine("--- Servi�os Importantes ---");
            try
            {
                string[] servicosImportantes = { "wuauserv", "WinDefend", "wscsvc", "Spooler" };
                foreach (var servico in servicosImportantes)
                {
                    try
                    {
                        dynamic sc = Activator.CreateInstance(serviceControllerType, new[] { servico });
                        AppendLine($"Servi�o: {sc.DisplayName}");
                        AppendLine($"Status: {sc.Status}");
                        AppendLine($"Tipo de Inicializa��o: {sc.StartType}");
                        AppendLine("");
                    }
                    catch
                    {
                        AppendLine($"Servi�o {servico} n�o encontrado");
                        AppendLine("");
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLine($"Erro ao coletar informa��es dos servi�os: {ex.Message}");
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBoxInfo.Text))
            {
                Clipboard.SetText(richTextBoxInfo.Text);
                MessageBox.Show("Informa��es copiadas para a �rea de transfer�ncia!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBoxInfo.Text))
                return;

            saveFileDialog.FileName = $"Diagnostico_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, richTextBoxInfo.Text);
                    MessageBox.Show("Relat�rio salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o relat�rio: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}