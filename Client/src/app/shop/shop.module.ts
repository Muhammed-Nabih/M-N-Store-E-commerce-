import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ShopItemComponent } from './shop-item/shop-item.component';



@NgModule({
  declarations: [
    ShopComponent,
    ShopItemComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }
