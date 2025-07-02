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

document.querySelectorAll('.li').forEach(function(item) {
    item.addEventListener('click', function() {
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

//vis.addEventListener('click', function(){
//    if(!vis.classList.contains("d-none")){
//        password.type = "text";
//        vis.classList.add("d-none");
//        invis.classList.remove("d-none");
//    }
//})
//invis.addEventListener('click', function(){
//    if(!invis.classList.contains("d-none")){
//        password.type = "password";
//        invis.classList.add("d-none");
//        vis.classList.remove("d-none");
//    }
//})

//let visreg = document.querySelector(".visible-register");
//let invisreg = document.querySelector(".invis-register");
//let passwordreg = document.querySelector(".pass-register");

//visreg.addEventListener('click', function(){
//    if(!visreg.classList.contains("d-none")){
//        passwordreg.type = "text";
//        visreg.classList.add("d-none");
//        invisreg.classList.remove("d-none");
//    }
//})
//invisreg.addEventListener('click', function(){
//    if(!invisreg.classList.contains("d-none")){
//        passwordreg.type = "password";
//        invisreg.classList.add("d-none");
//        visreg.classList.remove("d-none");
//    }
//})

//let visreg2 = document.querySelector(".visible-register2");
//let invisreg2 = document.querySelector(".invis-register2");
//let passwordreg2 = document.querySelector(".pass-register-conf");

//visreg2.addEventListener('click', function(){
//    if(!visreg2.classList.contains("d-none")){
//        passwordreg2.type = "text";
//        visreg2.classList.add("d-none");
//        invisreg2.classList.remove("d-none");
//    }
//})
//invisreg2.addEventListener('click', function(){
//    if(!invisreg2.classList.contains("d-none")){
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

//profile.addEventListener('click', function(){
//    if(login.classList.contains("d-none")){
//        login.classList.remove("d-none");
//    }
//    else{
//        login.classList.add("d-none");
//    }
//})

//document.addEventListener("click", function(event) {
//    if (!login.contains(event.target) && !profile.contains(event.target)) {
//        login.classList.add("d-none");
//    }
//});

//signup.addEventListener('click', function(){
//    if(!create.classList.contains("d-none")){
//        create.classList.add("d-none");
//        register.classList.remove("d-none");
//        signin.classList.remove("active");
//        signup.classList.add("active");
//    }
//    else{
//        register.classList.remove("d-none");
//        signin.classList.remove("active");
//        signup.classList.add("active")
//    }
//})

//signin.addEventListener('click', function(){
//    if(!register.classList.contains("d-none")){
//        register.classList.add("d-none");
//        create.classList.remove("d-none");
//        signup.classList.remove("active");
//        signin.classList.add("active")
//    }
//    else{
//        create.classList.remove("d-none");
//        signup.classList.remove("active");
//        signin.classList.add("active")
//    }
//})

//let forget = document.querySelector(".forgetpass");
//let forgetButton = document.querySelector(".forget");
//let back = document.querySelector(".backTo");

//forgetButton.addEventListener('click', function(){
//    login.classList.add("d-none");
//    forget.classList.remove("d-none");
//})

//back.addEventListener('click', function(){
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

//document.querySelector('.like-icon').addEventListener('click', function () {        //////////for making red when it is clicked
//    this.classList.toggle('liked');
//    this.classList.toggle('fa-regular');
//    this.classList.toggle('fa-solid');
//  });


const info = document.getElementById("info");
const address = info.getAttribute("addressForMap")
const apiKey = "AIzaSyBu6mYs894keUMuKa0Dx2HW756Xwg3yoQs";
const url = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(address)}&key=${apiKey}`;

fetch(url)
  .then(response => response.json())
  .then(data => {
    if (data.status === "OK") {
      const location = data.results[0].geometry.location;
      const lat = location.lat;
      const lng = location.lng;
      console.log("Coordinates:", lat, lng);


      initMap(lat, lng);
    } else {
      console.error("Geocoding error:", data.status);
    }
  })
  .catch(error => console.error("Request failed:", error));


  function initMap(lat, lng) {
  const map = new google.maps.Map(document.getElementById("map-canvas"), {
    center: { lat, lng },
    zoom: 15,
  });

  new google.maps.Marker({
    position: { lat, lng },
    map: map,
    title: address,
  });
}

function adjustInfoHeight() {
  const info = document.getElementById('info');
  if (!info) return;

 
  info.style.height = 'auto';

  const contentHeight = info.scrollHeight;


  const mainImage = document.querySelector('.mainImage');
  const map = document.querySelector('.map');

  let extraHeight = 0;

  if (mainImage && map) {
    const infoBottom = info.getBoundingClientRect().top + window.scrollY + contentHeight;
    const imageBottom = mainImage.getBoundingClientRect().bottom + window.scrollY;
    const mapBottom = map.getBoundingClientRect().bottom + window.scrollY;

    const maxBottom = Math.max(infoBottom, imageBottom, mapBottom);
    extraHeight = maxBottom - (info.getBoundingClientRect().top + window.scrollY);
  }

  const viewportHeight = window.innerHeight;

  info.style.height = Math.max(extraHeight, viewportHeight - 100) + 100 + 'px';
}


window.addEventListener('load', adjustInfoHeight);
window.addEventListener('resize', adjustInfoHeight);


const infoNode = document.getElementById('info');
if (infoNode) {
  const observer = new MutationObserver(adjustInfoHeight);
  observer.observe(infoNode, { childList: true, subtree: true });
}


let commentButton = document.querySelector(".button-comment");
let upComment = document.querySelector(".icon-up-comment");
let downComment = document.querySelector(".icon-down-comment");
let commentForm = document.querySelector(".comment-form");

commentButton.addEventListener('click', function(){
  if(!upComment.classList.contains("d-none")){
    upComment.classList.add('d-none');
    downComment.classList.remove('d-none');
    commentForm.classList.remove('d-none');
  }
  else{
    upComment.classList.remove('d-none');
    downComment.classList.add('d-none');
    commentForm.classList.add('d-none');
  }
})



let showMoreBtn = document.querySelector(".showMore");

showMoreBtn.addEventListener('click', function () {
    let hotelId = this.getAttribute("hotelId");
    let totalComment = parseInt(this.getAttribute("totalcount"));
    let commentsContainer = this.closest(".comments");
    let commentList = commentsContainer.querySelector(".comment-list");

    let displayedCommentCount = commentList.querySelectorAll(".comment").length;

    fetch(`https://localhost:7107/Hotel/ShowMore?hotelId=${hotelId}&skip=${displayedCommentCount}`)
        .then(response => {
            if (!response.ok) throw new Error("Failed to fetch");
            return response.json();
        })
        .then(comments => {
            comments.forEach(comment => {
                const div = document.createElement("div");
                div.classList.add("comment");
                div.innerHTML = `
                    <div class="author">
                        <p class="name">${comment.authorName}</p>
                        <p class="date">${comment.created}</p>
                    </div>
                    <div class="rating">
                        <div class="rate">
                            <span>${(comment.rate % 1 === 0) ? comment.rate.toFixed(0) : comment.rate.toFixed(1)}</span>
                            <span>/</span>
                            <span>5</span>
                        </div>
                        <p class="content">${comment.message}</p>
                    </div>
                `;
                commentList.appendChild(div);
            });
            let updatedCount = commentList.querySelectorAll(".comment").length;
            if (updatedCount >= totalComment) {
                showMoreBtn.style.display = "none";
            }
        })
        .catch(err => console.error("Fetch error:", err));
});



/////////////////////////////////


//const icon = document.querySelector('.like-icon');

//if (icon) {
//    icon.addEventListener('click', async function () {
//        const likeBtn = this.closest('.like.wishlist');
//        const hotelId = likeBtn.getAttribute('hotelId');

//        try {
//            await fetch("https://localhost:7107/WishList/Add?id=" + hotelId, {
//                method: 'POST',
//                headers: {
//                    "Content-Type": "application/json; charset=UTF-8"
//                }
//            });
//        } catch (error) {
//            console.error('Error sending wishlist request:', error);
//        }
//    });
//}
