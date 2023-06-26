import axios from 'axios';
import { useApplicationStore } from '../stores/Application';
import { Guid } from '../Models/Common/Guid';

let instance: Api;

class Api {
    private BASE_URL = 'https://localhost:7099';
    constructor() {
        if (instance) {
            throw new Error('You can only create one instance!');
        }
        axios.interceptors.request.use(function (config) {
            const token = useApplicationStore.getState().authToken;
            if (token !== undefined && token !== '') {
                config.headers.Authorization = `Bearer ${token}`;
            }
            return config;
        });
        instance = this;
    }
    async GetAccounts(): Promise<Array<any>> {
        const response = await axios.get(`${this.BASE_URL}/AWS/accounts`);
        return response.data;
    }
    async GetGitlabAccessTokens(): Promise<Array<any>> {
        const response = await axios.get(`${this.BASE_URL}/Gitlab/AccessTokens`);
        return response.data;
    }
    async GetCodeRepositories(accessToken: Guid): Promise<Array<any>> {
        const response = await axios.get(`${this.BASE_URL}/Gitlab/AccessToken/${accessToken}/CodeRepositories`);
        return response.data;
    }

    async GetRotationsFromCodeRepository(accessTokenId: Guid, codeRepositoryId: Guid): Promise<Array<any>> {
        const response = await axios.get(`${this.BASE_URL}/Gitlab/AccessToken/${accessTokenId}/CodeRepository/${codeRepositoryId}/Rotations`);
        return response.data;
    }

    async GetIAMs(accountId: Guid): Promise<Array<any>> {
        const response = await axios.get(`${this.BASE_URL}/AWS/Accounts/${accountId}/IAMs`);
        return response.data;
    }
    async GetRotationsFromIAM(accountId: Guid, iamId: Guid): Promise<Array<any>> {
        const response = await axios.get(`${this.BASE_URL}/AWS/Account/${accountId}/IAM/${iamId}/Rotations`);
        return response.data;
    }

    async AddGitlabAccessToken(name: string, token: string): Promise<Array<any>> {
        const response = await axios.post(`${this.BASE_URL}/Gitlab/AccessToken/Register`, {
            name: name,
            token: token,
        });
        return response.data;
    }

    async ScanGitlab(name: string, token: string, rotationIntervalInDays: number): Promise<Array<any>> {
        const response = await axios.post(`${this.BASE_URL}/Gitlab/AccessToken/Scan`, {
            name,
            token,
            rotationIntervalInDays,
        });
        return response.data;
    }
    async Authenticate(password: string): Promise<any> {
        const response = await axios.post(`${this.BASE_URL}/Authenticate`, { password: password });
        return response.data;
    }
    async IsAuthConfigured(): Promise<boolean> {
        const response = await axios.get(`${this.BASE_URL}/Authenticate/IsConfigured`);
        return response.data;
    }
    async SetupAuthentication(password: string): Promise<any> {
        const response = await axios.post(`${this.BASE_URL}/Authenticate/Setup`, { password: password });
        return response.data;
    }
}

const BackendApi = Object.freeze(new Api());
export default BackendApi;
