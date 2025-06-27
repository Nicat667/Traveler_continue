"use strict"


document.querySelectorAll(".deleteImage").forEach(btn => {
    btn.addEventListener("click", function () {
        if (btn.disabled) return;

        const imageDiv = this.closest(".Images");
        const id = this.getAttribute("data-id");

        fetch("https://localhost:7107/Admin/Room/DeleteImage?id=" + id, {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        })
            .then(response => {
                if (response.ok) {
                    imageDiv.remove();
                } else {
                    alert("Failed to delete image.");
                }
            })
            .catch(error => {
                console.error(error);
                alert("Error deleting image.");
            });
    });
});


document.querySelectorAll(".setmain").forEach(btn => {
    btn.addEventListener("click", function () {
        const newMainId = this.getAttribute("data-id");
        const hotelId = this.getAttribute("hotelId");

        fetch(`https://localhost:7107/Admin/Room/SetMain?imageId=${newMainId}&hotelId=${hotelId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        })
            .then(response => {
                if (response.ok) {
                    // Update old main image
                    const oldMain = document.querySelector('.Images[data-is-main="true"]');
                    if (oldMain) {
                        oldMain.setAttribute("data-is-main", "false");

                        const oldSetMainBtn = oldMain.querySelector(".setmain");
                        if (oldSetMainBtn) {
                            oldSetMainBtn.style.display = "inline-block";
                        }

                        const oldDeleteBtn = oldMain.querySelector(".deleteImage");
                        if (oldDeleteBtn) {
                            oldDeleteBtn.disabled = false;
                            oldDeleteBtn.style.display = "inline-block";
                        }
                    }

                    // Set new main image
                    const newMain = document.querySelector(`.Images[data-id="${newMainId}"]`);
                    if (newMain) {
                        newMain.setAttribute("data-is-main", "true");

                        const newSetMainBtn = newMain.querySelector(".setmain");
                        if (newSetMainBtn) {
                            newSetMainBtn.style.display = "none";
                        }

                        const newDeleteBtn = newMain.querySelector(".deleteImage");
                        if (newDeleteBtn) {
                            newDeleteBtn.disabled = true;
                            newDeleteBtn.style.display = "none";
                        }
                    }

                } else {
                    alert("Failed to set image as main.");
                }
            })
            .catch(error => {
                console.error(error);
                alert("Error setting image as main.");
            });
    });
});
