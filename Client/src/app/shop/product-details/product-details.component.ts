import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProducts } from 'src/app/shared/Models/Products';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  quantity:number=1;
  product: IProducts;

    constructor(private shopService: ShopService, private activeRoute: ActivatedRoute
      ,private bcService:BreadcrumbService,private basketService:BasketService) { 
        this.bcService.set('@productDetails',' ');
      }
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    this.shopService.getProdut(parseInt(this.activeRoute.snapshot.paramMap.get('id')))
      .subscribe(res => {
        this.product = res;
        this.bcService.set('@productDetails',res.name);
      });
  }
  addItemToBasket(){
    this.basketService.addItemToBasket(this.product,this.quantity);
  }
  incrementQuantity(){
    this.quantity++;
  }
  decrementQuantity(){
    if(this.quantity >1)
    {
      this.quantity--;
    }
  }

}
