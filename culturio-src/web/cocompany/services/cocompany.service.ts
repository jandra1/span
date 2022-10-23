import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { cocompanyDto } from '../models/cocompanyDto';
import { CocompanyGridInfoDto } from '../models/cocompanyGridInfo';
import { CreateCocompanyModel } from '../models/create-cocompany.model';
import { GetCocompanyDto } from '../models/getcocompanyDto';
import { UpdateCocompanyModel } from '../models/update-cocompany.model';

@Injectable({
  providedIn: 'root',
})
export class CocompanyService {
  static getUsers() {
    throw new Error('Method not implemented.');
  }
  private serviceBaseUrl;
  cocompany!: cocompanyDto[];

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    this.serviceBaseUrl = `https://localhost:7075/CultureObjectCompany`;
  }
  getCocompany(getCocompanyDto: GetCocompanyDto): Observable<CocompanyGridInfoDto> {
    this.httpClient
      .get<cocompanyDto[]>(`${this.serviceBaseUrl}`)
      .subscribe((data) => console.log(data));
    let params = new HttpParams();

    for (var property in getCocompanyDto) {
      if (
        getCocompanyDto.hasOwnProperty(property) &&
        getCocompanyDto[property as keyof GetCocompanyDto]
      ) {
        params = params.set(
          property,
          getCocompanyDto[property as keyof GetCocompanyDto] || ''
        );
      }
    }
    return this.httpClient.get<CocompanyGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }
 

  getCocompanyById(id: number) {
    return this.httpClient.get<cocompanyDto>(`${this.serviceBaseUrl}/${id}`);
  }

  createCocompany(newCocompany: CreateCocompanyModel) {
    console.log(newCocompany)
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, newCocompany);
  }

  
  updateCocompany(updatedCocompany : UpdateCocompanyModel){
    console.log(updatedCocompany);
    return this.httpClient.put<void>(`${this.serviceBaseUrl}`,updatedCocompany)
  }

}
