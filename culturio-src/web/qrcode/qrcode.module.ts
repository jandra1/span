import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { QrcodeRoutingModule } from './qrcode-routing.module';
import { QrcodeService } from './services/qrcode.service';
import { QRCodeModule } from 'angularx-qrcode';


@NgModule({
    declarations: [QrcodeRoutingModule.delcaredComponents],
    imports: [
      QrcodeRoutingModule,
      CommonModule,
      NzButtonModule,
      NzDividerModule,
      NzIconModule,
      NzLayoutModule,
      NzInputModule,
      NzTableModule,
      NzFormModule,
      NzDropDownModule,
      NzIconModule,
      NzCarouselModule,
      QRCodeModule
    ],
    providers: [QrcodeService],
  })
  export class QrcodeModule {}