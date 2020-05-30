import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, catchError, map } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { Role } from '../../models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RoleService {
    constructor(private http: HttpClient) { }

    getAll() {
      return this.http.get<Role[]>(`${environment.apiUrl}/roles`).pipe(map(roles => {
        localStorage.setItem('roles', JSON.stringify(roles));
        return roles;
      }));
    }
}
