import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class FileService {
  private api = `${environment.apiUrl}/files`;
  private publicApi = `${environment.apiUrl}/public`;

  constructor(private http: HttpClient) {}

uploadFile(title: string, type: number, file: File, classId: number) {
  const formData = new FormData();
  formData.append('title', title);
  formData.append('type', type.toString());
  formData.append('file', file);
  formData.append('classId', classId.toString()); // ✅ correspond à FileUploadRequest.ClassId
  return this.http.post(`${this.api}/upload`, formData);
}


getFilesByClass(classId: number) {
  return this.http.get<any[]>(`${this.api}/by-class/${classId}`);
}





getCahiersByProf(slug: string) {
  return this.http.get<any[]>(`${this.api}/cahiers/${slug}`);
}


  getFilesByProf(slug: string) {
    return this.http.get<any[]>(`${this.publicApi}/files/${slug}`);
  }

  deleteFile(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
