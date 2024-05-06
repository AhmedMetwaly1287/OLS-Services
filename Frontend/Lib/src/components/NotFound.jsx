import React from "react";
import "./assets/css/NotFound.css";
const handleNotFound = () => {
  window.location.replace("http://localhost:3002/Home");
};
const NotFound = () => {
  return (
    <div className="page-container">
      <h1>404 Not Found</h1>
      <div className="logout-btn">
        <button onClick={handleNotFound}>Home</button>
      </div>
    </div>
  );
};

export default NotFound;
