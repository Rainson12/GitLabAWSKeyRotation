import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './index.css';
import { CssBaseline } from '@mui/material';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <>
        <CssBaseline />
        <App />
    </>
);
