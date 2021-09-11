'use strict';


/* globals */

var gImgs = [{ id: 1, url: `~/img/1.jpg`,},
    { id: 2, url: `~/img/2.png` },
   
];

var gKeywords = ['funny', 'political', 'pets'];
var gCurrLineIdx = 0;
var gMeme = {
    selectedImgId: null,
    // selectedLineIdx: 0,
    lines: [{
            txt: 'Best Car Ever - ferarii',
            size: 20,
            align: 'center',
            color: 'black',
            stroke: 'white',
            font: 'impact',
            height: 0
        },
        {
            txt: 'I Like Speed',
            size: 20,
            align: 'center',
            color: 'black',
            stroke: 'white',
            font: 'impact',
            height: 150
        },
    ]
};



/* Meme */

function getMeme() {
    return gMeme;
}

function getCurrLineIdx() {
    return gCurrLineIdx
}

function getCurrMemeId() {
    return gMeme.selectedImgId;
}

function setMemeImgId(imgId) {
    gMeme.selectedImgId = imgId
}

/* Images */

function getImgs() {
    return gImgs
}

// function filterImgs(str) {
//     return gImgs.filter(img => {
//         var filtImg = img.keyWords.filter(word => word.includes(str.toLowerCase()));
//         if (filtImg[0]) return img;
//     });
// }

/* Lines */

function changeLineHeight(diff) {
    gMeme.lines[gCurrLineIdx].height += diff;
}

function switchLines() {
    if (gCurrLineIdx + 1 === gMeme.lines.length) gCurrLineIdx = 0;
    else gCurrLineIdx++;
}

function getLineIdx() {
    return gCurrLineIdx;
}



function deleteLine() {
    if (gMeme.lines.length === 0) return;
    gMeme.lines.splice(gCurrLineIdx, 1);
    gCurrLineIdx = 0
}

function addLine(txt = "newline") {
    gMeme.lines.push({
        txt,
        size: 40,
        align: 'center',
        color: 'black',
        stroke: 'white',
        font: 'impact',
        height: 180
    });
}



function changeFont(font) {
    gMeme.lines[gCurrLineIdx].font = font;
}

function setAlign(align) {
    gMeme.lines[gCurrLineIdx].align = align;
}

function changeColor(color) {
    gMeme.lines[gCurrLineIdx].color = color;
}

function changeStroke(color) {
    gMeme.lines[gCurrLineIdx].stroke = color;
}

function changeFontSize(diff) {
    gMeme.lines[gCurrLineIdx].size += diff;
}


