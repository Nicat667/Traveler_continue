let currency = document.querySelector(".currency");
let currency_tab = document.querySelector(".currency-tab");
let icon1 = document.querySelector(".curicon");
let icon2 = document.querySelector(".curicon_");

currency.addEventListener("click", function (e) {
    e.stopPropagation();
    currency_tab.classList.toggle("d-none");
    icon1.classList.toggle("d-none");
    icon2.classList.toggle("d-none");
});

document.querySelectorAll('.li').forEach(function (item) {
    item.addEventListener('click', function () {
        document.getElementById('currency').textContent = this.textContent;
        currency_tab.classList.add("d-none");
        icon2.classList.add("d-none");
        icon1.classList.remove("d-none");
    });
});

document.addEventListener("click", function (e) {
    if (!currency.contains(e.target) && !currency_tab.contains(e.target)) {
        if (!currency_tab.classList.contains("d-none")) {
            currency_tab.classList.add("d-none");
            icon2.classList.add("d-none");
            icon1.classList.remove("d-none");
        }
    }
});





//let vis = document.querySelector(".visible");
//let invis = document.querySelector(".invis");
//let password = document.querySelector(".pass");

//vis.addEventListener('click', function () {
//    if (!vis.classList.contains("d-none")) {
//        password.type = "text";
//        vis.classList.add("d-none");
//        invis.classList.remove("d-none");
//    }
//})
//invis.addEventListener('click', function () {
//    if (!invis.classList.contains("d-none")) {
//        password.type = "password";
//        invis.classList.add("d-none");
//        vis.classList.remove("d-none");
//    }
//})

//let visreg = document.querySelector(".visible-register");
//let invisreg = document.querySelector(".invis-register");
//let passwordreg = document.querySelector(".pass-register");

//visreg.addEventListener('click', function () {
//    if (!visreg.classList.contains("d-none")) {
//        passwordreg.type = "text";
//        visreg.classList.add("d-none");
//        invisreg.classList.remove("d-none");
//    }
//})
//invisreg.addEventListener('click', function () {
//    if (!invisreg.classList.contains("d-none")) {
//        passwordreg.type = "password";
//        invisreg.classList.add("d-none");
//        visreg.classList.remove("d-none");
//    }
//})

//let visreg2 = document.querySelector(".visible-register2");
//let invisreg2 = document.querySelector(".invis-register2");
//let passwordreg2 = document.querySelector(".pass-register-conf");

//visreg2.addEventListener('click', function () {
//    if (!visreg2.classList.contains("d-none")) {
//        passwordreg2.type = "text";
//        visreg2.classList.add("d-none");
//        invisreg2.classList.remove("d-none");
//    }
//})
//invisreg2.addEventListener('click', function () {
//    if (!invisreg2.classList.contains("d-none")) {
//        passwordreg2.type = "password";
//        invisreg2.classList.add("d-none");
//        visreg2.classList.remove("d-none");
//    }
//})


//let profile = document.querySelector(".profile");
//let login = document.querySelector(".login");
//let signin = document.querySelector(".in");
//let signup = document.querySelector(".up");
//let create = document.querySelector(".signin");
//let register = document.querySelector(".signup");

//profile.addEventListener('click', function () {
//    if (login.classList.contains("d-none")) {
//        login.classList.remove("d-none");
//    }
//    else {
//        login.classList.add("d-none");
//    }
//})

//document.addEventListener("click", function (event) {
//    if (!login.contains(event.target) && !profile.contains(event.target)) {
//        login.classList.add("d-none");
//    }
//});

//signup.addEventListener('click', function () {
//    if (!create.classList.contains("d-none")) {
//        create.classList.add("d-none");
//        register.classList.remove("d-none");
//        signin.classList.remove("active");
//        signup.classList.add("active");
//    }
//    else {
//        register.classList.remove("d-none");
//        signin.classList.remove("active");
//        signup.classList.add("active")
//    }
//})

//signin.addEventListener('click', function () {
//    if (!register.classList.contains("d-none")) {
//        register.classList.add("d-none");
//        create.classList.remove("d-none");
//        signup.classList.remove("active");
//        signin.classList.add("active")
//    }
//    else {
//        create.classList.remove("d-none");
//        signup.classList.remove("active");
//        signin.classList.add("active")
//    }
//})

//let forget = document.querySelector(".forgetpass");
//let forgetButton = document.querySelector(".forget");
//let back = document.querySelector(".backTo");

//forgetButton.addEventListener('click', function () {
//    login.classList.add("d-none");
//    forget.classList.remove("d-none");
//})

//back.addEventListener('click', function () {
//    forget.classList.add("d-none");
//    setTimeout(() => {
//        login.classList.remove("d-none");
//    }, 1);
//})


let menuIcon = document.querySelector('.menuIcon');
let sidebar = document.getElementById('sidebar');
let exitIcon = document.querySelector('.exitIcon');

menuIcon.addEventListener('click', () => {
    sidebar.classList.add('active');
    document.body.style.overflow = 'hidden';
});

exitIcon.addEventListener('click', () => {
    sidebar.classList.remove('active');
    document.body.style.overflow = '';
});

document.addEventListener('click', (e) => {
    if (
        sidebar.classList.contains('active') &&
        !sidebar.contains(e.target) &&
        !menuIcon.contains(e.target)
    ) {
        sidebar.classList.remove('active');
        document.body.style.overflow = '';
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const moveUpBtn = document.querySelector(".move-up");

    window.addEventListener("scroll", () => {
        if (window.scrollY > 800) {
            moveUpBtn.style.display = "flex";
        } else {
            moveUpBtn.style.display = "none";
        }
    });

    moveUpBtn.addEventListener("click", () => {
        window.scrollTo({
            top: 0,
            behavior: "smooth",
        });
    });
});

let currentPage = 1;
const totalPages = 10;

const currentPageEl = document.getElementById("current-page");
const prevBtn = document.getElementById("prev-page");
const nextBtn = document.getElementById("next-page");

function updatePageDisplay() {
    currentPageEl.textContent = currentPage;
    prevBtn.parentElement.classList.toggle("disabled", currentPage === 1);
    nextBtn.parentElement.classList.toggle("disabled", currentPage === totalPages);
}

prevBtn.addEventListener("click", function (e) {
    e.preventDefault();
    if (currentPage > 1) {
        currentPage--;
        updatePageDisplay();
    }
});

nextBtn.addEventListener("click", function (e) {
    e.preventDefault();
    if (currentPage < totalPages) {
        currentPage++;
        updatePageDisplay();
    }
});

updatePageDisplay();


document.addEventListener("DOMContentLoaded", () => {
    let sortButton = document.querySelector(".sort");
    let upButton = document.querySelector(".up2");
    let downButton = document.querySelector(".down2");
    let sortTab = document.querySelector(".sort-tab");
    let container = document.querySelector(".forHotels");

    let lowToHighRadio = document.getElementById("flexRadioDefault1");
    let highToLowRadio = document.getElementById("flexRadioDefault2");

    function sortHotelsByPrice(order = "asc") {
        let items = Array.from(container.querySelectorAll(".item"));

        items.sort((a, b) => {
            const priceA = parseFloat(a.dataset.price);
            const priceB = parseFloat(b.dataset.price);
            return order === "asc" ? priceA - priceB : priceB - priceA;
        });

        items.forEach(item => container.appendChild(item));
    }


    sortButton.addEventListener("click", function (e) {
        e.stopPropagation();
        const isOpen = !sortTab.classList.contains("d-none");

        sortTab.classList.toggle("d-none", isOpen);
        downButton.classList.toggle("d-none", !isOpen);
        upButton.classList.toggle("d-none", isOpen);
    });


    document.addEventListener("click", function (e) {
        const sortBox = document.querySelector(".sort");
        if (!sortBox.contains(e.target)) {
            sortTab.classList.add("d-none");
            upButton.classList.add("d-none");
            downButton.classList.remove("d-none");
        }
    });

    lowToHighRadio.addEventListener("change", () => {
        if (lowToHighRadio.checked) {
            sortHotelsByPrice("asc");
        }
    });

    highToLowRadio.addEventListener("change", () => {
        if (highToLowRadio.checked) {
            sortHotelsByPrice("desc");
        }
    });

    sortHotelsByPrice("asc");
});


// const name = document.querySelector(".location p").textContent;
// const city = document.querySelector(".location span").textContent;
// const address = `${name}, ${city}`;

// const apiKey = "AIzaSyBu6mYs894keUMuKa0Dx2HW756Xwg3yoQs";
// const url = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(address)}&key=${apiKey}`;


// fetch(url)
//   .then(response => response.json())
//   .then(data => {
//     if (data.status === "OK") {
//       const location = data.results[0].geometry.location;
//       const lat = location.lat;
//       const lng = location.lng;
//       console.log("Coordinates:", lat, lng);


//       initMap(lat, lng);
//     } else {
//       console.error("Geocoding error:", data.status);
//     }
//   })
//   .catch(error => console.error("Request failed:", error));


//   function initMap(lat, lng) {
//   const map = new google.maps.Map(document.getElementById("map-canvas"), {
//     center: { lat, lng },
//     zoom: 15,
//   });

//   new google.maps.Marker({
//     position: { lat, lng },
//     map: map,
//     title: address,
//   });
// }


let map;
let markersData = [];

function initMap() {
  map = new google.maps.Map(document.getElementById("map-canvas"), {
    center: { lat: 39.8283, lng: -98.5795 },
    zoom: 4,
    mapId: "b559224a8cff171a769dc502",
  });

  loadAndGeocodeHotels();
}

function loadAndGeocodeHotels() {
  const hotelElements = document.querySelectorAll(".forHotels .item");
  const apiKey = "AIzaSyBu6mYs894keUMuKa0Dx2HW756Xwg3yoQs";

  let geocodedCount = 0;

  hotelElements.forEach(item => {
   
    const locationElement = item.querySelector(".location span");
    const address = locationElement ? locationElement.textContent.trim() : null;


    const price = item.getAttribute("data-price") || "N/A";

    if (!address) return; 

    const url = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(address)}&key=${apiKey}`;

    fetch(url)
      .then(response => response.json())
      .then(data => {
        if (data.status === "OK") {
          const location = data.results[0].geometry.location;

          markersData.push({
            lat: location.lat,
            lng: location.lng,
            price: price,   
            address: address  
          });
        } else {
          console.error("Geocoding error:", data.status, "for", address);
        }
      })
      .catch(error => console.error("Request failed:", error))
      .finally(() => {
        geocodedCount++;
        if (geocodedCount === hotelElements.length) {
          placeMarkers();
        }
      });
  });
}

function placeMarkers() {
  const bounds = new google.maps.LatLngBounds();
  const { AdvancedMarkerElement } = google.maps.marker;

  markersData.forEach(markerInfo => {
    const position = { lat: markerInfo.lat, lng: markerInfo.lng };

    new AdvancedMarkerElement({
      map: map,
      position: position,
      title: `$${markerInfo.price} / night`, 
    });

    bounds.extend(position);
  });

  if (!bounds.isEmpty()) {
    map.fitBounds(bounds);
  }
}


// document.querySelector('.icon-like-hotel').addEventListener('click', function () {
//     this.classList.toggle('liked');
//     this.classList.toggle('fa-regular');
//     this.classList.toggle('fa-solid');
//   });


document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.icon-like-hotel').forEach(icon => {
      icon.addEventListener('click', function () {
        icon.classList.toggle('liked');
        icon.classList.toggle('fa-regular');
        icon.classList.toggle('fa-solid');
      });
    });
});


/////////////////////////////////////////////////////////////////////////////////////////

const filterPrice = document.querySelector(".price-filter");
const angleDown1 = document.querySelector(".price-filter .down1");
const angleUp1 = document.querySelector(".price-filter .up1");

const filterStar = document.querySelector(".star-filter");
const angleDown2 = document.querySelector(".star-filter .down22");
const angleUp2 = document.querySelector(".star-filter .up22");

const priceTab = document.querySelector(".price-tab");
const starTab = document.querySelector(".star-tab");


filterPrice.addEventListener("click", function () {

    const isVisible = !priceTab.classList.contains("d-none");


    starTab.classList.add("d-none");
    angleUp2.classList.add("d-none");
    angleDown2.classList.remove("d-none");

    if (isVisible) {

        priceTab.classList.add("d-none");
        angleUp1.classList.add("d-none");
        angleDown1.classList.remove("d-none");
    } else {

        priceTab.classList.remove("d-none");
        angleUp1.classList.remove("d-none");
        angleDown1.classList.add("d-none");
    }
});


filterStar.addEventListener("click", function () {

    const isVisible = !starTab.classList.contains("d-none");


    priceTab.classList.add("d-none");
    angleUp1.classList.add("d-none");
    angleDown1.classList.remove("d-none");

    if (isVisible) {

        starTab.classList.add("d-none");
        angleUp2.classList.add("d-none");
        angleDown2.classList.remove("d-none");
    } else {

        starTab.classList.remove("d-none");
        angleUp2.classList.remove("d-none");
        angleDown2.classList.add("d-none");
    }
});


document.addEventListener("DOMContentLoaded", function () {
    const priceSlider = document.getElementById('price-slider');
    const minInput = document.getElementById('minPriceInput');
    const maxInput = document.getElementById('maxPriceInput');
    const minDisplay = document.getElementById('minDisplay');
    const maxDisplay = document.getElementById('maxDisplay');

    if (priceSlider) {
        noUiSlider.create(priceSlider, {
            start: [50, 300], // default values
            connect: true,
            range: {
                min: 0,
                max: 1000
            },
            step: 1,
            format: {
                to: value => Math.round(value),
                from: value => Number(value)
            }
        });

        priceSlider.noUiSlider.on('update', function (values) {
            const min = values[0];
            const max = values[1];
            minInput.value = min;
            maxInput.value = max;
            minDisplay.textContent = min;
            maxDisplay.textContent = max;
        });
    }
});
