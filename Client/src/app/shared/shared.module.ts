import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './Components/paging-header/paging-header.component';
import { PagerComponent } from './Components/pager/pager.component';
import { OrderTotalsComponent } from './Components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from './Components/stepper/stepper.component';
import { BasketSummeryComponent } from './Components/basket-summery/basket-summery.component';
import { CheckoutSuccessComponent } from './Components/checkout-success/checkout-success.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    StepperComponent,
    BasketSummeryComponent,
    CheckoutSuccessComponent   
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    CdkStepperModule,
    RouterModule
  ],
  exports:[
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    CdkStepperModule,
    StepperComponent,
    BasketSummeryComponent
  ]
})
export class SharedModule { }