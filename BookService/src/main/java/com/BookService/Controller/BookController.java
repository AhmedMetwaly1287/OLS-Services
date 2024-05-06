package com.BookService.Controller;
import com.BookService.Model.Entity.Book;
import com.BookService.Service.BookServices;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;
import java.util.Optional;
@RestController
@CrossOrigin(origins = {"http://localhost:3001","http://localhost:3002"})
public class BookController {
    @Autowired
    private BookServices bookService;

    
    @GetMapping("GetAll")
    public ResponseEntity<List<Book>> getAllBooks() {
        List<Book> books = bookService.getAll();
        return ResponseEntity.ok(books);
    }
    @GetMapping("GetBook/{id}")
    public ResponseEntity<Book> getBookByID(@PathVariable int id) {
        Optional<Book> optionalBook = bookService.getBookById(id);
        return optionalBook.map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }
    @GetMapping("FilterByISBN/{ISBN}")
    public ResponseEntity<List<Book>> getBooksByISBN(@PathVariable String ISBN) {
        List<Book> books = bookService.getBooksByISBN(ISBN);
        return ResponseEntity.ok(books);
    }
    @GetMapping("FilterByRN/{RN}")
    public ResponseEntity<List<Book>> getBooksByRN(@PathVariable String RN) {
        List<Book> books = bookService.getBooksByRN(RN);
        return ResponseEntity.ok(books);
    }
    @GetMapping("FilterByTitle/{title}")
    public ResponseEntity<List<Book>> getBooksByTitle(@PathVariable String title) {
        List<Book> books = bookService.getBooksByTitle(title);
        return ResponseEntity.ok(books);
    }
    @GetMapping("FilterByAuthor/{author}")
    public ResponseEntity<List<Book>> getBooksByAuthor(@PathVariable String author) {
        List<Book> books = bookService.getBooksByAuthor(author);
        return ResponseEntity.ok(books);
    }
    @GetMapping("CheckISBN/{ISBN}")
    public ResponseEntity<Boolean> checkISBN(@PathVariable String ISBN) {
        boolean exists = bookService.isbnExists(ISBN);
        return ResponseEntity.ok(exists);
    }
    @PostMapping("AddBook")
    public ResponseEntity<String> addBook(@RequestBody Book book) {
        bookService.addBook(book);
        return ResponseEntity.status(HttpStatus.CREATED).body("New Book Added");
    }
    @PutMapping("TurnBorrowed/{id}")
    public ResponseEntity<String> turnBorrowed(@PathVariable int id) {
        if (bookService.bookExists(id) && !bookService.isBorrowed(id)) {
            bookService.turnBorrowed(id);
            return ResponseEntity.ok().body("Book with ID " + id + " has been made borrowed");
        } else {
            return ResponseEntity.badRequest().body("Error while making book borrowed (Book is already borrowed or it doesn't exist)");
        }
    }
    @PutMapping("TurnUnborrowed/{id}")
    public ResponseEntity<String> turnUnborrowed(@PathVariable int id) {
        if (bookService.bookExists(id) && bookService.isBorrowed(id)) {
            bookService.turnUnborrowed(id);
            return ResponseEntity.ok().body("Book with ID " + id + " has been made Unborrowed");
        } else {
            return ResponseEntity.badRequest().body("Error while making book Unborrowed (Book is already Unborrowed or it doesn't exist)");
        }
    }
    @DeleteMapping("DeleteBook/{id}")
    public ResponseEntity<String> deleteBook(@PathVariable int id) {
        if (bookService.bookExists(id)) {
            bookService.deleteBook(id);
            return ResponseEntity.ok("Book with the ID " + id + " has been deleted successfully");
        } else {
            return ResponseEntity.notFound().build();
        }
    }
    @PatchMapping("UpdateBook/{id}")
    public ResponseEntity<String> updateBook(@PathVariable int id, @RequestBody Book book) {
        if (!bookService.bookExists(id)) {
            return ResponseEntity.notFound().build();
        }
        bookService.updateBook(id, book);
        return ResponseEntity.ok("Book Updated Successfully");
    }
}