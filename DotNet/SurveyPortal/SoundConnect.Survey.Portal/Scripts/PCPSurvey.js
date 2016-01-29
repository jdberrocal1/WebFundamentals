$(document).ready(function () {
    $('input#txtbxQ1A3Specify').hide();

    $('#radioQ1A1').click(function () {
        $('input#txtbxQ1A3Specify').hide();
    });

    $('#radioQ1A2').click(function () {
        $('input#txtbxQ1A3Specify').hide();
    });

    $('#radioQ1A3').click(function () {
        $('input#txtbxQ1A3Specify').show();
    });
    

});