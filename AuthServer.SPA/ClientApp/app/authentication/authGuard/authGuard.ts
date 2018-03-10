/// <reference path="../authservice.ts" />
import { CanActivate } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from '../authService';

@Injectable()
export class AuthGuardService implements CanActivate {

    constructor(private authService: AuthService) {}

    canActivate(): boolean {
        if (this.authService.isLoggedIn()) {
            return true;
        }

        this.authService.startAuthentication();
        return false;
    }
}