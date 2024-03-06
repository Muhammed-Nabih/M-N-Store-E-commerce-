import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagniation } from '../shared/Models/Pagniation';
import { ICategory } from '../shared/Models/Category';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseURL = "https://localhost:44335/api/";
  constructor(private http: HttpClient) { }

  getProduct() {
    return this.http.get<IPagniation>(this.baseURL + 'Product/get-all-products');
  }

  getCategory() {
    return this.http.get<ICategory[]>(this.baseURL + 'Categories/get-all-categories');
  }
}
