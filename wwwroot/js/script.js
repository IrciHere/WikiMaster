var gameData;

var startTime;
var difference;
var updatedTime;
var tInterval;

function startGame() {
    gameData = document.getElementById("gameiframe").contentWindow.gameData;
    language = document.getElementById("gameiframe").contentWindow.language;

    document.getElementById("lPanelStartArticle").innerText = gameData.startArticle.articleTitle;
    document.getElementById("lPanelTargetArticle").innerText = gameData.targetArticle.articleTitle;

    var link = "api/Wiki/" + language + gameData.startArticle.articleUrl;
    getHtml(link);
    startStopwatch();
    document.getElementById("startGameButton").disabled = true;
}

function linkClicked(link) {
    if (link == "/game/startpage.html" || link == "srcdoc" || link[0] == '#') return;
    window.frames[0].stop();
    getHtml(link);
    var addToPath = decodeURI(link).replaceAll("_", " ").replace("/api/wiki/", "> ").replace(language, "").replace("/wiki%2F", "") + "\n";
    document.getElementById("yourGamePath").innerText += addToPath;
    var linkToCheck = "/api/wiki/" + language + gameData.targetArticle.articleUrl.replace("wiki/", "wiki%2F");
    if (link == linkToCheck) {       
        stopStopwatch();
    }
}


function getHtml(urlBody) {
    var page = urlBody.replace("wiki/", "wiki%2F").replace("api/wiki%2F", "api/wiki/");
    fetch(page)
        .then(response => response.text())
        .then((response) => loadPage(response))
        .catch(error => console.error('Unable to get items.', error));
}


function loadPage(data) {
    document.getElementById("gameiframe").srcdoc = data;
}

function startStopwatch() {
    startTime = new Date().getTime();
    tInterval = setInterval(getShowTime, 1);
}

function getShowTime() {
    updatedTime = new Date().getTime();

    difference = updatedTime - startTime;

    var minutes = Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((difference % (1000 * 60)) / 1000);
    var milliseconds = Math.floor(((difference % (1000 * 60)) / 10) % 100);
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;
    milliseconds = (milliseconds < 10) ? "0" + milliseconds : milliseconds;
    var textTime = minutes + ':' + seconds + ':' + milliseconds;
    document.getElementById("stopwatch").innerText = textTime;
}

function stopStopwatch() {
    clearInterval(tInterval);
}

function checkIfChallengeLink() {
    var urlHere = window.location.href;
    var checkedUrl = new URL(urlHere);
    var param = checkedUrl.searchParams.get("challenge");

    if (!param) return;


}