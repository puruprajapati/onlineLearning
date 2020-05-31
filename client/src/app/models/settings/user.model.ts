import { Guid } from "guid-typescript";

export class User {
  id: Guid;
  fullName: string;
  userName: string;
  email: string;
  user: string;
  active: string;
  isValid: Guid;
  userRole: Guid;
  accessToken?: string;
  refreshToken?: string;
  expiration?: string;

  constructor() {}
}
