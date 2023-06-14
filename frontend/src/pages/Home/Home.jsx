import React from 'react'
import photo from '../../assets/photoHome.jpg'
import { Box,Typography } from '@mui/material'

const Home = () => {
  return (
    <div style={{width: '100vw',height:'100vh', backgroundImage: `url(${photo})`,filter: 'brightness(65%)',backgroundRepeat:"no-repeat",backgroundSize:"contain"}}>
      <Box>
        <Typography>Esz</Typography>
      </Box>
    </div>
  )
}

export default Home