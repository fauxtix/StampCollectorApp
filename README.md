# StampCollectorApp

[🇬🇧 Versão em Inglês](./README.en.md)

![AppImage](https://github.com/user-attachments/assets/950f0edf-a3f5-4aae-9b06-989cbeffea34)

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-8.0-blueviolet)
![Platform](https://img.shields.io/badge/platforms-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20Mac-informational)
![License](https://img.shields.io/badge/license-MIT-green)

> **StampCollectorApp** é uma aplicação multiplataforma para colecionadores de selos, desenvolvida em .NET MAUI (.NET 8). Permite gerir selos, trocas, coleções, categorias e imagens de forma intuitiva e organizada.

---

## ✨ Funcionalidades

- **Gestão de Selos:** Adicione, edite, remova e pesquise selos.
- **Coleções:** Organize selos em coleções, com controlo de quantidade esperada e colecionada.
- **Categorias:** Classifique selos por categorias personalizadas.
- **Imagens:** Associe imagens locais ou pesquise imagens de selos via API do Pixabay.
- **Validações inteligentes:** Preenchimento obrigatório, datas, valores positivos, etc.
- **Modo Troca:** Marque selos como disponíveis para troca.
- **Registro de Trocas:** Registe facilmente a troca de um selo com outro colecionador. Após selecionar o selo para troca, preencha o nome e contacto do colecionador, e adicione notas se desejar. Um histórico de todas as trocas realizadas fica disponível na aplicação para consulta posterior.
- **Notas e Detalhes:** Registe informações detalhadas sobre cada selo e coleção.
- **Dashboard interativo:** Visualize estatísticas e resumos através de gráficos e indicadores, com filtros avançados por Categoria, Coleção e País. Permite uma análise personalizada dos seus dados de coleção.

---

## 📊 Dashboard

A página de dashboard oferece uma visão geral da sua coleção, com métricas como:

- Quantidade total de selos, coleções, categorias e trocas;
- Gráfico de selos por categoria, por coleção e por país;
- Evolução das trocas nos últimos 12 meses.

**Opções de filtragem:**  
Use os filtros no topo do dashboard para visualizar os dados de acordo com:

- **Categoria**: Mostra apenas selos de uma categoria selecionada.
- **Coleção**: Filtra estatísticas para uma coleção específica.
- **País**: Permite ver apenas selos de determinado país.

Após escolher os filtros desejados, clique em **"Aplicar filtros"** para atualizar os gráficos e indicadores. Para voltar à visualização global, basta usar a opção **"Limpar filtros"**.

---
## 🔄 Sobre o Registo de Trocas

A aplicação permite registar trocas de selos com outros colecionadores de forma prática:

1. **Selecione um selo marcado como disponível para troca.**
2. **Preencha as informações do colecionador** (nome, contacto e data da troca) e, se desejar, adicione notas sobre a troca.
3. **Registe a troca:** O selo será removido da sua coleção e a troca será adicionada ao histórico.
4. **Histórico de trocas:** Consulte todas as trocas realizadas numa secção específica da aplicação, com detalhes como data, nome do colecionador, contacto e notas.
5. **Dica:** Após registar a troca, caso ainda não tenha registado o selo recebido do outro colecionador, faça-o para manter a sua coleção sempre atualizada. A aplicação exibe um alerta para o efeito.

---



## 🚀 Tecnologias

- [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) (.NET 8)
- SQLite (armazenamento local)
- CommunityToolkit.Mvvm (MVVM)
- API Pixabay (busca de imagens)
- C#

---

## 📱 Plataformas Suportadas

- Android
- iOS
- Windows
- Mac Catalyst

---

## 🌐 Localização

Esta aplicação suporta os idiomas Português e Inglês. Pode selecionar o idioma no ecrã inicial (Selos). A escolha é persistida no armazenamento local.

- **Português**: Interface totalmente traduzida para português.
- **Inglês**: Interface totalmente traduzida para inglês.

Caso queira contribuir com traduções ou sugerir melhorias, sinta-se à vontade para abrir uma issue ou um pull request!

---

## 🛠️ Como executar

1. **Clone o repositório:**
   ```sh
   git clone https://github.com/fauxtix/StampCollectorApp.git
   cd StampCollectorApp
   ```
2. **Abra no Visual Studio 2022 ou superior.**
3. **Restaure os pacotes NuGet:**
   - O Visual Studio faz isso automaticamente ao abrir a solução.
4. **Selecione a plataforma desejada (Android, Windows, etc) e execute (F5).**

---

## 📂 Estrutura do Projeto

```
StampCollectorApp/
 ├── Models/           # Modelos de dados (Stamp, Collection, Category, etc)
 ├── ViewModels/       # Lógica de apresentação (MVVM)
 ├── Views/            # Páginas XAML
 ├── Services/         # Serviços de dados e integrações
 ├── Resources/        # Imagens, estilos, etc
 ├── AppShell.xaml     # Navegação principal
 ├── MauiProgram.cs    # Configuração de DI e inicialização
 └── ...
```

---

## 📝 Contribuição

Contribuições são bem-vindas!  
Abra uma issue ou envie um pull request.

---

## 📄 Licença

Este projeto está licenciado sob a licença MIT.

---

## 📷 Screenshots

![Splash](https://github.com/user-attachments/assets/792e9004-a1d5-41bf-bbf2-967da12d2570)
![Opcoes](https://github.com/user-attachments/assets/35373060-f11e-455e-83be-e9ffea3b0157)
![Dashboard_1](https://github.com/user-attachments/assets/ae624f08-24f7-48a3-b6a2-7381b200b207)
![Dashboard_2](https://github.com/user-attachments/assets/80423a0b-2dd8-4aeb-9596-b7b32604aabd)
![Colecoes](https://github.com/user-attachments/assets/32db104d-9c9d-45ed-aa2c-292cea31f685)
![Colecoes_Novo](https://github.com/user-attachments/assets/1a197a80-081f-4f8e-883c-3b8b0a147cc9)
![Colecoes_Edit](https://github.com/user-attachments/assets/dcbc9ed7-bbe2-4a47-9af5-496780c6105c)
![Collection_Delete_Warning](https://github.com/user-attachments/assets/c3cc9a55-a0e2-4dc0-ab1b-56f631c53a1e)
![Categorias](https://github.com/user-attachments/assets/8b58450e-3914-424d-ad39-1f7e2172c92c)
![Paises](https://github.com/user-attachments/assets/a032b801-34ff-4c94-afb0-4e368ff988a9)
![Paises_edit](https://github.com/user-attachments/assets/74a08561-5142-468c-8300-f4853accc49d)

![Stamps](https://github.com/user-attachments/assets/ec95d70a-5d12-446e-a7d1-61c087f36dd1)
![StampNovo_1](https://github.com/user-attachments/assets/25cbfa60-8815-4d95-80a2-8f54d024674b)
![StampNovo_2](https://github.com/user-attachments/assets/41b5eeaf-e707-4ff3-8171-5940341c5fe3)
![StampEdit_1](https://github.com/user-attachments/assets/cdf87ec8-7ea2-498d-bfa8-dde24992b48e)
![StampEdit_2](https://github.com/user-attachments/assets/7ae1cb65-6abf-4464-9b87-910d3fa7885a)
![Trocas](https://github.com/user-attachments/assets/425ec19d-6e45-434b-b8e3-72694213beea)


---

## 🤝 Contacto

Dúvidas ou sugestões?  
Abra uma issue, por favor.
