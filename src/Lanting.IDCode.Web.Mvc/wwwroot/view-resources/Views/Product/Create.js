(function () {
    $(function () {

        var _showPreview = true;
        var _service = abp.services.app.productInfo;
        var _$modal = $('.body');
        var _$form = _$modal.find('form');
        _$form.validate();

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }
            var htmlContent;
            var labelContent;
            if ($('#pageHtml').context == null) {
                htmlContent = $('#pageHtml').contents().find('head').parent().html();
            }
            else {
                htmlContent = $('#pageHtml').context.head.parentElement.outerHTML;
            }
            
            if ($('#pageLabel').context == null) {
                labelContent = $('#pageLabel').contents().find('head').parent().html();
            }
            else {
                labelContent = $('#pageLabel').context.head.parentElement.outerHTML;
            }
            var productinfo = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            productinfo.htmlContent = htmlContent;
            productinfo.labelContent = labelContent;
            abp.ui.setBusy(_$modal);
            _service.create(productinfo).done(function () {
                location.href = abp.appPath + 'Product/Index';//reload page to see new productinfo!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });
    });
})();