import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ShopItemComponent } from './shop-item/shop-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopRoutingModule } from './shop-routing.module';
import { BreadcrumbModule } from 'xng-breadcrumb';



@NgModule({
  declarations: [
    ShopComponent,
    ShopItemComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule,
    BreadcrumbModule
  ],
  exports:[

  ]
})
export class ShopModule { }
