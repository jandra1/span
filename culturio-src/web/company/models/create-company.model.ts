export interface CreateCompanyModel{
    name: string;
    taxId: number;
    vatId: number;
    phone: string;
    address: string;
    postalCode: number;
    city: string;
    state: string;
    correspondencePersonId: number;
}