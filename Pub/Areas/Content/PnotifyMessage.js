function showSuccess(message) {

    new PNotify({
        title: 'Thông báo',
        text: message,
        type: 'success',
        styling: 'bootstrap3',
        delay: 1000
    });
}

function showError(message) {

    new PNotify({
        title: 'Thông báo',
        text: message,
        type: 'error',
        styling: 'bootstrap3',
        delay: 1000
    });
}

function showWarring(message) {

    new PNotify({
        title: 'Thông báo',
        text: message,
        type: '',
        styling: 'bootstrap3',
        delay: 1000
    });
}