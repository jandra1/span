export class UserDto {
  public id: number;
  public firstName: string;
  public lastName: string;
  public address: string;
  public city: string;
  public email: string;
  public sexId: number;
  public roleId: number;
  public defaultLanguage: string;
  public termsAccepted: boolean;
  public postalCode: number;
  public phone: string;
  public qRcode: string;
  public isActive: boolean;
  public companyId: number;
  public cultureObjectId: number;
  constructor(
    id: number,
    firstName: string,
    lastName: string,
    address: string,
    city: string,
    email: string,
    sexId: number,
    roleId: number,
    defaultLanguage: string,
    termsAccepted: boolean,
    postalCode: number,
    phone: string,
    qRcode: string,
    isActive: boolean,
    companyId: number,
    cultureObjectId: number
  ) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.address = address;
    this.city = city;
    this.email = email;
    this.sexId = sexId;
    this.roleId = roleId;
    this.defaultLanguage = defaultLanguage;
    this.termsAccepted = termsAccepted;
    this.postalCode = postalCode;
    this.phone = phone;
    this.qRcode = qRcode;
    this.isActive = isActive;
    this.companyId = companyId;
    this.cultureObjectId = cultureObjectId;
  }
}
