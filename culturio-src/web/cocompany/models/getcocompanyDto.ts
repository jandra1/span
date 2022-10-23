export interface GetCocompanyDto{
    getOnlyActive?: boolean;
    sortOrder?: string;
    searchValue?: string;
    page?: number;
    pageSize?: number;
    correspondencePersonId?: number;
}