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
- **Categories:** Classify stamps under custom categories.
- **Images:** Attach local images or search for stamp images via the Pixabay API.
- **Smart validation:** Required fields, dates, positive values, etc.
- **Exchange Mode:** Mark stamps as available for exchange.
- **Notes and Details:** Record detailed information about each stamp and collection.

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

> *(Coming soon...)*

---

## 🤝 Contact

Questions or suggestions?  
Open an issue or contact us at [your-email@domain.com](mailto:your-email@domain.com).
