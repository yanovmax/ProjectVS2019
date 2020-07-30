$(document).ready(function () {
    var btnAddNewFeed = $('.btn.addNewFeed');

    btnAddNewFeed.click(function (event) {
        event.preventDefault();

        var idx = 0;
        var formContainer = $(this).closest(".form-container");
        //var action = formContainer.attr('data-action');
        var action = "/Home/AddNewFeed";
        var feedName = formContainer.find('#feedName').val();
        var feedLink = formContainer.find('#feedLink').val();

        if (feedLink) {

            var dataRequest = { name: feedName, link: feedLink };

            $.ajax({
                method: 'GET',
                contentType: "application/json",
                url: action,
                data: dataRequest,
                success: function (res) {
                    console.log("Result success !");
                    if (res) {
                        var objFeedData = $.parseJSON(res);
                        if (objFeedData.ResponseType == 1) {

                            var name = feedName || objFeedData.Name;
                            $('.left-side')
                                .find('.new-feeds ul')
                                .append("<li style='list-style: none; padding-top: 5px;'><a href='/Home/ShowFeedNews/?link=" + objFeedData.LinkAnchor + "' target='test1' style=' text-decoration: none;'>" + name + "</a></li > ")

                            //$('.left-side').find('.new-feeds').append("<li style='list-style: none;'><a data-link='" + objFeedData.LinkAnchor + "'>" + objFeedData.Name + "</a></li>");

                            $('.modal_div').animate({ opacity: 0, top: '45%' }, 100,
                                function () {
                                    $(this).css('display', 'none');
                                    $('#overlay').fadeOut(100);;
                                });
                        }
                        else if (objFeedData.ResponseType == 3) {
                            alert("No Result");
                        }
                        else if (objFeedData.ResponseType == 2)
                        {
                            alert("Bad Url :(  \nPlease enter the Url as RSS Feed type !");
                        }
                        else if (objFeedData.ResponseType == 0) {
                            alert("Bad jsonResult parsing :(")
                        }
                    }
                    else {
                        alert("No Result or Server response Error !");
                    }
                },
                error: function (a, b, c) {
                    alert(a.responseText);
                }
            });
        }
    });
});