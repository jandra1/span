import { VisitDto } from './visitDto';

export interface IVisitsGridInfoDto {
  totalCount: number;
  visits: VisitDto[];
}

export class VisitsGridInfoDto implements IVisitsGridInfoDto {
  public totalCount: number;
  public visits: VisitDto[];

  constructor(totalCount: number, visits: VisitDto[]) {
    this.totalCount = totalCount;
    this.visits = visits;
  }
}
