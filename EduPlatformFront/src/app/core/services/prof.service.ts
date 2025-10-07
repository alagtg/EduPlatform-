import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProfService {
  private api = `${environment.apiUrl}/prof`;

  constructor(private http: HttpClient) {}

  /** 🔐 Récupère les infos du prof connecté */
  me() {
    return this.http.get<any>(`${this.api}/me`);
  }

  /** 🌍 Récupère la liste de tous les profs (espace étudiant) */
  getAllProfs() {
    return this.http.get<any[]>(`${this.api}/all`);
  }
}
