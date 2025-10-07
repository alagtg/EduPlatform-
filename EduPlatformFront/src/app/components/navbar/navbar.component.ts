import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { ProfService } from '../../core/services/prof.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  userName: string = '';

  constructor(
    public auth: AuthService,
    private profService: ProfService,
    private router: Router
  ) {}

  ngOnInit() {
    // Si le prof est connecté → on récupère ses infos depuis l’API
    if (this.auth.isLoggedIn()) {
      this.profService.me().subscribe({
        next: (prof: any) => {
          this.userName = prof.name || '';
        },
        error: (err) => {
          console.error('Erreur lors du chargement du profil :', err);
        }
      });
    }
  }

  gotoDashboard() {
    this.router.navigate(['/dashboard']);
  }

  logout() {
    this.auth.logout();
  }
}
