export interface UpdateUserModel {
     id: number;
     firstName: string;
     lastName: string;
     address: string;
     city: string;
     email: string;
     sexId: number;
     roleId: number;
     defaultLanguage: string;
     termsAccepted: boolean;
     postalCode: number;
     phone: string;
     qRcode: string;
     isActive: boolean;
     companyId: number;
     cultureObjectId: number;
}