$(function () {

    if (typeof ckeditor_ids == 'undefined' || ckeditor_ids == null)
    {
        console.log('Lütfen \"ckeditor_ids\" değişkeninde ckeditor kullanılacak element ID lerini tanımlayınız.');
        console.log(ckeditor_ids);
        return;
    }
    
    // TODO : CK Editor için root path, fakat debug aşamada yüklemeyecektir.. Açıklama satırı yap.
    CKEDITOR.basePath = "http://www.muhendisasci.com/scripts/ckeditor/";

    for (var i = 0; i < ckeditor_ids.length; i++)
    {
        var editor = CKEDITOR.instances[ckeditor_ids[i]];
        if (editor) { editor.destroy(true); }

        CKEDITOR.replace(ckeditor_ids[i], {
            language: 'tr',
            enterMode: CKEDITOR.ENTER_BR
        });
    }

    CKEDITOR.config.toolbar = [
            ['Styles', 'Format', 'Font', 'FontSize'],
            '/',
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'Print'],
            '/',
            ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor', 'Source']
        ];
})