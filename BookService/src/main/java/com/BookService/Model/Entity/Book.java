package com.BookService.Model.Entity;
import jakarta.persistence.*;
import java.time.LocalDateTime;
@Entity
public class Book {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(length = 50, unique = true)
    private String isbn;

    @Column(length = 50)
    private String rackNumber;

    @Column(length = 50)
    private String author;

    @Column(length = 50)
    private String title;

    @Column(columnDefinition = "boolean default false")
    private boolean isBorrowed;

    @Column(columnDefinition = "TIMESTAMP DEFAULT CURRENT_TIMESTAMP")
    private LocalDateTime creationDate;

    public Book() {
        this.creationDate = LocalDateTime.now();
    }
    public Book(int id, String isbn, String rackNumber, String author, String title, boolean isBorrowed) {
        this.id = id;
        this.isbn = isbn;
        this.rackNumber = rackNumber;
        this.author = author;
        this.title = title;
        this.isBorrowed = isBorrowed;
        this.creationDate = LocalDateTime.now();
    }
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getIsbn() {
        return isbn;
    }

    public void setIsbn(String isbn) {
        this.isbn = isbn;
    }

    public String getRackNumber() {
        return rackNumber;
    }

    public void setRackNumber(String rackNumber) {
        this.rackNumber = rackNumber;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public boolean isBorrowed() {
        return isBorrowed;
    }

    public void setBorrowed(boolean borrowed) {
        isBorrowed = borrowed;
    }

    public LocalDateTime getCreationDate() {
        return creationDate;
    }

    public void setCreationDate(LocalDateTime creationDate) {
        this.creationDate = creationDate;
    }
}
