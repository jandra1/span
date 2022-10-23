export class VisitDto {
  public id: number;
  public userId: number;
  public cultureObjectId: number;
  public timeOfVisit: Date;
  constructor(
    id: number,
    userId: number,
    cultureObjectId: number,
    timeOfVisit: Date
  ) {
    this.id = id;
    this.userId = userId;
    this.cultureObjectId = cultureObjectId;
    this.timeOfVisit = timeOfVisit;
  }
}
