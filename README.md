# StampCollectorApp

[🇬🇧 Versão em Inglês](./README.en.md)

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-8.0-blueviolet)
![Platform](https://img.shields.io/badge/platforms-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20Mac-informational)
![License](https://img.shields.io/badge/license-MIT-green)

> **StampCollectorApp** é uma aplicação multiplataforma para colecionadores de selos, desenvolvida em .NET MAUI (.NET 8). Permite gerir selos, coleções, categorias e imagens de forma intuitiva e organizada.

---

## ✨ Funcionalidades

- **Gestão de Selos:** Adicione, edite, remova e pesquise selos.
- **Coleções:** Organize selos em coleções, com controlo de quantidade esperada e colecionada.
- **Categorias:** Classifique selos por categorias personalizadas.
- **Imagens:** Associe imagens locais ou pesquise imagens de selos via API do Pixabay.
- **Validações inteligentes:** Preenchimento obrigatório, datas, valores positivos, etc.
- **Modo Troca:** Marque selos como disponíveis para troca.
- **Notas e Detalhes:** Registre informações detalhadas sobre cada selo e coleção.

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

> *(Em breve...)*

---

## 🤝 Contacto

Dúvidas ou sugestões?  
Abra uma issue ou entre em contato pelo [seu-email@dominio.com](mailto:seu-email@dominio.com).
