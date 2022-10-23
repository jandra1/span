export class UserAuthDto {
  public id: number;
  public externalId: string;
  public role: string;
  constructor(
    id: number,
    externalId: string,
    role: string
  ) {
    this.id = id;
    this.externalId = externalId;
    this.role = role;
  }
}
