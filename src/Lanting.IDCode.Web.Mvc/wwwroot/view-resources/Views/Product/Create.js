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
            var tmp = $('#pageHtml').contents().find("body").html();
            var productinfo = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            productinfo.htmlContent = tmp;
            abp.ui.setBusy(_$modal);
            _service.create(productinfo).done(function () {
                location.href = abp.appPath + 'Product/Index';//reload page to see new productinfo!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        function show() {
            if (_showPreview) {
                var css = "<style type='text/css'>img {width: 100%; height:100%;}</style>";
                var tmp = css + tinymce.get('elm1').getContent();
                $('.easyui-navpanel').html(tmp);
            }
        }
      

    });
})();