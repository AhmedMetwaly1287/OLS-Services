import React, { useState, useEffect } from "react";
import axios from "axios";
import "./assets/css/Book.css";
import { Link, useParams } from "react-router-dom";
import Logout from "./Logout";
import { jwtDecode } from "jwt-decode";

const Book = () => {
  const { BookID } = useParams();
  const [book, setBook] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [borrowMessage, setBorrowMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  useEffect(() => {
    fetchBook();
  }, []);

  const fetchBook = async () => {
    setIsLoading(true);
    try {
      const token = sessionStorage.getItem("token");
      const response = await axios.get(
        `http://localhost:6010/GetBook/${BookID}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setBook(response.data);
      setError(null);
    } catch (error) {
      setError("Failed to communicate with Book Service");
    } finally {
      setIsLoading(false);
    }
  };

  const handleBorrow = async () => {
    try {
      //When borrowing, submit a request then turn the book status to Borrowed
      const token = sessionStorage.getItem("token");
      const decodedToken = jwtDecode(token);
      const UserID = decodedToken.UserID;
      const borrowResponse = await axios.post(
        `http://localhost:6003/api/BorrowedBook/SubmitRequest`,
        { UserID, BookID },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      const turnBorrowed = await axios.put(
        `http://localhost:6010/TurnBorrowed/${BookID}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setSuccessMessage(
        "Request Submitted Successfully, Please await the Librarian's decision"
      );
    } catch (error) {
      setBorrowMessage(
        "Error (Exceeded allowed limit of requests / Failed to communicate with the service)"
      );
    }
  };

  return (
    <div className="book-container">
      <div className="header">
        <h2 className="logo">OLS</h2>
        <div className="options">
          <div className="option">
            <Link to="/Home">Home</Link>
          </div>
          <div className="option">
            <Link to="/Home/UserRequests">Borrow Requests</Link>
          </div>
          <div className="option">
            <Link to="/Home/Archive">Archive</Link>
          </div>
          <div className="option" onClick={Logout}>
            Logout
          </div>
        </div>
      </div>
      {successMessage && (
        <div className="book-success-message-container">
          <p className="success-message">{successMessage}</p>
        </div>
      )}
      {borrowMessage && (
        <div className="error-message-container">
          <p className="error-message">{borrowMessage}</p>
        </div>
      )}
      {isLoading ? (
        <p>Loading...</p>
      ) : error ? (
        <p className="error-message">{error}</p>
      ) : book ? (
        <div className="book-details">
          <h2 className="book-title">{book.title}</h2>
          <p className="book-info">Author: {book.author}</p>
          <p className="book-info">ISBN: {book.isbn}</p>
          <p className="book-info">Rack Number: {book.rackNumber}</p>
          <p className="book-info">
            Is Borrowed: {book.borrowed ? "Yes" : "No"}
          </p>
          <button
            className={`borrow-button ${book.borrowed ? "disabled" : ""}`}
            onClick={handleBorrow}
            disabled={book.isBorrowed}
          >
            Borrow
          </button>
        </div>
      ) : (
        <p>No book found with ID {BookID}</p>
      )}
    </div>
  );
};

export default Book;
