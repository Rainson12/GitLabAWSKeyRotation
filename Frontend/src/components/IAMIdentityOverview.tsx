import { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import Box, { BoxProps } from '@mui/material/Box';
import React from 'react';
import { Container, Menu, MenuItem, Typography } from '@mui/material';
import BackendApi from '../services/Api';
import { useApplicationStore } from '../stores/application';
import { guid } from '../Models/Common/Guid';

const columns = [
    {
        field: 'name',
        headerName: 'Name',
        flex: 1,
    },
    {
        field: 'accessKeyId',
        headerName: 'Access Key Id',
        flex: 1,
    },
];
function IamIdentityOverview() {
    const [contextMenu, setContextMenu] = React.useState<{
        mouseX: number;
        mouseY: number;
    } | null>(null);
    const setSelectedIam = useApplicationStore((state) => state.setSelectedIAM);
    const selectedIam = useApplicationStore((state) => state.selectedIAM);
    const selectedAccount = useApplicationStore((state) => state.selectedAccount);

    const handleContextMenu = (event: React.MouseEvent) => {
        event.preventDefault();
        const iamIdGuid = guid(event.currentTarget.getAttribute('data-id')?.toString() ?? '');
        const iam = selectedAccount.iamIdentities.find((i: any) => i.id.value == iamIdGuid);
        setSelectedIam(iam);
        setContextMenu(contextMenu === null ? { mouseX: event.clientX - 2, mouseY: event.clientY - 4 } : null);
    };

    const handleClose = () => {
        setContextMenu(null);
    };

    const editItem = () => {
        console.log('edit selected:' + selectedIam);
        handleClose();
    };

    const deleteItem = () => {
        console.log('delete selected:' + selectedIam);
        handleClose();
    };

    const onRowSelectionModelChange = (newSelection: any) => {
        if (newSelection.length == 0) {
            setSelectedIam(undefined);
        } else {
            const iamIdGuid = guid(newSelection[0] as string);
            const iam = selectedAccount.iamIdentities.find((i: any) => i.id.value == iamIdGuid);
            setSelectedIam(iam);
        }
    };

    return (
        <Box sx={{ mt: 1 }}>
            {selectedAccount != undefined && (
                <>
                    <Typography variant="h6" gutterBottom>
                        IAM Identities
                    </Typography>
                    <Box>
                        <DataGrid
                            rowSelectionModel={selectedIam ? [selectedIam.id.value.toString()] : []}
                            autoHeight
                            sx={{
                                '&.MuiDataGrid-root .MuiDataGrid-cell:focus-within': {
                                    outline: 'none !important',
                                },
                            }}
                            columns={columns}
                            rows={selectedAccount.iamIdentities}
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

export default IamIdentityOverview;
