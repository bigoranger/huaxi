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
        if (window.location.pathname.toLocaleLowerCase() == $(this).attr('href').toLocaleLowerCase()) {
            $(".topNav").parent().removeClass('active');
            $(this).parent().addClass('active');
            return false;
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