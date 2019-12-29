function RerenderPugButtons(pageNumber, pagesCount) {
    let count = 5;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;
    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugFlights(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugFlights(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugFlights(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugButtons1").html(gen);
    $("#pugButtons2").html(gen);
}


function RerenderTicketPugButtons(pageNumber, pagesCount) {
    let count = 3;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;
    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugTickets(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugTickets(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugTickets(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugTicketButtons1").html(gen);
    $("#pugTicketButtons2").html(gen);
}

function RerenderBookingPugButtons(pageNumber, pagesCount) {
    let count = 3;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;
    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugBookings(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugBookings(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncPugBookings(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugBookingButtons1").html(gen);
    $("#pugBookingButtons2").html(gen);
}

function RerenderAdminFlightsPugButtons(pageNumber, pagesCount) {
    let count = 5;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;

    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugFlights(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugFlights(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugFlights(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugAdminFlightButtons").html(gen);
}
function RerenderAdminPassengersPugButtons(pageNumber, pagesCount) {
    let count = 5;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;

    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugPassengers(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugPassengers(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugPassengers(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugAdminPassengerButtons").html(gen);
}
function RerenderAdminBookingsPugButtons(pageNumber, pagesCount) {
    let count = 5;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;

    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugBookings(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugBookings(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugBookings(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugAdminBookingsButtons").html(gen);
}
function RerenderAdminTicketsPugButtons(pageNumber, pagesCount) {
    let count = 5;
    let p1 = pageNumber - count;
    let p2 = pageNumber + count;
    let r1 = 0;
    if (p1 < 0) {
        p2 += (-1 * p1);
        p1 = 0;
    }
    if (p2 > pagesCount - 1) {
        p1 -= (p2 - (pagesCount - 1));
        if (p1 < 0) p1 = 0;
        p2 = pagesCount - 1;

    }
    let gen = "";
    for (var i = p1; i < pageNumber; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugTickets(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugTickets(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px; background-color: gainsboro;" class="btn btn-def-pag btn-current">${pageNumber + 1}</button>`;
    for (var i = pageNumber + 1; i <= p2; i++) {
        gen += `<button value="${i}" style="border-radius:20px; margin-left:5px;" name="${pagesCount}" onclick="FuncAdminPugTickets(${i}, ${pagesCount})" id="page_btn" style="margin-left:5px" class="btn btn-def-pag ">${i + 1}</button>`;
    }
    $("#pugAdminTicketsButtons").html(gen);
}


function FuncAdminPugFlights(pageNumber, pagesCount) {
    $.ajax({
        type: "POST",
        url: '/Admin/GetFlightsOffset',
        data: {
            PageNumber: pageNumber,
            PagesCount: pagesCount
        },
        success: function (data) {
            $("#flights-container").empty();
            $("#flights-container").append(data);
            RerenderAdminFlightsPugButtons(pageNumber, pagesCount);
        }
    });
}
function FuncAdminPugPassengers(pageNumber, pagesCount) {
    $.ajax({
        type: "POST",
        url: '/Admin/GetPassengersOffset',
        data: {
            PageNumber: pageNumber,
            PagesCount: pagesCount
        },
        success: function (data) {
            $("#passengers-container").empty();
            $("#passengers-container").append(data);
            RerenderAdminPassengersPugButtons(pageNumber, pagesCount);
        }
    });
}
function FuncAdminPugBookings(pageNumber, pagesCount) {
    $.ajax({
        type: "POST",
        url: '/Admin/GetBookingsOffset',
        data: {
            PageNumber: pageNumber,
            PagesCount: pagesCount
        },
        success: function (data) {
            $("#bookings-container").empty();
            $("#bookings-container").append(data);
            RerenderAdminBookingsPugButtons(pageNumber, pagesCount);
        }
    });
}
function FuncAdminPugTickets(pageNumber, pagesCount) {
    $.ajax({
        type: "POST",
        url: '/Admin/GetTicketsOffset',
        data: {
            PageNumber: pageNumber,
            PagesCount: pagesCount
        },
        success: function (data) {
            $("#tickets-container").empty();
            $("#tickets-container").append(data);
            RerenderAdminTicketsPugButtons(pageNumber, pagesCount);
        }
    });
}

function GetFlightsByPriceAsc() {
    $.ajax({
        type: "POST",
        url: '/Home/GetFlightsOffset',
        data: {
            From: $("input[name=From]").val(),
            To: $("input[name=To]").val(),
            Date: $("input[name=Date]").val(),
            OrderAscending: true,
            SortByDate: false,
            PageNumber: 0
        },
        success: function (data) {
            $("#themes-container").empty();
            $("#themes-container").append(data);
        }
    });
}
function GetFlightsByPriceDesc() {
    $.ajax({
        type: "POST",
        url: '/Home/GetFlightsOffset',
        data: {
            From: $("input[name=From]").val(),
            To: $("input[name=To]").val(),
            Date: $("input[name=Date]").val(),
            OrderAscending: false,
            SortByDate: false,
            PageNumber: 0
        },
        success: function (data) {
            $("#themes-container").empty();
            $("#themes-container").append(data);
        }
    });
}
function GetFlightsByDateAsc() {
    $.ajax({
        type: "POST",
        url: '/Home/GetFlightsOffset',
        data: {
            From: $("input[name=From]").val(),
            To: $("input[name=To]").val(),
            Date: $("input[name=Date]").val(),
            OrderAscending: true,
            SortByDate: true,
            PageNumber: 0
        },
        success: function (data) {
            ScrollIndex1();
            $("#themes-container").empty();
            $("#themes-container").append(data);
        }
    });
}
function GetFlightsByDateDesc() {
    $.ajax({
        type: "POST",
        url: '/Home/GetFlightsOffset',
        data: {
            From: $("input[name=From]").val(),
            To: $("input[name=To]").val(),
            Date: $("input[name=Date]").val(),
            OrderAscending: false,
            SortByDate: true,
            PageNumber: 0
        },
        success: function (data) {
            $("#themes-container").empty();
            $("#themes-container").append(data);
        }
    });
}

$('input[type=radio][name=sortPrice]').on('change', function () {
    $("input:radio[name=sortDate]").prop('checked', false);
    RerenderPugButtons(0, $("#page_btn").attr('name'))
    switch ($(this).val()) {
        case 'asc':
            GetFlightsByPriceAsc();
            break;
        case 'desc':
            GetFlightsByPriceDesc();
            break;
    }
});

$('input[type=radio][name=sortDate]').on('change', function () {
    $("input:radio[name=sortPrice]").prop('checked', false);
    RerenderPugButtons(0, $("#page_btn").attr('name'))
    switch ($(this).val()) {
        case 'asc':
            GetFlightsByDateAsc();
            break;
        case 'desc':
            GetFlightsByDateDesc();
            break;
    }
});

function FuncPugFlights(pageNumber, pagesCount) {
    var order;
    var sort;

    if ($("input:radio[id=asc2]").prop('checked') == true) {
        order = true;
        sort = true;
    }
    if ($("input:radio[id=desc2]").prop('checked') == true) {
        order = false;
        sort = true;
    }
    if ($("input:radio[id=asc1]").prop('checked') == true) {
        order = true;
        sort = false;
    }
    if ($("input:radio[id=desc1]").prop('checked') == true) {
        order = false;
        sort = false;
    }

    $.ajax({
        type: "POST",
        url: '/Home/GetFlightsOffset',
        data: {
            From: $("input[name=From]").val(),
            To: $("input[name=To]").val(),
            Date: $("input[name=Date]").val(),
            OrderAscending: order,
            SortByDate: sort,
            PageNumber: pageNumber
        },
        success: function (data) {
            ScrollIndex1();
            $("#themes-container").empty();
            $("#themes-container").append(data);
            RerenderPugButtons(pageNumber, pagesCount);
        }
    });
}

function FuncPugBookings(searchString) {
    $.ajax({
        type: "GET",
        url: '/api/search?searchString=' + searchString,
        success: function (data) {
            //то шо деллать с ответом
        }
    });
}


function FuncPugBookings(pageNumber, pagesCount) {
    $.ajax({
        type: "POST",
        url: '/Booking/GetBookingsOffset',
        data: {
            PageNumber: pageNumber,
            PagesCount: pagesCount
        },
        success: function (data) {
            $("#themes-container").empty();
            $("#themes-container").append(data);
        }
    });
}

function FuncPugTickets(pageNumber, pagesCount) {
    $.ajax({
        type: "POST",
        url: '/Ticket/GetTicketsOffset',
        data: {
            PageNumber: pageNumber,
            PagesCount: pagesCount
        },
        success: function (data) {
            $("#themes-container").empty();
            $("#themes-container").append(data);
        }
    });
}

function MyNotifications() {
    console.log(1);
    $("MyNotifications").modal();
}

$('#btn-tooltip').tooltip();

/* =================================
BACK TO TOP 
=================================== */
// browser window scroll (in pixels) after which the "back to top" link is shown
var offset = 300,
    //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
    offset_opacity = 1200,
    //duration of the top scrolling animation (in ms)
    scroll_top_duration = 700,
    //grab the "back to top" link
    $back_to_top = $('.cd-top');

//hide or show the "back to top" link
$(window).scroll(function () {
    ($(this).scrollTop() > offset) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
    if ($(this).scrollTop() > offset_opacity) {
        $back_to_top.addClass('cd-fade-out');
    }
});

//smooth scroll to top
$back_to_top.on('click', function (event) {
    event.preventDefault();
    console.log(1);
    $('body,html').animate({
        scrollTop: 0,
    }, scroll_top_duration
    );
});

//var offset = 300,
//    //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
//    offset_opacity = 1200,
//    //duration of the top scrolling animation (in ms)
//    scroll_top_duration = 700,
//    //grab the "back to top" link
//    $go_top = $("#page_btn");

////smooth scroll to top
//$go_top.on('click', function (event) {
//    console.log(1);
//    event.preventDefault();
//    $('body,html').animate({
//        scrollTop: 0,
//    }, scroll_top_duration
//    );
//});

function Scroll(duration) {
    var target = $('.searchWindow');
    var offset = $(target).offset().top - $('.header').height() - $(target).height() - 20;
    console.log(offset);
    $('body, html').animate({
        scrollTop: offset
    }, duration);
}

function ScrollIndex() {
    var target = $('.searchWindow');
    var offset = $(target).offset().top - $(target).height() - 20;
    console.log(offset);
    $('body, html').animate({
        scrollTop: offset
    }, 700);
}

function ScrollIndex1() {
    var target = $('.searchWindow');
    var offset = $(target).offset().top - 66;
    console.log(offset);
    $('body, html').animate({
        scrollTop: offset
    }, 700);
}

function DeleteNotification(id) {
    $.ajax({
        type: "GET",
        url: '/Account/DeleteNotification?notificationId=' + id,
        success: function () {
            $(`#notificationItem${id}`).fadeOut(750);
        }
    });
}