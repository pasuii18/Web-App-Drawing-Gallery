// Inputs
const nickname = document.getElementById('nickname');
const password = document.getElementById('password');
nickname.value = localStorage.getItem('userNickname');
password.value = localStorage.getItem('userPassword');

// Div's
const nicknameDiv = document.getElementById('nicknameDiv');
nicknameDiv.style.display = "none";

const passwordDiv = document.getElementById('passwordDiv');
passwordDiv.style.display = "none";

var deleteMyAccount = document.getElementById('deleteMyAccount');
deleteMyAccount.style.display = "none";

var deleteAgreement = document.getElementById('deleteAgreement');

// Buttons
let deleteBtn = document.querySelector(".deleteBtn");
let deleteBtnDiv = document.getElementById('deleteBtn');

let changeBtn = document.querySelector(".changeBtn")
let changeBtnDiv = document.getElementById('changeBtn');

let saveBtn = document.querySelector(".saveBtn")
let saveBtnDiv = document.getElementById('saveBtn');
saveBtnDiv.style.display = "none";

let logOutBtn = document.querySelector(".logOutBtn")
let logOutBtnDiv = document.getElementById('logOutBtn');

deleteBtn.addEventListener("click", async () => {
    if (deleteAgreement.value === "I'm agree") {
        try {
            const result = await DeleteUser(localStorage.getItem('userId'), localStorage.getItem('userLogin'));
            if (result) 
            {
                deleteAgreement.value = "";
                localStorage.clear();

                showMessage("You have successfully deleted your account. Redirecting!");

                setTimeout(function () {
                    window.location.href = 'http://localhost:5148/index.html';
                }, 3000);
            } 
            else 
            {
                showMessage("For some reason, the account cannot be deleted. Please try again later.");
                return;
            }
        } 
        catch (error) {
            console.error("Error while getting data: ", error);
            return;
        }
    } 
    else 
    {
        nicknameDiv.style.display = "none";
        passwordDiv.style.display = "none";
        changeBtnDiv.style.display = "none";
        deleteMyAccount.style.display = "block";
        showMessage("This action cannot be undone!");
    }
});

changeBtn.addEventListener("click", () => {
    changeBtnDiv.style.display = "none";
    deleteBtnDiv.style.display = "none";
    nicknameDiv.style.display = "block";
    passwordDiv.style.display = "block";
    saveBtnDiv.style.display = "block";
    showMessage("The minimum length of the name and password is 3 characters!");
})

logOutBtn.addEventListener("click", () => {
    localStorage.clear();
    showMessage("You have successfully logged out!");
    setTimeout(function () {
        window.location.href = 'http://localhost:5148/index.html';
    }, 3000);
})

saveBtn.addEventListener("click", async () => {
    const userNickname = localStorage.getItem('userNickname');
    const userId = localStorage.getItem('userId');
    const userLogin = localStorage.getItem('userLogin');
    const newPassword = password.value;
    const newNickname = nickname.value;

    if (newPassword.length < 3 || newNickname.length < 3) {
        showMessage("Nickname and password must be at least 3 characters long.");
        return;
    }

    try {
        if (newNickname === userNickname) {
            await EditUser(userId, newNickname, userLogin, newPassword);
        } else {
            const userWithNicknameExists = await GetUserNickname(newNickname);
            if (userWithNicknameExists) {
                showMessage("User with this login already exists!");
                return;
            }
            await EditUser(userId, newNickname, userLogin, newPassword);
        }

        showMessage("The data has been successfully changed!");
        localStorage.setItem('userNickname', newNickname);
        localStorage.setItem('userPassword', newPassword);

        setTimeout(function () {
            window.location.href = 'http://localhost:5148/index.html';
        }, 3000);
    } catch (error) {
        console.error("Error while getting data: ", error);
    }
});

// Удаление пользователя
async function DeleteUser(userId, userLogin) {
    const response = await fetch("/api/v1/users/" + userId + "," + userLogin, {
        method: "DELETE",
        headers: { "Accept": "application/json"}
    });
    return response.ok;
}

// Изменение пользователя
async function EditUser(userId, userNickname, userLogin, userPassword) {
    const response = await fetch("api/v1/users/" + userId, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(userId, 10),
            nickname: userNickname,
            login: userLogin,
            password: userPassword
        })
    });
    return response.ok;
}

// Существует ли
async function GetUserNickname(nickname) {
    const response = await fetch("api/v1/users/isNicknameExist/" + nickname, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    return response.ok;
}