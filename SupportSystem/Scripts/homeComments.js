$(document).ready(function () {


    if ($("div[name = 'cbox']").length > 0) {

        $("div[name = 'cbox']").each(function (index) {
            brojKomentara++;
        });
    }

 

    
    $("#addComment").on("click", function (e) {
        e.preventDefault();
        MainCommentArea();
    });

    $(".comIndex").on("click", "#addComment", function (e) {
        e.preventDefault();
        MainCommentArea();
    });




    $("#commentArea").on("click", "#submitComment", function (e) {

        e.preventDefault();

        onDate = moment(new Date()).format("YYYY-MM-DD HH:mm:ss.SSS");
        komentar = $("#commentTextBox").val();

        //save and submit comment  <-----
        if (komentar) {

            if (cancelMainComment) {
                SaveComment(false);
                SubmitNewComment(brojKomentara, false);
            } else {
                SaveComment(true);
                SubmitNewComment(brojKomentara, true);
            }
        }

        $("#txtArea").remove();
        $("#addComment").text("Add Comment");
        cancelMainComment = false;
        prikazano = false;
        $("#btnReply").remove();
        $(this).css("padding-bottom", "10px");
        replyButton = false;
        brojKomentara++;

    });



    var replyButton = false;
    $("#commentArea").on('mouseenter', '#commentText', function () {

        var upisivanje = $("#commentTextBox").val() === undefined ? 0 : $("#commentTextBox").val().length;

        if (!replyButton && upisivanje === 0) {
            $("#txtArea").remove();
            prikazano = false;
            cancelMainComment = false;
            cancelReply = false;
            $("#addComment").text("Add Comment");
            $(this).append(`<a href="#" id="btnReply" class="btn btn-info btn-xs">Add Reply</a>`);
            $(this).css("padding-bottom", "30px");
            replyButton = true;
        }

    }).on('mouseleave', '#commentText', function () {

        if (!cancelReply && !prikazano) {

            if (replyButton) {
                $("#btnReply").remove();
                $(this).css("padding-bottom", "10px");
                replyButton = false;
            }
        }
    });
    
  
    $("#commentArea").on('click', '#btnReply', function (e) {

        e.preventDefault();

        if (!cancelReply) {
            $(this).text("Cancel Reply");

            if (cancelMainComment) {
                $("#txtArea").remove();
                $("#addComment").text("Add Comment");
                cancelMainComment = false;
                prikazano = false;                
            }
            var boxId = $(this).parent().parent().attr('id')
            AddCommentArea("Reply", boxId);
            $("#txtArea").css({ "margin-left": "10px", "margin-right": "10px" });
            cancelReply = true;
        } else {
            $(this).text("Add Reply");
            $("#txtArea").remove();
            cancelReply = false;
            prikazano = false;
        }


    });


    // kraj document ready
});

var cancelReply = false;
var cancelMainComment = false;
function MainCommentArea() {
    if (!cancelMainComment) {

        if (cancelReply) {
            $("#addComment").text("Add Reply");
            $("#txtArea").remove();
            $("#btnReply").remove();
            cancelReply = false;
            prikazano = false;
        }
        AddCommentArea("New");
        $("#addComment").text("Cancel");
        cancelMainComment = true;
    } else {

        $("#txtArea").remove();
        $("#addComment").text("Add Comment");
        prikazano = false;
        cancelMainComment = false;
    }
}



var idComment;
var onDate;
function SaveComment(isReply) {

    idComment = NewGuid();  

    if (!isReply) {
        dataid = idComment; 
    } else {
        dataid = $("#btnReply").parent().parent().attr("data-id");
    }


    $.ajax({
        type: "POST",
        url: "/Home/SaveComment",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            Id: idComment,
            Comment: komentar,
           // User: userName,
            IdReply: (isReply ? dataid : "0"),
            OnDate: onDate
        }),
        success: function (response) {
            if (response !== null) {
                //idComment = response;
                //alert(idComment);

                if (window.location.pathname === "/Home/Edit/" || window.location.pathname === "/Home/Edit") {
                    toastr["warning"](`Your comment will be saved after <b><i>Save Edited Suggestion</i></b><br>You currently got ${response} stored comments.`).css("width", "450px");
                }

                if (window.location.pathname === "/Home/Index/" || window.location.pathname === "/Home/Index" || window.location.pathname === "/") {
                    SaveSigleComment(IdMain);   
                }

            } else {
                alert("Error");
            }
        }
    });
}



function NewGuid() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    )
}


function ClearTempComments() {
    $.ajax({
        type: "GET",
        url: "/Home/ClearTempComments",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        success: function (response) {
            if (response !== null) {
                //alert(idComment);
            } else {
                alert("Error");
            }
        }
    });
}


var prikazano = false;
function AddCommentArea(replyOrNew, boxId) {

    if (!prikazano) {
        var html = (`<div id="txtArea" class="row form-group">
                <textarea class="form-control" rows="5" id="commentTextBox"></textarea>
                <a href="#" class="btn btn-warning btn-sm" id="submitComment">Submit</a></div>`);

        if (replyOrNew === "New") {
            $("#komentari").after(html);
        } else if (replyOrNew === "Reply") {
            $(`#${boxId}`).after(html);
        }

        prikazano = true;
    }
}

var commentsForEmial = "";
var emailHtml = `<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <title>Email</title>
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
</head>
<body>
  <center>
    <div style="border:2px solid black; margin: 20px 10px; padding: 25px 0; border-radius:40px; border-color: rgba(19,6,160,0.8)">
    <table width="70%">
        ${commentsForEmial}
    </table>
    </div>    
  </center>  
</body>
</html>`;


var dataid;
var komentar;
function SubmitNewComment(brBox, isReply) {

    var date = moment(onDate).format("YYYY-MM-DD hh:mm:ss");
    //var emailHtml = ``

    if (isReply) {

        var commentBox = $("#btnReply").parent().parent().attr(`id`);

        var attr = $(`#${commentBox}`).parent().attr('data-id');

        if (typeof attr !== typeof undefined && attr !== false) {

            var newBox = $(`#${commentBox}`).parent().attr('id');

            $(`#${newBox}`).append(`<div id="commentBox-${brBox}" data-id="${dataid}" name="cbox" class="row">
                                        <div id="commentHeader">
                                            <p>Posted by: ${userName} on ${date}</p>
                                        </div>
                                        <div id="commentText">
                                        <p>${komentar}</p>
                                        </div>
                                     </div>`);
            commentsForEmial += `<tr>
                                    <td>
                                    <div style="padding-left: 15px; margin-top:30px;  font-size: 0.8em; font-style: italic; text-align:left">
                                        <p>Posted by: ${userName} on ${date}</p>
                                    </div>
                                    <div style="padding: 5px 15px; background-color: rgba(135, 182, 229, 0.6); border-radius: 8px;">
                                        <p>${komentar}</p>
                                    </div>
                                    </td>
                                 </tr>`;

        } else {

            $(`#${commentBox}`).append(`<div id="commentBox-${brBox}" data-id="${dataid}" name="cbox" class="row">
                                        <div id="commentHeader">
                                            <p>Posted by: ${userName} on ${date}</p>
                                        </div>
                                        <div id="commentText">
                                        <p>${komentar}</p>
                                        </div>
                                     </div>`);

            commentsForEmial += `<tr>
                                    <td>
                                    <div style="padding-left: 15px; margin-top:30px;  font-size: 0.8em; font-style: italic; text-align:left">
                                        <p>Posted by: ${userName} on ${date}</p>
                                    </div>
                                    <div style="padding: 5px 15px; background-color: rgba(135, 182, 229, 0.6); border-radius: 8px;">
                                        <p>${komentar}</p>
                                    </div>
                                    </td>
                                 </tr>`;
        }


    } else {

        $("#commentArea").append(`<div id="commentBox-${brBox}" data-id="${dataid}" name="cbox" class="row">
                                        <div id="commentHeader">
                                            <p>Posted by: ${userName} on ${date}</p>
                                        </div>
                                        <div id="commentText">
                                        <p>${komentar}</p>
                                        </div>
                                     </div>`);

        commentsForEmial += `<tr>
                                    <td>
                                    <div style="padding-left: 15px; margin-top:30px;  font-size: 0.8em; font-style: italic; text-align:left">
                                        <p>Posted by: ${userName} on ${date}</p>
                                    </div>
                                    <div style="padding: 5px 15px; background-color: rgba(135, 182, 229, 0.6); border-radius: 8px;">
                                        <p>${komentar}</p>
                                    </div>
                                    </td>
                                 </tr>`;
    }


}


var brojKomentara = 1;
var checkIdMain = "";
function PrikaziKomentare(idMain, indexPage) {

    if (checkIdMain !== idMain) {

        var mainBox;

        $.ajax({
            type: "GET",
            url: "/Home/CommentsByRowId",
            data: { id: `${idMain}` },
            dataType: "json",
            success: function (data) {        

                if (indexPage) {
                    $('div[name="cbox"]').remove();
                    $("#addComment").remove();
                    $("#commentArea").prepend(`<a href="#" class="btn btn-success" id="addComment">Add Comment</a>`);
                }


                $.each(data, function (index, item) {

                    if (item.IdReply === "0") {
                        $("#commentArea").append(`<div id="commentBox-${brojKomentara}" data-id="${item.Id}" name="cbox" class="row">
                                    <div id="commentHeader">
                                        <p>Posted by: ${item.User} on ${item.OnDate}</p>
                                    </div>
                                    <div id="commentText">
                                    <p>${item.Comment}</p>
                                    </div>
                                 </div>`);

                        mainBox = brojKomentara;
                        brojKomentara++;

                    } else {

                        $(`#commentBox-${mainBox}`).append(`<div id="commentBox-${brojKomentara}" data-id="${item.IdReply}" name="cbox" class="row">
                                    <div id="commentHeader">
                                        <p>Posted by: ${item.User} on ${item.OnDate}</p>
                                    </div>
                                    <div id="commentText">
                                    <p>${item.Comment}</p>
                                    </div>
                                 </div>`);
                        brojKomentara++;
                    }

                });

            }

        });

        checkIdMain = idMain
    }

}


function SaveSigleComment(id) {
    $.ajax({
        type: "POST",
        url: "/Home/SaveSingleCommentInDb",
        data: "id=" + id,
        success: function (response) {
            if (response !== null) {
                toastr["info"](`${response}`);
                if (commentsForEmial !== "") {
                    SendCommentsMail("arandjic@gmail.com", "New Comment from Index Page.", commentsForEmial)
                    commentsForEmial = "";
                }   //<--------------------------------------- send mail
            }           
        }
    });

}

function SendCommentsMail(email, subject, msg) {
    $.ajax({
        type: "POST",
        url: "/Home/SendMailWithComments/",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            toEmail: email,
            subject: subject,
            msg: msg
        }),
        success: function (response) {
           // alert(response);
        }
    });

}