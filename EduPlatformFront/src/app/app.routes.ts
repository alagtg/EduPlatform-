import { Routes } from '@angular/router';

// Pages prof
import { ProfLoginComponent } from './pages/prof-login/prof-login.component';
import { ProfDashboardComponent } from './pages/prof-dashboard/prof-dashboard.component';
import { ClassroomListComponent } from './pages/classrooms/classroom-list.component';
import { ProfPublicComponent } from './pages/prof-public/prof-public.component';

// Pages étudiant
import { StudentHomeComponent } from './pages/student-home/student-home.component';
import { StudentClassesComponent } from './pages/student-classes/student-classes.component';
import { StudentFilesComponent } from './pages/student-files/student-files.component';
import { JoinClassComponent } from './pages/student-home2/join-class.component';

// Autres
import { HomeComponent } from './pages/home/home.component';
import { authGuard } from './core/guards/auth.guard';
import { StudentGuard } from './core/guards/student.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: ProfLoginComponent },

  // 🧑‍🎓 Espace étudiant
  { path: 'student', component: StudentHomeComponent },
  { path: 'student/join', component: JoinClassComponent },
  { path: 'student/:slug/classes', component: StudentClassesComponent },
  { path: 'student/class/:id', component: StudentFilesComponent, canActivate: [StudentGuard] },

  // 🧑‍🏫 Espace professeur
  { path: 'dashboard', component: ProfDashboardComponent, canActivate: [authGuard] },
  { path: 'dashboard/classes', component: ClassroomListComponent, canActivate: [authGuard] },
{ path: 'dashboard/prof', component: ProfPublicComponent, canActivate: [authGuard] },

  // 🔁 Redirection par défaut
  { path: '**', redirectTo: '' }
];
