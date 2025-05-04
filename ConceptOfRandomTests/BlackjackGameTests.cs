using System.Reflection;
using ConceptOfRandom.CardGameDir;
using ConceptOfRandom.BlackjackGameDir;
namespace ConceptOfRandomTests;

public class BlackjackGameTests
{
    // Card Calculation tests
    // 
    [Theory]
    [InlineData(new[] { Rank.Ace,Rank.King },21)]
    [InlineData(new[] { Rank.Ace,Rank.Six },17)]
    [InlineData(new[] { Rank.Ace,Rank.Nine },20)]
    [InlineData(new[] { Rank.Ace,Rank.Three,Rank.Five },19)]
    [InlineData(new[] { Rank.Ace,Rank.Nine,Rank.Five },15)]
    [InlineData(new[] { Rank.Ace,Rank.King,Rank.Nine },20)]
    [InlineData(new[] { Rank.Ace,Rank.Ace },12)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Nine },21)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Nine,Rank.Nine },20)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Ace,Rank.Nine },12)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Ace,Rank.Ace,Rank.Nine },13)]
    [InlineData(new[] { Rank.Jack,Rank.Queen },20)]
    [InlineData(new[] { Rank.Ten,Rank.Six,Rank.Five },21)]
    [InlineData(new[] { Rank.King,Rank.Queen,Rank.Two },22)]
    [InlineData(new[] { Rank.Ace,Rank.King,Rank.Queen,Rank.Nine },30)]
    public void BjHandValue_ReturnsExpectedTotal(Rank[] ranks,int expected)
    {
        Hand hand = [];
        foreach (var rank in ranks)
            hand.Add(new Card(rank,Suit.Spades));
        Assert.Equal(expected,BlackjackGame.BjHandValue(hand));
    }
    
    [Theory]
    [InlineData(new[] { Rank.Ace,Rank.King },new[] { Rank.Ace,Rank.King },true)]
    [InlineData(new[] { Rank.Ace,Rank.Two },new[] { Rank.Ace,Rank.King },false)]
    public void BjPushCheck_BlackjackVsBlackjack_ReturnsTrue(Rank[] dealerRanks,Rank[] playerRanks, bool expected)
    {
        var dealerCards = MakeSpadeHandsOutOfRankList(dealerRanks);
        var playerCards = MakeSpadeHandsOutOfRankList(playerRanks);
        Assert.Equal(BlackjackGame.BjPushCheck(dealerCards, playerCards),expected);
    }
    
    [Fact]
    public void DealerBusts_PlayerWins()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();
        // dealer starts with 16 (10♠ + 6♥)
        var dealer = new Deck
        {
            new Card(Rank.Ten,  Suit.Spades),
            new Card(Rank.Six,  Suit.Hearts)
        };
    
        // player holds a hard 20
        var player = new Deck
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Queen,Suit.Clubs)
        };

        // custom mini-deck: dealer will draw 10♣ → bust to 26
        var deck = new Deck([new Card(Rank.Ten,Suit.Clubs)]);

        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealer, player, deck));

        Assert.Contains("You win!", output);
        CardDisplay.Current = originalStrategy;

    }

    //Dealer 18, player 17
    [Fact]
    public void BjWinCalculation_DealerBeatsPlayer()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();
        // dealer 16 10+6 -->+ 2 to reach 18
        var dealer = new Hand
        {
            new Card(Rank.Ten,  Suit.Spades),
            new Card(Rank.Six,  Suit.Hearts)
        };

        // player 17 K+7
        var player = new Hand
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Seven,Suit.Clubs)
        };

        var deck = new Deck([new Card(Rank.Two,Suit.Clubs)]);

        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealer, player, deck));

        Assert.Contains("You lose!", output);
        CardDisplay.Current = originalStrategy;
    }
    
    [Fact]
    public void BjWinCalculation_IsPushBJvsBJ()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();
        Hand dealerCards = [new(Rank.Ace, Suit.Spades), new(Rank.King, Suit.Hearts)];
        
        Hand playerCards = [new(Rank.Ace,Suit.Clubs),new(Rank.Queen,Suit.Diamonds)];

        // no cards will be drawn
        var deck = new Deck(new List<Card>());

        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealerCards, playerCards, deck));

        Assert.DoesNotContain("win",  output, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("lose", output, StringComparison.OrdinalIgnoreCase);
        CardDisplay.Current = originalStrategy;
    }
    
     [Fact]
    public void BjWinCalculation_PlayerBeatsDealer_PrintsYouWin()
    {
        // Arrange
        var original = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        // Dealer is already ≥17 but lower than the player
        var dealer = new Hand
        {
            new Card(Rank.King, Suit.Hearts),  // 10
            new Card(Rank.Seven, Suit.Clubs)   // 7 → 17
        };
        var player = new Hand
        {
            new Card(Rank.Ten, Suit.Spades),   // 10
            new Card(Rank.King, Suit.Diamonds) // 10 → 20
        };
        var deck = new Deck(new List<Card>());  // no draws

        // Act
        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealer, player, deck));

        // Assert
        Assert.Contains("You win!", output);
        
        CardDisplay.Current = original;
    }

    [Fact]
    public void BjWinCalculation_MidLoopPush_ReturnsWithoutWinOrLose()
    {
        // Arrange
        var original = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        // Dealer starts under 17 and will draw into a push
        var dealer = new Hand
        {
            new Card(Rank.Ten,  Suit.Spades),  // 10
            new Card(Rank.Five, Suit.Hearts)   // 5 → 15
        };
        var player = new Hand
        {
            new Card(Rank.Ace, Suit.Clubs),    // 11
            new Card(Rank.Ten, Suit.Diamonds)  // 10 → 21
        };
        // Next draw is a Six → dealer hits 21, pushCheck == true, returns early
        var deck = new Deck(new List<Card>
        {
            new Card(Rank.Six, Suit.Clubs)
        });

        // Act
        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealer, player, deck));

        // Assert: no “win” or “lose” printed on a push
        Assert.DoesNotContain("win",  output, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("lose", output, StringComparison.OrdinalIgnoreCase);

        CardDisplay.Current = original;
    }
    
    
    
    // thank you magic robot for this
    [Fact]
    public void BjGameRunner_PlayerTotal21_AutomaticallyStands_NoHitPrompt()
    {
        // ---------- Arrange ----------
        // Dealer starts at 17 so the dealer phase needs no extra draws.
        var dealer = new Hand();
        dealer.Add(new Card(Rank.King,  Suit.Hearts));   // 10
        dealer.Add(new Card(Rank.Seven, Suit.Spades));   //  7  -> 17

        // Player already holds 21 (Queen + 2 + Ace + 8).
        var player = new Hand();
        player.Add(new Card(Rank.Queen, Suit.Clubs));    // 10
        player.Add(new Card(Rank.Two,   Suit.Clubs));    //  2
        player.Add(new Card(Rank.Ace,   Suit.Hearts));   // 11/1
        player.Add(new Card(Rank.Eight, Suit.Spades));   //  8  --> 21

        // Deck won’t actually be used because dealer already has 17.
        var deck = new Deck();

        // Capture console output (and give an empty input stream
        // – no ReadLine will occur, but it avoids blocking just in case).
        using var input  = new StringReader(string.Empty);
        using var output = new StringWriter();
        var originalIn  = Console.In;
        var originalOut = Console.Out;
        Console.SetIn(input);
        Console.SetOut(output);

        try
        {
            // ---------- Act ----------
            BlackjackGame.BjGameRunner(dealer, player, deck);

            // ---------- Assert ----------
            var text = output.ToString();

            // 1) Auto-stand message must appear …
            Assert.Contains("21! You stand automatically.", text);

            // 2) … and the usual hit/stand prompt must NOT appear.
            Assert.DoesNotContain("Type 1 to Hit", text);
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }
    
    //Helper Function for getting the console out, used smartbotman helped me 
    static string CaptureConsoleOut(Action action)
    {
        var original = Console.Out;
        var sw = new StringWriter();
        Console.SetOut(sw);

        try { action(); }
        finally { Console.SetOut(original); }

        return sw.ToString();
    }

    static Hand MakeSpadeHandsOutOfRankList(Rank[] rankList)
    {
        var hand = new Hand();
        foreach (var rank in rankList)
            hand.Add(new Card(rank,Suit.Spades));
        return hand;
    }
    
    
    [Fact]
    public void PrintSingleHand_WritesHeadingAndReturnsHandString()
    {
        // Arrange
        var hand    = new Hand();             // empty hand is fine for this test
        var heading = "Test Heading:";
        var expectedBody = hand.ToString();

        using var sw = new StringWriter();
        var originalOut = Console.Out;
        Console.SetOut(sw);

        try
        {
            // Act
            var result = BlackjackGame.PrintSingleHand(heading, hand);
            var consoleOutput = sw.ToString();

            // Assert
            // It should have written the heading (with a newline) to the console:
            Assert.Contains(heading + Environment.NewLine, consoleOutput);
            // And return exactly hand.ToString()
            Assert.Equal(expectedBody, result);
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void PrintHands_WritesBothHeadingsAndReturnsConcatenatedHandStrings()
    {
        // Arrange
        var dealer = new Hand();
        var player = new Hand();
        var expectedDealerBody = dealer.ToString();
        var expectedPlayerBody = player.ToString();
        var expectedReturn     = expectedDealerBody + expectedPlayerBody;

        using var sw = new StringWriter();
        var originalOut = Console.Out;
        Console.SetOut(sw);

        try
        {
            // Act
            var result = BlackjackGame.PrintHands(dealer, player);
            var consoleOutput = sw.ToString();

            // Assert: headings printed
            Assert.Contains("Dealer's cards:\n" + Environment.NewLine, consoleOutput);
            Assert.Contains("Your cards:" + Environment.NewLine,    consoleOutput);

            // Assert: return value is the two hand-strings concatenated
            Assert.Equal(expectedReturn, result);
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }
    
    
    // Magic robot thinking of ways to test the actual game in ways i could never imagine
    
     private static object? CallPrivateStatic(Type t, string name, params object?[] args)
    {
        var mi = t.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(mi);                          // fail fast if the signature ever changes
        return mi!.Invoke(null, args);
    }

    /* -----------------------------------------------------------------
       1)  BjGameRunner – player HITS once and BUSTS (prints “BUST!”)
       ----------------------------------------------------------------- */
    [Fact]
    public void BjGameRunner_PlayerBusts_PrintsBust()
    {
        // dealer 8
        var dealer = new Hand
        {
            new Card(Rank.Two,  Suit.Hearts),
            new Card(Rank.Six,  Suit.Hearts)
        };

        // player 19
        var player = new Hand
        {
            new Card(Rank.Ten,  Suit.Spades),
            new Card(Rank.Nine,  Suit.Spades)
        };

        // next card = K ♦  → bust
        var deck = new Deck
        {
            new Card(Rank.King, Suit.Diamonds)
        };

        using var input  = new StringReader("1\n");          // choose “Hit” once
        using var output = new StringWriter();
        var oldIn  = Console.In;
        var oldOut = Console.Out;
        Console.SetIn(input);
        Console.SetOut(output);

        try
        {
            CallPrivateStatic(typeof(BlackjackGame), "BjGameRunner",
                              dealer, player, deck);

            var text = output.ToString();
            Assert.Contains("BUST!", text);                  // message reached console
            Assert.Equal(3, player.Count);                   // drew exactly one card
        }
        finally { Console.SetIn(oldIn);  Console.SetOut(oldOut); }
    }

    /* -----------------------------------------------------------------
       2)  BjGameSetUp – deals two cards each & leaves dealer’s second
           card face-down; uses normal deck + shuffle.
       ----------------------------------------------------------------- */
    [Fact]
    public void BjGameSetUp_DealsTwoCards_Etc()
    {
        var dealer = new Hand();
        var player = new Hand();

        // first input = 0 (stand immediately)
        // second input = N (don’t play again)
        using var input  = new StringReader("0\nN\n");
        using var output = new StringWriter();
        var oldIn  = Console.In;
        var oldOut = Console.Out;
        Console.SetIn(input);
        Console.SetOut(output);

        try
        {
            CallPrivateStatic(typeof(BlackjackGame), "BjGameSetUp",
                              dealer, player);

            Assert.Equal(2, player.Count);

            var txt = output.ToString();
            Assert.Contains("Dealer's cards:", txt);
            Assert.Contains("Your cards:",    txt);
            Assert.Contains("Would you like to play again?", txt);
        }
        finally { Console.SetIn(oldIn);  Console.SetOut(oldOut); }
    }

    /* -----------------------------------------------------------------
       3)  BjGameStart – reached via the ctor; verifies that the user’s
           menu choice switches the global CardDisplay strategy.
       ----------------------------------------------------------------- */
    [Fact]
    public void BjGameStart_RespectsDisplayStyleChoice()
    {
        var originalStrategy = CardDisplay.Current;

        // 2 → Symbol display, 0 → stand, N → no replay
        using var input  = new StringReader("2\n0\nN\n");
        using var output = new StringWriter();
        var oldIn  = Console.In;
        var oldOut = Console.Out;
        Console.SetIn(input);
        Console.SetOut(output);

        try
        {
            _ = new BlackjackGame();                        // ctor calls BjGameStart

            Assert.IsType<SymbolDisplayStrategy>(CardDisplay.Current);

            var txt = output.ToString();
            Assert.Contains("Welcome to Blackjack!", txt);
            Assert.Contains("Choose your display style:", txt);
        }
        finally
        {
            Console.SetIn(oldIn);   Console.SetOut(oldOut);
            CardDisplay.Current = originalStrategy;         // leave global state untouched
        }
    }
    
    //
}

