# StampCollectorApp

[🇵🇹 Portuguese version](./README.md)

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-8.0-blueviolet)
![Platform](https://img.shields.io/badge/platforms-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20Mac-informational)
![License](https://img.shields.io/badge/license-MIT-green)

> **StampCollectorApp** is a cross-platform app for stamp collectors, developed with .NET MAUI (.NET 8). It allows intuitive and organized management of stamps, collections, categories, and images.

---

## ✨ Features

- **Stamp Management:** Add, edit, remove, and search for stamps.
- **Collections:** Organize stamps into collections, tracking expected and collected quantities.
- **Categories:** Classify stamps by custom categories.
- **Images:** Attach local images or search for stamp images via the Pixabay API.
- **Smart Validation:** Required fields, date checks, positive values, and more.
- **Exchange Mode:** Mark stamps as available for exchange.
- **Registering Exchanges:** Easily record a stamp exchange with another collector. After selecting a stamp for exchange, fill in the collector's name and contact information, and add notes if desired. The app keeps a full history of all exchanges for future reference.
- **Notes and Details:** Record detailed information about each stamp and collection.

---

## 🔄 About Registering Exchanges

The app allows you to register stamp exchanges with other collectors easily:

1. **Select a stamp marked as available for exchange.**
2. **Enter the collector's information** (name and contact) and add notes about the exchange if you wish.
3. **Register the exchange:** The stamp will be removed from your collection, and the exchange will be added to your history.
4. **Exchange history:** Review all exchanges in a dedicated section of the app, with details such as date, collector name, contact, and notes.
5. **Tip:** After registering an exchange, if you have not yet added the stamp received from the other collector, please do so to keep your collection up to date.

---

## 🚀 Technologies

- [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) (.NET 8)
- SQLite (local storage)
- CommunityToolkit.Mvvm (MVVM)
- Pixabay API (image search)
- C#

---

## 📱 Supported Platforms

- Android
- iOS
- Windows
- Mac Catalyst

---

## 🌐 Localization

This application supports both Portuguese and English languages. You can switch the language in the app settings or according to your system preferences.

- **Portuguese**: Fully translated user interface.
- **English**: Fully translated user interface.

If you'd like to contribute translations or suggest improvements, feel free to open an issue or pull request!
---


## 🛠️ How to run

1. **Clone the repository:**
   ```sh
   git clone https://github.com/fauxtix/StampCollectorApp.git
   cd StampCollectorApp
   ```
2. **Open in Visual Studio 2022 or newer.**
3. **Restore NuGet packages:**
   - Visual Studio does this automatically when opening the solution.
4. **Select the desired platform (Android, Windows, etc) and run (F5).**

---

## 📂 Project Structure

```
StampCollectorApp/
 ├── Models/           # Data models (Stamp, Collection, Category, etc)
 ├── ViewModels/       # Presentation logic (MVVM)
 ├── Views/            # XAML pages
 ├── Services/         # Data services and integrations
 ├── Resources/        # Images, styles, etc
 ├── AppShell.xaml     # Main navigation
 ├── MauiProgram.cs    # DI configuration and initialization
 └── ...
```

---

## 📝 Contributing

Contributions are welcome!  
Open an issue or submit a pull request.

---

## 📄 License

This project is licensed under the MIT license.

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

## 🤝 Contact

Questions or suggestions?  
Open an issue please.
