export class UserDto {
  public id: number;
  public firstName: string;
  public lastName: string;
  public address: string;
  public city: string;
  public email: string;
  public roleId: number;
  constructor(
    id: number,
    firstName: string,
    lastName: string,
    address: string,
    city: string,
    email: string,
    roleId: number
  ) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.address = address;
    this.city = city;
    this.email = email;
    this.roleId = roleId;
  }
}
