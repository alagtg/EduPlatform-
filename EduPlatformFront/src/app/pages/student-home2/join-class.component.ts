import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfService } from 'src/app/core/services/prof.service';

@Component({
  selector: 'app-join-class',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './join-class.component.html',
  styleUrls: ['./join-class.component.scss']
})
export class JoinClassComponent implements OnInit {
  code = '';
  classData: any = null;
  msg = '';
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private profService: ProfService,
    private router: Router
  ) {}

  ngOnInit() {
    // 🔹 Nettoyer les accès expirés à chaque ouverture de la page
    this.cleanExpiredAccess();

    // 🔹 Si un token est passé dans l’URL (ex: ?token=CL-B30267)
    const queryCode = this.route.snapshot.queryParamMap.get('token');
    if (queryCode) {
      this.code = queryCode;
      this.join();
    }
  }

  /** 🧹 Supprime les accès expirés (après 4h) */
  cleanExpiredAccess() {
    const stored = localStorage.getItem('studentAccessList');
    if (!stored) return;

    const now = new Date().getTime();
    const list = JSON.parse(stored);
    const updated = list.filter((item: any) => item.expiry > now);

    if (updated.length !== list.length) {
      localStorage.setItem('studentAccessList', JSON.stringify(updated));
    }
  }

  /** 🔐 Tente de rejoindre une classe avec un code */
  join() {
    if (!this.code.trim()) {
      this.msg = '⚠️ Code requis';
      return;
    }

    this.loading = true;
    this.msg = '';

    this.profService.getClassByCode(this.code.trim()).subscribe({
      next: (res: any) => {
        this.loading = false;
        if (res && res.id) {
          const classId = res.id;
          const expiry = new Date().getTime() + 4 * 60 * 60 * 1000; // 4h

          // 🔎 Vérifie s’il y a déjà un accès valide
          const stored = localStorage.getItem('studentAccessList');
          const list = stored ? JSON.parse(stored) : [];
          const existing = list.find((c: any) => c.classId === classId);

          if (!existing) {
            list.push({ classId, code: this.code, expiry });
            localStorage.setItem('studentAccessList', JSON.stringify(list));
          }

          // ✅ Redirige l’étudiant vers la classe
          this.router.navigate(['/student/class', classId]);
        } else {
          this.msg = '❌ Classe introuvable. Vérifiez le code.';
        }
      },
      error: err => {
        console.error('Erreur recherche classe:', err);
        this.msg = '❌ Classe introuvable ou code invalide.';
        this.loading = false;
      }
    });
  }
}
