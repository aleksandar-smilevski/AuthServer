//Native modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MzNavbarModule, MzSidenavModule, MzCardModule, MzSpinnerModule, MzCollectionModule } from 'ng2-materialize';

//Components
import { ProtectedComponent } from './components/protected/protected.component';

//Authentication
import { AuthGuardService } from './authentication/authGuard/authGuard';
import { AuthService } from './authentication/authService';
import { AuthCallback } from './components/auth-callback/auth-callback.component';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from "./components/home/home.component";
import {AllAuthorsComponent} from "./components/author/allAuthors/allAuthors.component";
import {CartService} from "./components/cart/cart.service";
import {CartItemsComponent} from "./components/cartItems/cartItems.component";


const routes: Routes = [
    
    {
        path: 'cart',
        // canActivate: [AuthGuardService],
        component: CartItemsComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'authors',
        component: AllAuthorsComponent
    },
    {
        path: 'auth-callback',
        component: AuthCallback
    }
];

@NgModule({
    declarations: [
        AuthCallback,
        AppComponent,
        HomeComponent,
        ProtectedComponent,
        AllAuthorsComponent,
        CartItemsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        RouterModule.forRoot(routes),
        MzSidenavModule,
        MzNavbarModule,
        MzCardModule,
        MzSpinnerModule,
        MzCollectionModule
    ],
    providers: [
        AuthGuardService,
        AuthService,
        CartService
    ]
})
export class AppModuleShared {
}
