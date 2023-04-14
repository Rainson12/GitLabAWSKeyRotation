import { create } from 'zustand';
import { devtools, persist } from 'zustand/middleware';
import { Guid } from '../Models/Common/Guid';
import { ApplicationState } from '../Models/ApplicationState/Interfaces/ApplicationState';
import BackendApi from '../services/Api';

export const useApplicationStore = create<ApplicationState>()(
    devtools(
        persist(
            (set, get) => ({
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
                        return get()?.gitlabAccessTokens?.map(x => x.codeRepositories).flat();
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
            }),
            {
                name: 'applicationStore',
            }
        )
    )
);
