import { Guid } from "guid-typescript";

export class User {
  id: Guid;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  isEmailConfirmed: string;
  active: string;
  parentId: Guid;
  roleId: Guid;  password: string;
  accessToken?: string;
  refreshToken?: string;
  expiration?: string;


  constructor(){

  }
}
