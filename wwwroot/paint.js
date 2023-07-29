// Drawing Script
const canvas = document.getElementById("canvas")
const ctx = canvas.getContext("2d")

canvas.height = window.innerHeight
canvas.width = window.innerWidth

ctx.strokeStyle = "#ffffff";
ctx.lineWidth = 4;

let clrs = document.querySelectorAll(".clr")
clrs = Array.from(clrs)
clrs.forEach(clr => {
    clr.addEventListener("click", () => {
        ctx.strokeStyle = clr.dataset.clr
    })
})

let prevX = null
let prevY = null
let draw = false

window.addEventListener("mousedown", (e) => draw = true)
window.addEventListener("mouseup", (e) => draw = false)

window.addEventListener("mousemove", (e) => {
    if(prevX == null || prevY == null || !draw){
        prevX = e.clientX
        prevY = e.clientY
        return
    }

    let currentX = e.clientX 
    let currentY = e.clientY 

    ctx.beginPath()
    ctx.moveTo(prevX, prevY)
    ctx.lineTo(currentX, currentY)
    ctx.stroke()

    prevX = currentX
    prevY = currentY
})


// Buttons
let clearBtn = document.querySelector(".clear")
clearBtn.addEventListener("click", () => {
    if(confirm("Do you realy want to?"))
    {
        ctx.clearRect(0, 0, canvas.width, canvas.height)
    }
})
let inputBox = document.getElementById('nameOfPicture');
inputBox.addEventListener("click", () => {
    if(inputBox.value === "Name of your picture")
    {
        inputBox.value = "";
    }
})

// Color Picker
colorPicker = document.querySelector("#color-picker");
const defaultColor = "#ffffff";
colorPicker.value = defaultColor;
function watchColorPicker(event) {
    ctx.strokeStyle = event.target.value;
}
colorPicker.addEventListener("change", watchColorPicker, false);


// Requests
// Post Creating
async function CreatePost(userNickname, userNameOfPicture, userPicture, userDate) {
    const response = await fetch("api/v1/posts", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                nickname: userNickname,
                nameofpicture: userNameOfPicture,
                picture: userPicture,
                date: userDate
            })
        });
        if (response.ok === true) {
            showMessage("Drawing was seccesfully created! Redirecting!");
            setTimeout(function() {
                window.location.href = 'http://localhost:5148/index.html';
              }, 3000);
        }
}

// Collecting information
let saveBtn = document.querySelector(".save")
saveBtn.addEventListener("click", () => {
    if(document.getElementById('nameOfPicture').value != null){
        if(localStorage.getItem('userNickname') != null)
        {
            const nickname = localStorage.getItem('userNickname');
            const nameOfPicture = document.getElementById('nameOfPicture').value;
            var canvasImg = document.getElementById('canvas');
            var picture = canvasImg.toDataURL("image/png");          
            var dateObj = new Date();
            var date = dateObj.toLocaleString();
                            
            CreatePost(nickname, nameOfPicture, picture, date);  
        }
        else{
            showMessage("There are some problems with your data, please try to log in again");
        }
    }
    else{
        showMessage("Сome up with a name for your painting!");
    }
})