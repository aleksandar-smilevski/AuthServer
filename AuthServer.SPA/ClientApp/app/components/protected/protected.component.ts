import { Component } from '@angular/core';
import { AuthService } from "../../authentication/authService";

@Component({
    selector: 'protected',
    templateUrl: './protected.component.html'
})
export class ProtectedComponent {

    public user: any;
    public claims: string[] = [];

    constructor(private authService: AuthService) {
        this.user = this.authService.getClaims();
        this.getParsedClaims();
    }

    getParsedClaims() {
        let propertyNames = Object.keys(this.user);
        for (var key in propertyNames) {
            this.claims.push(`${key} : ${this.user[key]}`);
        }

    }
}

//despina
//stefi
//irina
//vesova
//ilina
//joki
//olivera
//angela janevsla
//tina
//tamara ristevska
