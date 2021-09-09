'use strict';


/* globals */


var gImgs = [{ id: 1, url: `img/1.jpg`,},
    { id: 2, url: `img/2.png` },
   
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


/* View - fonts aligns colors and stuffs.. */


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


/* questions for later */

// how to put an svg currectly and how to put it on specifically on input 

// how to fix guthub bug with the imgs 

// ask about the home page "bug", maybe setTimout is the answer 

// how to work with outsided libraries currectly (import libraires) , same to fonts and all the other stuffs :)

// how to build the css structure and keep it clean 

// when and where we should use flex and grid (the differnce)

// responsive - shor brief on how to do it correectly

// ask about window functions and how it works 

// use strict 

// ui && ux

// how to choose colors correctly and how to improve the user experience , i cant see it by myself :(

// id,classes and all the score caluclations - how it works