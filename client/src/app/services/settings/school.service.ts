import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, catchError, map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { School } from '../../models';

@Injectable({ providedIn: 'root' })
export class SchoolService {
  private selectedSchool: BehaviorSubject<School>;
  public currentSelectedSchool: Observable<School>;

  constructor(private http: HttpClient) {
    this.selectedSchool = new BehaviorSubject(new School());
    this.currentSelectedSchool = this.selectedSchool.asObservable();
  }

  changeSelectedSchool(school) {
    this.selectedSchool.next(school);
  }

  getAll(pageNumber, pageSize) {
    return this.http
      .get<School[]>(
        `${environment.apiUrl}/school?PageNumber=${pageNumber}&PageSize=${pageSize}`,
        { observe: 'response' }
      )
      .pipe(tap((response) => response));
  }

  add(school: School) {
    return this.http.post(`${environment.apiUrl}/school`, school);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/school/${id}`);
  }

  deleteMultiple(ids) {
    return this.http.post(`${environment.apiUrl}/school/deletemultiple`, ids);
  }

  udpate(school) {
    return this.http.put(`${environment.apiUrl}/school/${school.id}`, school);
  }
}
