import router from './router'
import authenticationService from "../../services/authorizationService";


export function setupRouter(){

    router.beforeEach(async (to, from, next) => {

        if(to.fullPath === "/notfound") await next();
        if (to.fullPath === "/") await next();
        else {
            const isAuthorized = authenticationService.getLoggedUser() !== "";

            if (!isAuthorized) {
                await next({name:'notfound', replace: true});
            } else {
                await next();
            }
        }
    });

}