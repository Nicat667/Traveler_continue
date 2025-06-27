"use strict"


document.querySelectorAll(".deleteRoomImage").forEach(btn => {
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


//document.querySelectorAll(".setmainRoomImage").forEach(btn => {
//    btn.addEventListener("click", function () {
//        const newMainId = this.getAttribute("data-id");
//        const roomId = this.getAttribute("roomId");

//        fetch(`https://localhost:7107/Admin/Room/SetMain?imageId=${newMainId}&roomId=${roomId}`, {
//            method: "POST",
//            headers: {
//                "Content-Type": "application/json; charset=UTF-8"
//            }
//        })
//            .then(response => {
//                if (response.ok) {
//                    // Update old main image
//                    const oldMain = document.querySelector('.Images[data-is-main="true"]');
//                    if (oldMain) {
//                        oldMain.setAttribute("data-is-main", "false");

//                        const oldSetMainBtn = oldMain.querySelector(".setmainRoomImage");
//                        if (oldSetMainBtn) {
//                            oldSetMainBtn.style.display = "inline-block";
//                        }

//                        const oldDeleteBtn = oldMain.querySelector(".deleteRoomImage");
//                        if (oldDeleteBtn) {
//                            oldDeleteBtn.disabled = false;
//                            oldDeleteBtn.style.display = "inline-block";
//                        }
//                    }

//                    // Set new main image
//                    const newMain = document.querySelector(`.Images[data-id="${newMainId}"]`);
//                    if (newMain) {
//                        newMain.setAttribute("data-is-main", "true");

//                        const newSetMainBtn = newMain.querySelector(".setmainRoomImage");
//                        if (newSetMainBtn) {
//                            newSetMainBtn.style.display = "none";
//                        }

//                        const newDeleteBtn = newMain.querySelector(".deleteRoomImage");
//                        if (newDeleteBtn) {
//                            newDeleteBtn.disabled = true;
//                            newDeleteBtn.style.display = "none";
//                        }
//                    }

//                } else {
//                    alert("Failed to set image as main.");
//                }
//            })
//            .catch(error => {
//                console.error(error);
//                alert("Error setting image as main.");
//            });
//    });
//});



document.querySelectorAll(".setmainRoomImage").forEach(btn => {
    btn.addEventListener("click", function () {
        const newMainId = this.getAttribute("data-id");
        const roomId = this.getAttribute("roomId");
        debugger
        fetch(`https://localhost:7107/Admin/Room/SetMain?imageId=${newMainId}&roomId=${roomId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => {
                if (response.ok) {
                    // Update previous main image UI
                    const oldMain = document.querySelector('.Images[data-is-main="true"]');
                    if (oldMain) {
                        oldMain.setAttribute("data-is-main", "false");

                        const oldSetMainBtn = oldMain.querySelector(".setmainRoomImage");
                        if (oldSetMainBtn) {
                            oldSetMainBtn.style.display = "inline-block";
                        }

                        const oldDeleteBtn = oldMain.querySelector(".deleteRoomImage");
                        if (oldDeleteBtn) {
                            oldDeleteBtn.disabled = false;
                            oldDeleteBtn.style.display = "inline-block";
                        }
                    }

                    // Update new main image UI
                    const newMain = document.querySelector(`.Images[data-id="${newMainId}"]`);
                    if (newMain) {
                        newMain.setAttribute("data-is-main", "true");

                        const newSetMainBtn = newMain.querySelector(".setmainRoomImage");
                        if (newSetMainBtn) {
                            newSetMainBtn.style.display = "none";
                        }

                        const newDeleteBtn = newMain.querySelector(".deleteRoomImage");
                        if (newDeleteBtn) {
                            newDeleteBtn.disabled = true;
                            newDeleteBtn.style.display = "none";
                        }
                    }

                } else {
                    alert("Failed to set main image.");
                }
            })
            .catch(error => {
                console.error("Error setting main image:", error);
                alert("Error occurred while setting main image.");
            });
    });
});