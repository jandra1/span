export interface GetCompanyDto{
    sortOrder?: string;
    searchValue?: string;
    page?: number;
    pageSize?: number;
    correspondencePersonId?: number;
    companyID?: number;
}