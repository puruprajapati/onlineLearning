import { Guid } from "guid-typescript";

export class Session {
  id: Guid;
  sessionTitle: string;
  sessionDesc: string;
  classId: Guid;
  className: string;
  teacherId: Guid;
  teacherName: string;
  scheduledDate: Date;
  startingTime: string;
  endingTime: string;
  sessionStatusId: Guid;
  active: string;

  constructor() {}
}
