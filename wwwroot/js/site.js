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

//     $(".regular").slick({
//         dots: false,
//         infinite: false,
//         slidesToShow: 4,
//         slidesToScroll: 4
//     });

    $('#stock-name').keyup(function () {
        console.log("here");
        console.log("parent's serialize returned ", $(this).parent().serialize())
        $.ajax({
            url: 'stock-index/find',
            method: 'post',
            data: $(this).parent().serialize(),
            success: function(serverResponse){
                console.log('success', serverResponse);
            }
        });
    });

});