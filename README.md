# Actions Ring

**Actions Ring** est une application Windows développée en **WPF (.NET 8)** qui permet d’accéder rapidement à des actions ou des liens web via un **menu circulaire (radial menu)**.

Les actions sont définies dans un fichier **JSON**, ce qui permet de modifier facilement les icônes et les liens sans modifier le code.

L’application fonctionne depuis la **zone de notification Windows (system tray)**.

---

# Aperçu

Le menu radial apparaît au centre de l’écran et affiche des icônes disposées en cercle.

Chaque icône représente une action configurable.

Fonctionnalités principales :

* Menu radial graphique
* Configuration via JSON
* Icône dans la barre de notification
* Ouverture de liens web
* Interface légère et rapide

---

# Fonctionnement

1. L’application démarre dans la **zone de notification Windows**.
2. Double-clic sur l’icône pour afficher le **menu circulaire**.
3. Cliquer sur une icône pour exécuter l’action.
4. Appuyer sur **Échap** ou cliquer au centre pour fermer le menu.

---

# Configuration des actions

Les actions sont définies dans le fichier :

```
actions.json
```

Exemple :

```json
[
  {
    "title": "Google",
    "icon": "Assets/google.png",
    "url": "https://www.google.com"
  },
  {
    "title": "GitHub",
    "icon": "Assets/github.png",
    "url": "https://github.com"
  },
  {
    "title": "YouTube",
    "icon": "Assets/youtube.png",
    "url": "https://www.youtube.com"
  }
]
```

Propriétés :

| Champ | Description         |
| ----- | ------------------- |
| title | Nom affiché         |
| icon  | Chemin vers l’icône |
| url   | Lien ouvert au clic |

---

# Structure du projet

```
ActionsRing
├── Assets
│   └── icons
├── Models
│   └── RingAction.cs
├── Services
│   └── ActionLoader.cs
├── Views
│   └── MainWindow.xaml
├── App.xaml
├── actions.json
└── ActionsRing.csproj
```

---

# Technologies utilisées

* **.NET 8**
* **WPF**
* **System.Text.Json**
* **NotifyIcon (Windows Forms)**

---

# Installation

## Prérequis

* Windows
* .NET 8 SDK
* Visual Studio avec **.NET Desktop Development**

## Cloner le projet

```bash
git clone https://github.com/USERNAME/actions-ring.git
cd actions-ring
```

## Lancer l’application

Dans Visual Studio :

```
Build → Run
```

Ou en ligne de commande :

```bash
dotnet run
```

---

# Personnalisation

Tu peux facilement :

* ajouter de nouvelles actions
* modifier les icônes
* changer les liens
* adapter le rayon du menu

Tout se fait principalement dans :

```
actions.json
```

---

# Idées d'amélioration

* animation du menu radial
* raccourci clavier global
* support d’actions système
* lancement d’applications locales
* catégories ou sous-menus
* éditeur graphique des actions

---

# Licence

MIT License

---

# Auteur

Projet développé par **Didier Hermann**.
