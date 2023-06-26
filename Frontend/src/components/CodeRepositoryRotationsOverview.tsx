import { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import Box, { BoxProps } from '@mui/material/Box';
import React from 'react';
import { Container, Menu, MenuItem, Typography } from '@mui/material';
import BackendApi from '../services/Api';
import { useApplicationStore } from '../stores/Application';
import { guid } from '../Models/Common/Guid';

function CodeRepositoryRotationsOverview() {
    const columns = [
        // {
        //     field: 'codeRepositoryName',
        //     headerName: 'Code Repository Name',
        //     valueGetter: (value: any) => value.row.codeRepositoryId?.value && codeRepositories!.find((x: any) => x.id.value === value.row.codeRepositoryId.value)?.name,
        //     flex: 1,
        // },
        {
            field: 'environment',
            headerName: 'Environment',
            flex: 1,
        },
        {
            field: 'accessKeyIdVariableName',
            headerName: 'Access Key Id Variable Name',
            flex: 1,
        },
        {
            field: 'accessSecretVariableName',
            headerName: 'Access Secret Variable Name',
            flex: 1,
        },
    ];

    const [contextMenu, setContextMenu] = React.useState<{
        mouseX: number;
        mouseY: number;
    } | null>(null);
    const codeRepositoryRotations = useApplicationStore((state) => state.codeRepositoryRotations);

    const handleContextMenu = (event: React.MouseEvent) => {
        event.preventDefault();
        // setSelectedRow(Number(event.currentTarget.getAttribute('data-id')));
        setContextMenu(contextMenu === null ? { mouseX: event.clientX - 2, mouseY: event.clientY - 4 } : null);
    };

    const handleClose = () => {
        setContextMenu(null);
    };

    // const editItem = () => {
    //     console.log('edit selected:' + selectedAccount)
    //     handleClose();
    // };

    // const deleteItem = () => {
    //     console.log('delete selected:' + selectedAccount)
    //     handleClose();
    // };

    return (
        <Box sx={{ mt: 1 }}>
            {codeRepositoryRotations != undefined && (
                <>
                    <Typography variant="h6" gutterBottom>
                        Rotations
                    </Typography>
                    <Box>
                        <DataGrid
                            autoHeight
                            sx={{
                                '& .MuiDataGrid-columnHeader:focus-within, & .MuiDataGrid-cell:focus-within': {
                                    outline: 'none',
                                },
                                '& .MuiDataGrid-columnHeader:focus, & .MuiDataGrid-cell:focus': {
                                    outline: 'none',
                                },
                            }}
                            columns={columns}
                            rows={codeRepositoryRotations}
                            getRowId={(row) => row.id.value}
                            slotProps={{
                                row: {
                                    onContextMenu: handleContextMenu,
                                    style: { cursor: 'context-menu' },
                                },
                            }}
                        />
                        <Menu
                            open={contextMenu !== null}
                            onClose={handleClose}
                            anchorReference="anchorPosition"
                            anchorPosition={contextMenu !== null ? { top: contextMenu.mouseY, left: contextMenu.mouseX } : undefined}
                            slotProps={{
                                root: {
                                    onContextMenu: (e) => {
                                        e.preventDefault();
                                        handleClose();
                                    },
                                },
                            }}
                        >
                            <MenuItem>Edit</MenuItem>
                            <MenuItem>Delete</MenuItem>
                        </Menu>
                    </Box>
                </>
            )}
        </Box>
    );
}

export default CodeRepositoryRotationsOverview;
