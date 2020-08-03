var dataTable = {
    options: function (ctrlId, columns, url) {
        return {
            ctrlId,
            columns,
            url
        };
    },
    generate: function () {
        if (this.options.ctrlId === undefined || this.options.ctrlId === '') {
            alert('Table ID is required to generate this table!');
            return;
        }
        if (this.options.columns === undefined || this.options.columns.length < 0) {
            alert('Columns are required to generate this table!');
            return;
        }

        var cols = [];
        var colDefinitions = [];
        for (var i = 0; i < this.options.columns.length; i++) {
            //TODO: When you add a new data formatting here, make sure to add also in GlobalHelpers.cs Ln 54
            var def = this.options.columns[i].split(':');
            if (def.length > 1) {
                if ('enum,datetime'.indexOf(def[1]) > -1) {
                    def[0] = def[0] + 'Desc';
                }
                else {
                    colDefinitions.push({
                        "className": def[1], "targets": [i]
                    });
                }
                this.options.columns[i] = def[0];
            }
            cols.push({
                "data": this.options.columns[i]
            });
        }
        var isServerSide = false;
        var ajax = '';
        if (this.options.url !== null) {
            isServerSide = true;
            ajax = {
                "url": this.options.url,
                "type": "POST",
                "dataType": "JSON",
                //"success": function (data) {
                //    var x = data;
                //}
            };
        }

        $('#' + this.options.ctrlId).DataTable({
            "columnDefs": colDefinitions,
            "lengthChange": false,
            "ajax": ajax,
            "columns": cols,
            "processing": true,
            "serverSide": isServerSide,
            "language":
            {
                "processing": "<div class=''><i class='fa fa-cog fa-spin site-loader-color'></i></div>"
            },
            "dom": "<'row'<'col-sm-6'l><'col-sm-6'<'#buttonContainer.site-datatable-button-container'>f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        });
    },
    addButton: function (label, action) {
        $("#buttonContainer").addClass("float-right").append("<button class='btn btn-sm bg-success' onclick='" + action + "'>" + label + "</button>");
    }
}

//$('#TableId').DataTable(
//    {
//        searchDelay: 500,
//        "columnDefs": [
//            { "width": "5%", "targets": [0] },
//            { "className": "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
//            { "defaultContent": "<button class='btn btn-primary btn-sm'>Details</button>", "targets": [7] },
//            { "searchable": false, "targets": [0, 2, 4, 5, 6] },
//            {
//                "render": function (data, type, row) {
//                    return ' <label class="badge label-primary">' + data + '</label>';
//                },
//                "targets": [4]
//            },
//            {
//                "render": function (data, type, row) {
//                    if (row['quantity'] > 1)
//                        return ' <label class="badge label-success">' + row['quantity'] + '</label>'
//                    else
//                        return ' <label class="badge label-default">' + row['quantity'] + '</label>';
//                },
//                "targets": [2]
//            },
//        ],
//        "language":
//        {
//            "processing": "<div class=''><i class='fa fa-cog fa-spin site-loader-color'></i></div>",
//            "search": "filter",
//            "searchPlaceholder": "track num or product"
//        },
//        "processing": true,
//        "serverSide": true,
//        "ajax":
//        {
//            "url": "/Home/GetData",
//            "type": "POST",
//            "dataType": "JSON"
//        },
//        "columns": [
//            { "data": "sr" },
//            { "data": "ordertracknumber" },
//            { "data": "quantity" },
//            { "data": "productname" },
//            { "data": "specialoffer" },
//            { "data": "unitprice" },
//            { "data": "unitpricediscount" },
//        ],
//        "dom": "<'row'<'col-sm-6'l><'col-sm-6'<'#buttonContainer.site-datatable-button-container'>f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
//    });

//$("#buttonContainer").addClass("float-right").append("<button class='btn btn-sm bg-success'>Create</button>");