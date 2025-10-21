import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfService } from 'src/app/core/services/prof.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-classroom-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './classroom-list.component.html',
  styleUrls: ['./classroom-list.component.scss']
})
export class ClassroomListComponent implements OnInit {
  classes: any[] = [];
  newClass = { name: '', description: '' };
  msg = '';
  loading = false;

  constructor(private profService: ProfService ,     private router: Router,
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.loading = true;
    this.profService.getMyClasses().subscribe({
      next: data => {
        this.classes = data;
        this.loading = false;
      },
      error: err => {
        console.error('Erreur chargement classes:', err);
        this.msg = '❌ Erreur lors du chargement des classes';
        this.loading = false;
      }
    });
  }
 gotoClasses() {
    this.router.navigate(['/dashboard']);
  }
  create() {
    if (!this.newClass.name.trim()) {
      this.msg = '⚠️ Le nom est obligatoire';
      return;
    }

 this.profService.createClassroom(this.newClass).subscribe({
  next: () => {
    this.msg = '✅ Classe créée avec succès';
    this.newClass = { name: '', description: '' };
    this.load();
  },
  error: (err: any) => {
    console.error('Erreur création classe:', err);
    this.msg = '❌ Impossible de créer la classe';
  }
});

  }
}
