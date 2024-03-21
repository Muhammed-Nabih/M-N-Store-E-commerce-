import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/Models/order';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders:IOrder[];
  constructor(private ordersServices:OrdersService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(){
    this.ordersServices.getOrdersForUser().subscribe({
      next:((orders:IOrder[])=>{
        this.orders = orders
      }),
      error:((err)=>{console.error(err.message)})

    })
  }

}
