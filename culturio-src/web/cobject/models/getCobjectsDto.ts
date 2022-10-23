export interface GetCobjectsDto{
    sortOrder?: string;
    searchValue?: string;
    page?: number;
    pageSize?: number;
    responsiblePersonId?: number;
    cultureObjectCompanyId?: number;
}