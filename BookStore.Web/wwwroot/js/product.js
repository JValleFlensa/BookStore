var dataTable;
$(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = new DataTable('#dataTable', {
        ajax: '/Admin/Product/GetAll',
        columns: [
            { data: 'tittle' },
            { data: 'isbn' },
            { data: 'listPrice' }
        ]
    });
}