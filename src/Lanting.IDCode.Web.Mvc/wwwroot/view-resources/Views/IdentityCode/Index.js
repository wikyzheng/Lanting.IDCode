(function () {
    $(function () {

        var _service = abp.services.app.identityCode;
        var _$modal = $('#IdentityCodeCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-identitycode').click(function () {
            var id = $(this).attr("data-identitycode-id");
            var identitycodeName = $(this).attr('data-identitycode-name');

            deleteSingle(id, identitycodeName);
        });

        $('.edit-identitycode').click(function (e) {
            var id = $(this).attr("data-identitycode-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'IdentityCode/EditIdentityCodeModal?id=' + id,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#IdentityCodeEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var identitycode = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$modal);
            _service.create(identitycode).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new identitycode!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshList() {
            location.reload(true); //reload page to see new identitycode!
        }

        function deleteSingle(id, identitycodeName) {
            abp.message.confirm(
                "Are you sure you want to delete this record '" + identitycodeName + "'?",
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