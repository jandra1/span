import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class QrcodeService {
  private serviceBaseUrl;

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    //this.serviceBaseUrl = `${this.baseUrl}/User`;
    this.serviceBaseUrl = `https://localhost:7075/Qrcode`;
  }

}
