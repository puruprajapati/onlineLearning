import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";
import { BehaviorSubject, Observable } from "rxjs";

import { environment } from "../../../environments/environment";
import { Teacher, List } from "../../models";

@Injectable({ providedIn: "root" })
export class TeacherService {
  private selectedModel: BehaviorSubject<Teacher>;
  public currentSelectedModel: Observable<Teacher>;

  constructor(private http: HttpClient) {
    this.selectedModel = new BehaviorSubject(new Teacher());
    this.currentSelectedModel = this.selectedModel.asObservable();
  }

  changeSelectedModel(model) {
    this.selectedModel.next(model);
  }

  getAll(pageNumber, pageSize) {
    return this.http
      .get<Teacher[]>(
        `${environment.apiUrl}/teacher?PageNumber=${pageNumber}&PageSize=${pageSize}`,
        { observe: "response" }
      )
      .pipe(tap((response) => response));
  }

  add(model: Teacher) {
    return this.http.post(`${environment.apiUrl}/teacher`, model);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/teacher/${id}`);
  }

  deleteMultiple(ids) {
    return this.http.post(`${environment.apiUrl}/teacher/deletemultiple`, ids);
  }

  udpate(model) {
    return this.http.put(`${environment.apiUrl}/teacher/${model.id}`, model);
  }
}
