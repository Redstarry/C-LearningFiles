// 查询全部
var responseData = (function() {
    $.ajax({
        async:false,
        type: "get",
        url: "https://localhost:4880/v1/contacts",
        contentType: "application/json;charset=utf-8",
        headers:{"Authorization":"Bearer " + window.localStorage["Token"]},
        success: function (response) {
            // console.log(response);
            ShowData(response["data"]);
            PageShow(response["totalPages"]);
            responseData = response;
        }
    });
    return responseData
})();
var currentPage; 
var totalPages; 
var nextlink; 
var prve; 
console.log(responseData);
if(responseData != null)
{
    currentPage = responseData["currentPage"];
    totalPages = responseData["totalPages"];
    nextlink = responseData["nextLink"];
    prve = responseData["previousLink"];
}
$(".next").click(function (e) { 
   var index = $(".page .active").index();
   if(index == totalPages - 1)
   {
       $(".next").parent().addClass("disabled");
   }
   if(currentPage != totalPages)
   {
        SendAjax(nextlink);
       $(".page li").eq(currentPage).addClass("active");
       $(".page li").eq(currentPage-1).removeClass("active");
       $(".prev").parent().removeClass("disabled");
   }
   
})
$(".prev").click(function(){
    
    var index = $(".page .active").index();
    if(index == 2)
    {
        $(".prev").parent().addClass("disabled");
    }
    if(index > 1)
    {
        $(".page li").eq(index-1).addClass("active");
        $(".page li").eq(index).removeClass("active");
        $(".next").parent().removeClass("disabled");
        SendAjax(prve);
    }
    
})

function SendAjax(url) {
    $.ajax({
        async:false,
        type: "get",
        url: url,
        contentType:"application/json;charset=utf-8",
        headers:{"Authorization":"bearer " + window.localStorage["Token"]},
        success: function (response) {
            $("tbody").empty();
            ShowData(response["data"]);
            nextlink = response["nextLink"];
            currentPage = response["currentPage"];
            totalPages = response["totalPages"];
            prve = response["previousLink"];
            // GetData(response);
        }
    });
}
// 页码显示函数
function PageShow(totalPages) {
    if(totalPages == 1)
    {
        return;
    }
    for (var i = 2; i <= totalPages; i++) {
        
        var li = $("<li>").attr("class","page-item");
        $("ul.pagination li").last().before(li);
        var a = $("<a>").attr({"class":"page-link","href":"#"}).html(i);
        // console.log(i);
        $(li).append(a);
    }
}

// 全部查询的数据显示。
function ShowData(data) {
    // console.log(data)
    $.each(data,function(key,val) {
        var tr = $("<tr>")
        $("tbody").append(tr);
        // console.log(key,val)
        var td1 = $("<td>").html(key + 1);
        var td2 = $("<td>").html(val["name"]);
        var td3 = $("<td>").html(val["phone"]);
        var td4 = $("<td>").html(val["idCard"]);
        var td5 = $("<td>");
        $(tr).append(td1,td2,td3,td4,td5);

        var a1 = $("<a>").attr({"class":"btn btn-warning", "role":"button", "href":"#"}).html("更新");
        var a2 = $("<a>").attr({"class":"btn btn-danger ml-4", "role":"button", "href":"#"}).html("删除");

        $(td5).append(a1,a2);
    })
}

// 新增数据的确认按钮事件
function addyesclick(params) {
    var name = $("#name").val();
    var phone = $("#phone").val();
    var idcard = $("#idcard").val();
    if(!addvalid(phone, idcard, name))
    {
        return;
    }
    $.ajax({
        type: "post",
        url: "https://localhost:4880/v1/contacts",
        data: JSON.stringify({
            "name":name,
            "phone":phone,
            "idcard":idcard
        }),
        headers:{"Authorization": "Bearer " + window.localStorage["Token"]},
        contentType:"application/json;charset=utf-8",
        success: function (response) {
            if(response["resultStatus"] == 1)
            {
                alert(response["message"]);
                $("#phone").val("");
                $("#name").val("");
                $("#idcard").val("");
                return;
            }
            alert( response["message"]);
        }
    });
}

function addnoclick(params) {
    closeDom();
}
// 验证 姓名，电话，身份证是否合理。
function addvalid(phone, idcard, name) {
    var reg = new RegExp(/^\d{11}$/);
    var reg15 = new RegExp(/^\d{15}$/);
    var reg18 = new RegExp(/^\d{18}$/);
    if(phone == "" || idcard == "" || name == "")
    {
        alert("姓名，电话，身份证 都不能为空");
        return false;
    }
    if(!(phone.length == 11 && reg.test(phone)) )
    {
        alert("电话号码错误,请输入11位手机号码");
        return false;
    }
    if(!((idcard.length == 18 || idcard.length == 15) && (reg15.test(idcard) || reg18.test(idcard))))
    {
        alert("身份证号码错误,请输入15-18位身份号码");
        return false;
    }
    return true;

}

function closeDom() {
    $(".modelDom").remove();
}