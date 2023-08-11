function updateTextAreaHeight(textArea, maxRows) {
    textArea.style.height = '';  // Reset the height 
    var rows = textArea.value.split('\n').length;

    if (rows > maxRows) {
        textArea.style.overflow = 'auto';
        textArea.style.height = (textArea.scrollHeight) + 'px';
    }
    else if (rows == 0 || textArea.value == '') {
        textArea.style.height = 'auto';
    }
    else {
        textArea.style.height = (textArea.scrollHeight) + 'px';
    }
}
