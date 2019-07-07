// Initialize Firebase
var liStyle = '';

var config = {
    apiKey: "AIzaSyDlEhDpVtQBGmr2rAy90NrB-J02bq5KSak",
    authDomain: "shoesshop-822d0.firebaseapp.com",
    databaseURL: "https://shoesshop-822d0.firebaseio.com",
    projectId: "shoesshop-822d0",
    storageBucket: "shoesshop-822d0.appspot.com",
    messagingSenderId: "680116886508"
};
firebase.initializeApp(config);

var deletedArr = [];

function initFirebase() {

    $('#notiList').children().remove();
    const dbRefObject = firebase.database().ref().child('complaints');

    dbRefObject.on('value', function (snap) {
        //console.log('add: ' + snap.key);
        clearListNoti();
        updateNumberBell(countChild('complaints'));
        $.each(snap.val(), function (index, item) {
            addNoti(item);
        });
    });
}

function clearListNoti() {
    $('#notiList').children().remove();
}

function updateNumberBell(number) {
    $('#numBell').text(number);

    if (number > 0) {
        showBell('#numBell');
    } else {
        hideBell('#numBell');
    }
}

function addNoti(value) {
    if (value.State == 'pending') {
        liStyle = `style="background-color: aliceblue;"`;
        title = "New complaint";
    } else {
        liStyle = '';
        title = "Processing complaint";
    }

    $('#listBell').append(`
        <li ` + liStyle + `>
            <a href="/Admin/Complaint/Detail?id=` + value.Id + `" class="peers fxw-nw td-n p-20 bdB c-grey-800 cH-blue bgcH-grey-100">
                <div class="peer mR-15 liNoti">` + title + `</div>
                <div class="peer peer-greed">
                    <span>
                        <span class="fw-500">Type :</span>
                        <span class="c-grey-600">`+ value.Type + `<span class="text-dark"></span></span>
                    </span>
                    <p class="m-0"><small class="fsz-xs">`+ parseDate(value.CreatedTime) + `</small></p>
                </div>
            </a>
        </li>
    `);
}

function countChild(refer) {
    var tmp = 0;
    var ref = firebase.database().ref(refer);
    ref.on("value", function (snapshot) {
        tmp = snapshot.numChildren();
    });
    return tmp;
}
