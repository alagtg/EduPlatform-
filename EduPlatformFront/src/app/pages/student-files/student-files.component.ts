import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FileService } from 'src/app/core/services/file.service';
import { OrderByDatePipe } from 'src/app/core/order-by-date.pipe';

@Component({
  selector: 'app-student-files',
  standalone: true,
  imports: [CommonModule, OrderByDatePipe],
  templateUrl: './student-files.component.html',
  styleUrls: ['./student-files.component.scss']
})
export class StudentFilesComponent implements OnInit {
  classId!: number;
  files: any[] = [];
  filteredFiles: any[] = [];
  loading = true;

  selectedType = 'Tous';
  resourceTypes = [
    { label: 'Tous', icon: '🌍' },
    { label: 'Cours', icon: '📘' },
    { label: 'TD', icon: '🧩' },
    { label: 'TP', icon: '🧪' },
    { label: 'Autre', icon: '📄' }
  ];

  currentPage = 1;
  itemsPerPage = 10;

  constructor(private route: ActivatedRoute, private fileService: FileService) {}

  ngOnInit() {
    this.classId = +this.route.snapshot.params['id'];
    this.fileService.getFilesByClass(this.classId).subscribe({
      next: res => {
        this.files = res || [];
        this.files.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());

        this.updateFilteredFiles();
        this.loading = false;
      },
      error: err => {
        console.error('Erreur chargement fichiers:', err);
        this.loading = false;
      }
    });
  }

  setSelectedType(type: string) {
    this.selectedType = type;
    this.currentPage = 1;
    this.updateFilteredFiles();
  }

  updateFilteredFiles() {
    let filtered =
      this.selectedType === 'Tous'
        ? [...this.files]
        : this.files.filter(f => this.getTypeLabel(f.type) === this.selectedType);

    const start = (this.currentPage - 1) * this.itemsPerPage;
    this.filteredFiles = filtered.slice(start, start + this.itemsPerPage);
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
    return 'Autre';
  }

  // 🔹 Découpe titre et description si "—" existe
  getTitleOnly(fullTitle: string): string {
    return fullTitle.includes('—') ? fullTitle.split('—')[0].trim() : fullTitle;
  }

  getDescriptionOnly(fullTitle: string): string {
    return fullTitle.includes('—') ? fullTitle.split('—')[1].trim() : '';
  }

  get totalPages(): number {
    const count =
      this.selectedType === 'Tous'
        ? this.files.length
        : this.files.filter(f => this.getTypeLabel(f.type) === this.selectedType).length;
    return Math.ceil(count / this.itemsPerPage);
  }

  changePage(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updateFilteredFiles();
    }
  }
}
