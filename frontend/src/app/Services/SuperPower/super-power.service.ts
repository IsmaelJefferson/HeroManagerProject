import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface SuperPower {
  id: number;
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class SuperPowerService {
  private apiUrl = 'http://localhost:5214/api/SuperPower'; // ajuste se necessário

  constructor(private http: HttpClient) { }

  getAll(): Observable<SuperPower[]> {
    return this.http.get<SuperPower[]>(this.apiUrl);
  }
}