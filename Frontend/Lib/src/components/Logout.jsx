import axios from "axios";
import Cookies from "js-cookie";

const Logout = async () => {
  try {
    const token = sessionStorage.getItem("token");
    await axios.post(
      "http://localhost:6001/api/User/Logout",
      {},
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );
    // Clear sessionStorage
    sessionStorage.removeItem("token");
    Cookies.remove("JWT");
    // Redirect to login page
    window.location.href = "/";
  } catch (error) {
    console.error("Error while communicating with User Service");
  }
};

export default Logout;
