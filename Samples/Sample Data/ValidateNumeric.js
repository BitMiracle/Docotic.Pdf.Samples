function validateNumeric(event) {
	var validCharacters = "0123456789";
	for (var i = 0; i < event.change.length; i++) {
		if (validCharacters.indexOf(event.change.charAt(i)) == -1) {
			app.beep(0);
			event.rc = false;
			break;
		}
	}
}

validateNumeric(event);