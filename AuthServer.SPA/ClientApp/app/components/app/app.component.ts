import { Component } from '@angular/core';
import {Observable} from "rxjs/Observable";
import {Book} from "../../models/book";
import {CartService} from "../cart/cart.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public shoppingCartItems$: Observable<Book[]>;
    
    constructor(private cartService: CartService){
        this.shoppingCartItems$ = this
            .cartService
            .getBooks();

        this.shoppingCartItems$.subscribe(_ => _);
    }
    
    addToCart(item: Book){
        this.cartService.addToCart(item);
    }
}
