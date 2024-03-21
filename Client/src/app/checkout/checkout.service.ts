import { Injectable } from '@angular/core';
import { IDeliveryMethod } from '../shared/Models/deliveryMethod';
import { IOrderToCreate } from '../shared/Models/order';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  _baseURL = environment.baseURL;
  constructor(private http:HttpClient) { }
  
  getDeliveryMehtods(){
    return this.http.get(this._baseURL+'Orders/get-delivery-methods').pipe(
      map((res:IDeliveryMethod[])=>{
        return res.sort((a,b)=>b.price-a.price);
      })
    )
  }

  createOrder(order:IOrderToCreate){
    return this.http.post(this._baseURL+'Orders/create-order',order);
  }}