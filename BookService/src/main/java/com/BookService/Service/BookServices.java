package com.BookService.Service;
import com.BookService.Model.Entity.Book;
import com.BookService.Repository.BookRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.Optional;
@Service
public class BookServices {
    @Autowired
    private BookRepository bookRepository;

    public void addBook(Book book) {
        bookRepository.save(book);
    }

    public boolean deleteBook(int id) {
        if (bookRepository.existsById(id)) {
            bookRepository.deleteById(id);
            return true;
        }
        return false;
    }

    public List<Book> getAll() {
        return bookRepository.findAll();
    }

    public Optional<Book> getBookById(int id) {
        return bookRepository.findById(id);
    }

    public List<Book> getBooksByISBN(String isbn) {
        return bookRepository.findByIsbnContainingOrIsbnEquals(isbn);
    }

    public List<Book> getBooksByRN(String rackNumber) {
        return bookRepository.findByRackNumberContainingOrRackNumberEquals(rackNumber);
    }

    public List<Book> getBooksByTitle(String title) {
        return bookRepository.findByTitleContainingOrTitleEquals(title);
    }

    public List<Book> getBooksByAuthor(String author) {
        return bookRepository.findByAuthorContainingOrAuthorEquals(author);
    }

    public boolean isbnExists(String isbn) {
        return bookRepository.existsByIsbn(isbn);
    }

    public boolean bookExists(int id) {
        return bookRepository.existsById(id);
    }

    public boolean isBorrowed(int id) {
        return bookRepository.existsByIsBorrowedTrueAndId(id);
    }

    public void turnBorrowed(int id) {
        Optional<Book> optionalBook = bookRepository.findById(id);
        if (optionalBook.isPresent()) {
            Book book = optionalBook.get();
            book.setBorrowed(true);
            bookRepository.save(book);
        }
    }

    public void turnUnborrowed(int id) {
        Optional<Book> optionalBook = bookRepository.findById(id);
        if (optionalBook.isPresent()) {
            Book book = optionalBook.get();
            book.setBorrowed(false);
            bookRepository.save(book);
        }
    }
    public void updateBook(int id, Book updatedBook) {
        Optional<Book> optionalBook = bookRepository.findById(id);
        optionalBook.ifPresent(book -> {
            if (updatedBook.getIsbn() != null && !bookRepository.existsByIsbn(updatedBook.getIsbn())) {
                book.setIsbn(updatedBook.getIsbn());
            }
            if (updatedBook.getRackNumber() != null) {
                book.setRackNumber(updatedBook.getRackNumber());
            }
            if (updatedBook.getAuthor() != null) {
                book.setAuthor(updatedBook.getAuthor());
            }
            if (updatedBook.getTitle() != null) {
                book.setTitle(updatedBook.getTitle());
            }
            bookRepository.save(book);
        });
    }
}