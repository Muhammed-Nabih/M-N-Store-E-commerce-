import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutComponent } from './checkout.component';
import { CheckoutSuccessComponent } from '../shared/Components/checkout-success/checkout-success.component';

const routes:Routes = [
  {path:'',component:CheckoutComponent},
  {path:'success',component:CheckoutSuccessComponent},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class CheckoutRoutingModule { }