import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { FileService } from '../../core/services/file.service';

@Component({
  selector: 'app-prof-public',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './prof-public.component.html',
  styleUrls: ['./prof-public.component.scss']
})
export class ProfPublicComponent implements OnInit {
  slug = '';
  files: any[] = [];
  profName = '';

  constructor(private route: ActivatedRoute, private fileService: FileService) {}

  ngOnInit() {
    this.slug = this.route.snapshot.paramMap.get('slug') || '';
    this.profName = this.slug.split('-').map(s => s.charAt(0).toUpperCase() + s.slice(1)).join(' ');
    this.loadFiles();
  }
getTypeLabel(type: number): string {
  switch (type) {
    case 0: return 'Cours';
    case 1: return 'TD';
    case 2: return 'TP';
    case 3: return 'Autre';
    default: return 'Inconnu';
  }
}

  loadFiles() {
    if (!this.slug) return;
    this.fileService.getFilesByProf(this.slug).subscribe(res => this.files = res);
  }
}
