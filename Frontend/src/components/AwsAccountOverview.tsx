import { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import Box, { BoxProps } from '@mui/material/Box';
import React from 'react';
import { Container, Menu, MenuItem, Typography } from '@mui/material';
import BackendApi from '../services/Api';
import { guid } from '../Models/Common/Guid';
import { useApplicationStore } from '../stores/application';

const columns = [
    {
        field: 'displayName',
        headerName: 'Name',
        flex: 1,
    },
    {
        field: 'awsAccountId',
        headerName: 'Account Id',
        flex: 1,
    },
];
function AwsAccountOverview() {
    const [contextMenu, setContextMenu] = React.useState<{
        mouseX: number;
        mouseY: number;
    } | null>(null);
    const setSelectedAccount = useApplicationStore((state) => state.setSelectedAccount);
    const selectedAccount = useApplicationStore((state) => state.selectedAccount);
    const accounts = useApplicationStore((state) => state.accounts);
    const fetchAccounts = useApplicationStore((state) => state.fetchAccounts);

    useEffect(() => {
        fetchAccounts && fetchAccounts();
    }, [fetchAccounts]);

    const handleContextMenu = (event: React.MouseEvent) => {
        event.preventDefault();
        const accountIdGuid = guid(event.currentTarget.getAttribute('data-id')?.toString() ?? "");
        const account = accounts?.find((x) => x.id.value == accountIdGuid);
        setSelectedAccount(account);
        setContextMenu(contextMenu === null ? { mouseX: event.clientX - 2, mouseY: event.clientY - 4 } : null);
    };

    const handleClose = () => {
        setContextMenu(null);
    };

    const editItem = () => {
        console.log('edit selected:' + selectedAccount)
        handleClose();
    };

    const deleteItem = () => {
        console.log('delete selected:' + selectedAccount)
        handleClose();
    };

    const onRowSelectionModelChange = (newSelection: any) => {
        if (newSelection.length == 0) {
            setSelectedAccount(undefined);
        } else {
            const accountIdGuid = guid(newSelection[0] as string);
            const account = accounts?.find((x) => x.id.value == accountIdGuid);
            setSelectedAccount(account);
        }
    };

    return (
        <Box sx={{ mt: 1 }}>
            <Typography variant="h6" gutterBottom>
                Accounts
            </Typography>
            {accounts != undefined && (
                <Box>
                    <DataGrid
                        rowSelectionModel={selectedAccount ? [selectedAccount.id.value.toString()] : []}
                        autoHeight
                        sx={{
                            '&.MuiDataGrid-root .MuiDataGrid-cell:focus-within': {
                                outline: 'none !important',
                            },
                        }}
                        columns={columns}
                        rows={accounts}
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
            )}
        </Box>
    );
}

export default AwsAccountOverview;
