import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of, pipe, tap } from 'rxjs';
import { CreateUserComponent } from '../create-user/create-user.component';
import { CreateUserModel } from '../models/create-user.model';
import { GetUsersDto } from '../models/getUsersDto';
import { StringDto } from '../models/stringDto';
import { UpdateUserModel } from '../models/update-user.model';
import { UserAuthDto } from '../models/userAuthDto';
import { UserDto } from '../models/userDto';
import { UsersGridInfoDto } from '../models/usersGridInfoDto';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  private serviceBaseUrl;
  users!: UserDto[];
  currentUserRole:string;
  user: UserAuthDto;


  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    //this.serviceBaseUrl = `${this.baseUrl}/User`;
    this.serviceBaseUrl = `https://localhost:7075/User`;
  }

  getUsers(getUsersDto: GetUsersDto): Observable<UsersGridInfoDto> {
    let params = new HttpParams();
    for (var property in getUsersDto) {
      if (
        getUsersDto.hasOwnProperty(property) &&
        getUsersDto[property as keyof GetUsersDto]
      ) {
        params = params.set(
          property,
          getUsersDto[property as keyof GetUsersDto] || ''
        );
      }
    }
    return this.httpClient.get<UsersGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }

  getUserById(id: number): Observable<UserDto> {
    return this.httpClient.get<UserDto>(`${this.serviceBaseUrl}/${id}`);
  }

  getQRcode(id: number): Observable<StringDto> {
    return this.httpClient.get<StringDto>(
      `${this.serviceBaseUrl}/${id}/qrcode`
    );
  }

  getUserInfo() : Observable<UserAuthDto> {
    if(this.user){
      return of(this.user)
    }
    return this.httpClient.get<UserAuthDto>(
      //this.serviceBaseUrl = `https://localhost:7075/User`;
      `${this.serviceBaseUrl}/userInfo`
    ).pipe(
      tap(x => {
        this.user = x;
      })
    );
  }


  checkQRcode(qrcode: string): Observable<StringDto> {
    return this.httpClient.get<StringDto>(
      `${this.serviceBaseUrl}/${qrcode}/checkQRcode`
    );
  }

  createUser(newUser: CreateUserModel) {
    console.log(newUser);
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, newUser);
  }
  updateUser(updatedUserObject : UpdateUserModel){
    console.log(updatedUserObject);
    return this.httpClient.put<void>(`${this.serviceBaseUrl}`,updatedUserObject)
  }

  
 
}
