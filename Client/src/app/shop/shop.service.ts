import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagniation } from '../shared/Models/Pagniation';
import { ICategory } from '../shared/Models/Category';
import { map } from 'rxjs';
import { ShopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseURL = "https://localhost:44335/api/";
  constructor(private http: HttpClient) { }

  getProduct(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.categoryId !== 0) {
      params = params.append('categoryId', shopParams.categoryId.toString());
    }

    if(shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);

    params = params.append('pageNumber', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<IPagniation>(this.baseURL + 'Product/get-all-products', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      )

  }

  getCategory() {
    return this.http.get<ICategory[]>(this.baseURL + 'Categories/get-all-categories');
  }



}
