import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router,private toast:ToastrService ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError(
        (err) => {
          if (err) {
            if (err.status === 400) {
              if(err.error.errors){
                throw err.error;
              }
              else{
                this.toast.error(err.error.message, err.error.stuatusCode);
              }
              
            }
            if (err.status === 401) {
              this.toast.error(err.error.message, err.error.stuatusCode);
            }
            if (err.status === 404) {
              this.router.navigateByUrl('/not-found');
            }
            if (err.status === 500) {
              const navationExtra:NavigationExtras = {state: {error:err.error}}
              this.router.navigateByUrl('/server-error',navationExtra);
            }
          }
          return throwError(() => err.message || 'Server Not Found!');
        }
      )
    );
  }
}
