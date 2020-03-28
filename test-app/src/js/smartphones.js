'use strict'

let lineId;

$(document).ready(function () {
    $('#insert_new_record_button').click(
        function () {
            insertNewRecordWithAjax('Smartphones/Insert');
        }
    );

    $('#update_record_button').click(
        function (lineId) {
            updateRecordWithAjax('Smartphones/Update/', lineId);
        }
    );

    $(document).on('click', '.delete_record_button',
        function () {
            lineId = $(this).data('line-id');
            console.log(lineId);
            deleteRecordWithAjax();
        }
    );
});

function insertNewRecordWithAjax (url) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: $('#new_line').serialize(),

        success: function (response) {
            console.log(response)
            let result = $.parseJSON(response);
        },
        error: function (response) {
            console.log(response)
            window.aler('Ошибка');
        }
    })
}

function updateRecordWithAjax(url, lineId) {
    $.ajax({
        url: url + lineId,
        type: 'POST',
        dataType: 'json',
        data: $('#'+lineId).serialize(),

        success: function (response) {
            let result = $.parseJSON(response);
        },
        error: function (response) {
            window.aler('Ошибка');
        }
    })
}

function deleteRecordWithAjax() {
    let json = '{"id":' + lineId + '}';

    $.ajax({
        url: 'Delete/' + lineId,
        type: 'POST',
        dataType: 'json',
        data: json,

        success: function (response) {
            $('#' + lineId).remove();
        },
        error: function (response) {
            window.alert('Ошибка');
        }
    })
}