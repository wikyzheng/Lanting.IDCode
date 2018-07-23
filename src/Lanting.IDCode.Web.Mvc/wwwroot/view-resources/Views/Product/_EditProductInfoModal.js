(function ($) {
    //#htmlContentEdit
    tinymce.init({
        selector: '#htmlContentEdit',          //<textarea>中为编辑区域
        theme: "modern",                  //主题
        language: "zh_cn",                //语言 ，可自行下载中文
        height: 480,
        plugins: [                             //插件，可自行根据现实内容删除
            "advlist autolink lists charmap print preview hr anchor pagebreak spellchecker",
            "searchreplace wordcount visualblocks visualchars fullscreen insertdatetime  nonbreaking image imagetools template",
            "save table contextmenu directionality emoticons paste textcolor"
        ],
        content_css: "codepage/codepage.css",      //引用的外部CSS样式，可删除
        toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect| bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      | print previewButton fullpage | forecolor backcolor | image | template",                          //工具栏，可根据需求删除
        style_formats: [                        //初始时提供的默认格式
            { title: 'Bold text', inline: 'b' },
            { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
            { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
            { title: 'Example 1', inline: 'span', classes: 'example1' },
            { title: 'Example 2', inline: 'span', classes: 'example2' },
            { title: 'Table styles' },
            { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
        ],
        language: 'zh_CN',

        setup: function (editor) {
            editor.addButton('previewButton', {
                icon: 'preview',
                classes: 'mce-preview',
                tooltip: '手机预览效果',
                onclick: function () {
                    //editor.insertContent('&nbsp;<b>It\'s my button!</b>&nbsp;');
                    var tmp = editor.getContent();
                    $('.easyui-navpanel').html(tmp);
                    var imgs = $('.easyui-navpanel').find('img');
                    var attrs = imgs.attr('style');
                    attrs = attrs + "width:100%;height:100%";
                    imgs.attr('style', attrs);
                    $('#previewModal').modal('show');
                }
            });
        },

        images_upload_handler: function (blobInfo, success, failure) {

            var formData = new FormData();
            // formData.ppend(name, element);
            formData.append('file', blobInfo.blob(), blobInfo.filename());
            $.ajax({
                url: abp.appPath + 'Product/FileUpload',
                method: 'POST',
                data: formData,
                contentType: false, // 注意这里应设为false
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.result.statusCode == 200) {
                        success(data.result.data.url);
                    }
                },
                error: function (jqXHR) {
                    console.log(JSON.stringify(jqXHR));
                }
            }).done(function (data) {
                console.log('done');
            }).fail(function (data) {
                console.log('fail');
            }).always(function (data) {
                console.log('always');
            });
        },

        template_cdate_classes: "cdate creationdate",
        template_mdate_classes: "mdate modifieddate",
        template_selected_content_classes: "selcontent",
        template_cdate_format: "%m/%d/%Y : %H:%M:%S",
        template_mdate_format: "%m/%d/%Y : %H:%M:%S",
        template_replace_values: {
            username: "Jack Black",
            staffid: "991234"
        },
        template_popup_height: "400",
        template_popup_width: "320",
        templates: [
            {
                title: "商城(中文)",
                url: "template/shop.html",
                description: "商城模板"
            },
            {
                title: "欢迎页(英文)",
                url: "template/product.html",
                description: "页面编辑页"
            }
        ],

        plugin_preview_width: 250,
        plugin_preview_height: 500
    });
    var _service = abp.services.app.productInfo;
    var _$modal = $('#ProductInfoEditModal');
    var _$form = $('form[name=ProductInfoEditForm]');

    function save() {

        if (!_$form.valid()) {
            return;
        }
        var tmp = tinymce.get('htmlContentEdit').getContent();
        var productinfo = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        productinfo.htmlContent = tmp;
        abp.ui.setBusy(_$form);
        _service.update(productinfo).done(function () {
            _$modal.modal('hide');
            location.reload(true); //reload page to see edited productinfo!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }

    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.closest('div.modal-content').find(".close-button").click(function (e) {

        _$modal.modal('hide');
        location.reload(true); //reload page to see edited productinfo!

    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);