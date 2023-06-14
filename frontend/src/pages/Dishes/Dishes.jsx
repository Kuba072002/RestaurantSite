import { React, useState, useEffect, Fragment } from 'react'
import { DataGrid } from '@mui/x-data-grid';
import axios from 'axios';
import { Box, Card, Typography, Button, List, ListItem, ListItemText, ListSubheader, Fab, TextField, FormControl, InputLabel, Select, MenuItem, Divider } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import Dropzone from 'react-dropzone';

const Dishes = () => {
   const [data, setData] = useState([]);
   const [components, setComponents] = useState([]);
   const [dish, setDish] = useState(null);
   const [mode, setMode] = useState('data');

   const [name, setName] = useState('');
   const [price, setPrice] = useState('');
   const [desc, setDesc] = useState('');
   const [selectedComponents, setSelectedComponents] = useState([]);
   // const [dictionary, setDictionary] = useState();

   const [picture, setPicture] = useState('');

   const fetchDishes = async () => {
      try {
         const response = await axios.get("https://localhost:7122/api/Dish");
         setData(response.data);
      } catch (error) {
         console.log(error.response);
      }
   }

   const fetchComponents = async () => {
      try {
         const response = await axios.get("https://localhost:7122/api/Component");
         setComponents(Array.from(response.data));
      } catch (error) {
         console.log(error.response);
      }
   }
   useEffect(() => {
      fetchDishes();
      fetchComponents();
   }, [])

   const fetchDish = async (newRowSelectionModel) => {
      try {
         const response = await axios.get(`https://localhost:7122/api/Dish/${newRowSelectionModel}`);
         setDish(response.data);
      } catch (error) {
         console.log(error.response);
      }
   }

   const columns = [
      { field: 'id', headerName: 'id' },
      { field: 'name', headerName: 'Name', width: 180, },
      { field: 'price', headerName: 'Price', type: 'number' },
      // { field: 'description', headerName: 'description' },
      { field: 'created_at', headerName: 'Created at', align: 'center', headerAlign: 'center' },
   ];

   const handleChange = (event) => {
      const {
         target: { value },
      } = event;
      setSelectedComponents(
         // On autofill we get a stringified value.
         typeof value === 'string' ? value.split(',') : value,
      );
   };


   return (
      <Box
         sx={{
            width: '100%',
            height: '100%',
         }}
      >
         <Box sx={{ p: 3, display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: "column" }}>
            {mode === 'data' &&
               <Card sx={{ p: 3 }}>
                  <DataGrid rows={data} columns={columns}
                     onRowSelectionModelChange={(newRowSelectionModel) => {
                        // setSelectedDish(newRowSelectionModel)
                        fetchDish(newRowSelectionModel)
                        setMode("details")
                     }}
                  // rowSelectionModel={selectedDish}
                  />
               </Card>
            }
            {dish !== null && mode === 'details' &&
               <Card sx={{ p: 3 }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                     <Typography variant='h4'>{dish.name}</Typography>
                     <Button onClick={(e) => setMode('data')}>Back</Button>
                  </div>
                  <Typography variant='subtitle1'>{dish.price} $</Typography>
                  <Typography variant='subtitle1'>{dish.description}</Typography>
                  <Typography variant='subtitle1'>{dish.created_at}</Typography>
                  {/* <Typography variant='subtitle1'>Components</Typography> */}
                  <List
                     sx={{ width: '100%', border: '1px solid #ccc', borderRadius: 5, p: 1, mt: 2 }}
                     subheader={
                        <ListSubheader component="div" id="nested-list-subheader">
                           Components
                        </ListSubheader>

                     }>
                     <Divider />
                     {Object.entries(dish.dishComponents).map(([component, quantity], index) => (
                        <Fragment key={component}>
                           <ListItem>
                              <ListItemText primary={component} secondary={`Quantity: ${quantity}`} />
                           </ListItem>
                           {index !== Object.entries(dish.dishComponents).length - 1 && <Divider />}
                        </Fragment>
                     ))}
                  </List>
               </Card>
            }
            {mode === 'add' &&
               <Card sx={{ p: 3, display: 'flex', gap: 2, flexDirection: 'column', width: '100%', maxWidth: '500px' }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                     <Typography variant='h4'>Add dish</Typography>
                     <Button onClick={(e) => setMode('data')}>Back</Button>
                  </div>
                  <TextField
                     required sx={{ width: "100%" }} id="name"
                     name='name'
                     label="Name"
                     variant="outlined"
                     value={name}
                     onChange={(e) => setName(e.target.value)}
                  />
                  <TextField
                     required sx={{ width: "100%" }} id="price"
                     name='price'
                     label="Price"
                     variant="outlined"
                     type='number'
                     value={price}
                     onChange={(e) => setPrice(e.target.value)}
                  />
                  <TextField
                     required sx={{ width: "100%" }} id="description"
                     name='description'
                     label="Description"
                     variant="outlined"
                     multiline
                     value={desc}
                     onChange={(e) => setDesc(e.target.value)}
                  />

                  <FormControl>
                     <InputLabel id="components-label">Components</InputLabel>
                     <Select
                        labelId="components-label"
                        id="components-label"
                        multiple
                        value={selectedComponents}
                        onChange={handleChange}
                        label="Components"
                     >
                        {components.map((component) => (
                           <MenuItem
                              key={component.id}
                              value={component.id}
                           >
                              {component.name}
                           </MenuItem>
                        ))}
                     </Select>
                  </FormControl>

                  {selectedComponents.map((value) => {
                     const c = components.find(component => component.id === value)
                     // console.log(c)
                     return (
                        <Card key={c.id} sx={{ p: 1.5, textAlign: 'flex-start' }}>

                           <Typography variant='subtitle1'>{c.name}</Typography>
                           <TextField
                              required sx={{ width: "100%" }} id="quantity"
                              name='quantity'
                              label={"Quantity (" + c.unit + ")"}
                              variant="standard"
                              size='small'
                           // value={desc}
                           // onChange={(e) => setDesc(e.target.value)}
                           />
                        </Card>)
                  })}
                  <Box
                     gridColumn="span 4"
                     border={`1px solid #c4c4c4`}
                     borderRadius="5px"
                     p="1rem"
                  >
                     <Dropzone
                        acceptedFiles=".jpg,.jpeg,.png"
                        multiple={false}
                        onDrop={(acceptedFiles) =>
                           setPicture(acceptedFiles[0])
                        }
                     >
                        {({ getRootProps, getInputProps }) => (
                           <Box
                              {...getRootProps()}
                              border={`2px dashed #c4c4c4`}
                              p="1rem"
                              sx={{ "&:hover": { cursor: "pointer" } }}
                           >
                              <input {...getInputProps()} />
                              {!picture ? (
                                 <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                    <Typography variant='subtitle1'>Add picture</Typography>
                                    <AddIcon />
                                 </div>
                              ) : (
                                 <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                    <Typography variant='subtitle1'>{picture.name}</Typography>
                                    <EditIcon />
                                 </div>
                              )}
                           </Box>
                        )}
                     </Dropzone>
                  </Box>

                  <Button
                     fullWidth
                     type="submit"
                     variant='contained'
                  >
                     Submit
                  </Button>
               </Card>
            }
            <Fab sx={{
               position: 'absolute',
               bottom: 20,
               right: 20,
            }} color={"primary"} onClick={(e) => setMode('add')}>
               <AddIcon />
            </Fab>
         </Box>
      </Box>
   )
}

export default Dishes