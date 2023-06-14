import { Toolbar, TableContainer, Table, TableBody, TableHead, TableRow, TableCell, Paper, Checkbox, Typography, Tooltip, IconButton, Fab,Modal,Box,TextField,Button, TablePagination } from '@mui/material'
import axios from 'axios';
import { React, useEffect, useState, useMemo } from 'react'
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
const Users = () => {
    const [users, setUsers] = useState([]);
    const [selectedRows, setSelectedRows] = useState([]);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [page, setPage] = useState(0);
    // const [visibleUsers,setVisibleUsers] = useState([]);

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
            const response = await axios.get("https://localhost:7122/api/User");
            setUsers(response.data);
        } catch (error) {
            console.log(error.response);
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    const visibleUsers = useMemo(
        () =>
            users.slice(
                page * rowsPerPage,
                page * rowsPerPage + rowsPerPage,
            ),
        [page, rowsPerPage, users],
    );

    const handleSelectAllClick = (event) => {
        if (event.target.checked) {
            setSelectedRows(users.map((user) => user.id));
        } else {
            setSelectedRows([]);
        }
    };
    const handleRowClick = (event, userId) => {
        const selectedIndex = selectedRows.indexOf(userId);
        let newSelectedRows = [];

        if (selectedIndex === -1) {
            newSelectedRows = newSelectedRows.concat(selectedRows, userId);
        } else if (selectedIndex === 0) {
            newSelectedRows = newSelectedRows.concat(selectedRows.slice(1));
        } else if (selectedIndex === selectedRows.length - 1) {
            newSelectedRows = newSelectedRows.concat(selectedRows.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelectedRows = newSelectedRows.concat(
                selectedRows.slice(0, selectedIndex),
                selectedRows.slice(selectedIndex + 1),
            );
        }

        setSelectedRows(newSelectedRows);
    };

    const handleDelete = async () => {
        await axios.post("https://localhost:7122/api/User/DeleteUsers", { Ids: selectedRows }, {
            headers: { "Content-Type": "application/json" },
        })
            .then(response => {
                fetchData()
                setSelectedRows([]);
            })
            .catch(error => {
                console.error(error);
            });
    }

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    return (
        <>
        <div>
            <Toolbar component={Paper} sx={{ bgcolor: 'primary.light', color: 'primary.contrastText' }}>
                {selectedRows.length > 0 ? (
                    <Typography
                        sx={{ flex: '1 1 100%' }}
                        color="inherit"
                        variant="subtitle1"
                        component="div"
                    >{selectedRows.length} selected
                    </Typography>
                ) : (
                    <Typography
                        sx={{ flex: '1 1 100%' }}
                        variant="h6"
                        id="tableTitle"
                        component="div"
                    >Users
                    </Typography>
                )}

                {selectedRows.length > 0 && (
                    <Tooltip title="Delete">
                        <IconButton onClick={handleDelete}>
                            <DeleteIcon sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>
                )}
            </Toolbar>
            <TableContainer component={Paper} style={{maxHeight: "70vh"}}>
                <Table sx={{ maxWidth: 650}} stickyHeader aria-label="user table" >
                    <TableHead>
                        <TableRow>
                            <TableCell padding="checkbox">
                                <Checkbox
                                    indeterminate={selectedRows.length > 0 && selectedRows.length < users.length}
                                    checked={selectedRows.length === users.length}
                                    onChange={handleSelectAllClick}
                                    inputProps={{ 'aria-label': 'select all users' }}
                                />
                            </TableCell>
                            <TableCell >Username</TableCell>
                            <TableCell align="right">FirstName</TableCell>
                            <TableCell align="right">LastName</TableCell>
                            <TableCell align="right">Created At</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {visibleUsers.map((row) => {
                            const isSelected = selectedRows.indexOf(row.id) !== -1;
                            return (
                                <TableRow
                                    key={row.id}
                                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                    selected={isSelected}
                                    onClick={(event) => handleRowClick(event, row.id)}
                                >
                                    <TableCell padding="checkbox">
                                        <Checkbox
                                            checked={isSelected}
                                            inputProps={{ 'aria-labelledby': `select user ${row.id}` }}
                                        />
                                    </TableCell>
                                    <TableCell component="th" scope="row">
                                        {row.username}
                                    </TableCell>
                                    <TableCell align="right">{row.firstName}</TableCell>
                                    <TableCell align="right">{row.lastName}</TableCell>
                                    <TableCell align="right">{row.created_at}</TableCell>
                                </TableRow>
                            );
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[5, 10, 20, 30]}
                // component="div"
                count={users.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPage}
                component={Paper}
                sx={{ bgcolor: 'primary.light', color: 'primary.contrastText' }}
            />
            <Fab sx={{
                position: 'absolute',
                bottom: 20,
                right: 20,
            }} color={"primary"}
            onClick={() => setOpenAddModal(true)}>
                <AddIcon />
            </Fab>
        </div>
        <Modal
        open={openAddModal}
        onClose={() => setOpenAddModal(false)}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
    >
        <Box sx={style}>
            <form style={{ margin: '1.5rem', textAlign: 'center', gap: '1.7rem', display: 'flex', flexDirection: 'column' }}>
                <Typography variant="h5">Add user</Typography>
                <TextField
                    required sx={{ width: "100%" }} id="name"
                    name='name'
                    label="FirstName"
                    variant="outlined"
                    // value={addInput.name}
                    // onChange={onInputChange}
                />
                <TextField
                    required sx={{ width: "100%" }} id="desc"
                    name='description'
                    label="LastName"
                    variant="outlined"
                    // value={addInput.description}
                    // onChange={onInputChange}
                    multiline
                />
                <TextField
                    required sx={{ width: "100%" }} id="price"
                    name='price'
                    label="Username"
                    variant="outlined"
                    // value={addInput.price}
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

export default Users