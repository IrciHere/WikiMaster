var uri = "../api/Game/";
var gameData;
var language;

function getGameItem() {
    language = document.querySelector('input[name="language"]:checked').value;
    uri += language;
    document.getElementById("gameCreation").style.display = "none";
    document.getElementById("gameLoading").style.display = "inline";
    fetch(uri)
        .then(response => response.json())
        .then(data => showGameItem(data))
        .catch(error => console.error('Unable to get items.', error));
}

function showGameItem(data) {
    var startData = data.startArticle;
    var targetData = data.targetArticle;

    document.getElementById("startarticle").getElementsByClassName("articleTitle")[0].innerHTML = startData.articleTitle;
    document.getElementById("startarticle").getElementsByClassName("articleSummary")[0].innerHTML = startData.articleSummary;
    document.getElementById("startPicture").src = startData.articleThumbnailUrl;

    document.getElementById("targetarticle").getElementsByClassName("articleTitle")[0].innerHTML = targetData.articleTitle;
    document.getElementById("targetarticle").getElementsByClassName("articleSummary")[0].innerHTML = targetData.articleSummary;
    document.getElementById("targetPicture").src = targetData.articleThumbnailUrl;

    gameData = data;
    console.log(gameData);

    //generateChallengeLink(gameData);

    document.getElementById("gameLoading").style.display = "none";
    document.getElementById("gameSummary").style.display = "inline";
    parent.document.getElementById("startGameButton").disabled = false;
}


function generateChallengeLink(jsonData) {
    var strJson = JSON.stringify(jsonData);
    console.log(strJson);
    var strData = btoa(strJson);
    console.log(strData);
    var link = "localhost:44304/?challenge=" + strData;
    document.getElementById("challengeLinkBox").value = link;
}