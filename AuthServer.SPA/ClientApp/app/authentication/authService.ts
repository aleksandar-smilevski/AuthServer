import { Injectable } from "@angular/core";
import { UserManager, UserManagerSettings, User, WebStorageStateStore } from 'oidc-client'; 
import { Subject } from "rxjs/Subject";

@Injectable()
export class AuthService {
    private manager = new UserManager(getClientSettings());
    public user: User;

    private loggedInUser = new Subject<User>();

    constructor() {
        this.manager.getUser().then(user => {
            return this.user = user;
        });
    }

    isLoggedIn(): boolean {
        return this.user != null && !this.user.expired;
    }

    getClaims(): any {
        return this.user.profile;
    }

    getBearerToken(): string {
        return `${this.user.token_type} ${this.user.access_token}`;
    }

    startAuthentication(): Promise<void> {
        return this.manager.signinRedirect();
    }

    completeAuthentication(): Promise<void> {
        return this.manager.signinRedirectCallback().then(user => {
            this.user = user;
        });
    }
}

export function getClientSettings(): UserManagerSettings {
    return {
        authority: 'http://localhost:5000/',
        client_id: 'angular_spa',
        redirect_uri: 'http://localhost:5003/auth-callback',
        post_logout_redirect_uri: 'http://localhost:5003/',
        response_type: 'id_token token',
        scope: 'openid profile api1',
        filterProtocolClaims: true,
        loadUserInfo: true,
        userStore: new WebStorageStateStore({ store: window.localStorage })
    }
}