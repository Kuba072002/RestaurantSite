import { React, useEffect, useState, useMemo } from 'react'
import { Toolbar, TableContainer, Table, TableBody, TableHead, TableRow, TableCell, Paper, Checkbox, Typography, Tooltip, IconButton, TablePagination } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete';

const TableComponent = ({ name, fetchData, handleDelete, tableHead, properties }) => {
    const [rows, setRows] = useState([]);
    const [selectedRows, setSelectedRows] = useState([]);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [page, setPage] = useState(0);

    // useEffect(() => {
    //     setRows(fetchData());
    // },[fetchData]);

    useEffect(() => {
        const fetchRows = async () => {
          try {
            const data = await fetchData();
            setRows(data);
          } catch (error) {
            console.log(error);
          }
        };
      
        fetchRows();
      }, [fetchData]);

    const visibleRows = useMemo(
        () =>
            rows.slice(
                page * rowsPerPage,
                page * rowsPerPage + rowsPerPage,
            ),
        [page, rowsPerPage, rows],
    );

    const handleSelectAllClick = (event) => {
        if (event.target.checked) {
            setSelectedRows(rows.map((row) => row.id));
        } else {
            setSelectedRows([]);
        }
    };

    const handleRowClick = (event, rowId) => {
        const selectedIndex = selectedRows.indexOf(rowId);
        let newSelectedRows = [];

        if (selectedIndex === -1) {
            newSelectedRows = newSelectedRows.concat(selectedRows, rowId);
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

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    return (
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
                    >{name}
                    </Typography>
                )}

                {selectedRows.length > 0 && (
                    <Tooltip title="Delete">
                        <IconButton onClick={() =>{setRows(handleDelete(selectedRows)); setSelectedRows([]);}}>
                            <DeleteIcon sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>
                )}
            </Toolbar>
            <TableContainer component={Paper} style={{maxHeight: "70vh"}}>
                <Table sx={{ maxWidth: 650}} stickyHeader aria-label={`${name} table`} >
                    <TableHead>
                        <TableRow>
                            <TableCell padding="checkbox">
                                <Checkbox
                                    indeterminate={selectedRows.length > 0 && selectedRows.length < rows.length}
                                    checked={selectedRows.length === rows.length}
                                    onChange={handleSelectAllClick}
                                    inputProps={{ 'aria-label': 'select all rows' }}
                                />
                            </TableCell>
                            {tableHead.map((head,idx) => {
                                if(idx === 0)
                                    return <TableCell sx={{ textTransform: 'capitalize' }} key={head}>{head}</TableCell>
                                else
                                    return <TableCell sx={{ textTransform: 'capitalize' }} align="right" key={head}>{head}</TableCell>
                            })
                            }
                            {/* <TableCell >Username</TableCell>
                            <TableCell align="right">FirstName</TableCell>
                            <TableCell align="right">LastName</TableCell>
                            <TableCell align="right">Created At</TableCell> */}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {visibleRows.map((row) => {
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
                                            inputProps={{ 'aria-labelledby': `select row ${row.id}` }}
                                        />
                                    </TableCell>
                                    {properties.map((property,idx) => {
                                        if(idx === 0)
                                            return <TableCell component="th" scope="row" key={property}>{row[property]}</TableCell>
                                         else
                                            return <TableCell align="right" key={property}>{row[property]}</TableCell>
                                    })}
                                    {/* <TableCell component="th" scope="row">
                                        {row.username}
                                    </TableCell>
                                    <TableCell align="right">{row.firstName}</TableCell>
                                    <TableCell align="right">{row.lastName}</TableCell>
                                    <TableCell align="right">{row.created_at}</TableCell> */}
                                </TableRow>
                            );
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[5, 10, 20, 30]}
                count={rows.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPage}
                component={Paper}
                sx={{ bgcolor: 'primary.light', color: 'primary.contrastText' }}
            />
            
        </div>
    )
}

export default TableComponent