import { Guid } from "guid-typescript";

export class School {
  id: Guid;
  schoolCode: string;
  name: string;
  address: string;
  contactNumber: string;
  emailAddress: string;
  logoLocation: string;
  active: string;

  constructor() {}
}
