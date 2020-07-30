$(document).ready(function () { 
    var overlay = $('#overlay'); 
    var open_modal = $('.open-modal'); 
    var close = $('.modal_close, #overlay'); 
    var modal = $('.modal_div'); 

    open_modal.click(function (event) { 
        event.preventDefault(); 
        var div = $(this).attr('href'); 
        overlay.fadeIn(400, 
            function () { 
                $(div) 
                    .css('display', 'block')
                    .animate({ opacity: 1, top: '55%' }, 200); 
            });
    });

    close.click(function () { 
        modal 
            .animate({ opacity: 0, top: '45%' }, 200, 
                function () { 
                    $(this).css('display', 'none');
                    overlay.fadeOut(400); 
                }
            );
    });
});