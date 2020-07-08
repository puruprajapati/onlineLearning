import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, catchError, map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Class, List } from '../../models';

@Injectable({ providedIn: 'root' })
export class ClassService {
  private selectedClass: BehaviorSubject<Class>;
  public currentSelectedClass: Observable<Class>;

  constructor(private http: HttpClient) {
    this.selectedClass = new BehaviorSubject(new Class());
    this.currentSelectedClass = this.selectedClass.asObservable();
  }

  changeSelectedClass(classDetail) {
    this.selectedClass.next(classDetail);
  }

  getAll(pageNumber, pageSize) {
    return this.http
      .get<Class[]>(
        `${environment.apiUrl}/class?PageNumber=${pageNumber}&PageSize=${pageSize}`,
        { observe: 'response' }
      )
      .pipe(tap((response) => response));
  }

  add(classDetail: Class) {
    return this.http.post(`${environment.apiUrl}/class`, classDetail);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/class/${id}`);
  }

  deleteMultiple(ids) {
    return this.http.post(`${environment.apiUrl}/class/deletemultiple`, ids);
  }

  udpate(classDetail) {
    return this.http.put(
      `${environment.apiUrl}/class/${classDetail.id}`,
      classDetail
    );
  }
}
