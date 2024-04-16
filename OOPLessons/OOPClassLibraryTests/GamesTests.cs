using FluentAssertions;
using OOPClassLibrary.Games.Exceptions;
using OOPClassLibrary.Games.Models;

namespace OOPClassLibraryTests;

public class GamesTests
{
    [Fact]
    public void TestGameCreation()
    {
        Game eldenRing = new Game("elden-ring", "Elden Ring");
        eldenRing.IsDlc.Should().BeFalse();

        Game shadowOfErdtree = eldenRing.AddNewDlc("elden-ring-shadow-of-the-erdtree", "Elden Ring Shadow of the Erdtree");
        shadowOfErdtree.IsDlc.Should().BeTrue();

        eldenRing.DlcGames.Should().HaveCount(1);
        shadowOfErdtree.DlcGames.Should().HaveCount(0);

        Action addDlcAction = () => shadowOfErdtree.AddNewDlc("useless-id", "");
        addDlcAction.Should().Throw<DlcCannotHaveOtherDlcException>();

        eldenRing.MainGame.Should().BeNull();
        
        shadowOfErdtree.MainGame.Should().NotBeNull();
        shadowOfErdtree.MainGame.Should().Be(eldenRing);
    }

    [Fact]  
    public void TestStorePlatformAndLaunchers() 
    {
        Launcher gogLauncher = new Launcher("gog", "GOG Galaxy");
        Launcher epicLauncher = Launcher.EpicGames;

        gogLauncher.Equals(epicLauncher).Should().BeFalse();

        Store steamStore = Store.Steam;

        steamStore.Equals(Launcher.Steam).Should().BeFalse();

        Store steamStore2 = new("steam", "Steam Store 2");
        steamStore2.Equals(steamStore).Should().BeTrue();
        ReferenceEquals(steamStore2, steamStore).Should().BeFalse();
    }

    [Fact]
    public void PurchasePriceTest()
    {
        PurchasePrice p1 = 100.2m;
        PurchasePrice p2 = 10.252m;
        (p1 > p2).Should().BeTrue();

        (p2 >= 10.25m).Should().BeTrue();

        Action action = () => { PurchasePrice p3 = -5m; };
        action.Should().Throw<ArgumentException>().WithMessage("Price must be >= 0");
        PurchasePrice p4 = 150L;
        p4.Value.Should().Be(150.00m);

        decimal d1 = p1;
    }
}
