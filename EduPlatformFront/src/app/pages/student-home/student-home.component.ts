import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProfService } from '../../core/services/prof.service';

@Component({
  selector: 'app-student-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './student-home.component.html',
  styleUrls: ['./student-home.component.scss']
})
export class StudentHomeComponent implements OnInit {
  profs: any[] = [];
  loading = true;

  constructor(private profService: ProfService) {}

  ngOnInit() {
    this.profService.getAllProfs().subscribe({
      next: data => {
        this.profs = data;
        this.loading = false;
      },
      error: err => {
        console.error('Erreur chargement profs:', err);
        this.loading = false;
      }
    });
  }
}
