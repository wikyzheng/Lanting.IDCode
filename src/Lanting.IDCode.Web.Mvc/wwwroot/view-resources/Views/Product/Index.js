(function () {
    $(function () {

        var _service = abp.services.app.productInfo;
        var _$modal = $('#ProductInfoCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-productinfo').click(function () {
            var id = $(this).attr("data-productinfo-id");
            var productinfoName = $(this).attr('data-productinfo-name');

            deleteSingle(id, productinfoName);
        });

        $('.edit-productinfo').click(function (e) {
            var id = $(this).attr("data-productinfo-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Product/EditProductInfoModal?id=' + id,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#ProductInfoEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }
            var tmp = tinymce.get('elm1').getContent();
            var productinfo = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            productinfo.htmlContent = tmp;
            abp.ui.setBusy(_$modal);
            _service.create(productinfo).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new productinfo!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshList() {
            location.reload(true); //reload page to see new productinfo!
        }

        function deleteSingle(id, productinfoName) {
            abp.message.confirm(
                "Are you sure you want to delete this record '" + productinfoName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _service.delete({
                            id: id
                        }).done(function () {
                            refreshList();
                        });
                    }
                }
            );
        }
    });
})();