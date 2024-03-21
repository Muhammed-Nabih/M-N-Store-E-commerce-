import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../orders.service';
import { IOrder } from 'src/app/shared/Models/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  order: IOrder;
  constructor(private orderService: OrdersService, private breadCurmbSerevice: BreadcrumbService
    , private router: ActivatedRoute) {
    this.breadCurmbSerevice.set('@OrderDetails', '');
  }

  ngOnInit(): void {
    const id = +this.router.snapshot.paramMap.get('id');
    this.orderService.getOrdersDetails(id).subscribe({
      next:((order:IOrder)=>{
        this.order = order;
        this.breadCurmbSerevice.set('@OrderDetails', `Order ${order.id} - ${order.orderStatus}`)
      }),
      error:((err)=>{console.error(err.message)})
    })
  }

}
