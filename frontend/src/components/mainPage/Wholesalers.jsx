import { React, useState, useEffect } from 'react'
import axios from 'axios';
import { DataGrid } from '@mui/x-data-grid';
import { Box,Fab,Modal,Typography,TextField,Button } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

const Wholesalers = () => {
   const [data, setData] = useState([]);

   const [openAddModal, setOpenAddModal] = useState(false);
    const style = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: '80%',
        bgcolor: 'background.paper',
        boxShadow: 24,
        p: 4,
    };

   const fetchData = async () => {
      try {
         const response = await axios.get("https://localhost:7122/api/Wholesaler");
         setData(response.data);
      } catch (error) {
         console.log(error.response);
      }
   }

   useEffect(() => {
      fetchData();
   }, [])

   const columns = [
      { field: 'id', headerName: 'id' },
      { field: 'name', headerName: 'Name', width: 180, },
      { field: 'address', headerName: 'Address', },
   ];

   return (
      <>
         <Box
            sx={{
               width: '100vw',
               height: '100%',
            }}
         >
            <Box sx={{ p: 3, display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: "column" }}>
               <div>
                  <DataGrid rows={data} columns={columns}
                  />
               </div>
            </Box>
         </Box>
         <Fab sx={{
            position: 'absolute',
            bottom: 20,
            right: 20,
         }} color={"primary"}
            onClick={() => setOpenAddModal(true)}>
            <AddIcon />
         </Fab>
         <Modal
            open={openAddModal}
            onClose={() => setOpenAddModal(false)}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
         >
            <Box sx={style}>
               <form style={{ margin: '1.5rem', textAlign: 'center', gap: '1.7rem', display: 'flex', flexDirection: 'column' }}>
                  <Typography variant="h5">Add Wholesaler</Typography>
                  <TextField
                     required sx={{ width: "100%" }} id="name"
                     name='name'
                     label="Wholesaler"
                     variant="outlined"
                  // value={addInput.name}
                  // onChange={onInputChange}
                  />
                  <TextField
                     required sx={{ width: "100%" }} id="desc"
                     name='description'
                     label="Address"
                     variant="outlined"
                     // value={addInput.description}
                     // onChange={onInputChange}
                     multiline
                  />
                  <Button
                     fullWidth
                     type="submit"
                     variant='contained'
                  >
                     Submit
                  </Button>
               </form>
            </Box>
         </Modal>
      </>
   )
}

export default Wholesalers