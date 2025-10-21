import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CahierPedagogiqueService } from '../../core/services/cahier-pedagogique.service';
import { ProfService } from '../../core/services/prof.service';

@Component({
  selector: 'app-prof-public',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './prof-public.component.html',
  styleUrls: ['./prof-public.component.scss']
})
export class ProfPublicComponent implements OnInit {
  classes: any[] = [];
  cahiers: any[] = [];
  pagedCahiers: any[] = [];
  selectedClassId: number | null = null;
  selectedFile: File | null = null;
  fileName = '';
  description = '';
  uploading = false;

  // Pagination
  currentPage = 1;
  pageSize = 6;
  totalPages = 0;

  constructor(
    private cahierService: CahierPedagogiqueService,
    private profService: ProfService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadClasses();
  }

  gotoClasses() {
    this.router.navigate(['/dashboard']);
  }

  /** Charger les classes du prof */
  loadClasses() {
    this.profService.getMyClasses().subscribe({
      next: data => (this.classes = data || []),
      error: err => console.error('Erreur chargement classes:', err)
    });
  }

  /** Charger les cahiers */
  loadCahiers() {
    if (!this.selectedClassId) return;
    this.cahierService.getCahiersByClass(this.selectedClassId).subscribe({
      next: res => {
        this.cahiers = (res || []).sort(
          (a: any, b: any) =>
            new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
        );
        this.currentPage = 1;
        this.updatePagination();
      },
      error: err => console.error('Erreur chargement cahiers:', err)
    });
  }

  /** Pagination */
  updatePagination() {
    this.totalPages = Math.ceil(this.cahiers.length / this.pageSize);
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.pagedCahiers = this.cahiers.slice(start, end);
  }

  changePage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePagination();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  /** Upload du cahier pédagogique */
  uploadCahier() {
    if (!this.selectedFile || !this.selectedClassId || !this.fileName.trim()) {
      alert('Veuillez remplir le titre et choisir un fichier.');
      return;
    }

    // 💡 Combine titre + description pour ne rien changer au backend
    let finalTitle = this.fileName.trim();
    if (this.description.trim() !== '') {
      finalTitle += ' — ' + this.description.trim();
    }

    this.uploading = true;
    this.cahierService.uploadCahier(
      this.selectedFile,
      this.selectedClassId,
      finalTitle
    ).subscribe({
      next: () => {
        this.uploading = false;
        this.fileName = '';
        this.description = '';
        this.selectedFile = null;
        this.loadCahiers();
      },
      error: err => {
        console.error('Erreur upload:', err);
        this.uploading = false;
      }
    });
  }

  deleteCahier(id: number) {
    if (!confirm('Voulez-vous vraiment supprimer ce cahier ?')) return;

    this.cahierService.deleteCahier(id).subscribe({
      next: () => {
        this.cahiers = this.cahiers.filter(c => c.id !== id);
        this.updatePagination();
        alert('🗑️ Cahier supprimé avec succès');
      },
      error: err => console.error('Erreur suppression:', err)
    });
  }

  /** 🔍 Utilitaire pour séparer titre et description */
  splitTitle(title: string) {
    const parts = title.split('—');
    return {
      main: parts[0]?.trim() || title,
      desc: parts[1]?.trim() || ''
    };
  }
}
