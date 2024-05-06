import React, { useEffect } from "react";
import { jwtDecode } from "jwt-decode";
import Cookies from "js-cookie";

const Authorization = ({ children }) => {
  useEffect(() => {
    const token = sessionStorage.getItem("token");
    if (!Cookies.get("JWT") || !token) {
      sessionStorage.removeItem("token");
      window.location.href = "/";
      return;
    }
    try {
      const decodedToken = jwtDecode(token);
      if (decodedToken.RoleID === "0") {
        window.location.replace("http://localhost:3002/Home");
      }
    } catch (error) {
      window.location.href = "/";
    }
  }, []);
  return <>{children}</>;
};

export default Authorization;
