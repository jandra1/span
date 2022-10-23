import { CobjectDto } from "./cobjectDto";

export interface ICobjectGridInfoDto{
    totalCount:number;
    cultureObjects:CobjectDto[];
}

export class CobjectsGridInfoDto implements ICobjectGridInfoDto{
    public totalCount:number;
    public cultureObjects:CobjectDto[];
    //public cultureObject:CultureObjectDto;

    constructor(totalCount: number, cultureObjects: CobjectDto[]) {
        this.totalCount = totalCount;
        this.cultureObjects = cultureObjects;
    }

}