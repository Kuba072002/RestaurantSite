import { Box, Tabs, Tab } from '@mui/material'
import { React, useState } from 'react'
import Users from './Users';
import Dishes from './Dishes';
import Wholesalers from './Wholesalers';
const MainPage = () => {
  const [value, setValue] = useState(0);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  }
  // '#121212''#424242'
  return (
    <Box
      sx={{
        // width: '100vw',
        // minHeight: '100vh',
        height: '100%',
        // backgroundColor: '#424242',
      }}
    >
      <Box sx={{ p: 3}}>
        <Tabs value={value} onChange={handleChange} centered textColor="primary" indicatorColor="primary">
          <Tab label="Users" 
          // sx={{ color: 'primary.contrastText' }}
          />
          <Tab label="Dishes" 
          // sx={{ color: 'primary.contrastText' }}
          />
          <Tab label="Wholesalers" 
          // sx={{ color: 'primary.contrastText' }}
          />
        </Tabs>
      </Box>
      <Box sx={{p:3, display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
        {value === 0 && (<div>
          <Users />
        </div>)
        }
        {value === 1 && (<div>
          <Dishes />
        </div>)
        }
        {value === 2 && (<div>
          <Wholesalers />
        </div>)
        }
      </Box>
    </Box>

  );
}

export default MainPage