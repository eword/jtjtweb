var shareContent = '关注"'+document.title+'"请猛击：';
function sharesina() {
    var param = {
        url: location.href,
        type: '3',
        count: '', /**是否显示分享数，1显示(可选)*/
        appkey: '', /**您申请的应用appkey,显示分享来源(可选)*/
        title:shareContent, /**分享的文字内容(可选，默认为所在页面的title)*/
        pic: '', /**分享图片的路径(可选)*/
        ralateUid: '', /**关联用户的UID，分享微博会@该用户(可选)*/
        language: 'zh_cn', /**设置语言，zh_cn|zh_tw(可选)*/
        rnd: new Date().valueOf()
    }
    var temp = [];
    for (var p in param) {
        temp.push(p + '=' + encodeURIComponent(param[p] || ''))
    }
    var url_str = 'http://v.t.sina.com.cn/share/share.php?' + temp.join('&');
    window.open(url_str, target = 'blank');
}

function shareQQ() {
    var param = {
        url: location.href,
        title: shareContent /**分享的文字内容(可选，默认为所在页面的title)*/

    }
    var temp = [];
    for (var p in param) {
        temp.push(p + '=' + encodeURIComponent(param[p] || ''))
    }
    var url_str = 'http://v.t.qq.com/share/share.php?' + temp.join('&');
    window.open(url_str, target = 'blank');
}
function shareRenRen() {
var rrShareParam = {
    resourceUrl: location.href, //分享的资源Url
			srcUrl : '',	//分享的资源来源Url,默认为header中的Referer,如果分享失败可以调整此值为resourceUrl试试
			pic : '',		//分享的主题图片Url
			title: shareContent, /**分享的文字内容(可选，默认为所在页面的title)*/
			description: shareContent + location.href	//分享的详细描述
		};
		rrShareOnclick(rrShareParam);
}
function shareKX() {
    var param = {
        url: location.href,
        style:"11",
        content: shareContent /**分享的文字内容(可选，默认为所在页面的title)*/

    }
    var temp = [];
    for (var p in param) {
        temp.push(p + '=' + encodeURIComponent(param[p] || ''))
    }
    var url_str = 'http://www.kaixin001.com/rest/records.php?' + temp.join('&');
    window.open(url_str, target = 'blank');
}
function shareDB() {
    var param = {
        href: location.href,
        name: shareContent + location.href /**分享的文字内容(可选，默认为所在页面的title)*/

    }
    var temp = [];
    for (var p in param) {
        temp.push(p + '=' + encodeURIComponent(param[p] || ''))
    }
    var url_str = 'http://shuo.douban.com/!service/share?' + temp.join('&');
    window.open(url_str, target = 'blank');
}

function attention() {
    var param = {
        url: location.href,
        uids: "2058705335",
        wide: "2",
        color: "C2D9F2,FFFFFF,0082CB,666666",
        showtitle: "0",
        showinfo: "0",
        sense: "0",
        verified: "0",
        count: "1",
        refer: location.href,
        dpc: "1",
        language: 'zh_cn' /**设置语言，zh_cn|zh_tw(可选)*/
    }
    var temp = [];
    for (var p in param) {
        temp.push(p + '=' + encodeURIComponent(param[p] || ''))
    }
    var url_str = 'http://widget.weibo.com/relationship/bulkfollow.php?' + temp.join('&');
    window.open(url_str, target = 'blank');
}