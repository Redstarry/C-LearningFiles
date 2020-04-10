
$(".login").click(function (e) { 
    var user = $("#user").val();
    var pwd = $("#pwd").val();
    if(user == "" && pwd == "")
    {
        alert("账号或密码错误");
        return;
    }
    var data = login(user,pwd);

});

function login(user, pwd) {
    var data = $.ajax({
        type: "Post",
        url: "https://localhost:4880/v1/contacts/login",
        data: JSON.stringify({
            "UserName":user,
            "Pwd":pwd
        }),
        contentType:"application/json;charset=utf-8",
        success: function (response) 
        {
            localStorage.setItem("Token" , response["result"]["jwttoken"]);
            localStorage.setItem("expDate",response["result"]["overdue"]);
            if(response["resultStatus"] == 1)
            {
                $(window).attr("location", "./html/index.html");
            }
            else
            {
                alert("账号或密码错误");
            }
        }
    });
}
