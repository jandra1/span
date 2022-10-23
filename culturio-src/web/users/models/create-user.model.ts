export interface CreateUserModel{
    firstName : string,
    lastName: string,
    email: string,
    roleId : number,
    phone: string,
    address: string,
    postalCode: number,
    city: string,
    sex: string,
    defaultLanguage: string,
    dateCreated: Date
    isActive: boolean,
    termsAccepted: boolean
    company: string, //provjeri
    cultureObjectId: number,

    externalId: string
}