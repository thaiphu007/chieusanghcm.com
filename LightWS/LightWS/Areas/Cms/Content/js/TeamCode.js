function ShowNoti(Title, Text, Type) {
    var noti = new PNotify({
        title: Title,
        text: Text,
        type: Type,
        delay: 5000,
        styling: 'bootstrap3'
    });
}

function RefressPage() {
    location.reload();
}
