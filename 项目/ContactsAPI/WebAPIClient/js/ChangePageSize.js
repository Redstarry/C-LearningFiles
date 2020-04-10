// “显示数量的动画”
$(".PageSize li").click(function (e) { 
    $(".PageSize li").removeClass("active");
    $(this).addClass("active");
});
function timer() {
    window.setInterval(function() {
        var sj = Math.round(new Date() / 1000);
        var expsj = window.localStorage["expDate"];
        if(sj >= expsj)
        {
            $(window).attr("location", "../Login.html");
        }
    },1000);
}
timer();
var hidden, visibilityChange;
if (typeof document.hidden !== "undefined") {
    hidden = "hidden";
    visibilityChange = "visibilitychange";
} else if (typeof document.mozHidden !== "undefined") {
    hidden = "mozHidden";
    visibilityChange = "mozvisibilitychange";
} else if (typeof document.msHidden !== "undefined") {
    hidden = "msHidden";
    visibilityChange = "msvisibilitychange";
} else if (typeof document.webkitHidden !== "undefined") {
    hidden = "webkitHidden";
    visibilityChange = "webkitvisibilitychange";
}

document.addEventListener(visibilityChange,function(){
    if(document[hidden])
    {
        // 用户离开此页面的时候
        $(window).attr("location", "../Login.html");
    }
    else
    {

    }
},false);