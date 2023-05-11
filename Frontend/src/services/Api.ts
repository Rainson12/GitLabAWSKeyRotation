import axios from 'axios';
import { useApplicationStore } from '../stores/Application';

let instance: Api;

class Api {
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
        const response = await axios.get('https://localhost:7099/AWS/accounts');
        return response.data;
    }
    async GetGitlabAccessTokens(): Promise<Array<any>> {
        const response = await axios.get('https://localhost:7099/Gitlab/AccessTokens');
        return response.data;
    }
    async Authenticate(password: string): Promise<any> {
        const response = await axios.post('https://localhost:7099/Authenticate', { password: password });
        return response.data;
    }
    async IsAuthConfigured(): Promise<boolean> {
        const response = await axios.get('https://localhost:7099/Authenticate/IsConfigured');
        return response.data;
    }
    async SetupAuthentication(password: string): Promise<any> {
        const response = await axios.post('https://localhost:7099/Authenticate/Setup', { password: password });
        return response.data;
    }
}

const BackendApi = Object.freeze(new Api());
export default BackendApi;
