(function () {
    var systemNavigationManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    var page = WinJS.UI.Pages.define("/View/view.html", {
        ready: function (element, options) {
            systemNavigationManager.addEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.visible;
            getReviews();

        },
        unload: function () {
            systemNavigationManager.removeEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.collapsed;
            }
    });

    function backRequested() {
        WinJS.UI.Animation.exitPage(null).done(function () {
            WinJS.Navigation.navigate("/Welcome/welcome.html", "view");
        })
    }

    function getReviews()
    {
        var user = sessionStorage.getItem('user');
        var url = "http://resturantreviewsapi20160522100911.azurewebsites.net/api/resturantReviews/" + user;
        
        WinJS.xhr(
            {url: url}
             )
            .then
                (
                       function (response) {
                           var sourceData = JSON.parse(response.responseText);
                           var reviews = sourceData;
                           for (var i in reviews) {
                               var name = reviews[i].restName;
                               var address = reviews[i].address;
                               var rating = reviews[i].rating;

                               var div = document.createElement("div");
                               div.innerHTML = "Name: " + name;
                               var element = document.getElementById("reviews");
                               element.appendChild(div);

                               var div = document.createElement("div");
                               div.innerHTML = "Address: " + address;
                               element.appendChild(div);

                               var div = document.createElement("div");
                               var ratingObject = new WinJS.UI.Rating(div);
                               ratingObject.userRating = rating;
                               ratingObject.disabled = true;
                               element.appendChild(div);
                               var br = document.createElement("br");
                               element.appendChild(br);

                               var button = document.createElement("submit");
                               button.className = "win-button";
                               button.innerHTML = "EDIT";
                               button.id = name;
                               var reviewObj = {};
                               reviewObj.name = name;
                               reviewObj.address = address;
                               reviewObj.rating = rating;
                               button.value = reviewObj;
                               button.onclick = function (event)
                               {
                                   sessionStorage.setItem('resturant', this.value.name);
                                   sessionStorage.setItem('address', this.value.address);
                                   sessionStorage.setItem('rating', this.value.rating);
                                   WinJS.Navigation.navigate("/Edit/edit.html", "view");
                               };
                               element.appendChild(button);

                               var button = document.createElement("submit");
                               button.className = "win-button";
                               button.innerHTML = "Delete";
                               button.id = name;
                               var reviewObj = {};
                               reviewObj.name = name;
                               reviewObj.address = address;
                               reviewObj.rating = rating;
                               button.value = reviewObj;
                               button.onclick = function (event) {
                                   var reviewObj = {};
                                   reviewObj.username = sessionStorage.getItem('user');
                                   reviewObj.restName = this.value.name;
                                   reviewObj.address = this.value.address;
                                   reviewObj.rating = this.value.rating;
                                   var options = {
                                       url: "http://resturantreviewsapi20160522100911.azurewebsites.net/api/resturantReviews",
                                       type: "DELETE",
                                       data: JSON.stringify(reviewObj),
                                       headers: { "Content-Type": "application/json;charset=utf-8" }
                                   }
                                   WinJS.xhr(options).done(
                                       function success(req) {
                                           WinJS.Navigation.navigate("/View/view.html", "view");
                                       }
                                       ,
                                       function error(err) {
                                           document.getElementById("StatusMessage").innerHTML = "Error while deleting data! - " + err.statusText;

                                       }
                                           )
                               };
                               element.appendChild(button);

                               var br = document.createElement("br");
                               element.appendChild(br);
                               var br = document.createElement("br");
                               element.appendChild(br);
                           }
                       }
                )
    }

})();