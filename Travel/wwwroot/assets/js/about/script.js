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


document.addEventListener("DOMContentLoaded", () => {
  const video = document.getElementById("myVideo");
  const playBtn = document.getElementById("playBtn");
  const icon = playBtn.querySelector("i");

  playBtn.addEventListener("click", () => {
    if (video.paused) {
      video.play();
    } else {
      video.pause();
    }
  });

  video.addEventListener("play", () => {
    icon.classList.replace("fa-play", "fa-pause");
  });

  video.addEventListener("pause", () => {
    icon.classList.replace("fa-pause", "fa-play");
  });
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