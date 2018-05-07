﻿var lastId,
    topMenu = $("#ul_nawigacja"),
    topMenuHeight = topMenu.outerHeight() + 15,
    // Wszystkie elementy listy
    menuItems = topMenu.find("a"),
    // Kotwice do pozycji menu
    scrollItems = menuItems.map(function () {
        var item = $($(this).attr("href"));
        if (item.length) { return item; }
    });
//$('body').prepend('<a href="#" class="back-to-top">Back to Top</a>');

var amountScrolled = 300;

//var idUserValue = "";

//var imagePathUserAvatar;

//$('a.back-to-top').click(function () {
//    $('html, body').animate({
//        scrollTop: 0
//    }, 900);
//    return false;
//});

var userID;

menuItems.click(function (e) {
    var href = $(this).attr("href"),
        offsetTop = href === "#" ? 0 : $(href).offset().top - topMenuHeight + 1;
    $('html, body').stop().animate({
        scrollTop: offsetTop
    }, 1000);
    e.preventDefault();
});

$(window).scroll(function () {

    var fromTop = $(this).scrollTop() + topMenuHeight;


    var cur = scrollItems.map(function () {
        if ($(this).offset().top < fromTop)
            return this;
    });

    cur = cur[cur.length - 1];
    var id = cur && cur.length ? cur[0].id : "";

    if (lastId !== id) {
        lastId = id;

        menuItems
            .parent().removeClass("active")
            .end().filter("[href=#" + id + "]").parent().addClass("active");
    }

    if ($(window).scrollTop() > amountScrolled) {
        $('a.back-to-top').fadeIn('slow');
    } else {
        $('a.back-to-top').fadeOut('slow');
    }
});

$(document).ready(function () {

    // Przycisk rejestracji w menu
    //$("#regBtn").click(function () {
    //    document.getElementById("regiserForm").reset();
    //    $("#regModal").modal('show');
    //});

    $("#loginBtn").click(function () {
        document.getElementById("loginForm").reset();
        $("#logModal").modal('show');
    });
    // Przycisk rejestracji w modalu rejestracji

    //$("#regButton").click(function () {
    //    var data = $("#regiserForm").serialize();
    //    $("#loadingDiv").show();

    //    $.ajax({
    //        type: "POST",
    //        dataType: "json",
    //        url: "/Main/AddNewUser",
    //        data: data,
    //        success: function (response) {
    //            $("#loadingDiv").hide();
    //            $("#regModal").modal('hide');

    //            console.log(response.message);

    //            if (response.message) {
    //                bootbox.alert("Wykonano zapis");
    //            } else {
    //                bootbox.alert("Nie dokonano zapisu");
    //            }


    //        },
    //        error: function (res) {
    //            bootbox.alert("Nie dokonano zapisu");
    //        }

    //    });

    //});

    $('#loginButton').click(function () {

        var data = $("#loginForm").serialize();

        $.ajax({
            type: "POST",
            dataType: 'html',
            url: '/Main/LoginOperation',
            data: data,
            success: function (response) {
                $("#logModal").modal('hide');
                console.log(response);
               // $('#viewport').html(response);

                if (response === "") {
                    bootbox.alert('Użytkownik nie istnieje');
                } else {
                    $('body').css("background-color", "white");
                    $('body').css('background-image', 'none');
                    $('#stronaGlowna').html(response);

                }


            },
            error: function (error) {
                bootbox.alert("Napotkano problem");
            }
        });
    });


    

});



function DeleteUserModal(userId) {

 

    userID = userId;
    $('#userDeleteModal').modal('show');



}

function DeleteUserFunc() {

    $.ajax({
        type: "POST",
        dataType: 'html',
        url: '/Main/DeleteUser',
        data: { id: userID },
        success: function (response) {
            $("#userDeleteModal").modal('hide');
           // console.log(response);
           // $('#viewport').html(response);

            if (response === "") {
                bootbox.alert('Operacja usunięcia nieudana');
            } else {
                bootbox.alert('Pomyślnie usunięto');
                $('#adminUserTableContener').html(response);
            }


        },
        error: function (error) {
            bootbox.alert("Napotkano problem");
        }
    });

}

function UpdateUserModal(id) {
    //userID = id;
    $("#userIdHiddenInput").val(id);
    $("#updModal").modal("show");
}

function UpdateUserFunc() {

    var dataForm = $("#updateUserForm").serialize();

    $.ajax({
        type: "POST",
        dataType: 'html',
        url: '/Main/UpdateUser',
        data: dataForm,
        success: function (response) {
            $("#updModal").modal('hide');
            // console.log(response);
            // $('#viewport').html(response);

            if (response === "") {
                bootbox.alert('Operacja aktualizacji nieudana');
            } else {
                bootbox.alert('Pomyślnie zaktualizowano');
                $('#adminUserTableContener').html(response);
            }


        },
        error: function (error) {
            bootbox.alert("Napotkano problem");
        }
    });
}

var hideUTContener = function () {

    $("#adminUserTableContener").hide();
    $("#adminAddNewUserContener").show();
    $("#adminPartPosition").hide();
}

var hideANUContener = function () {

    $("#newAddUserForm")[0].reset();
    $("#adminUserTableContener").show();
    $("#adminAddNewUserContener").hide();
    $("#adminPartPosition").hide();

}

var hide2ActiveContener = function () {

    $("#adminUserTableContener").hide();
    $("#adminAddNewUserContener").hide();
    $("#adminPartPosition").show();
}

var AddNewUserFunc = function () {

   var dataForm = $("#newAddUserForm").serialize();

    $.ajax({
        type: "POST",
        dataType: 'html',
        url: '/Main/AddUser',
        data: dataForm,
        success: function (response) {
            
            // console.log(response);
            // $('#viewport').html(response);

            if (response === "") {
                bootbox.alert('Operacja dodania nieudana. \n Proszę sprawdzić dane formularza.');
            } else {
                bootbox.alert('Pomyślnie dodano');
                $('#adminUserTableContener').html(response);
            }


        },
        error: function (error) {
            bootbox.alert("Napotkano problem");
        }
    });
}