# 🏆 World Cup Application

A modern, interactive C# desktop application that visualizes FIFA World Cup data using both **WinForms** and **WPF**. This application offers detailed team views, player layouts, animations, settings persistence, and rich UI enhancements.

---

## Features

### Core Functionality

- View national teams and their full player rosters
- Display player positions on a soccer field (with dynamic layouts)
- Track match results, goals, and yellow cards
- Support for favorite team and favorite player selections
- Team-vs-Team comparison with animated match outcomes
- Custom loading spinners and progress feedback
- Fallback loading strategy (API vs. File)

### User Interface

- WinForms interface with:
  - Custom loading panel
  - MessageBox-style interaction for error handling
- WPF interface with:
  - Animated player positioning
  - Fancy ComboBoxes
  - Confetti-like celebration effects
  - Custom player card controls (large and small)
  - Loading spinner and overlay components
  - Full MVVM pattern with `INotifyPropertyChanged` bindings

### Architecture

- Shared `WorldCupData` class library for models, enums, and services
- `SettingsService` for loading/saving app preferences
- `PathHelper` for resolving relative paths regardless of execution context
- Modular and reusable custom controls (e.g., `LoadingPanel`, `PlayerCardControl`)
- Dynamic style switching, localization, and theme responsiveness

## Project structure

/WorldCupProject
│
├── WorldCupData/ # Shared models, enums, data access
│ ├── Models/
│ ├── Services/
│ └── Helpers/ # PathHelper, LanguageService, etc.
│
├── WorldCupForms/ # Windows Forms UI
│ ├── MainForm.cs
│ ├── CustomControls/
│ └── LoadingPanel.cs
│
├── WorldCupWPF/ # WPF MVVM UI
│ ├── Views/
│ ├── ViewModels/
│ ├── Controls/
│ ├── Resources/ # Localization & themes
│ └── App.xaml
│
└── README.md

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or newer
- NuGet dependencies (if any)

### Running the Application

#### WinForms App:

```bash
Set WorldCupForms as Startup Project
Run (F5)
```

#### WPF App:

```bash
Set WorldCupWPF as Startup Project
Run (F5)
```
