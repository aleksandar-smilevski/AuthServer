import {Injectable} from "@angular/core";
import {Book} from '../../models/book';
import {BehaviorSubject, Observable, Subject, Subscriber} from 'rxjs';
import {of} from 'rxjs/observable/of';

@Injectable()
export class CartService {
    private booksInCartSubject: BehaviorSubject<Book[]> = new BehaviorSubject<Book[]>([]);
    private booksInCart: Book[] = [];
    
    constructor(){
        this.booksInCartSubject.subscribe(_ => this.booksInCart = _);
    }
    
    public addToCart(book: Book){
        this.booksInCartSubject.next([...this.booksInCart, book]);
    }
    
    public getBooks(): Observable<Book[]>{
        return this.booksInCartSubject;
    }
}