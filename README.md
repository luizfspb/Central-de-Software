# Central de Software

Central de Software � uma aplica��o Windows Forms desenvolvida em C# (.NET Framework 4.8) para facilitar a instala��o, manuten��o e otimiza��o de computadores Windows em ambientes corporativos ou t�cnicos.

## Funcionalidades

- Instala��o e desinstala��o de softwares populares (AnyDesk, Google Chrome, WinRAR, WhatsApp Desktop, Adobe Acrobat Reader, Revo Uninstaller, Office 365, Visualizador Apollo, 3uTools).
- Execu��o de ferramentas de otimiza��o do Windows (limpeza de arquivos tempor�rios, desabilitar servi�os, restaurar mapeamentos de rede, etc).
- Acesso a fun��es administrativas e t�cnicas com modo protegido por senha.
- Interface moderna, intuitiva e responsiva.
- Suporte a execu��o de programas como administrador de dom�nio.
- Instala��o de softwares a partir de recursos embutidos (embedded resources).

## Como usar

1. Compile o projeto no Visual Studio (requer .NET Framework 4.8).
2. Execute o aplicativo.
3. Utilize o menu lateral para acessar as �reas de Softwares, Tools, Fiscal e Diagn�stico.
4. Para acessar o modo t�cnico, pressione `Ctrl+T` e insira a senha (padr�o: 1212).

## Estrutura do Projeto

- **UserControlSoftware.cs**: Interface para instala��o/desinstala��o de softwares.
- **UserControlSoftwareFunction.cs**: L�gica de instala��o/desinstala��o e execu��o de softwares.
- **UserControlTools.cs**: Ferramentas de otimiza��o do Windows.
- **UserControl3uTools.cs**: Acesso r�pido ao 3uTools.
- **FormMain.cs**: Tela principal e navega��o.
- **FormTecnico.cs**: Tela do modo t�cnico.
- **Utilities/**: Utilit�rios para recursos e instala��o de softwares.

## Observa��es

- Para softwares port�teis (ex: Visualizador Apollo), o bot�o apenas extrai e executa o arquivo, sem instala��o/desinstala��o.
- Certifique-se de que todos os execut�veis estejam na pasta `softwares` do projeto e marcados como **Embedded Resource**.
- O Visualizador Apollo e outros softwares port�teis n�o exibem bot�o de desinstala��o.

## Licen�a

Este projeto � privado e de uso interno.
