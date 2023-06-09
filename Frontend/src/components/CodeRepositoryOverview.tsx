import { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import Box, { BoxProps } from '@mui/material/Box';
import React from 'react';
import { Container, Menu, MenuItem, Typography } from '@mui/material';
import BackendApi from '../services/Api';
import { guid } from '../Models/Common/Guid';
import { useApplicationStore } from '../stores/Application';

const columns = [
    {
        field: 'name',
        headerName: 'Name',
        flex: 1,
    },
    {
        field: 'url',
        headerName: 'Url',
        flex: 1,
    },
];
function CodeRepositoryOverview() {
    const [contextMenu, setContextMenu] = React.useState<{
        mouseX: number;
        mouseY: number;
    } | null>(null);

    const codeRepositories = useApplicationStore((state) => state.codeRepositories);
    const selectedCodeRepository = useApplicationStore((state) => state.selectedCodeRepository);
    const setCodeRepository = useApplicationStore((state) => state.setSelectedCodeRepository);

    const handleContextMenu = (event: React.MouseEvent) => {
        event.preventDefault();
        const codeRepositoryIdGuid = guid(event.currentTarget.getAttribute('data-id')?.toString() ?? '');
        const codeRepository = codeRepositories?.find((x: any) => x.id.value == codeRepositoryIdGuid);
        setCodeRepository(codeRepository);
        setContextMenu(contextMenu === null ? { mouseX: event.clientX - 2, mouseY: event.clientY - 4 } : null);
    };

    const handleClose = () => {
        setContextMenu(null);
    };

    const editItem = () => {
        console.log('edit selected:' + selectedCodeRepository);
        handleClose();
    };

    const deleteItem = () => {
        console.log('delete selected:' + selectedCodeRepository);
        handleClose();
    };

    const onRowSelectionModelChange = (newSelection: any) => {
        if (newSelection.length == 0) {
            setCodeRepository(undefined);
        } else {
            const codeRepositoryIdGuid = guid(newSelection[0] as string);
            const codeRepository = codeRepositories?.find((x: any) => x.id.value == codeRepositoryIdGuid);
            setCodeRepository(codeRepository);
        }
    };

    return (
        <Box sx={{ mt: 1 }}>
            {codeRepositories != undefined && (
                <>
                    <Typography variant="h6" gutterBottom>
                        Code Repositories
                    </Typography>
                    <Box>
                        <DataGrid
                            rowSelectionModel={selectedCodeRepository ? [selectedCodeRepository.id.value.toString()] : []}
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
                            rows={codeRepositories}
                            onRowSelectionModelChange={onRowSelectionModelChange}
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
                            <MenuItem onClick={editItem}>Edit</MenuItem>
                            <MenuItem onClick={deleteItem}>Delete</MenuItem>
                        </Menu>
                    </Box>
                </>
            )}
        </Box>
    );
}

export default CodeRepositoryOverview;
