import { React, useState } from 'react'
import { Box, Button, } from '@mui/material'
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { DataGrid } from '@mui/x-data-grid';
import axios from 'axios';
import dayjs from 'dayjs';

const Components = () => {
   const [startDate, setStartDate] = useState(dayjs().subtract(5, 'day'));
   const [endDate, setEndDate] = useState(dayjs());
   const [data, setData] = useState([]);
   const [data2, setData2] = useState([]);

   const [data3, setData3] = useState([]);

   const [selectedRow, setSelectedRow] = useState([])

   const fetchData = async () => {
      try {
         const formData = new FormData();
         formData.append('startDate', dayjs(startDate).format('YYYY-MM-DD'));
         formData.append('endDate', dayjs(endDate).format('YYYY-MM-DD'));
         const response = await axios.post("https://localhost:7122/api/Component/Raport", formData,
            { headers: { 'Content-Type': 'application/json', }, }
         );
         const response2 = await axios.post("https://localhost:7122/api/Order/DictOfComponentIdDishNameNumber", formData,
            { headers: { 'Content-Type': 'application/json', }, }
         );
         console.log(response2.data);
         setData2(response2.data)
         setData(response.data.map((item, index) => ({
            id: index + 1, // Assuming index + 1 as the unique id
            componentId: item.key.id,
            name: item.key.name,
            freshnessTime: item.key.freshnessTime,
            unit: item.key.unit,
            value1: item.value[0],
            value2: item.value[1]
         })));
      } catch (error) {
         console.log(error.response);
      }
   }

   const columns = [
      { field: 'id', headerName: 'Name', width: 180 },
      { field: 'name', headerName: 'Name', width: 180, },
      { field: 'value1', headerName: 'Used', type: 'number' },
      { field: 'value2', headerName: 'Rotten', type: 'number' },
      { field: 'unit', headerName: 'Unit', align: 'center', headerAlign: 'center' },
   ];

   const columns2 = [
      { field: 'dishName', headerName: 'DishName', width: 180, },
      { field: 'quantity', headerName: 'Quantity', type: 'number' },
      { field: 'unit', headerName: 'Unit', align: 'center', headerAlign: 'center' },
   ];

   return (
      <Box
         sx={{
            width: '100vw',
            // minHeight: '90vh',
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
            <div style={{ display: 'flex', gap: '2rem', justifyContent: 'space-around' }}>
               <div style={{ height: '70vh' }}>
                  <DataGrid rows={data} columns={columns}
                     // checkboxSelection
                     onRowSelectionModelChange={(newRowSelectionModel) => {
                        setSelectedRow(newRowSelectionModel);
                        setData3(data2[data[newRowSelectionModel - 1]?.name].map((item, index) => ({
                           id: index + 1, // Assuming index + 1 as the unique id
                           dishName: item[0],
                           quantity: item[1],
                           unit: item[2],
                        })))
                     }}
                     rowSelectionModel={selectedRow}
                  />
               </div>
               {/* {selectedRow && (
                  <Box sx={{background: '#asdasd'}}>
                  <p>Selected Row: {data[selectedRow-1]?.componentId}</p>
                  
                  </Box>
               )} */}
                  <DataGrid rows={data3} columns={columns2}
                  />
            </div>
         </Box>
      </Box>
   )
}

export default Components