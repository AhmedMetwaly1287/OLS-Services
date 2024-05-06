import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { createRoot } from 'react-dom/client';
import App from './App';
import Users from './components/Users';
import Books from './components/Books';
import AddBook from './components/AddBook';
import UpdateBook from './components/UpdateBook';
import AdminRequests from './components/AdminRequests';
import NotFound from './components/NotFound';
import Authorization from './components/Authorization';
import AdminArchive from './components/AdminArchive';
import Approved from './components/Approved';
import Logout from './components/Logout';

createRoot(document.getElementById('root')).render(
    <Router>
        <Routes>
            <Route path="/NotFound" element={<Approved><NotFound /></Approved>} />

            <Route path="/" element={<App />} />
            <Route path="/Logout" element={<Logout />} />

            <Route path="/Books/AddBook" element={<Authorization><AddBook /></Authorization>} />
            <Route path="/Books/UpdateBook/:bookId" element={<Authorization><UpdateBook /></Authorization>} />
            <Route path="/Admin/UserRequests" element={<Authorization><AdminRequests /></Authorization>} />
            <Route path="/Admin/Archive" element={<Authorization><AdminArchive /></Authorization>} />
            <Route path="/Users" element={<Authorization><Users /></Authorization>} />
            <Route path="/Books" element={<Authorization><Books /></Authorization>} />
        </Routes>
    </Router>

);
