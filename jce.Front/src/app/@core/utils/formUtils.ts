
const DATE_REGEX = new RegExp(/^(\d{2}|\d)\/(\d{2}|\d)\/\d{4}$/);
const TIME_REGEX = new RegExp(/^((1[0-2]|0?[1-9]):([0-5][0-9]) ([AaPp][Mm]))$/);
const PHONE_REGEX = new RegExp( /^(0|\+33)[1-9]([-. ]?[0-9]{2}){4}$/);
const ZIPCODE_REGEX = new RegExp(/^(([0-8][0-9])|(9[0-5]))[0-9]{3}$/);
const REFPINTEL_REGEX = new RegExp(/^[0-9]{4}$/);
const PRICE_REGEX = new RegExp(/^\d*\.?\d*$/);
const EMAIL_REGEX = new RegExp(
  /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

const PASSWORD_REGEX = new RegExp(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,100}/);
const NUMBERS_AND_COMMA_REGEX = new RegExp(/^[+ -]?[0-9]{1,3}([.][0-9]{1,3})?$/);
const NUMBERS_REGEX = new RegExp(/^[0-9]*$/);
const LETTERS_AND_SPACES_REGEX = new RegExp(/^[a-zA-Z\s]*$/);
const DIMENSIONS_REGEX = new RegExp(/(\d+(?:,\d+)?) x (\d+(?:,\d+)?)(?: x (\d+(?:,\d+)?))?/);



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


export {PRICE_REGEX, REFPINTEL_REGEX, ZIPCODE_REGEX, PHONE_REGEX, DATE_REGEX, TIME_REGEX,EMAIL_REGEX, PASSWORD_REGEX,
  LETTERS_AND_SPACES_REGEX, NUMBERS_AND_COMMA_REGEX, NUMBERS_REGEX, DIMENSIONS_REGEX, stringsToDate, _compare };
