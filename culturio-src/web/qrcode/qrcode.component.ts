import { Component, OnInit } from '@angular/core';
import { Observable, takeWhile } from 'rxjs';
import { UserDto } from '../users/models/userDto';
import { UserService } from '../users/services/user.service';
import { timer } from 'rxjs';
@Component({
  selector: 'app-qrcode',
  templateUrl: './qrcode.component.html',
  styleUrls: ['./qrcode.component.scss'],
})
export class QrcodeComponent implements OnInit {
  user$: Observable<UserDto>;
  user: UserDto;
  qRcode: string;
  alive = true;
  refreshTime = 60;
  numbers = timer(this.refreshTime * 1000);
  id: number;

  constructor(private userService: UserService) {
    this.id = 6;
  }

  ngOnDestroy(): void {
    this.alive = false;
  }

  ngOnInit(): void {
    this.user$ = this.userService.getUserById(this.id);
    this.user$.subscribe((result) => {
      this.qRcode = result.qRcode;
    });
    this.startTimer();
    timer(0, this.refreshTime * 1000)
      .pipe(takeWhile(() => this.alive))
      .subscribe(() => {
        this.userService.getQRcode(this.id).subscribe((qrcode) => {
          console.log(qrcode.output);
          this.qRcode = qrcode.output;
        });
      });
  }

  getUserById(id: number) {
    this.user$ = this.userService.getUserById(id);
    console.log(this.userService.getUserById(id) + ' profile component');
  }

  timeLeft: number = this.refreshTime;
  interval: any;

  getQRcode(): string {
    return this.qRcode;
  }

  startTimer() {
    this.interval = setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        this.timeLeft = this.refreshTime;
      }
    }, 1000);
  }
}
