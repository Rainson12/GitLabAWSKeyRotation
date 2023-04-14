import { Guid } from "../../Common/Guid";

export interface ApplicationState {
    selectedAccount: any,
    selectedIAM: any,
    selectedAccessToken: any,
    selectedCodeRepository: any,
    setSelectedAccount: (account: any) => void,
    setSelectedIAM: (iam: any) => void,
    setSelectedAccessCode: (accessToken: any) => void,
    setSelectedCodeRepository: (codeRepo: any) => void,
    computed: any,
    gitlabAccessTokens: Array<any> | undefined,
    fetchGitlabAccessTokens: () => Promise<void>

    accounts: Array<any> | undefined,
    fetchAccounts: () => Promise<void>
}
