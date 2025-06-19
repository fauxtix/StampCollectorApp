# StampCollectorApp

[🇵🇹 Portuguese version](./README.md)

![AppImage](https://github.com/user-attachments/assets/b48b15ec-1b3e-49c1-9226-800059dd1074)

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-8.0-blueviolet)
![Platform](https://img.shields.io/badge/platforms-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20Mac-informational)
![License](https://img.shields.io/badge/license-MIT-green)

> **StampCollectorApp** is a cross-platform app for stamp collectors, developed with .NET MAUI (.NET 8). It allows intuitive and organized management of stamps, exchanges, collections, categories, and images.

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
- **Interactive dashboard:** View statistics and summaries through charts and indicators, with advanced filters by Category, Collection, and Country. Allows for personalized analysis of your collection data.

---
## 📊 Dashboard

The dashboard page provides an overview of your stamp collection, displaying metrics such as:

- Total number of stamps, collections, categories, and exchanges;
- Pie charts for stamps by category, by collection, and by country;
- Exchange activity over the last 12 months.

**Filtering options:**  
Use the filters at the top of the dashboard to display data according to:

- **Category**: Shows only stamps from the selected category.
- **Collection**: Filters statistics for a specific collection.
- **Country**: Allows viewing only stamps from a particular country.

After choosing your desired filters, tap **"Apply filters"** to update the charts and indicators. To return to the global view, simply use the **"Clear filters"** option.

---
## 🔄 About Registering Exchanges

The app allows you to register stamp exchanges with other collectors easily:

1. **Select a stamp marked as available for exchange.**
2. **Enter the collector's information** (name, contact and transaction date) and add notes about the exchange if you wish.
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

![Splash](https://github.com/user-attachments/assets/5f570aac-f09c-4b8b-977b-e4afd1fdf22f)
![Opcoes_en](https://github.com/user-attachments/assets/ce1f179e-7b07-4c63-bbef-a55743db704a)
![Dashboard_1](https://github.com/user-attachments/assets/6a3b5f4a-c050-429c-9192-c858e31011d0)
![Dashboard_2](https://github.com/user-attachments/assets/85545b01-3931-429f-b913-cc49e7f133b6)
![Colecoes_en](https://github.com/user-attachments/assets/e6788cde-96ed-44e1-bb38-351cbb16dabc)
![Colecoes_Novo_en](https://github.com/user-attachments/assets/9d3b02ad-9c95-43c7-b793-58dbfb4d52eb)
![Collection_Delete_Warning_en](https://github.com/user-attachments/assets/77ac317a-6050-44e2-a36d-029b37c99f8f)

![Stamps_en](https://github.com/user-attachments/assets/ef1abcf9-9d52-451b-9fac-631f40661a62)
![StampNovo_1_en](https://github.com/user-attachments/assets/c2f33719-8369-4c31-8ace-8c859c445de2)
![StampNovo_2_en](https://github.com/user-attachments/assets/19ecc078-d285-4bc8-808e-feffc8a74470)
![StampEdit_1_en](https://github.com/user-attachments/assets/c827c0b7-d4ce-4e0a-b67c-1d2da6ff748b)
![StampEdit_2_en](https://github.com/user-attachments/assets/efc4a4b2-0687-4dc3-8111-bc55b1edbadb)

![Trocas_en](https://github.com/user-attachments/assets/b90eb0e3-22d7-41e8-a08b-c38d8a348292)

---

## 🤝 Contact

Questions or suggestions?  
Open an issue please.
