import { Guid } from "guid-typescript";

export class Class {
  id: Guid;
  schoolId: Guid;
  className: string;
  description: string;
  active: boolean;

  constructor() {}
}
