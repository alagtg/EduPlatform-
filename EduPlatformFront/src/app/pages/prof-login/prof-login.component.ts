import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-prof-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './prof-login.component.html',
  styleUrls: ['./prof-login.component.scss']
})
export class ProfLoginComponent {
  email = '';
  password = '';
  loading = false;
  error = '';

  constructor(private auth: AuthService, private router: Router) {}

  onLogin() {
    this.loading = true;
    this.error = '';
    this.auth.login(this.email, this.password).subscribe({
      next: (res: any) => {
        this.auth.saveToken(res.token);
        this.router.navigate(['/dashboard']);
      },
      error: () => { this.error = 'Email ou mot de passe invalide'; },
      complete: () => this.loading = false
    });
  }
}
