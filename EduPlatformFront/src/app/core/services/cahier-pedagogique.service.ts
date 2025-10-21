import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CahierPedagogiqueService {
private api = `${environment.apiUrl}/cahiers`; // ✅ correspond à [Route("api/cahiers")]

  constructor(private http: HttpClient) {}

  /** 🔹 Upload d’un cahier pédagogique */
  uploadCahier(file: File, classId: number, fileName: string): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('classId', classId.toString());
    formData.append('fileName', fileName || file.name);
    return this.http.post(`${this.api}/upload-cahier`, formData);
  }

  /** 🔹 Récupérer tous les cahiers d’une classe */
  getCahiersByClass(classId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/by-class/${classId}`);
  }

  /** 🔹 Récupérer un cahier spécifique */
  getCahierById(id: number): Observable<any> {
    return this.http.get<any>(`${this.api}/cahier/${id}`);
  }

  /** 🔹 Supprimer un cahier pédagogique */
  deleteCahier(id: number): Observable<any> {
    return this.http.delete(`${this.api}/delete-cahier/${id}`);
  }
}
