function setDay(date) {
    var dayField = this.getField("day");
    if (dayField.value.length == 0) {
        dayField.value = util.printd("dd", date);
    }
}

function setMonth(date) {
    var monthField = this.getField("month");
    if (monthField.value.length == 0) {
        monthField.value = util.printd("date(en){MMMM}", date, true);
    }
}

function setYear(date) {
    var yearField = this.getField("year");
    if (yearField.value.length == 0) {
        yearField.value = util.printd("yyyy", date);
    }
}

function setCurrentDate() {
    var now = new Date();
    setDay(now);
    setMonth(now);
    setYear(now);
}

setCurrentDate();
