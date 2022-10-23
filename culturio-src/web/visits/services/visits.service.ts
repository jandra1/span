import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateVisitModel } from '../models/create-visit.model';
import { GetVisitsDto } from '../models/getVisitsDto';
import { VisitsGridInfoDto } from '../models/visitsGridInfoDto';

@Injectable({
  providedIn: 'root',
})
export class VisitService {
  private serviceBaseUrl;

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    //this.serviceBaseUrl = `${this.baseUrl}/CultureObject`;
    this.serviceBaseUrl = `https://localhost:7075/Visit`;
  }

  createVisit(newVisit: CreateVisitModel) {
    console.log(newVisit);
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, newVisit);
  }

  getVisitOfUser(getVisitsDto: GetVisitsDto): Observable<VisitsGridInfoDto> {
    let params = new HttpParams();
    for (var property in getVisitsDto) {
      if (
        getVisitsDto.hasOwnProperty(property) &&
        getVisitsDto[property as keyof GetVisitsDto]
      ) {
        params = params.set(
          property,
          getVisitsDto[property as keyof GetVisitsDto] || ''
        );
      }
    }
    return this.httpClient.get<VisitsGridInfoDto>(
      `${this.serviceBaseUrl}/visitsOfUser`,
      {
        params: params,
      }
    );
  }
}
