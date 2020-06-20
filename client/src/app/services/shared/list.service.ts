import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";

import { environment } from "../../../environments/environment";
import { List } from "../../models";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class ListService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<any>(`${environment.apiUrl}/list`).pipe(
      map((lists) => {
        // localStorage.setItem("list", JSON.stringify(lists));
        return lists;
      })
    );
  }
}
