import { create } from 'zustand';
import { devtools, persist } from 'zustand/middleware';
import { ApplicationState } from '../Models/ApplicationState/Interfaces/ApplicationState';
import BackendApi from '../services/Api';

export const useApplicationStore = create<ApplicationState>()(
    devtools(
        (set, get) => ({
            isAuthConfigured: undefined,
            authToken: undefined,
            selectedAccount: undefined,
            selectedIAM: undefined,
            selectedAccessToken: undefined,
            selectedCodeRepository: undefined,
            setSelectedAccount: (account) => set({ selectedAccount: account }),
            setSelectedIAM: (iam) => set({ selectedIAM: iam }),
            setSelectedAccessCode: (accessToken) => set({ selectedAccessToken: accessToken }),
            setSelectedCodeRepository: (codeRepo) => set({ selectedCodeRepository: codeRepo }),
            computed: {
                get codeRepositories() {
                    return get()
                        ?.gitlabAccessTokens?.map((x) => x.codeRepositories)
                        .flat();
                },
            },
            gitlabAccessTokens: undefined,
            fetchGitlabAccessTokens: async () => {
                const response = await BackendApi.GetGitlabAccessTokens();
                set({ gitlabAccessTokens: response });
            },
            accounts: undefined,
            fetchAccounts: async () => {
                const response = await BackendApi.GetAccounts();
                set({ accounts: response });
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
        })
    )
);
