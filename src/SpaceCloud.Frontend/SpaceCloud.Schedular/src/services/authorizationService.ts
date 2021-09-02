import {getCookie} from './handlers/cookieHandler';


class TokenViewModel{
    roomId:string;
    userId:string;
    token:string;
    exp:Date;

    constructor(roomId:string,userId:string,token:string, exp:number){
        this.token = token;
        this.userId = userId;
        this.roomId = roomId;
        this.exp = new Date(exp);
    }

}

function getToken(): string | undefined | null {
    return getCookie("token");
}

export function getTokenBody():TokenViewModel|null{
    const token = getToken();

    if(token === null || token === undefined)
        return null;

    const result = parseToken(token);
    return new TokenViewModel(result.RoomId, result.UserId, token, result.exp);
}

function parseToken(token:string) {
    try {
        return JSON.parse(atob(token.split('.')[1]));
    } catch {
        return '';
    }
}