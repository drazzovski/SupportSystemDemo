$(document).ready(function () {

    var deck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21];
    var counter = 0;
    var arr1 = [];
    var arr2 = [];
    var arr3 = [];
    
    var cardNo;

    $("#gameBtn").on("click", function () {

        if (counter === 0) {
            deck.sort(function () { return 0.5 - Math.random() });
            SelectedCard(0);
            Game();
        } else {
            counter = 0;
            SelectedCard(0);
            Game();
        }
    });


    $(".insideCard1").on("click", function () {
          SelectedCard(1);
          Game();
    })

    $(".insideCard2").on("click", function () {
          SelectedCard(2);
          Game();
    })
     
    $(".insideCard3").on("click", function () {
       
          SelectedCard(3);
          Game();
    })


    function Game() {

        $("span").remove()

        if (counter > 3) {
            return;
        }

        arr1.length = 0;
        arr2.length = 0;
        arr3.length = 0;

        for (i = 0; i < deck.length; i += 3) {
            arr1.push(deck[i]);
            arr2.push(deck[i + 1]);
            arr3.push(deck[i + 2]);
        } 

        var temp1 = arr1.slice(0).sort(function () { return 0.5 - Math.random() });
        var temp2 = arr2.slice(0).sort(function () { return 0.5 - Math.random() });
        var temp3 = arr3.slice(0).sort(function () { return 0.5 - Math.random() });

        for (i = 0; i < temp1.length; i++) {
            $(".insideCard1").append(`<span>${temp1[i]}</span>`)
        }

        for (i = 0; i < temp2.length; i++) {
            $(".insideCard2").append(`<span>${temp2[i]}</span>`)
        }

        for (i = 0; i < temp3.length; i++) {
            $(".insideCard3").append(`<span>${temp3[i]}</span>`)
        }
    }

    function SelectedCard(num) {

        if (counter === 0) {
            $(".gameh4").text("This is the game where computer guess your number. First remember one of the numbers and select the card that contains it.");
            $("#gameBtn").text("Start New Game");
        } else if (counter === 1) {
            $(".gameh4").text("Choose again the card where is your number.");
        } else if (counter === 2) {
            $(".gameh4").text("Select for last time the card where is your number.");
        }


        if (num === 1) {
            deck.length = 0;
            deck = arr2.concat(arr1, arr3)
        } else if (num === 2) {
            deck.length = 0;
            deck = arr1.concat(arr2, arr3);
        } else if (num === 3) {
            deck.length = 0;
            deck = arr2.concat(arr3, arr1);
        }

        counter++;

        if (counter > 3) {
            $(".gameh4").text(`Your number is ${deck[10]}`);
            return;
        }
    } 
});

