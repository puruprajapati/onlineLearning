import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";
import { BehaviorSubject, Observable } from "rxjs";

import { environment } from "../../../environments/environment";
import { Section, List } from "../../models";

@Injectable({ providedIn: "root" })
export class SectionService {
  private selectedModel: BehaviorSubject<Section>;
  public currentSelectedModel: Observable<Section>;

  constructor(private http: HttpClient) {
    this.selectedModel = new BehaviorSubject(new Section());
    this.currentSelectedModel = this.selectedModel.asObservable();
  }

  changeSelectedModel(model) {
    this.selectedModel.next(model);
  }

  getAll(pageNumber, pageSize) {
    return this.http
      .get<Section[]>(
        `${environment.apiUrl}/section?PageNumber=${pageNumber}&PageSize=${pageSize}`,
        { observe: "response" }
      )
      .pipe(tap((response) => response));
  }

  add(model: Section) {
    return this.http.post(`${environment.apiUrl}/section`, model);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/section/${id}`);
  }

  deleteMultiple(ids) {
    return this.http.post(`${environment.apiUrl}/section/deletemultiple`, ids);
  }

  udpate(model) {
    return this.http.put(`${environment.apiUrl}/section/${model.id}`, model);
  }
}
