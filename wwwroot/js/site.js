import { post } from "jquery";

import { get } from "jquery";

//function EnviarDB() {
//    var cep = $("#CEP").val();
//    console.log(cep);
//    $.ajax({
//        url: 'Home/Create',
//        type: 'post',
//        data: { cep: $('#CEP') },
//        success: function (result) {
//            console.log(result);
//        },
//        error: function (result) {

//        }
//    });
//} 

//function ConsultarDB() {
//    location.href = '@Url.Action(ConsultaDB?=' + document.getElementById(CEP) + ', "Home")';
//}

$('#form2').submit(function () {
    var jqxhr = $.post('Home/Search', { "": $('#cep').val() })
        .success(function () {
            var loc = jqxhr.getResponseHeader('Location');
            var a = $('<a/>', { href: loc, text: loc });
            $('#message').html(a);
        })
        .error(function () {
            $('#message').html("Error posting the update.");
        });
    return false;
});