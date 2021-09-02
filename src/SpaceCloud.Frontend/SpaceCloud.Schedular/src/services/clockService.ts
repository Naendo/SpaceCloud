class ClockService {
    constructor() {
    }

    run() {
        const time = new Date();
        let hours = time.getHours().toString();
        let minutes = time.getMinutes().toString();
        let seconds = time.getSeconds().toString();
        let getDate = time.getDate().toString();
        let getMonth = time.getMonth().toString();
        let getYear = time.getUTCFullYear().toString();

        if (getDate.length < 2) {
            getDate = '0' + getDate;
        }

        if (getMonth.length < 2) {
            getMonth = '0' + getMonth;
        }

        if (hours.length < 2) {
            hours = '0' + hours;
        }

        if (minutes.length < 2) {
            minutes = '0' + minutes;
        }

        if (seconds.length < 2) {
            seconds = '0' + seconds;
        }




        return getDate + '.' + getMonth + "." + getYear + "   " + hours + ':' + minutes;
    }

    runtime() {
        const time = new Date();
        let minutes = time.getMinutes().toString();
        let seconds = time.getSeconds().toString();


        if (minutes.length < 2) {
            minutes = '0' + minutes;
        }

        if (seconds.length < 2) {
            seconds = '0' + seconds;
        }




        return minutes + ':' + seconds;
    }


}

export default new ClockService;