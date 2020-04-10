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
var currentPage; //当前页
var totalPages; //总页数
var nextlink; //下一页的请求url
var prve; //上一页的请求url
var pageSize;
// console.log(responseData);
// 如果全部查询返回的不是null，就把数据提取出来。
if(responseData != null)
{
    currentPage = responseData["currentPage"];
    totalPages = responseData["totalPages"];
    nextlink = responseData["nextLink"];
    prve = responseData["previousLink"];
    pageSize = responseData["pageSize"];
}
// 下一页的点击事件
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
// 上一页的点击事件
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
// 上一页和下一页的ajax请求
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
            pageSize = responseData["pageSize"];
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
        
        var li = $("<li>").attr("class","page-item pageNum");
        $("ul.pagination li").last().before(li);
        var a = $("<a>").attr({"class":"page-link","href":"#"}).html(i);
        // console.log(i);
        $(li).append(a);
    }
}
// 页码点击事件
$(".page .pageNum").click(function (e) { 
    var index = $(".page .active").index();
    var indeclick = $(this).index();
    $(this).addClass("active");

    $(".page li").eq(index).removeClass("active");
    var url = "https://localhost:4880/v1/contacts?pageNumber=" + indeclick + "&pageSize=" + pageSize;
    SendAjax(url);
});
// 全部查询的数据显示。
function ShowData(data) {
    // console.log(data)
    $.each(data,function(key,val) {
        var tr = $("<tr>")
        $("tbody").append(tr);
        // console.log(key,val)
        var td1 = $("<td>").attr({"class":"SerialNumber","color":"white"}).html(key + 1).css("color","white");
        var td2 = $("<td>").attr({"class": "Name","color":"white"}).html(val["name"]).css("color","white");
        var td3 = $("<td>").attr({"class": "Phone","color":"white"}).html(val["phone"]).css("color","white");
        var td4 = $("<td>").attr({"class": "IdCard","color":"white"}).html(val["idCard"]).css("color","white");
        var td5 = $("<td>");
        $(tr).append(td1,td2,td3,td4,td5);

        var a1 = $("<a>").attr({"class":"btn btn-warning", "role":"button", "href":"#","onclick":"updateData.call(this)"}).html("更新");
        var a2 = $("<a>").attr({"class":"btn btn-danger ml-4", "role":"button", "href":"#","onclick":"removeData.call(this)"}).html("删除");
        var a3 = $("<div>").css("display","none").html(val["id"]);

        $(td5).append(a1,a2,a3);
    })
}
// “更新” 按钮事件
function updateData() {
    var name = $(this).parent().siblings(".Name").html();
    var phone = $(this).parent().siblings(".Phone").html();
    var idcard = $(this).parent().siblings(".IdCard").html();
    var id = $(this).siblings("div").html();
    console.log(id);
    var groupDiv = $("<div>").attr("class","form-group");
    var idform = $("<label>").html("Id：");
    var inputform = $("<input>").attr({"class":"form-control","type":"text","id":"GuId","disabled":"disabled"}).val(id);
    
    
    createModel("更新","updateYesBtn", "updateNoBtn","updateyesclick.call(this)","closeDom()");
    $(".centerdiv form").prepend(groupDiv);
    $(groupDiv).append(idform,inputform);
    $("#name").val(name);
    $("#phone").val(phone);
    $("#idcard").val(idcard);
}

// "更新" 按钮中的 “确定” 事件
function updateyesclick() {
    var name = $("#name").val();
    var phone = $("#phone").val();
    var idcard = $("#idcard").val();
    var id = $("#GuId").val();
    $.ajax({
        type: "put",
        url: "https://localhost:4880/v1/contacts/" + id,
        data: JSON.stringify({
            "Name":name,
            "Phone":phone,
            "IdCard":idcard
        }),
        contentType:"application/json;charset=utf-8",
        headers:{
            "Authorization":"Bearer " + window.localStorage["Token"]
        },
        success: function (response) {
            if(response["resultStatus"] >= 1)
            {
                var el = "div:contains(" + response["result"]["id"] + ")";
                $(el).eq(1).parent().siblings(".Name").html(response["result"]["name"]);
                $(el).eq(1).parent().siblings(".Phone").html(response["result"]["phone"]);
                $(el).eq(1).parent().siblings(".IdCard").html(response["result"]["idCard"]);
                closeDom();
            }
            else
            {
                console.log(response);
                console.log(response["message"]);
            }
        }
    });
}
// "删除" 按钮事件
function removeData() {
    var id = $(this).siblings("div").html();
    var resultData;
    $.ajax({
        type: "delete",
        url: "https://localhost:4880/v1/contacts/" + id,
        contentType:"application/json;charset=utf-8",
        headers:{
            "Authorization":"Bearer " + window.localStorage["Token"]
        },
        success: function (response) {
            if(response["resultStatus"] >= 1)
            {
                alert(response["message"]);
                window.location.reload();
                // SendAjax("https://localhost:4880/v1/contacts");
            }
            else
            {
                alert(response["message"]);
            }
            
        }
    });
    // console.log(resultData);
    // if(resultData["resultStatus"] >= 1)
    // {
    //     alert(resultData["message"]);
    //     // SendAjax("https://localhost:4880/v1/contacts");
    //     window.location.reload();
    // }
    // else
    // {
    //     alert(resultData["message"]);
    // }
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

function selectyesclick(params) {
    var name = $("#name").val();
    var phone = $("#phone").val();
    var idcard = $("#idcard").val();
    var pageSize = $(".PageSize .active").find("a").html();
    
    if(name == "" && phone =="" && idcard == "")
    {
        var url = "https://localhost:4880/v1/contacts?pageNumber=1"  + "&pageSize=" + pageSize;
        SendAjax(url)

        $(".firstPage").siblings(".pageNum").remove(".pageNum");
        PageShow(totalPages);
        closeDom();
    }
    else
    {
        $.ajax({
            type: "post",
            url: "https://localhost:4880/v1/contacts/propselect",
            data: JSON.stringify({
                "name" : name,
                "phone": phone,
                "idcard" : idcard
            }),
            contentType: "application/json;charset=utf-8",
            headers:{
                "Authorization":"Bearer " + window.localStorage["Token"]
            },
            success: function (response) {
                $("tbody").empty();
                ShowData(response["result"]);
                $(".firstPage").siblings(".pageNum").remove(".pageNum");
                console.log(totalPages)
                closeDom();
            }
        });
    }
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