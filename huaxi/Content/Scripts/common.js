$(function () {
    /*navselect*/
    $(".sideNav").each(function () {
        if (window.location.search.toLocaleLowerCase().indexOf($(this).attr('select').toLocaleLowerCase(), 1) > 0) {
            $(".sideNav").parent().removeClass('active');
            $(this).parent().addClass('active');
            return false;
        }
    });

    /*navselect*/
    $(".topNav").each(function () {
        var _host = window.location.pathname.toLocaleLowerCase();
        _host = 'http://' + _host;
        var _url = $(this).attr('href').toLocaleLowerCase();
        if (_host.indexOf(_url,1)>0||_host==_url) {
            $(".topNav").parent().removeClass('active');
            $(this).parent().addClass('active');
            return true;
        }
    });

    //全选的实现
    $(".check-all").click(function () {
        $(".id").prop("checked", this.checked);
    });
    $(".id").click(function () {
        var option = $(".id");
        option.each(function (i) {
            if (!this.checked) {
                $(".check-all").prop("checked", false);
                return false;
            } else {
                $(".check-all").prop("checked", true);
            }
        });
    });

    //ajax post submit请求
    $('.ajax-post').click(function () {
        var target, query, form;
        var target_form = $(this).attr('target-form');
        form = $('.' + target_form);
        target = $(this).attr('url');
        query = form.serialize();

        if (query.length > 0) {
            if ($(this).hasClass('confirm')) {
                if (!confirm('确认要执行该操作吗?')) {
                    return false;
                }
            }
        } else {
            alert("请选择要操作的项");
            return false;
        }

        if (target.indexOf('?') > 0) {
            target += '&' + query;
        } else {
            target += '?' + query;
        }

        window.location.href = target;
        return false;
    });


    $(function () {
        //搜索功能
        $("#search").click(function () {
            var url = $(this).attr('url');
            var query = $('.search-input').serialize();
            query = query.replace(/(&|^)(\w*?\d*?\-*?_*?)*?=?((?=&)|(?=$))/g, '');
            query = query.replace(/^&/g, '');

            if (url.indexOf('?') > 0) {
                url += '&' + query;
            } else {
                url += '?' + query;
            }
            window.location.href = url;
        });

        //回车自动提交
        $('.search-input').keyup(function (event) {
            if (event.keyCode === 13) {
                $("#search").click();
            }
        });

    })

})
function loading() {
    $("#load").remove();
    $("body").append('<div  id="load" style="z-index:99999; position:fixed; left:45%; top:30%"><img src="/Content/img/loading.gif" /></div>');
}
function removeloading() {
    $("#load").remove();
}
/*分页样式*/
function initPagination(selector) {
    selector = selector || '.page';
    $(selector).each(function (i, o) {
        var html = '<ul class="pagination">';
        $(o).find('a,span').each(function (i2, o2) {
            var linkHtml = '';
            if ($(o2).is('a')) {
                linkHtml = '<a href="' + ($(o2).attr('href') || '#') + '">' + $(o2).text() + '</a>';
            } else if ($(o2).is('span')) {
                linkHtml = '<a>' + $(o2).text() + '</a>';
            }

            var css = '';
            if ($(o2).hasClass('current')) {
                css = ' class="active" ';
            }

            html += '<li' + css + '>' + linkHtml + '</li>';
        });

        html += '</ul>';
        $(o).html(html).fadeIn();
    });
}

$(document).ready(function () {
    $(function () { $("[data-toggle='tooltip']").tooltip(); });
    $(function () { $('a[title]').tooltip(); });

    $('.active-step').on('click', function (e) {
        $('#myTab li a[href="' + $(this).attr('href') + '"]').trigger('click');
    })

    $('#myCarousel').carousel({
        interval: 5000
    });

    //Handles the carousel thumbnails
    $('[id^=carousel-selector-]').click(function () {
        var id_selector = $(this).attr("id");
        var id = id_selector.substr(id_selector.length - 1);
        var id = parseInt(id);
        $('#myCarousel').carousel(id);
    });


    // When the carousel slides, auto update the text
    $('#myCarousel').on('slid.bs.carousel', function (e) {
        var id = $('.item.active').data('slide-number');
        $('#carousel-text').html($('#slide-content-' + id).html());
    });

    $('#myTab a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })

    /*导航栏切换*/


    /* 侧边栏导航切换 */
    $(".list-group").find("a").each(function () {
        if (window.location.href.toLocaleLowerCase().indexOf($(this).attr("href").replace(".html", "").toLocaleLowerCase(), 1) > 0) {
            $(".list-group").children().removeClass("active");
            $(this).addClass("active");
        }
    });



    //自动显示下拉列表
    $('.dropdown-toggle').mouseenter(function () {
        $('.dropdown-menu').slideDown(200);
    });

    $('.dropdown').mouseleave(function () {
        $('.dropdown-menu').stop().slideUp(200);
    });

});

/*errormsg successmsg   msgbox*/
loadmsg();
function alertmsg(id, msg, intime, outtime) {
    id = '#' + id;
    $(id).stop();
    $(id).children('.msgbox').html(msg);
    $(id).fadeIn(intime);
    setTimeout(function () {
        $(id).fadeOut(outtime);
    },
    outtime);

}
function showerrormsg(msg, intime, outtime) {
    alertmsg('errormsg', msg, intime, outtime);
}
function showsuccessmsg(msg, intime, outtime) {
    alertmsg('successmsg', msg, intime, outtime);
}

function loadmsg() {
    $('#successmsg').remove();
    $('#errormsg').remove();
    $("body").append('<div id="successmsg" class=" text-center alertmsg"  role="alert"> <span class="alert alert-success  msgbox">msg</span> </div>');
    $("body").append('<div id="errormsg" class="text-center alertmsg"  role="alert"> <span class="alert alert-danger msgbox">msg</span> </div>');
}

function randomString(len) {
    len = len || 32;
    var $chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';
    /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/

    var maxPos = $chars.length;
    var pwd = '';
    for (i = 0; i < len; i++) {
        pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
    }
    return pwd;
}
function submitserach() {
    var _v = $.trim($('input[serach]').val());
    if (!_v) {
        return false;
    } else {
        return true;
    }
}