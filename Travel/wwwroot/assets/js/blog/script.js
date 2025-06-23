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


let basketIcon = document.querySelector('.basket');
let basketTab = document.querySelector('.basket-tab');

basketIcon.addEventListener('click', function (e) {
    e.stopPropagation(); 
    basketTab.classList.toggle('d-none');
});

basketTab.addEventListener('click', function (e) {
    e.stopPropagation();
});

document.addEventListener('click', function () {
    if (!basketTab.classList.contains('d-none')) {
        basketTab.classList.add('d-none');
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