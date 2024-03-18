import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';

const routes: Routes = [
  {path:'', component:HomeComponent, data:{breadcurmb:'Home'}},
  {path:'not-found', component:NotFoundComponent,data:{breadcurmb:'Not Found'}},
  {path:'server-error', component:ServerErrorComponent,data:{breadcurmb:'Server Error'}},
  {path:'test-error', component:TestErrorComponent, data:{breadcurmb:'Test Error'}},
  {path:'shop', loadChildren:()=>import('./shop/shop.module').then(mo=>mo.ShopModule),data:{breadcurmb:'Shop'}},
  {path:'basket', loadChildren:()=>import('./basket/basket.module').then(mo=>mo.BasketModule),data:{breadcurmb:'Basket'}},

  {path:'**', redirectTo:'/not-found',pathMatch:'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
