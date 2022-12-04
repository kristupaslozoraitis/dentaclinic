import React, { useState } from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { HomePage } from "./HomePage";
import { Box, ChakraProvider } from "@chakra-ui/react";
import { PatientCard } from "./Pages/PatientCard";
import { NavBar } from "./components/Navbar/Navbar";
import { VisitsHistory } from "./Pages/VisitsHistory/VisitsHistory";
import { VisitsAdminPanel } from "./Pages/VisitsAdminPanel";
import { slide as Menu } from "react-burger-menu";
import { FreeVisitsView } from "./Pages/FreeVisitsView";

function App() {
  return (
    <BrowserRouter>
      <Box display={{ base: "flex", md: "none" }}>
        <Menu>
          <NavBar />
        </Menu>
      </Box>
      <Box display={{ base: "none", md: "block" }}>
        <NavBar />
      </Box>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/patientCard" element={<PatientCard />} />
        <Route path="/visitsHistory" element={<VisitsHistory />} />
        <Route path="/visitsAdminPanel" element={<VisitsAdminPanel />} />
        <Route path="/freeVisits" element={<FreeVisitsView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
