import { Component, Input, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { IOrder } from 'src/app/shared/Models/order';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';
import { FormGroup } from '@angular/forms';
import { IBasket } from 'src/app/shared/Models/basket';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() checkoutForm:FormGroup;
  constructor(private checkoutServices:CheckoutService,private basketServices:BasketService
    ,private toastr:ToastrService,private router:Router) { }

  ngOnInit(): void {
  }
  submitOrder(){
    const basket = this.basketServices.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutServices.createOrder(orderToCreate).subscribe({
      next:((order:IOrder)=>{
        this.toastr.success('Order Submitted Succssfully!');
        this.basketServices.deleteLocalBasekt(basket.id);
        const navigationExtras:NavigationExtras = {state:order}
        this.router.navigate(['checkout/success'],navigationExtras)
      }),
      error:((err)=>{this.toastr.error('Error on Submitting Order');console.error(err)})
    })
  }
  private getOrderToCreate(basket: IBasket) {
    return {
      basketId:basket.id,
      deliveryMethodId: this.checkoutForm.get('deliveryForm.deliveryMethod').value,
      shipToAddress : this.checkoutForm.get('addressForm').value
    }
  }

}
