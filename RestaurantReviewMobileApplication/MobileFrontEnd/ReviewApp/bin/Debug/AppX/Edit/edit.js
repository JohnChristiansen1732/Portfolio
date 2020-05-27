(function () {
    var systemNavigationManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    var page = WinJS.UI.Pages.define("/Edit/edit.html", {
        ready: function (element, options) {
            systemNavigationManager.addEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.visible;
            preFillItem();
            editButton.addEventListener("click", edit, false);
        },
        unload: function () {
            systemNavigationManager.removeEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.collapsed;
            editButton.removeEventListener("click", edit);
        }
    });
    function backRequested() {
        WinJS.UI.Animation.exitPage(null).done(function () {
            WinJS.Navigation.navigate("/View/view.html", "edit");
        })
    }
    function preFillItem()
    {
        document.getElementById("name").value = sessionStorage.getItem('resturant');
        document.getElementById("address").value = sessionStorage.getItem('address');
        var ratingObject = new WinJS.UI.Rating(document.getElementById("rating"));
        ratingObject.userRating = sessionStorage.getItem('rating');
    }
    function edit()
    {
        if(document.getElementById("name").value == "" || document.getElementById("address").value == "")
        {
            document.getElementById("StatusMessage").innerHTML = "Resturant Name and Address are required";
        }
        else if (document.getElementById("name").value != sessionStorage.getItem('resturant'))
        {
            var reviewObj = {};
            reviewObj.username = sessionStorage.getItem('user');
            reviewObj.restName = sessionStorage.getItem('resturant');
            reviewObj.address = sessionStorage.getItem('address');
            reviewObj.rating = sessionStorage.getItem('rating');
            var options = {
                url: "http://resturantreviewsapi20160522100911.azurewebsites.net/api/resturantReviews",
                type: "DELETE",
                data: JSON.stringify(reviewObj),
                headers: { "Content-Type": "application/json;charset=utf-8" }
            }
            WinJS.xhr(options).done(
                function success(req) {
                    updateReview();
                }
                ,
                function error(err) {
                    document.getElementById("StatusMessage").innerHTML = "Error while storing data! - " + err.statusText + " - Check if has already been entered";
                    
                }
                    )
        }
        else
        {
            updateReview();
        }
    }

    function updateReview()
    {
        var reviewObj = {};
        reviewObj.username = sessionStorage.getItem('user');
        reviewObj.restName = document.getElementById("name").value;
        reviewObj.address = document.getElementById("address").value;
        reviewObj.rating = document.getElementById("rating").winControl.userRating;

        var options = {
            url: "http://resturantreviewsapi20160522100911.azurewebsites.net/api/resturantReviews",
            type: "PUT",
            data: JSON.stringify(reviewObj),
            headers: { "Content-Type": "application/json;charset=utf-8" }
        }
        WinJS.xhr(options).done(
            function success(req) {
                WinJS.Navigation.navigate("/View/view.html", "edit");
            }
            ,
            function error(err) {
                document.getElementById("StatusMessage").innerHTML = "Error while storing data! - " + err.statusText + " - Check if has already been entered";
            }
                )
    }

})();