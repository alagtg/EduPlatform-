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

## 🧱 Structure du projet

### 🖥️ Backend (`EduPlatform.API/`)

EduPlatform.API/
├── Controllers/
│ ├── AuthController.cs
│ ├── ProfController.cs
│ ├── FilesController.cs
│ └── PublicController.cs
├── Data/
│ └── EduDbContext.cs
├── DTOs/
│ ├── Prof/
│ │ ├── CreateProfRequest.cs
│ │ └── ProfResponse.cs
│ ├── Files/
│ │ ├── FileUploadRequest.cs
│ │ └── FileResponse.cs
│ └── Comments/
│ ├── CreateCommentRequest.cs
│ └── CommentResponse.cs
├── Models/
│ ├── Prof.cs
│ ├── FileResource.cs
│ ├── Comment.cs
│ └── Enums.cs
├── Repositories/
│ ├── Interfaces/
│ └── Implementations/
├── Services/
│ ├── Interfaces/
│ └── Implementations/
├── appsettings.json
└── Program.cs

yaml
Copier le code

---

### 🌐 Frontend (`EduPlatformFront/`)

EduPlatformFront/
├── src/
│ ├── app/
│ │ ├── core/
│ │ │ ├── guards/
│ │ │ └── services/
│ │ ├── components/
│ │ │ ├── navbar/
│ │ │ └── footer/
│ │ ├── pages/
│ │ │ ├── home/
│ │ │ ├── student-home/
│ │ │ ├── prof-login/
│ │ │ ├── prof-dashboard/
│ │ │ └── prof-public/
│ │ └── app.routes.ts
│ ├── assets/
│ ├── environments/
│ │ ├── environment.ts
│ │ └── environment.prod.ts
│ └── main.ts
└── package.json

yaml
Copier le code

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
Applique les migrations :

bash
Copier le code
cd EduPlatform.API
dotnet ef database update
3️⃣ Lancer le backend
bash
Copier le code
cd EduPlatform.API
dotnet run
➡️ L’API sera disponible sur :

arduino
Copier le code
https://localhost:7142
4️⃣ Lancer le frontend
bash
Copier le code
cd EduPlatformFront
npm install
ng serve
➡️ Application accessible sur :

arduino
Copier le code
http://localhost:4200
🔑 Comptes de test
Type	       Email	             Mot de passe	        Slug
Professeur	ala@example.com	            P@ssw0rd!	     mr-ala
Professeur	houda@example.com	        P@ssw0rd!	       mme-houda

🧠 API principale
Méthode	Endpoint	Description
POST	/api/auth/login	Connexion prof
POST	/api/prof	Création d’un prof
GET	/api/prof/all	Liste de tous les professeurs
GET	/api/prof/me	Infos du prof connecté
POST	/api/files/upload	Upload fichier
GET	/api/public/files/{slug}	Liste des fichiers d’un prof
GET	/api/public/download/{id}	Télécharger un fichier

🎨 Interface
Design clair et moderne (bleu/blanc inspiré de Google Classroom)

Bootstrap 5 intégré

Responsive sur mobile et desktop

Pied de page personnalisé : © @ala

🧩 Technologies
Côté	Technologies
Backend	.NET 8, EF Core, SQL Server, JWT Auth
Frontend	Angular 18, Bootstrap 5, SCSS
Outils	Swagger, Visual Studio Code, SSMS

