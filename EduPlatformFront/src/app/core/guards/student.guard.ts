import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class StudentGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const stored = localStorage.getItem('studentAccessList');
    if (!stored) {
      alert('⛔ Accès refusé : aucun code enregistré.');
      this.router.navigate(['/student/join']);
      return false;
    }

    const list = JSON.parse(stored);
    const now = new Date().getTime();
    const routeId = Number(route.params['id']);

    // 🔎 Cherche si cette classe est autorisée
    const access = list.find((x: any) => x.classId === routeId && x.expiry > now);

    if (!access) {
      alert('🚫 Vous n’avez pas accès à cette classe ou le code a expiré.');
      this.router.navigate(['/student/join']);
      return false;
    }

    return true;
  }
}
