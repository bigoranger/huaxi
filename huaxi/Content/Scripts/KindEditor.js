$(function () {
        var editorContent = null;
        KindEditor.ready(function (k) {

            editorContent = k.create('textarea[name="Contents"]', {
                allowImageUpload: true,
                uploadJson: '/Upload/UploadFile'
                , items: [
                     'source', '|', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'code', 'image', 'flash', 'map', 'media', 'insertfile', 'table', 'link', '|', 'fullscreen']
            });
        });
    })
