import { Guid } from "guid-typescript";

export class Teacher {
  id: Guid;
  name: string;
  address: string;
  contactNumber: string;
  emailAddress: string;
  active: string;
  schoolId: Guid;
  userName: string;

  constructor() {}
}
