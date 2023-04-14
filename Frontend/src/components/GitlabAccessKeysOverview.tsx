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
        field: 'tokenName',
        headerName: 'Name',
        flex: 1,
    },
    {
        field: 'token',
        headerName: 'Token',
        flex: 1,
    },
];
function GitlabAccessKeysOverview() {
    const [contextMenu, setContextMenu] = React.useState<{
        mouseX: number;
        mouseY: number;
    } | null>(null);

    const selectedAccessToken = useApplicationStore((state) => state.selectedAccessToken);
    const gitlabAccessTokens = useApplicationStore((state) => state.gitlabAccessTokens);
    const setSelectedAccessCode = useApplicationStore((state) => state.setSelectedAccessCode);
    const fetchGitlabAccessTokens = useApplicationStore((state) => state.fetchGitlabAccessTokens);

    useEffect(() => {
        fetchGitlabAccessTokens && fetchGitlabAccessTokens();
    }, [fetchGitlabAccessTokens]);

    const handleContextMenu = (event: React.MouseEvent) => {
        event.preventDefault();
        const accessTokenIdGuid = guid(event.currentTarget.getAttribute('data-id')?.toString() ?? "");
        const codeRepository = gitlabAccessTokens?.find((x) => x.id.value == accessTokenIdGuid);
        setSelectedAccessCode(codeRepository);
        setContextMenu(contextMenu === null ? { mouseX: event.clientX - 2, mouseY: event.clientY - 4 } : null);
    };

    const handleClose = () => {
        setContextMenu(null);
    };
    const editItem = () => {
        console.log('edit selected:' + selectedAccessToken);
        handleClose();
    };

    const deleteItem = () => {
        console.log('delete selected:' + selectedAccessToken);
        handleClose();
    };

    const onRowSelectionModelChange = (newSelection: any) => {
        if (newSelection.length == 0) {
            setSelectedAccessCode(undefined);
        } else {
            const accessTokenIdGuid = guid(newSelection[0] as string);
            const codeRepository = gitlabAccessTokens?.find((x) => x.id.value == accessTokenIdGuid);
            setSelectedAccessCode(codeRepository);
        }
    };

    return (
        <Box sx={{ mt: 1 }}>
            <Typography variant="h6" gutterBottom>
                Gitlab Access Tokens
            </Typography>
            {gitlabAccessTokens != undefined && (
                <Box>
                    <DataGrid
                        rowSelectionModel={selectedAccessToken ? [selectedAccessToken.id.value.toString()] : []}
                        autoHeight
                        sx={{
                            '&.MuiDataGrid-root .MuiDataGrid-cell:focus-within': {
                                outline: 'none !important',
                            },
                        }}
                        columns={columns}
                        rows={gitlabAccessTokens}
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

export default GitlabAccessKeysOverview;
