//Native modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

//Components
import { ProtectedComponent } from './components/protected/protected.component';

//Authentication
import { AuthGuardService } from './authentication/authGuard/authGuard';
import { AuthService } from './authentication/authService';
import { AuthCallback } from './components/auth-callback/auth-callback.component';
import { AppComponent } from './components/app/app.component';
import { RootComponent } from './authentication/root.component';

const routes: Routes = [
    {
        path: '',
        component: RootComponent,
        canActivate: [AuthGuardService],
        children: [
            {
                path: 'protected',
                component: ProtectedComponent,
                canActivate: [AuthGuardService]
            },
        ]
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
        RootComponent,
        ProtectedComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot(routes)
    ],
    providers: [
        AuthGuardService,
        AuthService
    ]
})
export class AppModuleShared {
}
