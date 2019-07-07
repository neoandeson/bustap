const apiURL = 'https://pbsa.azurewebsites.net/';
const urlGetComplaints = '/Admin/Complaint/GetComplaints';
//const urlGetComplaints = apiURL + 'complaints';
const urlApproveComplaint = apiURL + 'complaint/approve';
const urlRejectComplaint = apiURL + 'complaint/reject';

function GetURL(action, params) {
    var url = apiURL;
    var count = 0;

    if (action) {
        url = url + action;
        if (params) {
            url = url + "?";
            for (var k in params) {
                if (params.hasOwnProperty(k)) {
                    if (++count > 1) {
                        url += "&"
                    }
                    url = url + k + "=" + params[k];
                }
            }
        }
    }

    return url;
}

function GetAPI(action, params) {
    $.ajax({
        headers: {
            "Authorization": GetAccessToken(),
        },
        type: "GET",
        url: GetURL(action, params),
        success: function (data) {
            return data;
        }
    });
}

function PostAPI(action, params) {//TODO
    $.ajax({
        headers: {
            "Authorization": GetAccessToken(),
        },
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: action,
        data: JSON.stringify(params),
        success: function (data) {
            return data;
        }
    });
}

function hide(target) {
    $(target).hide();//css('visibility', 'hidden');
}

function show(target) {
    $(target).show();//css('visibility', 'visible');
}

function hideBell(target) {
    $(target).css('visibility', 'hidden');
}

function showBell(target) {
    $(target).css('visibility', 'visible');
}

function isvalidTextInput(target) {
    var value = $(target).val();
    if (value) {
        return true;
    }
    return false;
}

///Notification
var number = 0;
var liStyle = '';
var title = '';
function getPendingComplaintNoti() {
    $.ajax({
        type: "POST",
        url: "/Admin/Complaint/GetPendingComplaints",
        success: function (data) {
            number = data.length;
            if (number != 0) {
                show('#numBell');
                $('#numBell').text(number);
                $('#listBell').children().remove();
                $.each(data, function (index, value) {
                    if (value.state == 'pending') {
                        liStyle = `style="background-color: aliceblue;"`;
                        title = "New complaint";
                    } else {
                        liStyle = '';
                        title = "Processing complaint";
                    }
                    $('#listBell').append(`
                        <li ` + liStyle + `>
                            <a href="/Admin/Complaint/Detail?id=` + value.id + `" class="peers fxw-nw td-n p-20 bdB c-grey-800 cH-blue bgcH-grey-100">
                                <div class="peer mR-15 liNoti">` + title + `</div>
                                <div class="peer peer-greed">
                                    <span>
                                        <span class="fw-500">Type :</span>
                                        <span class="c-grey-600">`+ value.type + `<span class="text-dark"></span></span>
                                    </span>
                                    <p class="m-0"><small class="fsz-xs">`+ parseDate(value.createdTime) + `</small></p>
                                </div>
                            </a>
                        </li>
                    `);
                });
            } else {
                hide('#numBell');
            }
        }
    });
}

function parseDate(str_date) {
    return (new Date(str_date)).toLocaleString('en-GB');
}

function setTarget(content) {
    window.localStorage.setItem('target', content);
}

function toMoney(money) {
    money = money.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
    return money;
}

function distance(lat1, lon1, lat2, lon2) {
    var R = 6371; // km (change this constant to get miles)
    var dLat = (lat2 - lat1) * Math.PI / 180;
    var dLon = (lon2 - lon1) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    if (d > 1) return Math.round(d) + "km";
    else if (d <= 1) return Math.round(d * 1000) + "m";
    return d;
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}