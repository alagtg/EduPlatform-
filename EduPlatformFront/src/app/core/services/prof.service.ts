import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ProfService {
  private api = `${environment.apiUrl}/prof`;
  private classroomApi = `${environment.apiUrl}/classrooms`;
  private publicApi = `${environment.apiUrl}/public`;

  constructor(private http: HttpClient) {}

  /** 🔐 Récupère le profil du professeur connecté */
  me(): Observable<any> {
    return this.http.get<any>(`${this.api}/me`);
  }

  /** 🌍 Liste de tous les professeurs (espace étudiant) */
  getAllProfs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/all`);
  }

  /** 🏫 Liste des classes du prof connecté */
  getMyClasses(): Observable<any[]> {
    return this.http.get<any[]>(`${this.classroomApi}`);
  }

  /** ➕ Crée une nouvelle classe (prof connecté) */
  createClassroom(payload: { name: string; description?: string }): Observable<any> {
    return this.http.post(`${this.classroomApi}`, payload);
  }

  /** 🔎 Rejoindre une classe via un code d’accès */
  getClassByCode(code: string): Observable<any> {
    return this.http.get<any>(`${this.classroomApi}/${code}`);
  }

  /** 🧩 Fichiers publics d’un prof (pour espace étudiant) */
  getPublicFilesByProfSlug(slug: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.publicApi}/files/${slug}`);
  }
}
