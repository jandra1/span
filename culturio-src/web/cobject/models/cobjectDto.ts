export class CobjectDto {
  public id: number;
  public name: string;
  public phone: string;
  public address: string;
  public postalCode: string;
  public city: string;
  public state: string;
  public workingHours: string;
  public latitude: number;
  public longitude: number;
  public cultureObjectType: string;
  public notes: string;
  public responsiblePerson: string;
  public cultureObjectCompany: string;

  constructor(
    id: number,
    name: string,
    phone: string,
    address: string,
    postalCode: string,
    city: string,
    state: string,
    workingHours: string,
    latitude: number,
    longitude: number,
    cultureObjectType: string,
    notes: string,
    responsiblePerson: string,
    cultureObjectCompany: string
  ) {
    this.id = id;
    this.name = name;
    this.phone = phone;
    this.address = address;
    this.postalCode = postalCode;
    this.city = city;
    this.state = state;
    this.workingHours = workingHours;
    this.latitude = latitude;
    this.longitude = longitude;
    this.cultureObjectType = cultureObjectType;
    this.notes = notes;
    this.responsiblePerson = responsiblePerson;
    this.cultureObjectCompany = cultureObjectCompany;
  }
}
