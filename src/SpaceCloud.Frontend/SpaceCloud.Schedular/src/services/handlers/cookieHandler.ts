export function setCookie(name: string, val: string, exp: Date) {
    document.cookie = name + "=" + val + "; " + "expires=" + exp.toUTCString() + "; path=/";
}

export function getCookie(name: string):string | null | undefined {
    const value = "; " + document.cookie;
    const parts = value.split("; " + name + "=");

    if (!parts) return null;

    if (parts.length == 2) {
        // @ts-ignore
        return parts.pop().split(";").shift();
    }
}

export function deleteCookie(name: string) {
    const date = new Date();

    // Set it expire in -1 days
    date.setTime(date.getTime() - 1);

    // Set it
    document.cookie = name + "=; expires=" + date.toUTCString() + "; path=/";
}