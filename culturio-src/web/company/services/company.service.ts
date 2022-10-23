import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CompanyDto } from '../models/companyDto';
import { CompanyGridInfoDto } from '../models/companyGridInfo';
import { CreateCompanyModel } from '../models/create-company.model';
import { GetCompanyDto } from '../models/getcopmanyDto';
import { UpdateCompanyModel } from '../models/update-company.model';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  static getCompany() {
    throw new Error('Method not implemented.');
  }
  private serviceBaseUrl;
  company!: CompanyDto[];

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    this.serviceBaseUrl = `https://localhost:7075/Company`;
  }

  getCompanys(getcopmanyDto: GetCompanyDto): Observable<CompanyGridInfoDto> {
    this.httpClient
      .get<CompanyDto[]>(`${this.serviceBaseUrl}`)
      .subscribe((data) => console.log(data));
    let params = new HttpParams();

    for (var property in getcopmanyDto) {
      if (
        getcopmanyDto.hasOwnProperty(property) &&
        getcopmanyDto[property as keyof GetCompanyDto]
      ) {
        params = params.set(
          property,
          getcopmanyDto[property as keyof GetCompanyDto] || ''
        );
      }
    }
    return this.httpClient.get<CompanyGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }


  getCompanyById(id: number) {
    return this.httpClient.get<CompanyDto>(`${this.serviceBaseUrl}/${id}`);
  }

  
  createCompany(newCompany: CreateCompanyModel) {
    console.log(newCompany)
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, newCompany);
  }

  updateCompany(updatedCompany : UpdateCompanyModel){
    console.log(updatedCompany);
    return this.httpClient.put<void>(`${this.serviceBaseUrl}`,updatedCompany)
  }



}
