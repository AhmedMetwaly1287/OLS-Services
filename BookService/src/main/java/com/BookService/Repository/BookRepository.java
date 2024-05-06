package com.BookService.Repository;
import com.BookService.Model.Entity.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import java.util.List;
@Repository
public interface BookRepository extends JpaRepository<Book, Integer> {
    @Query("SELECT b FROM Book b WHERE b.isbn LIKE %:isbn% OR b.isbn = :isbn")
    List<Book> findByIsbnContainingOrIsbnEquals(@Param("isbn") String isbn);

    @Query("SELECT b FROM Book b WHERE b.rackNumber LIKE %:rackNumber% OR b.rackNumber = :rackNumber")
    List<Book> findByRackNumberContainingOrRackNumberEquals(@Param("rackNumber") String rackNumber);

    @Query("SELECT b FROM Book b WHERE b.title LIKE %:title% OR b.title = :title")
    List<Book> findByTitleContainingOrTitleEquals(@Param("title") String title);

    @Query("SELECT b FROM Book b WHERE b.author LIKE %:author% OR b.author = :author")
    List<Book> findByAuthorContainingOrAuthorEquals(@Param("author") String author);
    boolean existsByIsbn(String isbn);
    boolean existsById(int id);
    boolean existsByIsBorrowedTrueAndId(int id);
}
