var dataTable;
$(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = new DataTable('#dataTable', {
        ajax: '/Admin/Product/GetAll',
        columns: [
            { data: 'tittle', width: '20%' },
            { data: 'isbn', width: '8%' },
            { data: 'category.name', width: '15%' },
            { data: 'listPrice', width: '15%' },
            { data: 'author', width: '10%' },
            {
                data: 'id',
                render: function (data) {
                    return `
                    <div class="w-75 btn-group">
                        <a href="/Admin/Product/Upsert?productId=${data}" class="btn btn-primary"><i class="bi bi-pencil-square"></i> Editar</a>
                        <a onclick="Delete('/Admin/Product/Delete/${data}')" class="btn btn-danger"><i class="bi bi-trash"></i> Borrar</a>
                    </div>`;
                },
                width: '20%'
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Estas seguro?",
        text: "Este cambio no puede ser revertido!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, borrar!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: (data) => {
                    dataTable.ajax.reload()
                    ShowMessage(data)
                }
            });

        }
    });
}

function ShowMessage(data) {
    if (data.success) {
        toastr.success(data.message)
    }
    else {
        toastr.error(data.message)
    }
}