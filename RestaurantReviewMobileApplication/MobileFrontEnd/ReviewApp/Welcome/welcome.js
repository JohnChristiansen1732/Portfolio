(function () {
    var systemNavigationManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    var page = WinJS.UI.Pages.define("/Welcome/welcome.html", {
        ready: function (element, options) {
            var user = sessionStorage.getItem('user');
            systemNavigationManager.addEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.visible;
            log.addEventListener("click", logout, false);
            add.addEventListener("click", addnew, false);
            view.addEventListener("click", views, false);
            document.getElementById("header").innerHTML = "Welcome, " + user + "!";
            

        },
        unload: function () {
            systemNavigationManager.removeEventListener("backrequested", backRequested);
            systemNavigationManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.collapsed;
            log.removeEventListener("click", logout);
            view.removeEventListener("click", views);
            add.removeEventListener("click", addnew);
        }
    });

    function backRequested() {
        WinJS.UI.Animation.exitPage(null).done(function () {
            sessionStorage.setItem('user', "");
            WinJS.Navigation.navigate("/Login/login.html", "welcome");
        })
    }
    function logout()
    {
        sessionStorage.removeItem('user');
        WinJS.Navigation.navigate("/Login/login.html", "welcome");
    }
    function views() {
        WinJS.Navigation.navigate("/View/view.html", "welcome");
    }
    function addnew()
    {
        WinJS.Navigation.navigate("/Addnew/addnew.html", "welcome");

    }

})();