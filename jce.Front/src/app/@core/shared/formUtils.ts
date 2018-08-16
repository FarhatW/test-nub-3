const DATE_REGEX = new RegExp(/^(\d{2}|\d)\/(\d{2}|\d)\/\d{4}$/);
// h:mm am/pm, hh:mm AM/PM
// https://regex101.com/r/j2Cfqd/1/
const TIME_REGEX = new RegExp(/^((1[0-2]|0?[1-9]):([0-5][0-9]) ([AaPp][Mm]))$/);

// Converts date + time strings to a Date object.
// Date and time parameters should have already
// been validated with DATE_REGEX and TIME_REGEX.
function stringsToDate(dateStr: string, timeStr: string) {
  if (!DATE_REGEX.test(dateStr) || !TIME_REGEX.test(timeStr)) {
    console.error('Cannot convert date/time to Date object.');
    return;
  }
  const date = new Date(dateStr);
  const timeArr = timeStr.split(/[\s:]+/); // https://regex101.com/r/H4dMvA/1
  let hour = parseInt(timeArr[0], 10);
  const min = parseInt(timeArr[1], 10);
  const pm = timeArr[2].toLowerCase() === 'pm';

  if (!pm && hour === 12) {
    hour = 0;
  }
  if (pm && hour < 12) {
    hour += 12;
  }
  date.setHours(hour);
  date.setMinutes(min);
  return date;
}

function _compare(item1, item2): boolean {
  return item1 === item2;
}

export { DATE_REGEX, TIME_REGEX, stringsToDate };

