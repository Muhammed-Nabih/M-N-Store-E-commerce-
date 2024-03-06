import { Component, OnInit } from '@angular/core';
import { IProducts } from '../shared/Models/Products';
import { ShopService } from './shop.service';
import { ICategory } from '../shared/Models/Category';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products:IProducts[];
  category:ICategory[];
  categoryIdSelected:number=0;
  constructor(private shopService:ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }
  
  getProducts(){
    this.shopService.getProduct(this.categoryIdSelected).subscribe(res => {
      this.products = res.data;
    })
  }
  getCategories(){
    this.shopService.getCategory().subscribe(res => {
      this.category = [{id:0,name:'All',description:''},...res];
    })
  }
  onCategorySelect(categoryId: number) {
    this.categoryIdSelected = categoryId;
    this.getProducts();
  }

}
