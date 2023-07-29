// Получение всех постов
async function GetPosts() { 
  const response = await fetch("/api/v1/posts", {
      method: "GET",
      headers: { "Accept": "application/json" }
  });
  if (response.ok === true) {
      const posts = await response.json();
      posts.forEach(postInfo => {
          MakePost(postInfo);
      });
  }
}

// создание строки для таблицы
function MakePost(postInfo) {
  var post = document.createElement('div');
  var galleryBottom = document.getElementById("galleryBottom");
  var parentDiv = galleryBottom.parentNode;

  post.id ='galleryBottom';
  post.className = 'post';

  var htmlText = "  <img src=\"  " + postInfo.picture + "  \"><div class=\"text\"><h1>  " + postInfo.nameOfPicture + "</h1><p class=\"nickname\">" + "by " + postInfo.nickname + "</p><p class=\"date\">"+ postInfo.date +"</p></div>";
  
  post.innerHTML = htmlText;

  parentDiv.insertBefore(post, galleryBottom);
}

if(localStorage.getItem('userId') != null && localStorage.getItem('userNickname') != null && localStorage.getItem('userLogin') != null && localStorage.getItem('userPassword') != null)
{
  var loggedDiv = document.createElement('div');
  var newDrawBtn = document.getElementById("newDrawBtn");
  var parentDiv = newDrawBtn.parentNode;

  loggedDiv.id ='loggedDiv';

  var htmlText = "<a title=\"Account Settings\" class=\"loggedText\" href=\"account.html\">Glad to see you: " + localStorage.getItem('userNickname') + "</a>";
  
  loggedDiv.innerHTML = htmlText;

  parentDiv.insertBefore(loggedDiv, newDrawBtn);
}
else
{
  var notLoggedDiv = document.createElement('div');
  var newDrawBtn = document.getElementById("newDrawBtn");
  newDrawBtn.disabled = "true";
  newDrawBtn.style.opacity = "0.6";
  newDrawBtn.style.cursor = "default";
  var parentDiv = newDrawBtn.parentNode;

  notLoggedDiv.id ='notLoggedDiv';

  var htmlText = "<a class=\"notLogged\" href=\"logIn.html\">Log In</a><a class=\"notLogged\" href=\"signUp.html\">Sign Up</a>";
  
  notLoggedDiv.innerHTML = htmlText;

  parentDiv.insertBefore(notLoggedDiv, newDrawBtn);
}

newDrawBtn = document.querySelector(".newDrawBtn")
newDrawBtn.addEventListener("click", () => {
    window.location.href = 'http://localhost:5148/paint.html';
})


GetPosts();