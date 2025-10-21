# 🎓 EduPlatform – Plateforme de partage de cours pour professeurs et étudiants

**EduPlatform** est une application complète permettant aux professeurs de partager facilement leurs **cours, TD et TP**, et aux étudiants de les consulter librement selon le professeur sélectionné.  
Développée avec **.NET 8 (API)**, **Angular 18 (Frontend)** et **SQL Server**, elle repose sur une architecture moderne et modulaire.

---

## 🚀 Fonctionnalités principales

### 👨‍🏫 Espace Professeur
- Authentification sécurisée (JWT)
- Gestion de profil
- Upload de fichiers (cours, TD, TP, autres)
- Suppression et téléchargement des ressources
- Interface claire et responsive

### 🎓 Espace Étudiant
- Liste dynamique de tous les professeurs (chargée depuis la base)
- Consultation des fichiers (cours, TD, TP) selon le professeur choisi
- Téléchargement direct des ressources

### ⚙️ Architecture technique
- **Backend :** ASP.NET Core Web API
- **Frontend :** Angular + Bootstrap
- **Base de données :** SQL Server
- **Authentification :** JWT
- **ORM :** Entity Framework Core
- **Modèle :** Architecture en couches (Controller → Service → Repository → DbContext)

---


---

## ⚙️ Installation et exécution

### 1️⃣ Prérequis
- [Node.js](https://nodejs.org/en/) (v18+)
- [Angular CLI](https://angular.dev/cli)
- [.NET SDK 8](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

### 2️⃣ Configuration de la base de données

Crée une base `EduPlatform` dans SQL Server.

Mets à jour la chaîne de connexion dans :
`EduPlatform.API/appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EduPlatform;Trusted_Connection=True;TrustServerCertificate=True;"
}
🔑 Comptes de test
Type	           Email       	Mot de passe	Slug
Professeur	ala@example.com
                            	123456	mr-ala
Professeur	houda@example.com
                             	123456	mme-houda

🧠 API principale
Méthode	Endpoint	Description
POST	/api/auth/login	Connexion prof
POST	/api/prof	Création d’un prof
GET	/api/prof/all	Liste de tous les professeurs
GET	/api/prof/me	Infos du prof connecté
POST	/api/files/upload	Upload fichier
GET	/api/public/files/{slug}	Liste des fichiers d’un prof
GET	/api/public/download/{id}	Télécharger un fichier
