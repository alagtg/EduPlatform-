# EduPlatformFront (Angular 18)

Style clair & moderne (bleu/blanc), page publique `/prof/:slug`,
authentification JWT pour professeurs, dashboard avec upload & gestion des fichiers.

## Installation

```bash
npm install
ng serve
```

Par défaut, l'app utilise l'API: `https://localhost:7043/api` (voir `src/environments/`).
Assure-toi que le backend EduPlatform.API tourne sur ce port ou adapte l'URL.

## Pages

- **/prof/:slug** — Page publique d’un professeur (ex: `/prof/mme-houda`)
- **/:slug** — Variante courte (ex: `/mme-houda`)
- **/login** — Connexion Professeur
- **/dashboard** — Tableau de bord (protégé)

## Design

- Bootstrap 5 importé via `angular.json` (CSS+JS)
- Styles globaux dans `src/styles.scss`
- Navbar + Footer minimaliste (`© @ala`)
