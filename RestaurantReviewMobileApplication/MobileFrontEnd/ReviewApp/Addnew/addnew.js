(function () {
    var systemNavigationManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    var page = WinJS.UI.Pages.define("/Addnew/addnew.html", {
        ready: function (element, options) {
            systemNavigationManager.addEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.visible;
            addButton.addEventListener("click", add, false);
        },
        unload: function () {
            systemNavigationManager.removeEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.collapsed;
            addButton.removeEventListener("click", add);
        }
    });
    function backRequested() {
        WinJS.UI.Animation.exitPage(null).done(function () {
            WinJS.Navigation.navigate("/Welcome/welcome.html", "add");
        })
    }
    function add() {
        if (document.getElementById("name").value == "" || (!(document.getElementById("currentAddress").checked) && document.getElementById("address").value == "")){
            document.getElementById("StatusMessage").innerHTML = "Resturant Name is required required";
        }
        else
        {
            document.getElementById("StatusMessage").innerHTML = "Adding Review...";
            if (document.getElementById("currentAddress").checked)
            {
                getLocation();
            }
            else
            {
                addReview();
            }
        }
    }
    function getLocation() {
        var locator;

        if (locator == null) {
            locator = new Windows.Devices.Geolocation.Geolocator();
        }
        if (locator != null) {
            locator.getGeopositionAsync().then(getPositionHandler, errorHandler);
        }
    }
    function getPositionHandler(pos) {
        var point = pos.coordinate.point.position;
        url = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + point.latitude + "," + point.longitude;
        WinJS.xhr(
                { url: url }
            )
            .then
                (
                    function (response) {
                        var sourceData = JSON.parse(response.responseText);
                        var address = sourceData.results[0].formatted_address;
                        document.getElementById("address").value = address;
                        addReview();
                    }
                );
    }
    function errorHandler(e) {
        var status = locator.locationStatus;
        switch (status) {
            case Windows.Devices.Geolocation.PositionStatus.ready:
                console.log("status:ready");
                break;

            case Windows.Devices.Geolocation.PositionStatus.notInitialized:
                console.log("status:not Initiaized");
                break;
        }
    }
    function addReview()
    {
        var reviewObj = {};
        reviewObj.username = sessionStorage.getItem('user');
        reviewObj.restName = document.getElementById("name").value;
        reviewObj.address = document.getElementById("address").value;
        reviewObj.rating = document.getElementById("rating").winControl.userRating;

        var options = {
            url: "http://resturantreviewsapi20160522100911.azurewebsites.net/api/resturantReviews",
            type: "POST",
            data: JSON.stringify(reviewObj),
            headers: { "Content-Type": "application/json;charset=utf-8" }
        }
        WinJS.xhr(options).done(
            function success(req) {
                document.getElementById("StatusMessage").innerHTML = "Review for " + reviewObj.restName + " entered."
            }
            ,
            function error(err) {
                document.getElementById("StatusMessage").innerHTML = "Error while storing data! - " + err.statusText + " - Check if has already been entered";
            })
    }

})();