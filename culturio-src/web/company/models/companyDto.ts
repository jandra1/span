export class CompanyDto {
    public id: number;
    public name: string;
    public taxId: number;
    public vatId: number;
    public phone: string;
    public address: string;
    public postalCode: number;
    public city: string;
    public state: string;
    public correspondencePerson: string;
    constructor(
        id: number,
        name: string,
        taxId: number,
        vatId: number,
        phone: string,
        address: string,
        postalCode: number,
        city: string,
        state: string,
        correspondencePerson: string,

    ){
        this.id = id;
        this.name = name;
        this.taxId = taxId;
        this.vatId = vatId;
        this.phone = phone;
        this.address = address;
        this.postalCode = postalCode;
        this.city = city;
        this.state = state;
        this.correspondencePerson = correspondencePerson;
    }
}