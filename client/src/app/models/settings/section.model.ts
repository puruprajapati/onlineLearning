import { Guid } from "guid-typescript";

export class Section {
  id: Guid;
  sectionName: string;
  description: string;
  active: boolean;

  constructor() {}
}
