import {Component, ElementRef, ViewChild} from "@angular/core";
import {Http} from "@angular/http";

@Component({
    template: `
            <div class="container">
                <div class="row">
                     <div class="col s12">
                         <mz-card [hoverable]="false">
                             <mz-card-title>
                                 All Authors
                             </mz-card-title>
                         </mz-card>
                     </div>
                     <div class="col s12">
                        <mz-card [hoverable]="false">
                            <mz-card-content>
                                <div class="input-field">
                                </div>
                            </mz-card-content>
                        </mz-card> 
                     </div>
                     <div class="col s12 offset-s4">
                         <mz-spinner [hidden]="authorsLoaded"
                                     [color]="'blue'"
                                     [size]="'big'">
                         </mz-spinner>
                    </div>
                    <div *ngFor="let author of allAuthors" class="col s4">
                        <mz-card [hoverable]="true">
                            <mz-card-title>
                                {{ author.firstName }} {{ author.lastName }}
                            </mz-card-title>
                            <mz-card-action>
                                <a href="#">This is a link</a>
                                <a href="#">This is a link</a>
                            </mz-card-action>
                        </mz-card>
                    </div>
                </div>
            </div>
                `,
    selector: 'allAuthors'
})
export class AllAuthorsComponent {
    
    @ViewChild('spinner')
    private spinner: ElementRef;
    private authorsLoaded: boolean = false;
    private allAuthors: Array<Author> = [];
    
    constructor(private http: Http) {
        this.getAll();
    }
    
    getAll() {
        this.http.get('http://localhost:5004/api/authors')
            .map(res => res.json())
            .subscribe(res => {
                console.log(res);
                if (res.responseType == WebResponseType.Success){
                    debugger;
                    this.allAuthors = res.data;
                    console.log(this.allAuthors);
                    setTimeout(() => {
                        
                    }, 50000);
                    this.authorsLoaded = true;
                }
                else {
                    console.log("Error!!!");
                }
                
            }, error => {
                console.log(error);
            });
    }
}

export class Author {
    id: string;
    firstName: string;
    lastName: string;
    
    constructor(id: string, firstName: string, lastName:string){
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
    }
}

export enum WebResponseType {
    Success = 1000,
    Error = 2000,
    NoDataFound = 3000
}