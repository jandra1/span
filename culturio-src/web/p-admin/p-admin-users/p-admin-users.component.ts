import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GetUsersDto } from 'src/app/users/models/getUsersDto';
import { UserDto } from 'src/app/users/models/userDto';
import { UsersGridInfoDto } from 'src/app/users/models/usersGridInfoDto';
import { UserService } from 'src/app/users/services/user.service';

@Component({
  selector: 'app-p-admin-users',
  templateUrl: './p-admin-users.component.html',
  styleUrls: ['./p-admin-users.component.scss'],
})
export class PAdminUsersComponent implements OnInit {
  users$: Observable<UsersGridInfoDto>;
  searchValue = '';
  visible = false;
  sortOrder: string;
  users: UserDto[];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
    this.users$.subscribe((result) => {
      this.users = result.users;
    });
  }
  getUsers() {
    const model: GetUsersDto = {
      sortOrder: 'asc',
      searchValue: '',
      page: 1,
      pageSize: 10,
    };
    this.users$ = this.userService.getUsers(model);
  }

  search() {}
}
