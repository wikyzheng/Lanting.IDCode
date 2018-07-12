(function () {
    $(function () {

        tinymce.init({
            selector: "h1.editable#elm2",       //elm2为ID                 可将selector值理解为css中class、ID等，以此使用tinymce中样式（比如编辑框内文本显示样式、工具栏样式）--个人理解，不保证正确
            inline: true,                       //为true时，编辑工具栏隐藏
            toolbar: "undo redo",
            menubar: false
        });
        tinymce.init({
            selector: 'textarea#elm1',          //<textarea>中为编辑区域
            theme: "modern",                  //主题
            language: "zh_cn",                //语言 ，可自行下载中文

            height: 280,

            

            plugins: [                             //插件，可自行根据现实内容删除
                "advlist autolink lists charmap print preview hr anchor pagebreak spellchecker",
                "searchreplace wordcount visualblocks visualchars fullscreen insertdatetime  nonbreaking image imagetools template",
                "save table contextmenu directionality emoticons paste textcolor"
            ],


            //content_css: "css/content.css",      //引用的外部CSS样式，可删除
            toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect| bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      | print preview fullpage | forecolor backcolor | image | template",                          //工具栏，可根据需求删除
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
                    title: "商品",
                    url: "template/product.htm",
                    description: "产品编辑页"
                },
                {
                    title: "商城",
                    url: "template/shop.html",
                    description: "商城模板"
                }
            ],

            plugin_preview_width: 250,
            plugin_preview_height: 500,
        });

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