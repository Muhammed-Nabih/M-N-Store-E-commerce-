import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IDeliveryMethod } from 'src/app/shared/Models/deliveryMethod';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() checkoutForm:FormGroup;
  deliveryMethods:IDeliveryMethod[];
  constructor(private checkoutService:CheckoutService,private basketService:BasketService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMehtods().subscribe({
      next:(res:IDeliveryMethod[])=> {
        this.deliveryMethods = res;
      },
      error:(err)=>{console.error(err)}
    })
  }

  setShippingPrice(deliceryMethod:IDeliveryMethod){
    this.basketService.setShippingPrice(deliceryMethod);
  }

}
