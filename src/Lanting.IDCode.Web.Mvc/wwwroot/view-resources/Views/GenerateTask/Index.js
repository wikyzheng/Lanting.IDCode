(function () {
    $(function () {

        var _service = abp.services.app.generateTask;
        var _$modal = $('#GenerateTaskCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-generatetask').click(function () {
            var id = $(this).attr("data-generatetask-id");
            var generatetaskName = $(this).attr('data-generatetask-name');

            deleteSingle(id, generatetaskName);
        });

        $('.edit-generatetask').click(function (e) {
            var id = $(this).attr("data-generatetask-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Task/EditGenerateTaskModal?id=' + id,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#GenerateTaskEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var generatetask = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$modal);
            _service.create(generatetask).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new generatetask!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshList() {
            location.reload(true); //reload page to see new generatetask!
        }

        function deleteSingle(id, generatetaskName) {
            abp.message.confirm(
                "Are you sure you want to delete this record '" + generatetaskName + "'?",
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