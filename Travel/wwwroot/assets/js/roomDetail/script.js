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

document.querySelector('.like-icon').addEventListener('click', function () {
    this.classList.toggle('liked');
    this.classList.toggle('fa-regular');
    this.classList.toggle('fa-solid');
  });



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



const roomInput = document.querySelector('.number-input2 input');
const total = document.querySelector('.sum');


function calculateTotal() {
    const roomContainer = document.querySelector(".book");
    const pricePerNight = parseFloat(roomContainer.getAttribute("roomPrice"));
    const startDate = new Date(document.getElementById("startDate").value);
    const endDate = new Date(document.getElementById("endDate").value);
    const roomCount = parseInt(roomContainer.querySelector("input[type='number']").value);
    const totalSpan = roomContainer.querySelector(".sum");

    if (isNaN(startDate.getTime()) || isNaN(endDate.getTime()) || endDate <= startDate || roomCount <= 0) {
        totalSpan.textContent = "$ 0.00";
        return;
    }

    const millisecondsPerDay = 1000 * 60 * 60 * 24;
    const nights = Math.round((endDate - startDate) / millisecondsPerDay);
    const total = nights * pricePerNight * roomCount;

    totalSpan.textContent = `$ ${total.toFixed(2)}`;
}

document.querySelector('.increment2').addEventListener('click', () => {
    roomInput.value = Number(roomInput.value) + 1;

    calculateTotal(); // <-- Make sure this is called
});

document.querySelector('.decrement2').addEventListener('click', () => {
    if (Number(roomInput.value) > 1) {
        roomInput.value = Number(roomInput.value) - 1;
        calculateTotal(); // <-- Also call here
    }
});

// Optional: also call calculateTotal when dates change
document.getElementById("startDate").addEventListener("change", calculateTotal);
document.getElementById("endDate").addEventListener("change", calculateTotal);



