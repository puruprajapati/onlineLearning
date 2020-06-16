import { Guid } from "guid-typescript";

export class Student {
  id: Guid;
  name: string;
  schoolId: Guid;
  classId: Guid;
  sectionId: Guid;
  userName: string;
  rollNumber: string;

  parentName: string;
  primaryContanctNo: string;
  secondaryContactNo: string;
  parentEmailAddress: string;

  constructor() {}
}
