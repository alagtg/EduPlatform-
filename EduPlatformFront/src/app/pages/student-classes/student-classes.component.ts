import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProfService } from 'src/app/core/services/prof.service';

@Component({
  selector: 'app-student-classes',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './student-classes.component.html',
  styleUrls: ['./student-classes.component.scss']
})
export class StudentClassesComponent implements OnInit {
  slug!: string;
  classes: any[] = [];
  loading = true;

  constructor(private route: ActivatedRoute, private profService: ProfService) {}

ngOnInit() {
  this.slug = this.route.snapshot.params['slug'];
  this.profService.getPublicFilesByProfSlug(this.slug).subscribe({
    next: (files: any[]) => {
      // Extraire les classes uniques à partir des fichiers
      const classesMap = new Map();
      files.forEach(f => {
        if (f.classroomName && !classesMap.has(f.classroomName)) {
          classesMap.set(f.classroomName, { id: f.classroomId, name: f.classroomName });
        }
      });
      this.classes = Array.from(classesMap.values());
      this.loading = false;
    },
    error: err => {
      console.error('Erreur chargement classes:', err);
      this.loading = false;
    }
  });
}

}
