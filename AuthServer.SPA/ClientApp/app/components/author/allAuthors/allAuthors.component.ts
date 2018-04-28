
import {Component, ElementRef, ViewChild} from "@angular/core";
import {Http} from "@angular/http";
import {WebResponseType} from "../../../helpers/webResponseType";
import {Author} from "../../../models/author";
import {Router} from "@angular/router";

@Component({
    templateUrl: 'allAuthors.component.html',
    selector: 'allAuthors'
})
export class AllAuthorsComponent {
    
    @ViewChild('spinner')
    private spinner: ElementRef;
    private authorsLoaded: boolean = false;
    private errorOnLoad: boolean = false;
    private allAuthors: Array<Author> = [];
    
    constructor(private http: Http, private router: Router) {
        this.getAll();
    }
    
    getAll() {
        this.http.get('http://localhost:5004/api/authors')
            .map(res => res.json())
            .subscribe(res => {
                console.log(res);
                if (res.responseType == WebResponseType.Success){
                    this.allAuthors = res.data;
                    setTimeout(() => {
                        
                    }, 50000);
                    this.authorsLoaded = true;
                }
                else {
                    console.log("Error!!!");
                }
                
            }, error => {
                this.errorOnLoad = true;
            });
    }
    
    click(e : Event, authorId: string){
        this.router.navigate(['/author-details', authorId]);
    }
}

