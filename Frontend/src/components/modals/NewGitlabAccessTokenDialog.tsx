import { Button, Checkbox, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControlLabel, TextField } from '@mui/material';
import axios from 'axios';
import React, { useState } from 'react';
import BackendApi from '../../services/Api';

export interface NewGitlabAccessTokenDialogProps {
    open: boolean;
    handleClose: () => void;
}

export function NewGitlabAccessTokenDialog(props: NewGitlabAccessTokenDialogProps) {
    const accessTokenInput = React.createRef<HTMLInputElement>();
    const nameInput = React.createRef<HTMLInputElement>();
    const scanInput = React.createRef<HTMLInputElement>();
    const [error, setError] = useState('');

    const handleSave = async () => {
        if (
            accessTokenInput.current?.value !== undefined &&
            accessTokenInput.current?.value !== '' &&
            nameInput.current?.value !== undefined &&
            nameInput.current?.value !== '' &&
            scanInput.current?.value !== undefined &&
            scanInput.current?.value !== ''
        ) {
            try {
                await BackendApi.AddGitlabAccessToken(nameInput.current.value, accessTokenInput.current.value, scanInput.current.checked);
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    setError(error.response?.data.title ?? error.message);
                } else {
                    setError(JSON.stringify(error));
                }
            }
        } else {
            setError('Password is required');
        }
    };
    return (
        <Dialog open={props.open} onClose={props.handleClose}>
            <DialogTitle>Add Gitlab Access Token</DialogTitle>
            <DialogContent>
                <DialogContentText>Enter the gitlab access token. The access token should have permission to access and change variables of the projects.</DialogContentText>
                <TextField autoFocus margin="dense" id="name" label="Name" inputRef={nameInput} type="text" fullWidth variant="standard" />
                <TextField margin="dense" id="accessToken" label="Access Token" inputRef={accessTokenInput} type="text" fullWidth variant="standard" error={Boolean(error)} helperText={error} />
                <FormControlLabel control={<Checkbox inputRef={scanInput} />} label="Scan for AWS connected Repositories?" />
            </DialogContent>
            <DialogActions>
                <Button onClick={props.handleClose}>Cancel</Button>
                <Button onClick={handleSave}>Save</Button>
            </DialogActions>
        </Dialog>
    );
}
