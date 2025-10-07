import { Routes } from '@angular/router';
import { ProfLoginComponent } from './pages/prof-login/prof-login.component';
import { ProfDashboardComponent } from './pages/prof-dashboard/prof-dashboard.component';
import { ProfPublicComponent } from './pages/prof-public/prof-public.component';
import { authGuard } from './core/guards/auth.guard';
import { HomeComponent } from './pages/home/home.component';
import { StudentHomeComponent } from './pages/student-home/student-home.component';

export const routes: Routes = [
  // ✅ Page de connexion
  { path: 'login', component: ProfLoginComponent },
  { path: '', component: HomeComponent },
  { path: 'student', component: StudentHomeComponent }, // 🧩 Nouvelle route

  // ✅ Dashboard (protégé)
  { path: 'dashboard', component: ProfDashboardComponent, canActivate: [authGuard] },

  // ✅ Page publique du prof
  { path: 'prof/:slug', component: ProfPublicComponent },

   // ✅ Gestion des routes inconnues
  { path: '**', redirectTo: '' }
];
