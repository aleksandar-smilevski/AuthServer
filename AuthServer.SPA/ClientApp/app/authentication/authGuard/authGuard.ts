/// <reference path="../authservice.ts" />
import { CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from '../authService';

@Injectable()
export class AuthGuardService implements CanActivate {

    constructor(private authService: AuthService) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.isLoggedIn()) {
            return true;
        }
        this.authService.setRedirectUrl(state.url);
        this.authService.startAuthentication();
        return false;
    }
}