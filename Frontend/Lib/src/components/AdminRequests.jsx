import React, { useState, useEffect } from "react";
import axios from "axios";
import "./assets/css/AdminRequests.css";
import Logout from "./Logout";
import { Link } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

const AdminRequests = () => {
  const [requests, setRequests] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState("");

  useEffect(() => {
    fetchRequests();
  }, []);

  const fetchRequests = async () => {
    setIsLoading(true);
    try {
      const token = sessionStorage.getItem("token");
      const response = await axios.get(
        `http://localhost:6003/api/BorrowedBook/GetAll`,

        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setRequests(response.data);
      setError(null);
    } catch (error) {
      setError("Error while communicating with Borrowed Book Service");
    } finally {
      setIsLoading(false);
    }
  };

  const handleApproveRequest = async (requestID) => {
    setIsLoading(true);
    try {
      //Do Nothing
      const token = sessionStorage.getItem("token");
      await axios.put(
        `http://localhost:6003/api/BorrowedBook/ApproveRequest/${requestID}`,
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setSuccessMessage("Request Approved Successfully");
      fetchRequests();
    } catch (error) {
      setError("Error while communicating with Borrowed Book Service");
    } finally {
      setIsLoading(false);
    }
  };

  const handleRejectRequest = async (requestID, bookID) => {
    setIsLoading(true);
    try {
      const decodedToken = jwtDecode(sessionStorage.getItem("token"));
      const UserID = Number.parseInt(decodedToken.UserID);
      //Upon Rejecting, Make an API Call to turn book unborrowed, Add to Archive then remove from BorrowedBook
      const token = sessionStorage.getItem("token");
      let formattedDateTime = new Date().toISOString();
      //Add to Archive
      await axios.post(
        `http://localhost:6004/api/Archive/AddArchive`,
        {
          BorrowedBookRequestID: requestID,
          UserID,
          BookID: bookID,
          ReturnDate: formattedDateTime,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      //Turn book status back to unborrowed
      await axios.put(
        `http://localhost:6010/TurnUnborrowed/${bookID}`,
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      //Reject Request
      await axios.delete(
        `http://localhost:6003/api/BorrowedBook/DeleteRequest/${requestID}`,
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      // Remove the deleted request from the requests array
      fetchRequests();
      setSuccessMessage("Request rejected successfully");
    } catch (error) {
      setError("Error while communicating with the service");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="requests-container">
      <div className="header">
        <h2 className="logo">OLS</h2>
        <div className="options">
          <div className="option">
            <Link to="/Users">Users</Link>
          </div>
          <div className="option">
            <Link to="/Books">Books</Link>
          </div>
          <div className="option">
            <Link to="/Admin/UserRequests">Borrow Requests</Link>
          </div>
          <div className="option">
            <Link to="/Admin/Archive">Archive</Link>
          </div>
          <div className="option" onClick={Logout}>
            Logout
          </div>
        </div>
      </div>
      <h3 className="page-title">Borrow Requests</h3>
      <div className="content">
        {successMessage && (
          <div className="book-success-message-container">
            <p className="success-message">{successMessage}</p>
          </div>
        )}
        {isLoading ? (
          <p>Loading...</p>
        ) : error ? (
          <p className="error-message">{error}</p>
        ) : (
          <table className="requests-table">
            <thead>
              <tr>
                <th>Request ID</th>
                <th>User ID</th>
                <th>Book ID</th>
                <th>Date of Borrowing</th>
                <th>Date of Returning</th>
                <th>Status</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {requests
                .filter((request) => request.returnDate === null)
                .map((request) => (
                  <tr key={request.requestID}>
                    <td>{request.requestID}</td>
                    <td>{request.userID}</td>
                    <td>{request.bookID}</td>
                    <td>{request.borrowedDate}</td>
                    <td>{request.returnDate}</td>
                    <td>{request.requestStatus}</td>

                    <td>
                      {request.requestStatus === "Pending" ? (
                        <div>
                          <button
                            className="cancel-button"
                            onClick={() =>
                              handleApproveRequest(request.requestID)
                            }
                          >
                            Approve
                          </button>
                          <button
                            className="reject-button"
                            onClick={() =>
                              handleRejectRequest(
                                request.requestID,
                                request.bookID
                              )
                            }
                          >
                            Reject
                          </button>
                        </div>
                      ) : (
                        <div>-</div>
                      )}
                    </td>
                  </tr>
                ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default AdminRequests;
