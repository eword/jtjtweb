
function ajaxLoad(_boxID, _url, _imgUrl) {
    $.ajax({
        type: "get",
        url: _url,
        cache: false,
        async: true,
        dataType: "html",
        contentType: 'text/html;charset=utf-8', //编码格式 
        beforeSend: function (data) {
            $(_boxID).html('');
            //$(_boxID).append('加载中……');
            $(_boxID).append('<img alt=\"列表加载中……\" width=\"100\" height=\"100\"  src=\"' + _imgUrl + '\" ');
        }, //发送请求前
        success: function (Htmlobj) {
            $(_boxID).html(Htmlobj);
        },
        error: function (data) {
            $(_boxID).html('failed to load data.')
        } //请求错误 
    });
}