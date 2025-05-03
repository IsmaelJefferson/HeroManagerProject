import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private apiUrl = 'http://localhost:5214/api/HeroContoller';

  constructor(private http: HttpClient) { }

  getHeroes(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getHeroById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  addHero(hero: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, hero);
  }

  updateHero(id: number, hero: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, hero);
  }

  deleteHero(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
