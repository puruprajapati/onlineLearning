import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";
import { BehaviorSubject, Observable } from "rxjs";

import { environment } from "../../../environments/environment";
import { Student, List } from "../../models";

@Injectable({ providedIn: "root" })
export class StudentService {
  private selectedModel: BehaviorSubject<Student>;
  public currentSelectedModel: Observable<Student>;

  constructor(private http: HttpClient) {
    this.selectedModel = new BehaviorSubject(new Student());
    this.currentSelectedModel = this.selectedModel.asObservable();
  }

  changeSelectedModel(model) {
    this.selectedModel.next(model);
  }

  getAll(pageNumber, pageSize) {
    return this.http
      .get<Student[]>(
        `${environment.apiUrl}/student?PageNumber=${pageNumber}&PageSize=${pageSize}`,
        { observe: "response" }
      )
      .pipe(tap((response) => response));
  }

  add(model: Student) {
    return this.http.post(`${environment.apiUrl}/student`, model);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/student/${id}`);
  }

  deleteMultiple(ids) {
    return this.http.post(`${environment.apiUrl}/student/deletemultiple`, ids);
  }

  udpate(model) {
    return this.http.put(`${environment.apiUrl}/student/${model.id}`, model);
  }
}
