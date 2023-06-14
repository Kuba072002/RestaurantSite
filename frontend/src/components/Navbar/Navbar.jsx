import MenuIcon from '@mui/icons-material/Menu';
import { React, useState } from 'react'
import { Link } from "react-router-dom";
import { Drawer, AppBar, Toolbar, Typography, IconButton, Box, ListItem, ListItemText, List } from '@mui/material';
const Navbar = () => {
   const [open, setOpen] = useState(false);
   const toggleDrawer = () => {
      setOpen(!open);
   };
   const closeDrawer = () => {
      setOpen(false);
   };
   
   return (
      <>
         <AppBar position="static">
            <Toolbar>
               <IconButton
                  size="large"
                  edge="start"
                  color="inherit"
                  aria-label="menu"
                  sx={{ mr: 2 }}
                  onClick={toggleDrawer}
               >
                  <MenuIcon />
               </IconButton>
               <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                  App
               </Typography>
               {/* <Button color="inherit">Login</Button> */}
            </Toolbar>
         </AppBar>

         <Drawer anchor="left" open={open} onClose={toggleDrawer}>
            <Box sx={{ width: 250 }} onClick={closeDrawer}>
               <List>
                  <ListItem button component={Link} to="/">
                     <ListItemText primary="Home" />
                  </ListItem>
                  <ListItem button component={Link} to="/orders">
                     <ListItemText primary="Orders" />
                  </ListItem>
                  <ListItem button component={Link} to="/wholesalers">
                     <ListItemText primary="Wholesalers" />
                  </ListItem>
                  <ListItem button component={Link} to="/components">
                     <ListItemText primary="Components" />
                  </ListItem>
                  <ListItem button component={Link} to="/users">
                     <ListItemText primary="Users" />
                  </ListItem>
                  <ListItem button component={Link} to="/dishes">
                     <ListItemText primary="Dishes" />
                  </ListItem>
               </List>
            </Box>
         </Drawer>
      </>
   )
}

export default Navbar