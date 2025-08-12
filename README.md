# ⚙️ Central de Software

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue)](https://dotnet.microsoft.com/download/dotnet-framework/net48)
[![Windows](https://img.shields.io/badge/Windows-10%2F11-green)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/License-Proprietary%20(View%20Only)-red)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Portfolio%20Demo-yellow)]()

**Central de Software** é uma solução corporativa abrangente desenvolvida em C# Windows Forms (.NET Framework 4.8) para automatizar e centralizar tarefas críticas de TI em ambientes Windows empresariais. A ferramenta visa aumentar a produtividade técnica, padronizar procedimentos operacionais e reduzir o tempo de manutenção de sistemas.

> 🔒 **DEMO ONLY - Proprietary software for portfolio demonstration. Download/use prohibited. Contact for licensing: luizfspb@gmail.com**

---

## 📋 Índice

- [Características Principais](#-características-principais)
- [Funcionalidades Detalhadas](#-funcionalidades-detalhadas)
- [Requisitos do Sistema](#-requisitos-do-sistema)
- [Instalação e Configuração](#-instalação-e-configuração)
- [Guia de Uso](#-guia-de-uso)
- [Arquitetura do Projeto](#-arquitetura-do-projeto)
- [Configurações Avançadas](#-configurações-avançadas)
- [Troubleshooting](#-troubleshooting)
- [Contribuição](#-contribuição)
- [Licença](#-licença)

---

## ✨ Características Principais

- **🎯 Interface Unificada**: Centralização de todas as ferramentas de TI em uma única aplicação
- **📦 Instaladores Embutidos**: Software packages integrados para funcionamento offline
- **🔒 Modo Técnico Protegido**: Acesso administrativo com autenticação segura
- **📊 Diagnóstico Completo**: Análise detalhada de hardware e software do sistema
- **🚀 Automação de Tarefas**: Otimização e manutenção automatizadas
- **🌐 Integração Corporativa**: Suporte para ambientes de domínio Active Directory

---

## 🔧 Funcionalidades Detalhadas

### 1. **Gerenciamento de Software** 
*Módulo: `UserControlSoftware.cs` | `UserControlSoftwareFunction.cs`*

#### Instalação Automatizada
- **AnyDesk** - Acesso remoto corporativo
- **Google Chrome** - Navegador web padrão
- **WinRAR** - Compactador de arquivos
- **WhatsApp Desktop** - Comunicação empresarial
- **Adobe Acrobat Reader** - Visualizador PDF
- **Revo Uninstaller** - Desinstalação completa
- **Microsoft Office 365** - Suíte de produtividade

#### Software Portátil
- **Visualizador Apollo** - Execução direta sem instalação
- Suporte extensível para outros executáveis portáteis

### 2. **Ferramentas de Otimização e Manutenção**
*Módulo: `UserControlTools.cs`*

#### Limpeza do Sistema
- Remoção de arquivos temporários do Windows
- Limpeza de cache de navegadores (Chrome, Edge, Firefox)
- Limpeza da fila de impressão
- Remoção de atualizações antigas via DISM

#### Otimização de Rede
- Diagnóstico e correção automática de problemas de rede
- Restauração de mapeamentos de rede perdidos
- Limpeza e renovação de cache DNS
- Reset completo do stack TCP/IP

#### Manutenção do Sistema
- Criação automática de pontos de restauração
- Desativação de Cortana e telemetria
- Otimização do plano de energia para máximo desempenho
- Remoção de bloatware e aplicações desnecessárias

### 3. **Modo Técnico Avançado**
*Módulo: `FormTecnico.cs`*

- **Ativação**: `Ctrl+T` na tela principal
- **Autenticação**: Senha padrão configurável (`1212`)
- **Funcionalidades Exclusivas**:
  - Ferramentas de diagnóstico avançado
  - Configurações de sistema críticas
  - Utilitários de recuperação
  - Logs detalhados de operações

### 4. **Diagnóstico Completo do Sistema**
*Módulo: `UserControlDiagnostics.cs`*

#### Informações de Hardware
- Especificações detalhadas de CPU
- Status e utilização de memória RAM
- Informações de armazenamento (HDDs/SSDs)
- Configuração de rede ativa

#### Análise de Software
- Status do antivírus instalado
- Serviços críticos do Windows
- Aplicações instaladas
- Drivers e versões

#### Relatórios
- Exportação em formatos múltiplos
- Cópia para área de transferência
- Histórico de diagnósticos

### 5. **Integração com 3uTools**
*Módulo: `UserControl3uTools.cs`*

- Execução automática com privilégios administrativos
- Autenticação integrada com credenciais de domínio
- Caminho configurável: `C:\Program Files\3uToolsV3\3uTools.exe`

---

## 💻 Requisitos do Sistema

### Mínimos
- **SO**: Windows 10 (build 1809) ou superior
- **Framework**: .NET Framework 4.8
- **RAM**: 4 GB
- **Armazenamento**: 500 MB livres
- **Privilégios**: Administrador local

### Recomendados
- **SO**: Windows 11 22H2
- **RAM**: 8 GB ou superior
- **Armazenamento**: 1 GB livres
- **Rede**: Conexão estável para atualizações

### Dependências
- Visual C++ Redistributable (incluído)
- Windows Management Instrumentation (WMI)
- PowerShell 5.1 ou superior

---

## 🚀 Instalação e Configuração

### 1. **Preparação do Ambiente de Desenvolvimento**
# Clone o repositório (se aplicável)
git clone [https://github.com/luizfspb/Central-de-Software]

# Navegue para o diretório
cd central-de-software
### 2. **Configuração no Visual Studio**

1. Abra o arquivo `.sln` no Visual Studio 2019 ou superior
2. Restaure os pacotes NuGet automaticamente
3. Verifique se o Target Framework está definido como `.NET Framework 4.8`

### 3. **Configuração de Recursos Embutidos**

Certifique-se de que todos os arquivos na pasta `softwares/` tenham:
- **Build Action**: `Embedded Resource`
- **Copy to Output Directory**: `Do not copy`

### 4. **Configuração de Credenciais**

**IMPORTANTE**: Edite o arquivo `UserControl3uTools.cs` antes da compilação:
// Substitua pelas credenciais do seu domínio
private const string DOMAIN_USER = "SEU_USUARIO_ADMIN";
private const string DOMAIN_PASSWORD_BASE64 = "SUA_SENHA_EM_BASE64";
Para converter senha para Base64:string password = "sua_senha_aqui";
string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
### 5. **Compilação**

1. **Debug**: `F5` ou `Ctrl+F5`
2. **Release**: 
   - Build → Configuration Manager
   - Selecione "Release"
   - Build → Build Solution

---

## 📖 Guia de Uso

### Primeira Execução

1. **Execute como Administrador** (obrigatório)
2. A interface principal será exibida com o menu lateral
3. Navegue entre as seções usando os botões do menu

### Navegação Principal
┌─ 🏠 Início
├─ 💿 Software
├─ 🔧 Ferramentas  
├─ 📊 Diagnóstico
├─ 📱 3uTools
└─ ℹ️ Sobre
### Atalhos de Teclado

| Atalho | Função |
|--------|--------|
| `Ctrl+T` | Acessar Modo Técnico |
| `F1` | Ajuda contextual |
| `F5` | Atualizar diagnóstico |
| `Ctrl+S` | Salvar configurações |

### Workflow Recomendado

1. **Diagnóstico Inicial**: Execute uma análise completa do sistema
2. **Limpeza**: Realize manutenção básica do sistema  
3. **Instalação**: Deploy dos softwares necessários
4. **Otimização**: Execute ferramentas de performance
5. **Verificação**: Novo diagnóstico para validar alterações

---

## 🏗️ Arquitetura do Projeto
Central-de-Software/
│
├── 📁 Forms/
│   ├── FormMain.cs              # Interface principal
│   ├── FormTecnico.cs           # Modo técnico
│   └── FormAbout.cs             # Informações da aplicação
│
├── 📁 UserControls/
│   ├── UserControlSoftware.cs   # Gerenciamento de software
│   ├── UserControlTools.cs      # Ferramentas de manutenção
│   ├── UserControlDiagnostics.cs # Diagnóstico do sistema
│   └── UserControl3uTools.cs    # Integração 3uTools
│
├── 📁 Classes/
│   ├── SoftwareManager.cs       # Lógica de instalação
│   ├── SystemDiagnostics.cs     # Coleta de informações
│   ├── NetworkTools.cs          # Utilitários de rede
│   └── SystemOptimizer.cs       # Otimizações automáticas
│
├── 📁 Resources/
│   ├── softwares/              # Instaladores embutidos
│   ├── icons/                  # Ícones da interface
│   └── configs/                # Arquivos de configuração
│
└── 📁 Utils/
    ├── Logger.cs               # Sistema de logs
    ├── Security.cs             # Funções de segurança
    └── Constants.cs            # Constantes da aplicação
---

## ⚙️ Configurações Avançadas

### Personalização de Software

Para adicionar novos instaladores:

1. Adicione o arquivo à pasta `softwares/`
2. Configure como `Embedded Resource`
3. Edite `SoftwareManager.cs`:
public void InstallCustomSoftware()
{
    var resourceName = "Central_de_Software.softwares.meu_software.exe";
    ExtractAndInstall(resourceName, "meu_software.exe", "/S");
}
### Configuração de Logs

Edite `Logger.cs` para personalizar o sistema de logs:
public static class Logger
{
    private static readonly string LogPath = @"C:\Logs\CentralSoftware\";
    private static readonly LogLevel MinLevel = LogLevel.Info;
}
### Customização da Interface

Modifique cores e temas em `FormMain.Designer.cs`:
// Cores corporativas personalizáveis
private Color PrimaryColor = Color.FromArgb(51, 122, 183);
private Color SecondaryColor = Color.FromArgb(245, 245, 245);
---

## 🔍 Troubleshooting

### Problemas Comuns

#### "Erro ao extrair recursos embutidos"
**Causa**: Arquivo não configurado como Embedded Resource  
**Solução**: 
1. Clique direito no arquivo
2. Properties → Build Action → Embedded Resource

#### "Acesso negado durante instalação"
**Causa**: Aplicação não executada como administrador  
**Solução**: Clique direito → "Executar como administrador"

#### "3uTools não encontrado"
**Causa**: Caminho de instalação diferente  
**Solução**: Edite o caminho em `UserControl3uTools.cs`

#### "Falha na autenticação de domínio"
**Causa**: Credenciais incorretas ou expiradas  
**Solução**: 
1. Verifique usuário e senha
2. Recompile após alterações
3. Teste conectividade com domínio

### Logs e Diagnóstico

#### Localização dos LogsC:\Logs\CentralSoftware\
├── application.log     # Log principal
├── install.log        # Log de instalações  
├── diagnostic.log     # Log de diagnósticos
└── error.log          # Log de erros
#### Coleta de Informações de Debug// Ativar modo debug
#if DEBUG
    Logger.LogLevel = LogLevel.Debug;
    Console.WriteLine("Debug mode active");
#endif
---

## 🤝 Contribuição

### Padrões de Desenvolvimento

- **Naming Convention**: PascalCase para classes, camelCase para variáveis
- **Comentários**: Documentar métodos públicos com XML comments
- **Error Handling**: Sempre usar try-catch em operações críticas
- **Logging**: Registrar operações importantes e erros

### Estrutura de Commits
tipo(escopo): descrição

fix(software): corrigir instalação do Chrome
feat(tools): adicionar limpeza de registro
docs(readme): atualizar guia de instalação
### Pull Requests

1. Fork do projeto
2. Criar branch feature (`git checkout -b feature/nova-funcionalidade`)
3. Commit das alterações (`git commit -m 'feat: adicionar nova funcionalidade'`)
4. Push para branch (`git push origin feature/nova-funcionalidade`)
5. Abrir Pull Request

---

## 📄 Licença e Termos de Uso

### 🔒 **PROJETO PROPRIETÁRIO - APENAS DEMONSTRAÇÃO**

Este repositório está disponível **publicamente apenas para fins de portfólio e demonstração técnica**. 

#### ⚠️ **RESTRIÇÕES IMPORTANTES:**

- **❌ Download Proibido**: O código-fonte não deve ser baixado, clonado ou utilizado
- **❌ Uso Comercial Vedado**: Qualquer uso comercial é estritamente proibido
- **❌ Modificação Não Autorizada**: Alterações ou derivações não são permitidas
- **❌ Redistribuição Proibida**: Não compartilhe, distribua ou republique este código

#### ✅ **USOS PERMITIDOS:**

- **👀 Visualização**: Para análise de técnicas de desenvolvimento e arquitetura
- **📚 Referência**: Como exemplo de boas práticas em C# Windows Forms
- **💼 Portfólio**: Demonstração de competências técnicas do desenvolvedor

#### 🏢 **USO CORPORATIVO RESTRITO**

Este software foi desenvolvido exclusivamente para ambiente corporativo interno. Qualquer implementação requer:

1. **Autorização expressa por escrito** do detentor dos direitos
2. **Licenciamento específico** para uso empresarial  
3. **Acordo de confidencialidade** assinado

#### ⚖️ **AVISOS LEGAIS**

- **Copyright © 2024** - Todos os direitos reservados
- **Violações serão processadas** conforme legislação aplicável
- **Monitoramento ativo** de uso não autorizado
- **DMCA takedowns** serão emitidos quando necessário

---

### 🚨 **IMPORTANTE PARA RECRUTADORES/EMPRESAS**

Este projeto demonstra competências técnicas em:
- Desenvolvimento C# Windows Forms
- Arquitetura de software corporativo  
- Integração com APIs do Windows
- Gerenciamento de recursos embutidos
- Implementação de segurança e autenticação

**Para discussões sobre licenciamento ou desenvolvimento similar, entre em contato através dos canais oficiais.**

---