function synchronizeFields(sourceFieldName, destinationFieldName) {
	var source = this.getField(sourceFieldName);
	var destination = this.getField(destinationFieldName);
	if (source != null && destination != null) {
		destination.value = source.value;
	}
}