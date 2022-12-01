import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { HomePage } from './HomePage';
import { ChakraProvider } from '@chakra-ui/react';
import { PatientCard } from './Pages/PatientCard';
import { NavBar } from './components/Navbar/Navbar';
import { VisitsHistory } from './Pages/VisitsHistory/VisitsHistory';
import { VisitsAdminPanel } from './Pages/VisitsAdminPanel';

function App() {

  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path='/patientCard' element={<PatientCard />} />
        <Route path='/visitsHistory' element={<VisitsHistory />} />
        <Route path='/visitsAdminPanel' element={<VisitsAdminPanel />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
