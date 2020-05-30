import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";
import { BehaviorSubject, Observable } from 'rxjs';

import { environment } from "../../../environments/environment";
import { User } from "../../models";

@Injectable({ providedIn: "root" })
export class UserService {
  private selectedUser: BehaviorSubject<User>;
  public currentSelectedUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.selectedUser = new BehaviorSubject(new User());
    this.currentSelectedUser = this.selectedUser.asObservable();
  }

  changeSelectedUser(user){
    this.selectedUser.next(user);
  }

  getAll() {
    return this.http
      .get<User[]>(`${environment.apiUrl}/users`)
      .pipe(tap((users) => users));
  }

  register(user: User) {
    return this.http.post(`${environment.apiUrl}/users`, user);
  }

  delete(id) {
    return this.http.delete(`${environment.apiUrl}/users/${id}`);
  }

  udpate(user) {
    return this.http.put(`${environment.apiUrl}/users/${user.id}`, user);
  }
}
