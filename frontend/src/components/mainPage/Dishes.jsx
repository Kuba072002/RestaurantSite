import React from 'react'
import TableComponent from '../Table/TableComponent';
import axios from 'axios';
import AddIcon from '@mui/icons-material/Add';
import { Fab, Modal, Box, TextField, Typography, Button } from '@mui/material';

const Dishes = () => {
    const [openAddModal, setOpenAddModal] = React.useState(false);
    const [addInput, setAddInput] = React.useState({
        name:'',
        description:'',
        price: 0.0
    });

    const onInputChange = e => {
        const { name, value } = e.target;
        setAddInput(inputs => ({
          ...inputs,
          [name]: value
        }));
    };

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
            const response = await axios.get("https://localhost:7122/api/Dish");
            return response.data;
        } catch (error) {
            console.log(error.response);
        }
    }

    const handleDelete = async (selectedRows) => {
        try {
            await axios.post(
                "https://localhost:7122/api/Dish/DeleteDishes",
                { Ids: selectedRows },
                {
                    headers: { "Content-Type": "application/json" },
                }
            );
            return fetchData();
        } catch (error) {
            console.error(error);
        }
    }

    const handleAdd = async () => {
        try{
            await axios.post(
                "https://localhost:7122/api/Dish/AddDish",{ 
                    name: addInput.name,
                    description: addInput.description,
                    price: addInput.price 
                },{
                    headers: { "Content-Type": "application/json" },
                }
            ).then(response => 
                {
                    //if(response.status === 200)
                        //fetchData(); 
                });
        }catch(error){
            console.error(error);
        }
    }

    return (
        <>
            <TableComponent name="Dishes"
                fetchData={fetchData} handleDelete={handleDelete}
                tableHead={['Name', 'description', 'price', 'created at']}
                properties={['name', 'description', 'price', 'created_at']}
            />
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
                    <form onSubmit={handleAdd} style={{ margin: '1.5rem', textAlign: 'center', gap: '1.7rem', display: 'flex', flexDirection: 'column' }}>
                        <Typography variant="h5">Add dish</Typography>
                        <TextField
                            required sx={{ width: "100%" }} id="name"
                            name='name'
                            label="Name"
                            variant="outlined"
                            value={addInput.name}
                            onChange={onInputChange}
                        />
                        <TextField
                            required sx={{ width: "100%" }} id="desc"
                            name='description'
                            label="Description"
                            variant="outlined"
                            value={addInput.description}
                            onChange={onInputChange}
                            multiline
                        />
                        <TextField
                            required sx={{ width: "100%" }} id="price"
                            name='price'
                            label="Price"
                            variant="outlined"
                            value={addInput.price}
                            onChange={onInputChange}
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

export default Dishes