const gameBoard =
    document.getElementById("gameBoard");

const statusText =
    document.getElementById("status");

const restartButton =
    document.getElementById("restartButton");

const difficulty =
    document.getElementById("difficulty");


let firstCard = null;

let secondCard = null;

let lockBoard = false;

let matchedPairs = 0;


function getSymbols() {

    if (
        difficulty.value === "easy"
    ) {

        return [

            "🍎",
            "🍌",
            "🍇",
            "🍒",

            "🍎",
            "🍌",
            "🍇",
            "🍒"
        ];
    }


    if (
        difficulty.value === "medium"
    ) {

        return [

            "🍎",
            "🍌",
            "🍇",
            "🍒",
            "🥝",
            "🍉",

            "🍎",
            "🍌",
            "🍇",
            "🍒",
            "🥝",
            "🍉"
        ];
    }


    return [

        "🍎",
        "🍌",
        "🍇",
        "🍒",
        "🥝",
        "🍉",
        "🍍",
        "🍑",

        "🍎",
        "🍌",
        "🍇",
        "🍒",
        "🥝",
        "🍉",
        "🍍",
        "🍑"
    ];
}


function shuffle(array) {

    for (
        let i = array.length - 1;
        i > 0;
        i--
    ) {

        const j =
            Math.floor(
                Math.random() * (i + 1)
            );

        [array[i], array[j]] =
        [array[j], array[i]];
    }

    return array;
}


function createBoard() {

    gameBoard.innerHTML = "";

    matchedPairs = 0;

    const symbols =
        getSymbols();

    const shuffled =
        shuffle([...symbols]);


    if (
        difficulty.value === "easy"
    ) {

        gameBoard.style.gridTemplateColumns =
            "repeat(4, 120px)";
    }


    if (
        difficulty.value === "medium"
    ) {

        gameBoard.style.gridTemplateColumns =
            "repeat(4, 120px)";
    }


    if (
        difficulty.value === "hard"
    ) {

        gameBoard.style.gridTemplateColumns =
            "repeat(4, 120px)";
    }


    shuffled.forEach((symbol) => {

        const card =
            document.createElement("div");

        card.classList.add("card");

        card.dataset.symbol =
            symbol;

        card.innerText =
            symbol;

        card.addEventListener(
            "click",
            flipCard
        );

        gameBoard.append(card);
    });
}


function flipCard() {

    if (
        lockBoard ||
        this === firstCard ||
        this.classList.contains("matched")
    ) {

        return;
    }


    this.classList.add("flipped");


    if (!firstCard) {

        firstCard = this;

        return;
    }


    secondCard = this;

    checkMatch();
}


function checkMatch() {

    const isMatch =

        firstCard.dataset.symbol ===
        secondCard.dataset.symbol;


    if (isMatch) {

        firstCard.classList.add("matched");

        secondCard.classList.add("matched");

        matchedPairs++;

        resetTurn();


        const totalPairs =

            getSymbols().length / 2;


        if (
            matchedPairs === totalPairs
        ) {

            statusText.innerText =
                "You Win!";
        }

    } else {

        lockBoard = true;

        setTimeout(() => {

            firstCard.classList.remove(
                "flipped"
            );

            secondCard.classList.remove(
                "flipped"
            );

            resetTurn();

        }, 1000);
    }
}


function resetTurn() {

    firstCard = null;

    secondCard = null;

    lockBoard = false;
}


restartButton.addEventListener(
    "click",
    restartGame
);


function restartGame() {

    firstCard = null;

    secondCard = null;

    lockBoard = false;

    matchedPairs = 0;

    statusText.innerText =
        "Find all pairs!";

    createBoard();
}


difficulty.addEventListener(
    "change",
    restartGame
);


createBoard();