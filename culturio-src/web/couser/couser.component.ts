import { Component, OnInit } from '@angular/core';
import { NzResultComponent, NzResultStatusType } from 'ng-zorro-antd/result';
import { Observable } from 'rxjs';
import { StringDto } from '../users/models/stringDto';
import { UserService } from '../users/services/user.service';
import { CobjectService } from '../cobject/services/cobject.service';
import { CobjectDto } from '../cobject/models/cobjectDto';
import { CobjectsGridInfoDto } from '../cobject/models/cobjectsGridInfoDto';
import { UserDto } from '../users/models/userDto';
import { GetUsersDto } from '../users/models/getUsersDto';
import { UsersGridInfoDto } from '../users/models/usersGridInfoDto';
import { VisitService } from '../visits/services/visits.service';
import { CreateVisitModel } from '../visits/models/create-visit.model';
@Component({
  selector: 'app-couser',
  templateUrl: './couser.component.html',
  styleUrls: ['./couser.component.scss'],
})
export class CouserComponent implements OnInit {
  inputValue: string;
  result$: Observable<StringDto>;
  result: StringDto;
  status: number;
  myId = 32;
  invalidCode = 'x'; // ne postoji kod samo sa jednim znakom
  cObjectOfUserWorking: CobjectsGridInfoDto; //pretp: company object user radi samo na jednom company objectu.
  userWithEnteredCode: UserDto;
  users: UserDto[];
  user: UserDto;
  users$: Observable<UsersGridInfoDto>;

  constructor(
    private userService: UserService,
    private cObjectService: CobjectService,
    private visitService: VisitService
  ) {}

  ngOnInit(): void {
    this.status = 0;

    this.cObjectService
      .getCobjectOfResponsiblePerson(this.myId)
      .subscribe((cObject) => {
        this.cObjectOfUserWorking = cObject;
      });
  }

  checkQRcode(): Observable<StringDto> {
    if (this.inputValue == '') {
      this.status = 0;
      return this.userService.checkQRcode(this.invalidCode);
    }
    return this.userService.checkQRcode(this.inputValue);
  }

  validateQRcode(): void {
    this.result$ = this.checkQRcode();
    this.result$.subscribe((output) => {
      this.result = output;
      if (this.result.output == 'Code is valid') {
        this.status = 1;
        this.getUsers(this.inputValue);
      } else {
        this.status = 2;
      }
      if ((this.inputValue = '')) {
        this.status = 2;
      }
    });
    //this.inputValue = '';
  }

  createVisit(userToCreate: number, cultureObjectToCreate: number) {
    const createModel: CreateVisitModel = {
      userId: userToCreate,
      cultureObjectId: cultureObjectToCreate,
      timeOfVisit: new Date(),
    };
    this.visitService.createVisit(createModel).subscribe((_) => {
      console.log('VISIT CREATED');
    });
  }

  getUsers(code: string) {
    const model: GetUsersDto = {
      sortOrder: '',
      searchValue: '',
      page: 1,
      pageSize: 10000,
    };
    this.users$ = this.userService.getUsers(model);
    this.users$.subscribe((result) => {
      this.users = result.users;
      console.log(this.users);
      this.users.forEach((i) => {
        if (i.qRcode == code) {
          console.log(i.id);
          console.log(this.cObjectOfUserWorking.cultureObjects[0].id);
          this.createVisit(
            i.id,
            this.cObjectOfUserWorking.cultureObjects[0].id
          );
          this.userWithEnteredCode = i;
        }
      });
    });
  }
}
