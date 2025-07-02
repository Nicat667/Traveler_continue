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





document.addEventListener("DOMContentLoaded", function () {
    // Tabs: Log in / Register
    let signup = document.querySelector(".up");
    let signin = document.querySelector(".in");
    let create = document.querySelector(".signin");
    let register = document.querySelector(".signup");

    if (signup && signin && create && register) {
        signup.addEventListener("click", function () {
            create.classList.add("d-none");
            register.classList.remove("d-none");
            signin.classList.remove("active");
            signup.classList.add("active");
        });

        signin.addEventListener("click", function () {
            register.classList.add("d-none");
            create.classList.remove("d-none");
            signup.classList.remove("active");
            signin.classList.add("active");
        });
    }

    // Forget password logic
    let forget = document.querySelector(".forgetpass");
    let forgetButton = document.querySelector(".forget");
    let login = document.querySelector(".login");
    let back = document.querySelector(".backTo");

    if (forget && forgetButton && login && back) {
        forgetButton.addEventListener("click", function () {
            login.classList.add("d-none");
            forget.classList.remove("d-none");
        });

        back.addEventListener("click", function () {
            forget.classList.add("d-none");
            setTimeout(() => {
                login.classList.remove("d-none");
            }, 1);
        });
    }

    // Password visibility toggle function
    function setupPasswordToggle(invisSelector, visSelector, inputSelector) {
        const invis = document.querySelector(invisSelector);
        const vis = document.querySelector(visSelector);
        const input = document.querySelector(inputSelector);

        if (!invis || !vis || !input) return;

        // Default state: password hidden, "invisible" eye icon visible, "visible" eye icon hidden
        vis.classList.add("d-none");
        invis.classList.remove("d-none");

        invis.addEventListener("click", function () {
            input.type = "text";
            invis.classList.add("d-none");
            vis.classList.remove("d-none");
        });

        vis.addEventListener("click", function () {
            input.type = "password";
            vis.classList.add("d-none");
            invis.classList.remove("d-none");
        });
    }

    // Setup toggles for Register form password inputs
    setupPasswordToggle(".invis-register", ".visible-register", ".pass-register");
    setupPasswordToggle(".invis-register2", ".visible-register2", ".pass-register-conf");

    // Setup toggle for Sign In form password input
    setupPasswordToggle(".invis", ".visible", ".pass");
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









