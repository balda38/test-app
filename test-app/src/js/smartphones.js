﻿'use strict'

let rowId;
let tableAsJson;

$(document).ready(function () {
    getTableData();

    $(document).on('click', '.add_row_button',
        function () {
            $('.add_row_button').prop('disabled', true);
            addRowWithAjax();
        }
    );

    $(document).on('click', '.accept_new_record_button',
        function () {
            acceptNewRecordWithAjax();
        }
    );

    $(document).on('click', '.cancel_new_record_button',
        function () {
            cancelNewRecord();
        }
    );

    $(document).on('click', '.edit_record_button',
        function () {
            rowId = $(this).data('row-id');
            prepareRowToUpdate();
        }
    );

    $(document).on('click', '.accept_update_record_button',
        function () {
            updateRecordWithAjax();
        }
    );

    $(document).on('click', '.cancel_update_record_button',
        function () {
            cancelUpdateRecord();
        }
    );

    $(document).on('click', '.delete_record_button',
        function () {
            rowId = $(this).data('row-id');
            deleteRecordWithAjax();
        }
    );
});

function getTableData() {
    $.ajax({
        url: 'GetAll',
        type: 'GET',
        dataType: 'json',

        success: function (response) {
            tableAsJson = JSON.parse(response);
            buildHtmlTable('.table');
        },
        error: function (response) {
            window.alert('Ошибка');
        }
    })
}

function buildHtmlTable(selector) {
    var columns = addAllColumnHeaders(tableAsJson, selector);

    for (var i = 0; i < tableAsJson.length; i++) {
        var row$ = $('<form id="' + tableAsJson[i][columns[0]] +'" class="table_row"></form>');
        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
            var cellValue = tableAsJson[i][columns[colIndex]];
            if (cellValue == null) cellValue = "";
            row$.append($('<input class="table_row_cell" name="' + columns[colIndex] +'"value="' + cellValue + '">'));
        }
        row$.append('<div class="table_row_cell" id="rowOptions' + tableAsJson[i][columns[0]] + '">' +
                    '<img class="edit_record_button" data-row-id="' + tableAsJson[i][columns[0]] +
                    '" src="/src/icons/edit.png" onclick="edit(@row["id"])">' +
                    '<img class="delete_record_button" data-row-id="' + tableAsJson[i][columns[0]] + '" src="/src/icons/delete.png">' +
                    '<input class="row_choose" data-row-id="' + tableAsJson[i][columns[0]] + '" type="checkbox">' +
                    '</div>');
        $(selector).append(row$);
    }
}

function addAllColumnHeaders(tableAsJson, selector) {
    $(selector).empty();
    var columnSet = [];
    var headerTr$ = $('<div class="table_header"></div>');

    for (var i = 0; i < tableAsJson.length; i++) {
        var rowHash = tableAsJson[i];
        for (var key in rowHash) {
            if ($.inArray(key, columnSet) == -1) {
                columnSet.push(key);
                headerTr$.append($('<div class="table_header_cell" name="' + key + '"></div>').html(key));
            }
        }
    }
    $(selector).append(headerTr$);
    $(selector).append('<div class="table_header_cell"><p>Действия</p></div>');

    return columnSet;
}

function addRowWithAjax() {
    let newRow = document.createElement('form');
    newRow.id = 'newRow';
    newRow.className = 'table_row';
    newRow.method = 'POST';

    let tableCellsCount = $('.table_header_cell');
    console.log(tableCellsCount);

    for (let i = 0; i < tableCellsCount.length; i++)
    {
        let cell;

        if (i != (tableCellsCount.length - 1)) {
            cell = document.createElement('input');
            cell.name = tableCellsCount[i].getAttribute('name');
        } else {
            cell = document.createElement('div');

            let acceptButton = document.createElement('img');
            acceptButton.className = 'accept_new_record_button';
            acceptButton.src = '../src/icons/accept.png';
            cell.append(acceptButton);

            let cancelButton = document.createElement('img');
            cancelButton.className = 'cancel_new_record_button';
            cancelButton.src = '../src/icons/cancel.png';
            cell.append(cancelButton);
        }

        cell.className = 'table_row_cell';
        cell.style.border = '3px solid #FF0000';
        cell.style.pointerEvents = 'auto';

        newRow.appendChild(cell);
    }

    let table = $('.table');
    table.append(newRow);
}

function acceptNewRecordWithAjax() {
    let json = {};
    let data = $.each($('#newRow').serializeArray(), function () {
        json[this.name] = this.value;
    });

    $.ajax({
        url: 'Create',
        type: 'POST',
        dataType: 'json',
        data: { "json": JSON.stringify(json) },

        success: function (response) {
            $('.add_row_button').prop('disabled', false);
            getTableData();
            window.alert('OK');
        },
        error: function (response) {
            window.alert('Ошибка');
        }
    })
}

function cancelNewRecord() {
    $('#newRow').remove();
    $('.add_row_button').prop('disabled', false);
}

function prepareRowToUpdate()
{
    let formCells = $('#' + rowId).children();
    for (let i = 0; i < formCells.length; i++)
    {
        formCells[i].style.border = '3px solid #FF0000';
        formCells[i].style.zIndex = '100';
        formCells[i].style.pointerEvents = 'auto';
    }

    let options = $('#rowOptions' + rowId);
    options.empty();

    options.append('<img class="accept_update_record_button"' +
                    '" src="/src/icons/accept.png" onclick="edit(@row["id"])">' +
                    '<img class="cancel_update_record_button" src="/src/icons/cancel.png">');
}

function cancelUpdateRecord() {
    let formCells = $('#' + rowId).children();
    for (let i = 0; i < formCells.length; i++) {
        formCells[i].style.border = '3px solid #000000';
        formCells[i].style.zIndex = '1';
        formCells[i].style.pointerEvents = 'none';
    }

    let options = $('#rowOptions' + rowId);
    options.empty();

    options.append('<img class="edit_record_button" data-row-id="' + rowId +
                    '" src="/src/icons/edit.png" onclick="edit(@row["id"])">' +
                    '<img class="delete_record_button" data-row-id="' + rowId + '" src="/src/icons/delete.png">' +
                    '<input class="row_choose" data-row-id="' + rowId + '" type="checkbox">');
}

function updateRecordWithAjax() {
    let json = {};
    let data = $.each($('#' + rowId).serializeArray(), function () {
        json[this.name] = this.value;
    });

    $.ajax({
        url: 'Edit/' + rowId,
        type: 'POST',
        dataType: 'json',
        data: { "json": JSON.stringify(json) },

        success: function (response) {
            getTableData();
            window.alert('Запись успешно изменена!');
        },
        error: function (response) {
            window.aler('Ошибка');
        }
    })
}

function deleteRecordWithAjax() {
    let json = '{"id":"' + rowId + '"}';

    $.ajax({
        url: 'Delete/' + rowId,
        type: 'POST',
        dataType: 'json',
        data: { "json" : json },

        success: function (response) {
            getTableData();
            window.alert('Запись успешно удалена!');
        },
        error: function (response) {
            window.alert('Ошибка');
        }
    })
}