(function () {
    "use strict";
    var page = WinJS.UI.Pages.define("/Login/login.html", {
        ready: function (element, options) {
            loginBTN.addEventListener("click", login, false);
            createBTN.addEventListener("click", createnew, false);
        },

        unload: function () {
            loginBTN.removeEventListener("click", login);
            createBTN.removeEventListener("click", createnew);
        }

    });
    function createnew(eventObject)
    {
        var newAccount = {};
        newAccount.username = document.getElementById("username").value;
        newAccount.password = document.getElementById("password").value;
        if (newAccount.username == "" || newAccount.password == "")
        {
            document.getElementById("StatusMessage").innerHTML = "Username and Password are required";
        }
        else
        {
            var options = {
                url: "http://resturantreviewsapi20160522100911.azurewebsites.net/api/accounts",
                type: "POST",
                data: JSON.stringify(newAccount),
                headers: { "Content-Type": "application/json;charset=utf-8" }
            }
            WinJS.xhr(options).done(
            function success(req) {
                document.getElementById("StatusMessage").innerHTML = "Account Created!";
            }
             ,
            function error(err) {
                document.getElementById("StatusMessage").innerHTML = "Error creating account- " + err.statusText + " - Check if Account already created";
            });
        }
    }

    function login(eventObject) {
        var name = document.getElementById("username").value;
        var pass = document.getElementById("password").value;
        if (name == "" || pass == "")
        {
            document.getElementById("StatusMessage").innerHTML = "Username and Password are required";
        }
        else
        {
            var url = "http://resturantreviewsapi20160522100911.azurewebsites.net/api/accounts/" + name;
            WinJS.xhr(
                { url: url }
                )
            .then
                (
                    function (response) {
                        var sourceData = JSON.parse(response.responseText);
                        if (sourceData[0] == null) {
                            document.getElementById("StatusMessage").innerHTML = "That Username does not exist";
                        }
                        else {
                            var password = sourceData[0].password;
                            if (pass != password) {
                                document.getElementById("StatusMessage").innerHTML = "Incorrect Password";
                            }
                            else {
                                sessionStorage.setItem('user', name);
                                WinJS.UI.Animation.exitPage(homepage, null).done(function () {
                                    WinJS.Navigation.navigate("/Welcome/welcome.html", "login");
                                })
                            }
                        }
                    }
                )
        }
    }

})();