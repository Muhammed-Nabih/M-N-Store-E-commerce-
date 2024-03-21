import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrdersComponent } from './orders.component';

const routes: Routes = [
  {path:'',component:OrdersComponent},
  {path:':id',component:OrderDetailsComponent,data:{breadcrumb:{alias:'OrderDetails'}}},
  
  ]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],exports:[
    RouterModule
  ]
})
export class OrdersRoutingModule { }
