import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../authentication/authService";


@Component({
    templateUrl: './auth-callback.component.html'
})
export class AuthCallback implements OnInit {

    constructor(private authService: AuthService) {
        
    }

    ngOnInit(): void {
        this.authService.completeAuthentication();
    }
}