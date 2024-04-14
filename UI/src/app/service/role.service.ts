import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Role } from '../models/role.model';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  constructor(private http: HttpClient) {}
  public getAllRole(): Observable<Role[]> {
    return this.http.get<Role[]>('https://localhost:7111/api/Role');
  }
}
