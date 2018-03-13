import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../authentication/authService";
import { Router } from "@angular/router";


@Component({
    templateUrl: './auth-callback.component.html'
})
export class AuthCallback implements OnInit {

    constructor(private authService: AuthService, private router: Router) {}

    ngOnInit(): void {
        this.authService.completeAuthentication().then(() => {
            this.router.navigate([this.authService.getRedirectUrl()]);
        });
    }
}