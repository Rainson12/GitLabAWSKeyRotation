import './App.css';
import Box, { BoxProps } from '@mui/material/Box';
import AwsAccountOverview from './components/AwsAccountOverview';
import { useApplicationStore } from './stores/application';
import IamIdentityOverview from './components/IAMIdentityOverview';
import RotationsOverview from './components/RotationsOverview';
import CodeRepositoryOverview from './components/CodeRepositoryOverview';
import GitlabAccessKeysOverview from './components/GitlabAccessKeysOverview';
import Authentication from './components/Authentication';

function App() {
    const selectedAccount = useApplicationStore((state) => state.selectedAccount);

    return (
        <Box display={'flex'} justifyContent={'center'} sx={{ mt: 1 }}>
            <Box sx={{ width: '100%', maxWidth: 'md' }}>
                <Authentication>
                    <GitlabAccessKeysOverview></GitlabAccessKeysOverview>
                    <CodeRepositoryOverview></CodeRepositoryOverview>
                    <AwsAccountOverview></AwsAccountOverview>
                    <IamIdentityOverview></IamIdentityOverview>
                    <RotationsOverview></RotationsOverview>
                </Authentication>
            </Box>
        </Box>
    );
}

export default App;
