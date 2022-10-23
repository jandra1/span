import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CobjectDto } from '../models/cobjectDto';
import { CobjectsGridInfoDto } from '../models/cobjectsGridInfoDto';
import { CreateCobjectModel } from '../models/create-cobject.model';
import { GetCobjectsDto } from '../models/getCobjectsDto';

@Injectable({
  providedIn: 'root',
})
export class CobjectService {
  static getCobjects() {
    throw new Error('Method not implemented.');
  }
  private serviceBaseUrl;
  cobjects!: CobjectDto[];

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    //this.serviceBaseUrl = `${this.baseUrl}/CultureObject`;
    this.serviceBaseUrl = `https://localhost:7075/CultureObject`;
  }

  getCobjects(getCobjectsDto: GetCobjectsDto): Observable<CobjectsGridInfoDto> {
    this.httpClient
      .get<CobjectDto[]>(`${this.serviceBaseUrl}`)
      .subscribe((data) => console.log(data));
    let params = new HttpParams();

    for (var property in getCobjectsDto) {
      if (
        getCobjectsDto.hasOwnProperty(property) &&
        getCobjectsDto[property as keyof GetCobjectsDto]
      ) {
        params = params.set(
          property,
          getCobjectsDto[property as keyof GetCobjectsDto] || ''
        );
      }
    }
    return this.httpClient.get<CobjectsGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }

  getCobjectById(id: number) {
    return this.httpClient.get<CobjectDto>(`${this.serviceBaseUrl}/${id}`);
  }

  getCobjectOfResponsiblePerson(id: number): Observable<CobjectsGridInfoDto> {
    return this.httpClient.get<CobjectsGridInfoDto>(
      `${this.serviceBaseUrl}?ResponsiblePersonId=${id}`
    );
  }

  createCobject(newCobject: CreateCobjectModel) {
    console.log(newCobject);
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, newCobject);
  }
}
