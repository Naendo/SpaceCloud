import {AxiosRequestConfig, AxiosStatic, Method} from "axios";

class AxiosService {
    private readonly axios: AxiosStatic

    public constructor() {
        this.axios = require('axios').default;
    }

    public get(url: string, jwt: string = "") {
        if (jwt === "") {
            const instance = this.createInstance("GET", jwt)
            return instance.get(url);
        } else {
            const instance = this.createInstance("GET", jwt)
            return instance.get(url);
        }
    }

    public post(url: string, body: any, jwt: string = "") {
        if (jwt === "") {
            const instance = this.createInstance("POST", jwt)
            return instance.post(url, body);
        } else {
            const instance = this.createInstance("POST", jwt)
            return instance.get(url, body);
        }
    }

    private createInstance(method: Method, jwt: string = "", baseURI: string = "https://api.spacecloud.cc/") {
        if (jwt !== "") {
            return this.axios.create({
                method: method,
                baseURL: baseURI,
                headers: {
                    'content-type': "application/json",
                    'Authorization': jwt,
                }
            });
        }
        return this.axios.create({
            method: method,
            baseURL: baseURI,
        })
    }
}

export default new AxiosService();
