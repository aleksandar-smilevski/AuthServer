export class Book {
    id: string;
    title: string;
    description: string;
    stock: number;
    datePublished: string;
    isbn: string;

    constructor(){
        
    }
}

// [Key]
// public Guid Id { get; set; }
//
// public string Title { get; set; }
// public string Description { get; set; }
// public int Stock { get; set; }
// public DateTime DatePublished { get; set; }
// public string ISBN { get; set; }
// public virtual ICollection<BookAuthor> Authors { get; set; }