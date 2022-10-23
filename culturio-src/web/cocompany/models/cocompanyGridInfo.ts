import { cocompanyDto } from "./cocompanyDto";

export interface CocompanyGridInfoDto {
    totalCount: number;
    cultureObjectCompanies: cocompanyDto[];
}

export class CocompanyGridInfoDto implements CocompanyGridInfoDto {
    public totalCount: number;
    public cultureObjectCompanies: cocompanyDto[];

    constructor(totalCount: number, cultureObjectCompanies: cocompanyDto[]) {
        this.totalCount = totalCount;
        this.cultureObjectCompanies = cultureObjectCompanies;
    }
}