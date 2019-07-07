function Login() {
    var username = $('#txtUsername').val();
    var password = $('#txtPassword').val();
    $.ajax({
        type: 'POST',
        url: `${apiURL}login?username=${username}&password=${password}`,
        success: function (result) {
            var tomorrow = new Date();
            tomorrow.setDate(tomorrow.getDate() + 1);
            document.cookie = "accessToken=" + result.token + "; expires=" + tomorrow.toUTCString();
            document.cookie = "avatar=" + result.avatar + "; expires=" + tomorrow.toUTCString();
            document.cookie = "fullName=" + result.fullName + "; expires=" + tomorrow.toUTCString();
            document.cookie = "username=" + result.username + "; expires=" + tomorrow.toUTCString();
            document.cookie = "homepage=" + result.homePage + "; expires=" + tomorrow.toUTCString();
            window.location = "" + result.homePage;
        },
        statusCode: {
            409: function (result) {
                alert(result.responseText);
            }
        }
    });
}

function Redirect() {
    var token = getCookie("accessToken");
    if (token == "") {
        window.location = "/login";
    } else {
        window.location = getCookie("homeurl");
    }
}

function CheckAuthorized() {
    var token = getCookie("accessToken");
    if (token == "") {
        window.location = "/login";
    }
}

function GetAccessToken() {
    return getCookie("accessToken");
}

function Logout() {
    document.cookie = "accessToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "fullName=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "avatar=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "homepage=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    Redirect();
}