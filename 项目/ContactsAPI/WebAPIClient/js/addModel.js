// 点击“新增”按钮 创建的弹出框
$(".add").click(function (e) { 
    
    createModel("新增", "addyesbtn", "addnobtn","addyesclick()", "closeDom()");
});
// 点击“查询”按钮 创建的弹窗框
$(".select").click(function(){
    createModel("查询", "selectYesBtn", "selectNoBtn","selectyesclick()","closeDom()");
});
// “新增”、“查询” 按钮，共有的创建弹窗的函数
function createModel(title, yesbtnName, nobtnName, addyesclick, addnoclick) {
    
    var modelDom = $("<div>").attr("class", "modelDom");
    //$("body").append(modelDom);
    $(".container").after(modelDom);
    var centerdiv = $("<div>").attr("class", "container centerdiv");
    $(".modelDom").append(centerdiv);
    var title = $("<h2>").attr("class","text-center mt-4").html(title);
    $(".centerdiv").append(title);
    var row1 = $("<div>").attr("class", "row row1");
    $(".centerdiv").append(row1);
    var row1col1 = $("<div>").attr("class", "col-3");
    var row1col2 = $("<div>").attr("class", "col-6");
    $(".row1").append(row1col1, row1col2);
    var form = $("<form>");
    $(row1col2).append(form);
    var nameform = $("<div>").attr("class","form-group");
    $(form).append(nameform);
    var namelabel = $("<label>").html("姓名：");
    var nameinput = $("<input>").attr("class","form-control").attr({"type":"text","id":"name"});
    $(nameform).append(namelabel,nameinput);
    var phoneform = $("<div>").attr("class","form-group");
    $(form).append(phoneform);
    var phonelabel = $("<label>").html("电话号码：");
    var phoneinput = $("<input>").attr("class","form-control").attr({"type":"text","id":"phone","maxlength":"11"});
    $(phoneform).append(phonelabel,phoneinput);

    var idcardform = $("<div>").attr("class","form-group");
    $(form).append(idcardform);
    var idcardlabel = $("<label>").html("身份证号码：");
    var idcardinput = $("<input>").attr("class","form-control").attr({"type":"text","id":"idcard","maxlength":"18"});
    $(idcardform).append(idcardlabel,idcardinput);


    var row2 = $("<div>").attr("class","row row2");
    $(".centerdiv").append(row2);

    var row2col = $("<div>").attr("class","col-6 offset-3");
    $(".row2").append(row2col);

    var yesbutton = $("<button>").attr("class","btn btn-info float-left " + yesbtnName).html("确定").attr("onclick",addyesclick);
    var nobutton = $("<button>").attr("class","btn btn-warning float-right " + nobtnName).html("取消").attr("onclick",addnoclick);

    $(row2col).append(yesbutton, nobutton);
}