"use strict";

/* Gloablas */

var gCanvas;
var gCtx;
var gImgId;

var keyWordsSizes = [25, 25, 25];

/* Init() */

function init() {
  renderImgs();
  gCanvas = document.querySelector("#my-canvas");
  gCtx = gCanvas.getContext("2d");
  setTimeout(() => {
    var img = new Image();
    img.src = `img/1.jpg`;
    setMemeImgId(1);
    gCtx.drawImage(img, 0, 0, gCanvas.width, gCanvas.height);
    drawText();
    console.log("setTimeout , gCanvas", gCanvas);
  },100);
}

function onHome() {
  document.querySelector(".search-words").classList.remove("hidden");
}

/* renders */

function renderImgs(str = "") {
  var imgs = getImgs();
  var elImgs = document.querySelector(".img-wrapper");
  var strHtml = "";
  // var imgs = filterImgs(str)
  for (var i = 0; i < imgs.length; i++) {
    strHtml += `<div class="img-container">
        <img src="img/${i + 1}.jpg" alt="" class="img" onclick="onImgClick(${
      i + 1
    })" onclick="renderCanvas(${i + 1})">
    </div>`;
  }
  elImgs.innerHTML = strHtml;
}

//
function renderCanvas(imgId) {
  var img = new Image();
  img.src = `img/${imgId}.jpg`;
  gCtx.drawImage(img, 0, 0, gCanvas.width, gCanvas.height);
  drawText();
}

/* iImages  */

function onImgClick(imgId) {
  document.querySelector(".main-wrapper").classList.add("hidden");
  document.querySelector(".canvas-container").classList.remove("hidden");
  document.querySelector("#txt-input").classList.remove("hidden");
  document.querySelector(".btn-container").classList.remove("hidden");
  // document.querySelector('.words-container').classList.add('hidden')
  // document.querySelector('.search').classList.add('hidden')
  var img = new Image();
  img.src = `img/${imgId}.jpg`;
  if (!imgId) imgId = 0;
  setMemeImgId(imgId);
  gCtx.drawImage(img, 0, 0, gCanvas.width, gCanvas.height);
  drawText();
}

// function onSetFilterImgs(str) {
//     return filterImgs(str);
// }

function onClickFilter(value) {
  if (value === "funny") {
    keyWordsSizes[0] += 1;
    var size = keyWordsSizes[0];
    var elFunny = document.querySelector(".funny");
    var elAll = document.querySelector(".all");
    elAll.style.fontSize = `${getMaxFontSize()}px`;
    elFunny.style.fontSize = `${size}px`;
    renderImgs(value);
    if (keyWordsSizes[0] > 40) keyWordsSizes[0] = 16;
  }
  if (value === "political") {
    keyWordsSizes[1] += 1;
    var size = keyWordsSizes[1];
    var elPolitical = document.querySelector(".political");
    elPolitical.style.fontSize = `${size}px`;
    var elAll = document.querySelector(".all");
    elAll.style.fontSize = `${getMaxFontSize()}px`;
    renderImgs(value);
    if (keyWordsSizes[1] > 40) keyWordsSizes[1] = 16;
  }
  if (value === "pets") {
    keyWordsSizes[2] += 1;
    var size = keyWordsSizes[2];
    var elPets = document.querySelector(".pets");
    elPets.style.fontSize = `${size}px`;
    var elAll = document.querySelector(".all");
    elAll.style.fontSize = `${getMaxFontSize()}px`;
    renderImgs(value);
    if (keyWordsSizes[2] > 40) keyWordsSizes[2] = 16;
  }
  if (value === "") {
    keyWordsSizes = [25, 25, 25];
    size = 25;
    var elFunny = document.querySelector(".funny");
    elFunny.style.fontSize = `${size}px`;
    var elPolitical = document.querySelector(".political");
    elPolitical.style.fontSize = `${size}px`;
    var elPets = document.querySelector(".pets");
    elPets.style.fontSize = `${size}px`;
    renderImgs();
  }
}

function getMaxFontSize() {
  if (keyWordsSizes.length === 0) return;
  var maxSize = keyWordsSizes[0];
  keyWordsSizes.map(function (size) {
    if (maxSize < size) maxSize = size;
  });
  return maxSize;
}

function onSearch(value) {
  if (value === "funny") renderImgs(value);
  if (value === "political") renderImgs(value);
  if (value === "pets") renderImgs(value);
  if (value === "all") renderImgs("");
  else return;
}

/* texts */

function drawText() {
  const meme = getMeme();
  if (!meme.selectedImgId) meme.selectedImgId = gImgId;
  var memeTexts = meme.lines;
  memeTexts.forEach((memeText) => {
    gCtx.lineWidth = "2";
    gCtx.font = `${memeText.size}px ${memeText.font}`;
    gCtx.textAlign = memeText.align;
    gCtx.strokeStyle = memeText.stroke;
    var x;
    if (memeText.align === "left") x = 10;
    else if (memeText.align === "center") x = gCanvas.width / 2;
    else x = gCanvas.width - 10;
    gCtx.strokeText(memeText.txt, x, 40 + memeText.height);
    gCtx.fillStyle = memeText.color;
    gCtx.fillText(memeText.txt, x, 40 + memeText.height);
  });
}

function onTxtInput(elInput) {
  var value = elInput.value;
  var meme = getMeme();
  var currLineIdx = getLineIdx();
  meme.lines[currLineIdx].txt = value;
  const currImgId = meme.selectedImgId;
  renderCanvas(currImgId);
}

/* Lines */

function onSwitchLines() {
  switchLines();
  const currMeme = getMeme();
  const currLineIdx = getCurrLineIdx();
  if (currMeme.lines.length === 0) return;
  const memeText = currMeme.lines[currLineIdx].txt;
  var elLineInput = document.querySelector(".txt-input");
  elLineInput.value = "";
  elLineInput.placeholder = memeText;
}

function onAddLine() {
  addLine();
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onDeleteLine() {
  deleteLine();
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

/* styles changes :) */

function onChangeColor(color) {
  changeColor(color);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onChangeStroke(color) {
  changeStroke(color);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onChangeFont(font) {
  changeFont(font);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onAlign(align) {
  setAlign(align);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onFontSizeChange(diff) {
  changeFontSize(diff);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onLineHeightChange(diff) {
  changeLineHeight(diff);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onFontSizeChange(diff) {
  changeFontSize(diff);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

function onLineHeightChange(diff) {
  changeLineHeight(diff);
  const currImgId = getCurrMemeId();
  renderCanvas(currImgId);
}

/* utils & Helpers - check what needed */

function uploadImg(elForm, ev) {
  ev.preventDefault();
  document.getElementById("imgData").value = gCanvas.toDataURL("image/jpeg");

  function onSuccess(uploadedImgUrl) {
    uploadedImgUrl = encodeURIComponent(uploadedImgUrl);
    document.querySelector(".share-container").innerHTML = `
        <a class="btn home-btn" href="https://www.facebook.com/sharer/sharer.php?u=${uploadedImgUrl}&t=${uploadedImgUrl}" title="Share on Facebook" target="_blank" onclick="window.open('https://www.facebook.com/sharer/sharer.php?u=${uploadedImgUrl}&t=${uploadedImgUrl}'); return false;">
           Share   
        </a>`;
  }

  doUploadImg(elForm, onSuccess);
}

function doUploadImg(elForm, onSuccess) {
  var formData = new FormData(elForm);
  fetch("http://ca-upload.com/here/upload.php", {
    method: "POST",
    body: formData,
  })
    .then(function (res) {
      return res.text();
    })
    .then(onSuccess)
    .catch(function (err) {
      console.error(err);
    });
}

function loadImageFromInput(ev, onImageReady) {
  document.querySelector(".share-container").innerHTML = "";
  var reader = new FileReader();

  reader.onload = function (event) {
    var img = new Image();
    img.onload = onImageReady.bind(null, img);
    img.src = event.target.result;
  };
  reader.readAsDataURL(ev.target.files[0]);
}

function onImgInput(ev) {
  loadImageFromInput(ev, renderCanvas);
}

function downloadImg(elLink) {
  const data = gCanvas.toDataURL();
  elLink.href = data;
  elLink.download = "my-img.jpg";
}
