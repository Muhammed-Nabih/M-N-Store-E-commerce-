import { AbstractControl } from "@angular/forms";

export function ConfirmPassowrd(contorls: AbstractControl): { [key: string]: boolean } | null {
    var password = contorls.get('password');
    var confirmPassword = contorls.get('confirmPassword');
    if(password.pristine || confirmPassword.pristine){
        return null;
    }
    return password && confirmPassword && password.value !== confirmPassword.value
    ? {'misMatch':true}
    :null;
}