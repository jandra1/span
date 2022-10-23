import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';

import {
  MsalService,
  MsalBroadcastService,
  MSAL_GUARD_CONFIG,
  MsalGuardConfiguration,
} from '@azure/msal-angular';
import {
  InteractionType,
  InteractionStatus,
  PopupRequest,
  RedirectRequest,
  AuthenticationResult,
} from '@azure/msal-browser';

import { b2cPolicies } from './auth-config';
import { HttpClient } from '@angular/common/http';
import { UserService } from './users/services/user.service';
import { NavigationStart, Router } from '@angular/router';
import { UserAuthDto } from './users/models/userAuthDto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Culturio';
  isIframe = false;
  isCollapsed = false;
  loginDisplay = false;
  private readonly _destroying$ = new Subject<void>();
  userRole: string;
  fetchedUser: UserAuthDto;

  showHead: boolean = false;

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private authService: MsalService,
    private msalBroadcastService: MsalBroadcastService,
    private httpClient: HttpClient,
    private userService: UserService,
    private router: Router
  ) {
    router.events.forEach((event) => {
      if (event instanceof NavigationStart) {
        if (
          event['url'] == '/' ||
          event['url'] == '/visit' ||
          event['url'] == '/visit/cinema' ||
          event['url'] == '/visit/museum' ||
          event['url'] == '/visit/theatre' ||
          event['url'] == '/qrcode'
        ) {
          this.showHead = false;
        } else {
          this.showHead = true;
        }
      }
    });
  }

  ngOnInit(): void {
    this.isIframe = window !== window.parent && !window.opener;

    this.msalBroadcastService.inProgress$
      .pipe(
        filter(
          (status: InteractionStatus) => status === InteractionStatus.None
        ),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.setLoginDisplay();
      });
    this.getUserInfo();
  }

  setLoginDisplay() {
    this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
  }

  login(userFlowRequest?: RedirectRequest | PopupRequest) {
    if (this.msalGuardConfig.interactionType === InteractionType.Popup) {
      if (this.msalGuardConfig.authRequest) {
        this.authService
          .loginPopup({
            ...this.msalGuardConfig.authRequest,
            ...userFlowRequest,
          } as PopupRequest)
          .subscribe((response: AuthenticationResult) => {
            this.authService.instance.setActiveAccount(response.account);
          });
      } else {
        this.authService
          .loginPopup(userFlowRequest)
          .subscribe((response: AuthenticationResult) => {
            this.authService.instance.setActiveAccount(response.account);
          });
      }
    } else {
      if (this.msalGuardConfig.authRequest) {
        this.authService.loginRedirect({
          ...this.msalGuardConfig.authRequest,
          ...userFlowRequest,
        } as RedirectRequest);
      } else {
        this.authService.loginRedirect(userFlowRequest);
      }
    }
  }

  logout() {
    this.authService.logout();
  }

  editProfile() {
    let editProfileFlowRequest = {
      scopes: ['openid'], ///PROMJENIT TREBA VJV
      authority: b2cPolicies.authorities.editProfile.authority,
    };

    this.login(editProfileFlowRequest);
  }

  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }

  onFetchClick() {
    this.httpClient.get('https://localhost:7075/user').subscribe((response) => {
      console.log('aaaa', response);
    });
    //current user
    var temp1 = this.authService.instance.getAllAccounts();
    var temp2 =
      this.authService.instance.getAllAccounts()[0]['idTokenClaims']?.sub;
    console.log('\n\n', temp1);
    console.log('\n\n\nCURRENT USER:', temp2);
  }

  getUserInfo() {
    this.userService.getUserInfo().subscribe((response) => {
      console.log('GET USER INFO:', response);
    });
  }
}
