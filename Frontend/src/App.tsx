import './App.css';
import Box, { BoxProps } from '@mui/material/Box';
import AwsAccountOverview from './components/AwsAccountOverview';
import { useApplicationStore } from './stores/Application';
import IamIdentityOverview from './components/IAMIdentityOverview';
import CodeRepositoryRotationsOverview from './components/CodeRepositoryRotationsOverview';
import CodeRepositoryOverview from './components/CodeRepositoryOverview';
import GitlabAccessKeysOverview from './components/GitlabAccessKeysOverview';
import Authentication from './components/Authentication';
import { Tab, Tabs } from '@mui/material';
import React from 'react';
import IAMRotationsOverview from './components/IAMRotationsOverview';

function App() {
    const [selectedTab, setSelectedTab] = React.useState('Gitlab');
    const selectedAccount = useApplicationStore((state) => state.selectedAccount);
    const changeSelectedTab = (event: React.SyntheticEvent, newValue: string) => {
        setSelectedTab(newValue);
    };
    return (
        <Box display={'flex'} justifyContent={'center'} sx={{ mt: 1 }}>
            <Box sx={{ width: '100%', maxWidth: 'md' }}>
                <Authentication>
                    <Tabs value={selectedTab} onChange={changeSelectedTab} aria-label="basic tabs example">
                        <Tab label="Gitlab" value="Gitlab" />
                        <Tab label="AWS" value="AWS"></Tab>
                    </Tabs>
                    {selectedTab === 'Gitlab' ? (
                        <>
                            <GitlabAccessKeysOverview></GitlabAccessKeysOverview>
                            <CodeRepositoryOverview></CodeRepositoryOverview>
                            <CodeRepositoryRotationsOverview></CodeRepositoryRotationsOverview>
                        </>
                    ) : (
                        <>
                            <AwsAccountOverview></AwsAccountOverview>
                            <IamIdentityOverview></IamIdentityOverview>
                            <IAMRotationsOverview></IAMRotationsOverview>
                        </>
                    )}
                </Authentication>
            </Box>
        </Box>
    );
}

export default App;
