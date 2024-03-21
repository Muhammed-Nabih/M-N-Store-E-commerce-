import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  _baseURL:string = environment.baseURL;
  constructor(private http:HttpClient) { }

  getOrdersForUser(){
    return this.http.get(this._baseURL+'Orders/get-orders-for-user');
  }
  getOrdersDetails(id:number){
    return this.http.get(this._baseURL+'Orders/get-order-by-id/'+id);
  }

}
