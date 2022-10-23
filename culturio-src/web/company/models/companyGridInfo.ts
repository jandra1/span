import { CompanyDto } from "./companyDto";

export interface ICompanyGridInfoDto {
    totalCount: number;
    companies: CompanyDto[];
}

export class CompanyGridInfoDto implements ICompanyGridInfoDto {
    public totalCount: number;
    public companies: CompanyDto[];

    constructor(totalCount: number, companies: CompanyDto[]) {
        this.totalCount = totalCount;
        this.companies = companies;
    }
}