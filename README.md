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

![Splash](https://github.com/user-attachments/assets/6281c02c-f3d1-45b4-ad4b-aed2b21f4379)
![Opcoes](https://github.com/user-attachments/assets/2fd55f64-e831-4d93-9856-bf4b6056ebe4)
![Stamps](https://github.com/user-attachments/assets/6b289627-6af5-4c1f-ba35-9174a32260b0)
![StampEdit_1](https://github.com/user-attachments/assets/f3bc1688-5de0-417a-84d2-9e2195788891)
![StampEdit_2](https://github.com/user-attachments/assets/f90d7ec0-ece3-4d73-9b15-a974f6219cb9)
![Paises](https://github.com/user-attachments/assets/472b6cae-c530-4ee7-a5cd-6952202c287f)
![Colecoes](https://github.com/user-attachments/assets/9209d9f6-a494-4fcb-8cdf-a035dac04819)
![Colecoes_Novo](https://github.com/user-attachments/assets/424d2f0c-0e2a-41ec-b827-34757aa273d2)
![Colecoes_Edit](https://github.com/user-attachments/assets/995ea6c3-769c-4e2f-8440-80864a7fd695)
![Categorias](https://github.com/user-attachments/assets/c80168d9-d403-48f8-8895-09f478e3aebe)

---

## 🤝 Contacto

Dúvidas ou sugestões?  
Abra uma issue ou entre em contato pelo [seu-email@dominio.com](mailto:seu-email@dominio.com).
