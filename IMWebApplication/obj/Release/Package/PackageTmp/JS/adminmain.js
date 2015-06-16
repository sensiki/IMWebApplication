/*导航鼠标移上滑出*/
var lis = $('.content li');
lis.hover(function() {
    lis.eq($(this).index()).stop().animate({
        'width': '160',
        'margin-left': '0'
    }, 300);
}, function() {
    lis.eq($(this).index()).stop().animate({
        'width': '150',
        'margin-left': '10'
    }, 300);
});

/*选项卡点击切换*/
lis.click(function() {
    $(this).addClass('selected')
		.siblings().removeClass('selected');
    var index = $(this).index();
    $('.content div').eq(index).addClass('show')
		.siblings().removeClass('show');
});

/*文本框获取焦点时清空内容*/
$('.text').focus(function() {
    if ($(this).context.value == "请输入查询条件，如患者姓名或手机号" || $(this).context.value == "请输入查询条件，如医师姓名或手机号")
        $(this).val('');
});
/*文本框失去焦点时显示提示信息*/
$('.text').blur(function() {
    //if ($(this).context.value == "")
    //   $(this).val('请输入查询条件，如患者姓名或手机号');
});

/*点击创建病例，打开新页面*/
$('.createcase').click(function() {
    window.open("newcase.ashx");
});


/*全选*/
$(".checkall").click(function() {
    $(".task :checkbox").attr("checked", "true");
});

/*点击添加医师，打开天津爱医师页面*/
$('.createdoc').click(function() {
    window.open("newdoc.ashx");
});

/*点击添加医师中的修改按钮，打开医师信息修改界面*/
$('.revisedoc').click(function() {
    window.open("revisedoc.ashx");
});

/*点击诊治管理的tr后,背景颜色改变，打开详细页*/
$('.cure tr').click(function() {
    $(this).css({
        'background': '#ccc'
    });
    window.open("cureinfo.ashx");
});