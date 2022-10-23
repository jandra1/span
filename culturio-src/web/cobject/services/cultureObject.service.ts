import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CobjectDto } from '../models/cobjectDto';
import { CobjectsGridInfoDto } from '../models/cobjectsGridInfoDto';
import { CreateCobjectModel } from '../models/create-cobject.model';
import { GetCobjectsDto } from '../models/getCobjectsDto';
import { UpdateCobjectModel } from '../models/update-cobject.model';

@Injectable({
  providedIn: 'root',
})
export class CobjectService {
  static getCultureObjects() {
    throw new Error('Method not implemented.');
  }
  private serviceBaseUrl;
  cultureObjects!: CobjectDto[];

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    //this.serviceBaseUrl = `${this.baseUrl}/User`;
    this.serviceBaseUrl = `https://localhost:7075/CultureObject`;
  }

  getCultureObjects(getCultureObjectsDto: GetCobjectsDto): Observable<CobjectsGridInfoDto> {
    this.httpClient
      .get<CobjectDto[]>(`${this.serviceBaseUrl}`)
      .subscribe((data) => console.log(data));
    let params = new HttpParams();

    for (var property in getCultureObjectsDto) {
      if (
        getCultureObjectsDto.hasOwnProperty(property) &&
        getCultureObjectsDto[property as keyof GetCobjectsDto]
      ) {
        params = params.set(
          property,
          getCultureObjectsDto[property as keyof GetCobjectsDto] || ''
        );
      }
    }
    return this.httpClient.get<CobjectsGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }

  getCultureObjectById(id: number) {
    return this.httpClient.get<CobjectDto>(`${this.serviceBaseUrl}/${id}`);
  }

  createCultureObject(newCultureObject : CreateCobjectModel){
    console.log(newCultureObject);
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`,newCultureObject)
  }

  updateCobject(updatedCultureObject : UpdateCobjectModel){
    console.log(updatedCultureObject);
    return this.httpClient.put<void>(`${this.serviceBaseUrl}`,updatedCultureObject)
  }
}
