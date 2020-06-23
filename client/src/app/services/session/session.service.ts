import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";
import { BehaviorSubject, Observable } from "rxjs";

import { environment } from "../../../environments/environment";
import { Session, List } from "../../models";

@Injectable({ providedIn: "root" })
export class SessionService {
  private selectedModel: BehaviorSubject<Session>;
  public currentSelectedModel: Observable<Session>;

  constructor(private http: HttpClient) {
    this.selectedModel = new BehaviorSubject(new Session());
    this.currentSelectedModel = this.selectedModel.asObservable();
  }

  changeSelectedModel(model) {
    this.selectedModel.next(model);
  }

  getAll(pageNumber, pageSize) {
    return this.http
      .get<Session[]>(
        `${environment.apiUrl}/session?PageNumber=${pageNumber}&PageSize=${pageSize}`,
        { observe: "response" }
      )
      .pipe(tap((response) => response));
  }

  add(model: Session) {
    return this.http.post(`${environment.apiUrl}/session`, model);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/session/${id}`);
  }

  deleteMultiple(ids) {
    return this.http.post(`${environment.apiUrl}/session/deletemultiple`, ids);
  }

  udpate(model) {
    return this.http.put(`${environment.apiUrl}/session/${model.id}`, model);
  }
}
