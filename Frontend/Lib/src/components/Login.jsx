import React, { useState } from "react";
import axios from "axios";
import "./assets/css/Login.css";
import { jwtDecode } from "jwt-decode";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleLogin = async (event) => {
    event.preventDefault(); // Prevent default form submission behavior

    if (!email || !password) {
      setMessage("Fields cannot be empty!");
      return;
    }

    setIsLoading(true);
    try {
      const response = await axios.post(
        `http://localhost:6001/api/User/Login`,
        { email, password }
      );
      const { data } = response;
      setMessage(data.msg);

      // Set expiration time for 10 minutes from now
      const expirationTime = new Date(Date.now() + 10 * 60 * 1000);

      document.cookie = `JWT=${
        data.token
      }; expires=${expirationTime.toUTCString()}; path=/;`;

      sessionStorage.setItem("token", data.token);
      const decodedToken = jwtDecode(data.token);
      if (decodedToken.IsApproved === "true" && decodedToken.RoleID === "1") {
        window.location.href = "/Users";
      } else if (
        decodedToken.IsApproved === "true" &&
        decodedToken.RoleID === "0"
      ) {
        window.location.replace("http://localhost:3002/Home");
      } else if (decodedToken.IsApproved === "false") {
        window.location.replace("http://localhost:3002/PendingApproval");
      }
    } catch (error) {
      if (error.response && error.response.data && error.response.data.error) {
        setMessage(error.response.data.error);
      } else {
        setMessage(
          "Error (Invalid Creds / Failed to communicate with User Service"
        );
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="login-container">
      <h1>Online Library System</h1>
      <form className="login-form" onSubmit={handleLogin}>
        {message && (
          <div className="login-error-message-container">
            <div className="login-error-message">{message}</div>
          </div>
        )}
        <br />
        <div className="form-group">
          <label>Email:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button className="login-btn" type="submit" disabled={isLoading}>
          {isLoading ? "Logging in..." : "Login"}
        </button>
        <div className="hypertext"></div>
      </form>
    </div>
  );
};

export default Login;
