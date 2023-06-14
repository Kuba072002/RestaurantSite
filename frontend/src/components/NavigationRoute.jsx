import React from 'react';
import { Outlet } from "react-router-dom";
import { Box } from '@mui/material';
import Navbar from './Navbar/Navbar';
const NavigationRoute = () => {
   return (
      <Box sx={{ height: '100vh', display: 'flex', flexDirection: 'column' }}>
         <Navbar />
         <Outlet />
      </Box>
   )
}

export default NavigationRoute