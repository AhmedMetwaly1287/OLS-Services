import React, { useState, useEffect } from "react";
import axios from "axios";
import "./assets/css/AdminArchive.css";
import Logout from "./Logout";
import { Link } from "react-router-dom";

const AdminArchive = () => {
  const [archives, setArchives] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchArchives();
  }, []);

  const fetchArchives = async () => {
    setIsLoading(true);
    try {
      const token = sessionStorage.getItem("token");
      const response = await axios.get(
        `http://localhost:6004/api/Archive/GetAll`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setArchives(response.data);
      setError(null);
    } catch (error) {
      setError("Failed to communicate with Archive Service");
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
      <h3 className="page-title">Users Archive</h3>
      <div className="content">
        {isLoading ? (
          <p>Loading...</p>
        ) : error ? (
          <p className="error-message">{error}</p>
        ) : (
          <table className="requests-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Borrow Request ID</th>
                <th>User ID</th>
                <th>Book ID</th>
                <th>Date of Returnal</th>
              </tr>
            </thead>
            <tbody>
              {archives
                .filter((archive) => archive.returnDate !== null)
                .map((archive) => (
                  <tr key={archive.archiveID}>
                    <td>{archive.archiveID}</td>
                    <td>{archive.borrowedBookRequestID}</td>
                    <td>{archive.userID}</td>
                    <td>{archive.bookID}</td>
                    <td>{archive.returnDate}</td>
                  </tr>
                ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default AdminArchive;
