import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProducts } from '../shared/Models/Products';
import { ShopService } from './shop.service';
import { ICategory } from '../shared/Models/Category';
import { ShopParams } from '../shared/Models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search') searchTerm: ElementRef;
  products: IProducts[];
  category: ICategory[];

  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    { name: 'Name', value: 'Name' },
    { name: 'Price: Max-Min', value: 'PriceDesc' },
    { name: 'Price: Min-Max', value: 'PriceAsc' }

  ]
  pageSize: any;
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }

  getProducts() {
    this.shopService.getProduct(this.shopParams).subscribe(res => {
      this.products = res.data;
      this.totalCount = res.count;
      this.shopParams.pageNumber = res.pageNumber;
      this.shopParams.pageSize = res.pageSize;
    })
  }
  getCategories() {
    this.shopService.getCategory().subscribe(res => {
      this.category = [{ id: 0, name: 'All', description: '' }, ...res];
    })
  }
  onCategorySelect(categoryId: number) {
    this.shopParams.categoryId = categoryId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelect(sort: Event) {
    let sortValue = (sort.target as HTMLInputElement).value;
    this.shopParams.sort = sortValue;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }

  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }

  onRest() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts()
  }

}
