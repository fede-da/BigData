function updateTextAreaHeight(textArea, maxRows) {
    textArea.style.height = 'auto';  // Reset the height 
    var rows = textArea.value.split('\n').length;

    if (rows > maxRows) {
        textArea.style.overflow = 'auto';
        // We should multiply maxRows by a line-height. Assuming line-height = 20px.
        textArea.style.height = (maxRows * 20) + 'px';
    }
    else if (rows === 0 || textArea.value === '') {
        textArea.style.height = 'auto';
    }
    else {
        textArea.style.overflow = 'hidden';
        textArea.style.height = (textArea.scrollHeight) + 'px';
    }
}
