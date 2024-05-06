import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { createRoot } from 'react-dom/client';
import App from './App';
import Register from './components/Register';
import PendingApprovalPage from './components/PendingApprovalPage';
import Home from './components/Home';
import Book from './components/Book';
import UserRequests from './components/UserRequests';
import UserArchive from './components/UserArchive';
import Approved from './components/Approved';
import Logout from './components/Logout';

createRoot(document.getElementById('root')).render(
    <Router>
        <Routes>
            <Route path="/Home" element={<Approved><Home /></Approved>} />
            <Route path="/Home/UserRequests" element={<Approved><UserRequests /></Approved>} />
            <Route path="/Home/Archive" element={<Approved><UserArchive /></Approved>} />
            <Route path="/Home/Book/:BookID" element={<Approved><Book /></Approved>} />

            <Route path="/" element={<App />} />
            <Route path="/Register" element={<Register />} />
            <Route path="/Logout" element={<Logout />} />
            <Route path="/PendingApproval" element={<PendingApprovalPage />} />
        </Routes>
    </Router>

);
