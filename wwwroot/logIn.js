async function GetUser(login, password) {
    const response = await fetch("/api/v1/users/logging/" + login + "," + password, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if(response.ok)
    {
        const userNicknameAndId = await response.json();

        let userArray = userNicknameAndId.split(',');

        localStorage.setItem('userNickname', userArray[0]);
        localStorage.setItem('userId', userArray[1]);
    }
    return response.ok;
}

document.forms["userForm"].addEventListener("submit", async e => {
    e.preventDefault();
    const form = document.forms["userForm"];
    const login = form.elements["login"].value;
    const password = form.elements["password"].value;

    try {
        const result = await GetUser(login, password);
        if (result) 
        {
            showMessage("Login was successful! Redirecting!");

            localStorage.setItem('userLogin', login);
            localStorage.setItem('userPassword', password);

            setTimeout(function () {
                window.location.href = 'http://localhost:5148/index.html';
            }, 3000);
        } 
        else
        {
            showMessage("Login or password entered incorrectly!");
        }
    } 
    catch (error) {
        console.error("Error while getting data: ", error);
    }
});

if(localStorage.getItem('userLogin') != null ||  localStorage.getItem('userPassword') != null || localStorage.getItem('userId') != null)
{
    const form = document.forms["userForm"];
    form.elements["login"].value = localStorage.getItem('userLogin');
    form.elements["password"].value = localStorage.getItem('userPassword');
}
else{
    console.log("No saved data");
}
