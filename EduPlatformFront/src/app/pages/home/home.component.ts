import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  // Tu peux ajouter ici des variables dynamiques si besoin
  appName = 'EduPlatform';

  constructor() {}

  // Exemple (facultatif) : fonction pour scroller vers le bas
  scrollToContent() {
    window.scrollTo({ top: 800, behavior: 'smooth' });
  }
}
