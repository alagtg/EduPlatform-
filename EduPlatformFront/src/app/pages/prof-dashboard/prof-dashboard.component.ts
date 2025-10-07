import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FileService } from '../../core/services/file.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-prof-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './prof-dashboard.component.html',
  styleUrls: ['./prof-dashboard.component.scss']
})
export class ProfDashboardComponent implements OnInit {
  files: any[] = [];
  title = '';
  type = 0;
  selectedFile: File | null = null;
  slug = '';

  uploading = false;
  deletingId: number | null = null;

  constructor(private fileService: FileService, private auth: AuthService) {}
getTypeLabel(type: number): string {
  switch (type) {
    case 0: return 'Cours';
    case 1: return 'TD';
    case 2: return 'TP';
    case 3: return 'Autre';
    default: return 'Inconnu';
  }
}

  ngOnInit() {
    this.slug = this.auth.getProfSlugFromToken();
    this.loadFiles();
  }

  onFileSelected(e: any) { this.selectedFile = e.target.files[0]; }

  onUpload() {
    if (!this.selectedFile || !this.title) return;
    this.uploading = true;
    this.fileService.uploadFile(this.title, this.type, this.selectedFile).subscribe({
      next: () => { this.title=''; this.type=0; this.selectedFile=null; this.loadFiles(); },
      complete: () => this.uploading = false
    });
  }

  loadFiles() {
    if (!this.slug) return;
    this.fileService.getFilesByProf(this.slug).subscribe(res => this.files = res);
  }

  delete(id: number) {
    this.deletingId = id;
    this.fileService.deleteFile(id).subscribe({
      next: () => this.loadFiles(),
      complete: () => this.deletingId = null
    });
  }
}
