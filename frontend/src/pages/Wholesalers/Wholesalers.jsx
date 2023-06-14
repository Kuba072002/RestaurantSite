import { React, useState, useEffect } from 'react'
import { Box, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material'
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { DataGrid } from '@mui/x-data-grid';
import axios from 'axios';
import dayjs from 'dayjs';

const Wholesalers = () => {
   const [startDate, setStartDate] = useState(dayjs().subtract(5, 'day'));
   const [endDate, setEndDate] = useState(dayjs());
   const [wholesalers, setWholesalers] = useState([]);
   const [wholesalerId, setWholesalerId] = useState(1);
   const [data, setData] = useState([]);

   useEffect(() => {
      fetchWholesalers();
   }, []);

   const fetchWholesalers = async () => {
      try {
         const response = await axios.get("https://localhost:7122/api/Wholesaler");
         setWholesalers(response.data);
      } catch (error) {
         console.log(error.response);
      }
   }

   const fetchData = async () => {
      try {
         const formData = new FormData();
         formData.append('wholesalerId', wholesalerId);
         formData.append('startDate', dayjs(startDate).format('YYYY-MM-DD'));
         formData.append('endDate', dayjs(endDate).format('YYYY-MM-DD'));
         const response = await axios.post("https://localhost:7122/api/Wholesaler/Raport", formData,
            { headers: { 'Content-Type': 'application/json', }, }
         );
         // console.log(response.data);
         setData(response.data);
      } catch (error) {
         console.log(error.response);
      }
   }

   const handleChange = (event) => {
      setWholesalerId(event.target.value);
   };

   const columns = [
      { field: 'id', headerName: 'Id', type: 'number', editable: true, align: 'left', headerAlign: 'left' },
      { field: 'componentName', headerName: 'Name', width: 180, editable: true },

      { field: 'quantity', headerName: 'Quantity', type: 'number', editable: true },
      { field: 'unit', headerName: 'Unit', editable: true,align:'center', headerAlign: 'center'},
   ];

   return (
      <Box
         sx={{
            width: '100vw',
            minHeight: '100vh',
            height: '100%',
            // backgroundColor: '#424242',
         }}
      >
         <Box sx={{ p: 3, display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: "column" }}>
            <Box sx={{ display: 'flex', gap: 2, justifyContent: 'space-between', mb: 3 }}>
               <LocalizationProvider dateAdapter={AdapterDayjs}>
                  <DatePicker
                     label="Start date"
                     value={startDate}
                     onChange={(date) => { setStartDate(date) }}
                  />
                  <DatePicker
                     label="End date"
                     value={endDate}
                     onChange={(date) => { setEndDate(date) }}
                  />
               </LocalizationProvider>
               <Button variant="contained" onClick={fetchData}>Submit</Button>
            </Box>
            <Box sx={{ minWidth: 120, mb: 3 }}>
               <FormControl fullWidth>
                  <InputLabel id="wholesaler-select-label">Wholesaler</InputLabel>
                  <Select
                     labelId="wholesaler-select-label"
                     id="wholesaler-select"
                     value={wholesalerId}
                     label="Wholesaler"
                     onChange={handleChange}
                  >
                     {wholesalers.map((wholesaler) =>
                        <MenuItem key={wholesaler.id} value={wholesaler.id}>{wholesaler.name}</MenuItem>
                     )}
                  </Select>
               </FormControl>
            </Box>
            <div style={{ maxHeight: '60vh' }}>
               <DataGrid rows={data} columns={columns} />
            </div>
         </Box>
      </Box>
   )
}

export default Wholesalers