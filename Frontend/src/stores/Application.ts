import { create } from 'zustand';
import { devtools, persist } from 'zustand/middleware';
import { ApplicationState } from '../Models/ApplicationState/Interfaces/ApplicationState';
import BackendApi from '../services/Api';
import { Guid } from '../Models/Common/Guid';

export const useApplicationStore = create<ApplicationState>()(
    devtools((set, get) => ({
        isAuthConfigured: undefined,
        authToken: undefined,
        selectedAccount: undefined,
        selectedIAM: undefined,
        selectedAccessToken: undefined,
        selectedCodeRepository: undefined,
        setSelectedAccount: (account) => {
            set({ selectedAccount: account });
            get().fetchIAMs(account.id.value);
        },
        setSelectedIAM: (iam) => {
            set({ selectedIAM: iam });
            get().fetchIAMRotation(get().selectedAccount.id.value, iam.id.value);
        },
        setSelectedAccessCode: (accessToken) => {
            set({ selectedAccessToken: accessToken });
            get().fetchCodeRepositories(accessToken.id.value);
        },
        setSelectedCodeRepository: (codeRepo) => {
            set({ selectedCodeRepository: codeRepo });
            get().fetchCodeRepositoryRotations(get().selectedAccessToken.id.value, codeRepo.id.value);
        },
        gitlabAccessTokens: undefined,
        fetchGitlabAccessTokens: async () => {
            const response = await BackendApi.GetGitlabAccessTokens();
            set({ gitlabAccessTokens: response });
        },
        codeRepositories: undefined,
        fetchCodeRepositories: async (accessTokenId: Guid) => {
            const response = await BackendApi.GetCodeRepositories(accessTokenId);
            set({ codeRepositories: response });
        },
        codeRepositoryRotations: undefined,
        fetchCodeRepositoryRotations: async (accessTokenId: Guid, codeRepositoryId: Guid) => {
            const response = await BackendApi.GetRotationsFromCodeRepository(accessTokenId, codeRepositoryId);
            set({ codeRepositoryRotations: response });
        },
        accounts: undefined,
        fetchAccounts: async () => {
            const response = await BackendApi.GetAccounts();
            set({ accounts: response });
        },
        iams: undefined,
        fetchIAMs: async (accountId: Guid) => {
            const response = await BackendApi.GetIAMs(accountId);
            set({ iams: response });
        },
        iamRotations: undefined,
        fetchIAMRotation: async (accountId: Guid, iamId: Guid) => {
            const response = await BackendApi.GetRotationsFromIAM(accountId, iamId);
            set({ iamRotations: response });
        },
        login: async (password: string) => {
            const response = await BackendApi.Authenticate(password);
            set({ authToken: response });
        },
        fetchIsAuthConfigured: async () => {
            const response = await BackendApi.IsAuthConfigured();
            set({ isAuthConfigured: response });
        },
        setupAuth: async (password: string) => {
            const response = await BackendApi.SetupAuthentication(password);
            set({ authToken: response });
            set({ isAuthConfigured: true });
        },
    }))
);
