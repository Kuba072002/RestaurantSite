import { React, useState } from 'react'
// import { DataGrid } from '@mui/x-data-grid';
import { Box, Button, Typography, Card, Grid } from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import {
    Chart,
    CategoryScale,
    LinearScale,
    BarElement,
    // Title,
    // Tooltip,
    // Legend
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
import axios from 'axios';
import dayjs from 'dayjs';
// import Dishes from '../Dishes/Dishes';

Chart.register(
    CategoryScale,
    LinearScale,
    BarElement,
    // Title,
    // Tooltip,
    // Legend,
)

const Orders = () => {
    const [startDate, setStartDate] = useState(dayjs().subtract(5, 'day'));
    const [endDate, setEndDate] = useState(dayjs());
    const [data, setData] = useState(null);
    const [barData, setBarData] = useState({
        labels: [],
        datasets: [
            {
                label: 'Sales',
                data: [],
                backgroundColor: 'rgba(75, 192, 192, 0.6)',
            },
        ],
    });

    const options = {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
            },
        },
    };

    const updateBarData = (map) => {
        const updatedData = {
            ...barData,
            labels: Array.from(map.keys()),
            datasets: [
                {
                    ...barData.datasets[0],
                    data: Array.from(map.values()),
                },
            ],
        };
        setBarData(updatedData);
    };

    const fetchData = async () => {
        try {
            const formData = new FormData();
            formData.append('startDate', dayjs(startDate).format('YYYY-MM-DD'));
            formData.append('endDate', dayjs(endDate).format('YYYY-MM-DD'));
            // formData.append('startDate', '2023-06-03');
            // formData.append('endDate', '2023-06-06');
            const response = await axios.post("https://localhost:7122/api/Order/GetOrdersRaport", formData,
                { headers: { 'Content-Type': 'application/json', }, }
            );
            setData(response.data);
            updateBarData(new Map(Object.entries(response.data.dictionary)));
        } catch (error) {
            console.log(error.response);
        }
    }

    return (
        <Box
            sx={{
                // width: '100vw',
                // minHeight: '100vh',
                display: 'flex',
                flexDirection: 'row',
                height: '100%',
                // backgroundColor: '#424242',
            }}
        >
            <Box sx={{ p: 3, display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: "column", width: '100%' }}>
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
                <div>
                    <Card sx={{ width: '100%', height: "60vh", p: 3, display: 'flex', justifyContent: 'center' }}>
                        {/* <Card sx={{p:3}}> width: '100%'*/}
                        <Bar data={barData} options={options} style={{ maxWidth: '100%' }} />
                        {/* </Card> */}
                    </Card>
                    <Box sx={{ width: '100%', mt: 3 }}>
                        {/* display: 'flex', justifyContent: 'space-between', */}
                        {data &&
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={4}>
                                    <Card sx={{ p: 3 }}>
                                        <Typography variant='subtitle1' color="text.secondary">Number of orders</Typography>
                                        <Typography variant='h4' sx={{ textAlign: "right" }}>{data.numberOfOrder}</Typography>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={4}>
                                    <Card sx={{ p: 3 }}>
                                        <Typography variant='subtitle1' color="text.secondary">Number of distinct users</Typography>
                                        <Typography variant='h4' sx={{ textAlign: "right" }}>{data.numberOfDistinctUsers}</Typography>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} sm={4}>
                                    <Card sx={{ p: 3 }}>
                                        <Typography variant='subtitle1' color="text.secondary">Average number of orders for user</Typography>
                                        <Typography variant='h4' sx={{ textAlign: "right" }}>{data.averageNumberOfOrdersForUser}</Typography>
                                    </Card>
                                </Grid>
                            </Grid>
                        }
                    </Box>
                </div>
            </Box>
            {/* <div>
            <Dishes />
            </div> */}
        </Box>
    )
}

export default Orders