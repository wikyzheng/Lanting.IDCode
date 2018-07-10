/**
 * tinymce plugin
 * Created by jerry on 16/8/5.
 */
tinymce.PluginManager.add('uploadimage', function (editor) {

    function selectLocalImages() {
        var dom = editor.dom;
        var input_f = $('<input type="file" name="thumbnail" accept="image/jpg,image/jpeg,image/png,image/gif" multiple="multiple">');
        input_f.on('change', function () {
            var form = $("<form/>",
                {
                    action: editor.settings.upload_image_url, //�����ϴ�ͼƬ��·�ɣ������ڳ�ʼ��ʱ
                    style: 'display:none',
                    method: 'post',
                    enctype: 'multipart/form-data'
                }
            );
            form.append(input_f);
            //ajax�ύ����
            form.ajaxSubmit({
                beforeSubmit: function () {
                    return true;
                },
                success: function (data) {
                    if (data && data.file_path) {
                        editor.focus();
                        data.file_path.forEach(function (src) {
                            editor.selection.setContent(dom.createHTML('img', {src: src}));
                        })
                    }
                }
            });
        });

        input_f.click();
    }

    editor.addCommand("mceUploadImageEditor", selectLocalImages);

    editor.addButton('uploadimage', {
        icon: 'image',
        tooltip: '�ϴ�ͼƬ',
        onclick: selectLocalImages
    });

    editor.addMenuItem('uploadimage', {
        icon: 'image',
        text: '�ϴ�ͼƬ',
        context: 'tools',
        onclick: selectLocalImages
    });
});