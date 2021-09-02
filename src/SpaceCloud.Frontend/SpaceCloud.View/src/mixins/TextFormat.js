export default {
  methods: {
    truncate(text, length = 20) {
      if (text.length > length)
        return `${text.substring(0, length - 1)}...`;
      return text;
    },
    truncateMail(m, length = 30) {
      if (m.length > length)
        return `${m.substring(
          0,
          length - (m.length - m.indexOf('@')) - 1,
        )}...${m.substring(m.indexOf('@'), m.length)}`;
      return m;
    },
    formatDate(
      date,
      addDate = true,
      addTime = true,
      convertUTC = false,
    ) {
      let d = '';
      if (addDate)
        // date.getMonth() starts at 0 -> jan = 0 -> month + 1
        d = `${this.dblDig(date.getDate())}.${this.dblDig(
          date.getMonth() + 1,
        )}.${date
          .getFullYear()
          .toString()
          .substr(-2)}`;
      if (addDate && addTime) d += ' ';
      if (addTime) {
        if (convertUTC) {
          // adjust for Timezones with browser timezone offset
          // not necessary (happens automatically when date is converted to string)

          const UTCoffsetMinutes =
            new Date().getTimezoneOffset() * -1;
          date.setTime(date.getTime() + UTCoffsetMinutes * 60 * 1000);
        }
        d += `${this.dblDig(date.getHours())}:${(this.dblDig(
          date.getMinutes(),
        ) < 10
          ? '0'
          : '') + date.getMinutes()}`;
      }
      return d;
    },
    getDateUTC(date) {
      return new Date(
        `${new Date(date).toString().split('GMT')[0]} UTC`,
      )
        .toISOString()
        .split('.')[0]
        .slice(0, -3);
    },
    getDateUTCNow() {
      return new Date(`${new Date().toString().split('GMT')[0]} UTC`)
        .toISOString()
        .split('.')[0]
        .slice(0, -3);
    },
    roundUTCtoNextQuarter(dateStr) {
      const date = new Date(dateStr);

      // Getting minutes
      const mins = date.getMinutes();

      // Getting hours
      let hrs = date.getHours();
      let m = (parseInt((mins + 15) / 15, 10) * 15) % 60;

      // Converting '09:0' to '09:00'
      m = m < 10 ? `0${m}` : m;
      // eslint-disable-next-line no-nested-ternary
      let h = mins > 45 ? (hrs === 23 ? 0 : (hrs += 1)) : hrs;

      // Converting '9:00' to '09:00'
      h = h < 10 ? `0${h}` : h;

      return `${dateStr.slice(0, -5)}${h}:${m}`;
    },
    getRoleName(roleId, adminLong = false) {
      switch (roleId) {
        case 1:
          return 'Worker';
        case 2:
          return adminLong ? 'Administrator' : 'Admin';
        case 3:
          return 'Owner';
        default:
          return '? ? ?';
      }
    },
    dblDig(n) {
      return n < 10 ? `0${n}` : n;
    },
    // invalid String: returns true if string is undefined, null or empty
    validStr(str) {
      if (typeof str !== 'string') return false;
      return !(str === '');
    },
  },
};
