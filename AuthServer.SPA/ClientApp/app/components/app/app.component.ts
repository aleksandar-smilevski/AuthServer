import { Component } from '@angular/core';
import {Observable} from "rxjs/Observable";
import {Book} from "../../models/book";
import {CartService} from "../cart/cart.service";
import {Router} from "@angular/router";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public shoppingCartItems$: Observable<Book[]>;
    
    constructor(private cartService: CartService, private router: Router){
        this.shoppingCartItems$ = this
            .cartService
            .getBooks();

        this.shoppingCartItems$.subscribe(_ => _);
    }
    
    addToCart(item: Book){
        this.cartService.addToCart(item);
    }
    
    navigateTo(location: string){
        this.router.navigate([location]);
    }
}
