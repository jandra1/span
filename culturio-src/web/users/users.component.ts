import { AfterViewInit, ElementRef, ViewChild } from '@angular/core';

import {
  debounceTime,
  distinctUntilChanged,
  forkJoin,
  fromEvent,
  map,
  Observable,
  shareReplay,
} from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { NzFormModule } from 'ng-zorro-antd/form'

import {
  NzPaginationComponent,
  NzPaginationModule,
} from 'ng-zorro-antd/pagination';
import { HttpClient } from '@angular/common/http';
import { UserDto } from './models/userDto';
import { UserService } from './services/user.service';
import { GetUsersDto } from './models/getUsersDto';




@Component({
  selector: 'app-company',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users$: Observable<UserDto[]> | undefined;
  userById$: Observable<UserDto>;
  totalCount$:Observable<number>;

  searchValue = '';
  visible = false;
  sortOrder:string

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    const model:GetUsersDto={
      sortOrder:'asc',
      searchValue: '',
      page: 1,
      pageSize: 10,
  };
  const source = this.userService.getUsers(model).pipe(shareReplay());
  this.totalCount$ = source.pipe(map((x) => x.totalCount));
  this.users$ = source.pipe(map((x) => x.users));
}

  getUserById(id: number) {
    this.userById$ = this.userService.getUserById(id);
  }

  public search() {}

}
