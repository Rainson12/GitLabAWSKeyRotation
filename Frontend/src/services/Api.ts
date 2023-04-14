import axios from "axios";

let instance: Api;

class Api {
  constructor() {
    if (instance) {
      throw new Error("You can only create one instance!");
    }
    instance = this;
  }
 
  getInstance() {
    return this;
  }

  async GetAccounts(): Promise<Array<any>>{
    const response = await axios.get('https://localhost:7099/AWS/accounts')
    return response.data
  }
  async GetGitlabAccessTokens(): Promise<Array<any>>{
    const response = await axios.get('https://localhost:7099/Gitlab/AccessTokens')
    return response.data
  }
}
 
const BackendApi = Object.freeze(new Api());
export default BackendApi;