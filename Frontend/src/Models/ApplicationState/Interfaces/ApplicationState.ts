import { Guid } from '../../Common/Guid';

export interface ApplicationState {
    isAuthConfigured: boolean | undefined;
    authToken: string | undefined;
    selectedAccount: any;
    selectedIAM: any;
    selectedAccessToken: any;
    selectedCodeRepository: any;
    setSelectedAccount: (account: any) => void;
    setSelectedIAM: (iam: any) => void;
    setSelectedAccessCode: (accessToken: any) => void;
    setSelectedCodeRepository: (codeRepo: any) => void;
    gitlabAccessTokens: Array<any> | undefined;
    fetchGitlabAccessTokens: () => Promise<void>;
    codeRepositories: Array<any> | undefined;
    fetchCodeRepositories: (accessTokenId: Guid) => Promise<void>;
    codeRepositoryRotations: Array<any> | undefined;
    fetchCodeRepositoryRotations: (accessTokenId: Guid, codeRepositoryId: Guid) => Promise<void>;
    accounts: Array<any> | undefined;
    fetchAccounts: () => Promise<void>;
    iams: Array<any> | undefined;
    fetchIAMs: (accountId: Guid) => Promise<void>;
    iamRotations: Array<any> | undefined;
    fetchIAMRotation: (accountId: Guid, iamId:Guid) => Promise<void>;
    fetchIsAuthConfigured: () => Promise<void>;
    login: (password: string) => Promise<void>;
    setupAuth: (password: string) => Promise<void>;
}
