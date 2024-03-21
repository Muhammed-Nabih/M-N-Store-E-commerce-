import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrdersRoutingModule } from './orders-routing.module';
import { OrdersComponent } from './orders.component';



@NgModule({
  declarations: [
    OrdersComponent,
    OrderDetailsComponent
  ],
  imports: [
    CommonModule,
    OrdersRoutingModule
  ]
})
export class OrdersModule { }
