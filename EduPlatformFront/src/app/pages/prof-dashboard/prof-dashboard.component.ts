import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FileService } from '../../core/services/file.service';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { ProfService } from '../../core/services/prof.service';
import { OrderByDatePipe } from 'src/app/core/order-by-date.pipe';

@Component({
  selector: 'app-prof-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, OrderByDatePipe],
  templateUrl: './prof-dashboard.component.html',
  styleUrls: ['./prof-dashboard.component.scss']
})
export class ProfDashboardComponent implements OnInit {
  files: any[] = [];
  classes: any[] = [];
  selectedClassId: number | null = null;
description = '';
  title = '';
  type = 0;
  selectedFile: File | null = null;

  uploading = false;
  deletingId: number | null = null;
  loadingFiles = false;

  // 🔹 Pagination
  currentPage = 1;
  itemsPerPage = 3; // ✅ 3 fichiers par page pour le prof

  // 🔹 Filtres
  selectedType = 'Tous';
  resourceTypes = [
    { label: 'Tous', icon: '🌍' },
    { label: 'Cours', icon: '📘' },
    { label: 'TD', icon: '🧩' },
    { label: 'TP', icon: '🧪' },
    { label: 'Autre', icon: '📄' }
  ];

  constructor(
    private fileService: FileService,
    private auth: AuthService,
    private router: Router,
    private profService: ProfService
  ) {}

  ngOnInit() {
    this.loadClasses();
  }

  gotoClasses() {
    this.router.navigate(['/dashboard/classes']);
  }

  gotoprof() {
    this.router.navigate(['/dashboard/prof']);
  }
  loadClasses() {
    this.profService.getMyClasses().subscribe({
      next: data => {
        this.classes = data;
        if (this.classes.length > 0) {
          this.selectedClassId = this.classes[0].id;
          this.loadFiles();
        }
      }
    });
  }

  loadFiles() {
    if (!this.selectedClassId) return;
    this.loadingFiles = true;

    this.fileService.getFilesByClass(this.selectedClassId).subscribe({
      next: res => {
        this.files = res || [];
        this.files.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());

        this.loadingFiles = false;
        this.currentPage = 1; // reset pagination
      },
      error: () => (this.loadingFiles = false)
    });
  }

  onFileSelected(e: any) {
    this.selectedFile = e.target.files[0];
  }

 onUpload() {
  if (!this.selectedFile || !this.title || !this.selectedClassId) return;

  // 🧠 Combine le titre et la description pour ne pas perdre les infos
  let fullTitle = this.title;
  if (this.description && this.description.trim() !== '') {
    fullTitle += ' — ' + this.description.trim();
  }

  this.uploading = true;
  this.fileService.uploadFile(fullTitle, this.type, this.selectedFile, this.selectedClassId)
    .subscribe({
      next: () => {
        this.title = '';
        this.description = '';
        this.type = 0;
        this.selectedFile = null;
        this.loadFiles(); // recharge avec les nouveaux titres
      },
      complete: () => this.uploading = false
    });
}


  delete(id: number) {
    this.deletingId = id;
    this.fileService.deleteFile(id).subscribe({
      next: () => this.loadFiles(),
      complete: () => (this.deletingId = null)
    });
  }

  // 🔹 Filtres
  setSelectedType(type: string) {
    this.selectedType = type;
    this.currentPage = 1;
  }

  get filteredFiles() {
    let filtered = this.selectedType === 'Tous'
      ? this.files
      : this.files.filter(f => this.getTypeLabel(f.type) === this.selectedType);

    // Pagination slice
    const start = (this.currentPage - 1) * this.itemsPerPage;
    return filtered.slice(start, start + this.itemsPerPage);
  }

  get totalPages(): number {
    const count =
      this.selectedType === 'Tous'
        ? this.files.length
        : this.files.filter(f => this.getTypeLabel(f.type) === this.selectedType).length;
    return Math.ceil(count / this.itemsPerPage);
  }

  changePage(page: number) {
    if (page >= 1 && page <= this.totalPages) this.currentPage = page;
  }

  getCount(type: string): number {
    if (type === 'Tous') return this.files.length;
    return this.files.filter(f => this.getTypeLabel(f.type) === type).length;
  }

  getTypeLabel(type: any): string {
    if (typeof type === 'number') {
      switch (type) {
        case 0: return 'Cours';
        case 1: return 'TD';
        case 2: return 'TP';
        case 3: return 'Autre';
      }
    }
    if (typeof type === 'string') {
      switch (type.toLowerCase()) {
        case 'cours': return 'Cours';
        case 'td': return 'TD';
        case 'tp': return 'TP';
        case 'autre': return 'Autre';
      }
    }
    return 'Inconnu';
  }
}
