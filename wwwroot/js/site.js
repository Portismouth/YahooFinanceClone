// Write your Javascript code.
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}
window.onclick = function (e) {
    if (!e.target.matches('.dropbtn')) {
        var myDropdown = document.getElementById("myDropdown");
        if (myDropdown.classList.contains('show')) {
            myDropdown.classList.remove('show');
        }
    }
}

//slide
$(document).on('ready', function () {
    $('#stock-name').keyup(function () {
        $.ajax({
            url: 'stock-index/find',
            method: 'get',
            data: $(this).parent().serialize(),
            success: function(serverResponse){
                console.log('success', serverResponse);

            }
        });
    });
});
    // $(function() {
    //     $("#stock-name").autocomplete({
    //         source: function(request,response){
    //             $.ajax( {
    //                 url: 'stock-index/find',
    //                 method: "post",
    //                 data: $(this).parent().serialize(),
    //                 success: function( data ) {
    //                     response( data );
    //                 }
    //             });
    //         },
    //         minLength: 2
    //     });
    // });

    // $("#stock-name").autocomplete({
    //     source: function (request, response) {
            
    //         $.ajax({
    //             url: 'stock-index/find',
    //             data: {
    //                 query: request.term
    //             },
    //             success: function(serverResponse){
    //                 console.log(serverResponse);
    //                 serverResponse.forEach(element => {
    //                     console.log(element["symbol"]);
    //                     console.log(element["companyName"]);
    //                 })
    //                 response(serverResponse);
    //             }
    //         });
    //     },
    //     select: function (event, ui) {
    //         console.log(ui.name)
    //     }
    //     })
