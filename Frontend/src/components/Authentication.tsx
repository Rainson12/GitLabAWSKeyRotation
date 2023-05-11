import { useState } from 'react';
import { Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material';
import { useApplicationStore } from '../stores/Application';
import React from 'react';
import axios from 'axios';

/* React function component that opens a modal dialog if the user is
    not authenticated. In the modal dialog, the user can authenticate with
   the application by entering a password. The password will then be validated
   by the backend using a rest query. If the password is valid, the user will receive
   an authentication token that will be stored in the state management and the modal dialog will be closed.
   The components used should be from material-ui. 
*/

function Authenticate(props: React.PropsWithChildren<{}>) {
    const setupPasswordInput = React.createRef<HTMLInputElement>();
    const passwordInput = React.createRef<HTMLInputElement>();
    const [error, setError] = useState('');
    const authToken = useApplicationStore((state) => state.authToken);
    const isAuthConfigured = useApplicationStore((state) => state.isAuthConfigured);
    const fetchIsAuthConfigured = useApplicationStore((state) => state.fetchIsAuthConfigured);
    const login = useApplicationStore((state) => state.login);
    const setupAuth = useApplicationStore((state) => state.setupAuth);

    if (isAuthConfigured === undefined) {
        fetchIsAuthConfigured();
        return <></>;
    }

    const handleAuthenticate = async () => {
        if (passwordInput.current?.value !== undefined && passwordInput.current?.value !== '') {
            try {
                await login(passwordInput.current?.value);
            } catch (error) {
              if (axios.isAxiosError(error))  {
                setError(error.response?.data.title ?? error.message);
              } else {
                setError(JSON.stringify(error));
              }
            }
        } else {
            setError('Password is required');
        }
    };

    const handleSetup = async () => {
        if (setupPasswordInput.current?.value !== undefined && setupPasswordInput.current?.value !== '') {
            try {
              await setupAuth(setupPasswordInput.current?.value);
          } catch (error) {
            if (axios.isAxiosError(error))  {
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
        <>
            {isAuthConfigured === false ? (
                <Dialog open={true}>
                    <DialogTitle>Setup Authentication</DialogTitle>
                    <DialogContent>
                        <TextField label="Password" autoFocus fullWidth variant="standard" margin="dense" type="password" inputRef={setupPasswordInput} error={Boolean(error)} helperText={error} />
                    </DialogContent>
                    <DialogActions>
                        <Button variant="contained" color="primary" onClick={handleSetup}>
                            Authenticate
                        </Button>
                    </DialogActions>
                </Dialog>
            ) : authToken !== undefined && authToken !== '' ? (
                props.children
            ) : (
                <Dialog open={true}>
                    <DialogTitle>Authenticate</DialogTitle>
                    <DialogContent>
                        <TextField label="Password" autoFocus fullWidth variant="standard" margin="dense" type="password" inputRef={passwordInput} error={Boolean(error)} helperText={error} />
                    </DialogContent>
                    <DialogActions>
                        <Button variant="contained" color="primary" onClick={handleAuthenticate}>
                            Authenticate
                        </Button>
                    </DialogActions>
                </Dialog>
            )}
        </>
    );
}
export default Authenticate;
