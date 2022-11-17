using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Windows.Media;
using GameClient;
using GameClient.MVVM.Builder;
using GameClient.MVVM.Core;
using GameClient.MVVM.Model;
using GameClient.MVVM.Model.ShotModels;
using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels;
using GameClient.Net.Decorator;
using System.Threading;
using System;
using System.Collections.ObjectModel;
using GameClient.MVVM.ViewModel;
using Game;
using GameServer;
using GameSever;
using System.Threading.Tasks;

namespace TestProject;

[TestClass]
public class UnitTest
{
    public Server server;
    MainViewModel mainViewModel;
    MainWindow mainWindow;

    [TestInitialize]
    public void SetUpTests()
    {
        InitUI();
        server = new Server();
        mainViewModel = new MainViewModel();

    }
    //System.InvalidOperationException: The calling thread must be STA, because many UI components require this.
    /*[TestMethod]
    [STAThread]// Testuojama ar tile yra tinkamoje pozicijoje ir ar þaidëjas sugebës atlikti ðûvá
    public void TestMethodPlayerFireTrue()
    {
        Player player = new Player();
        Player player2 = new Player();
        player.SetEnemy(player2);
        Tile tile = new Tile(1, 1);
        Assert.AreEqual(player.Fire(tile), true);
    }
    [TestMethod]// Testuojama ar tile yra tinkamoje pozicijoje ir ar þaidëjas sugebës atlikti ðûvá
    public void TestMethodPlayerFireFalse()
    {
        Player player = new Player();
        Player player2 = new Player();
        player.SetEnemy(player2);
        try { Tile tile = new Tile(-1, -1); }
        catch { }
        Assert.AreNotEqual(false, player.Fire(tile));
    }*/

    [TestMethod]// Testuojama ar padavus strategijà classicShot ðûvio þala pasikeis á 1
    public void TestShootStrategySetDamageClassicShot()
    {
        ShootStrategy shootStrategy = new ShootStrategy();
        ClassicShot classicShot = new ClassicShot();
        shootStrategy.SetStrategy(classicShot);
        Assert.AreEqual(1, shootStrategy.SetDamage());
    }

    [TestMethod]// Testuojama ar padavus strategijà FugueShoot ðûvio þala pasikeis á 4
    public void TestShootStrategySetDamageFugueShot()
    {
        ShootStrategy shootStrategy = new ShootStrategy();
        FugueShoot classicShot = new FugueShoot();
        shootStrategy.SetStrategy(classicShot);
        Assert.AreEqual(4, shootStrategy.SetDamage());
    }

    //BattleshipDestroyable klasës testai
    [TestMethod]// Testuojama ar pridëjus raketà "Rocket1" Display teisingai atvaizduos raketø sàraðà
    public void TestBattleshipDestroyableAddAndDisplayRockets()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        battleshipDestroyable.AddRocket("Rocket1");
        Assert.AreEqual("Rockets:\nRocket1\n", battleshipDestroyable.Display());
    }

    [TestMethod]// Testuojama ar NEpridëjus raketos "Rocket1" Display teisingai atvaizduos raketø sàraðà
    public void TestBattleshipDestroyableAddAndDisplayEmptyRockets()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        Assert.AreEqual("Rockets:\n", battleshipDestroyable.Display());
    }

    [TestMethod]// Testuojama ar pridëjus raketas "Rocket1" ir "Rocket2", tuomet iððovus "Rocket1" Display teisingai atvaizduos raketø sàraðà
    public void TestBattleshipDestroyableAddShootAndDisplayRocketsList()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        battleshipDestroyable.AddRocket("Rocket1");
        battleshipDestroyable.AddRocket("Rocket2");
        battleshipDestroyable.ShootRocket("Rocket2");
        Assert.AreEqual("Rockets:\nRocket1\n", battleshipDestroyable.Display());
    }
    //System.InvalidOperationException: The calling thread must be STA, because many UI components require this.
    /*[TestMethod]// Testuojama ar nustaèius mygtuko fonà jis pasikeis á Moccasin spalvà
    public void TestBattleshipDestroyableSetButtonBackground()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        Button button = new Button();
        battleshipDestroyable.SetButtonBackground(button);
        Assert.AreEqual(button.Background, Brushes.Moccasin);
    }*/

    //CarrierLoadable klasës testai
    [TestMethod]// Testuojama ar pridëjus lëktuvà "Plane1" Display teisingai atvaizduos lëktuvø sàraðà
    public void TestCarrierLoadableAddAndDisplayPlanes()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        carrierLoadable.AddPlane("Plane1");
        Assert.AreEqual("Planes:\nPlane1\n", carrierLoadable.Display());
    }

    [TestMethod]// Testuojama ar NEpridëjus lëktuvo "Plane1" Display teisingai atvaizduos lëktuvø sàraðà
    public void TestCarrierLoadableAddAndDisplayEmptyPlanesList()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        Assert.AreEqual("Planes:\n", carrierLoadable.Display());
    }

    [TestMethod]// Testuojama ar pridëjus lëktuvus "Plane1" ir "Plane2", tuomet iðëmus "Plane1" Display teisingai atvaizduos lëktuvø sàraðà
    public void TestCarrierLoadableAddPlanesRemovePlaneAndDisplayPlanesList()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        carrierLoadable.AddPlane("Plane1");
        carrierLoadable.AddPlane("Plane2");
        carrierLoadable.RemovePlane("Plane2");
        Assert.AreEqual("Planes:\nPlane1\n", carrierLoadable.Display());
    }
    //System.InvalidOperationException: The calling thread must be STA, because many UI components require this.
    /*[TestMethod]// // Testuojama ar nustaèius mygtuko fonà jis pasikeis á LightGreen spalvà
    public void TestCarrierLoadableSetButtonBackground()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        Button button = new Button();
        carrierLoadable.SetButtonBackground(button);
        Assert.AreEqual(button.Background, Brushes.LightGreen);
    }*/

    //DestroyerDecorated klasës testai
    [TestMethod]// Testuojama ar pridëjus bombà "Bomb1" Display teisingai atvaizduos bombø sàraðà
    public void TestDestroyerDecoratedAddAndDisplayBombs()
    {
        Ship ship = new Ship();
        DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
        destroyerDecorated.AddBomb("Bomb1");
        Assert.AreEqual("Bombs:\nBomb1\n", destroyerDecorated.Display());
    }

    [TestMethod]// Testuojama ar NEpridëjus bombos "Bomb1" Display teisingai atvaizduos bombø sàraðà
    public void TestDestroyerDecoratedAddAndDisplayEmptyBombsList()
    {
        Ship ship = new Ship();
        DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
        Assert.AreEqual("Bombs:\n", destroyerDecorated.Display());
    }

    [TestMethod]// Testuojama ar pridëjus bombas "Bomb1" ir "Bomb2", tuomet iðëmus "Bomb2" Display teisingai atvaizduos bombø sàraðà
    public void TestDestroyerDecoratedAddBombsDropBombsAndDisplayBombsList()
    {
        Ship ship = new Ship();
        DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
        destroyerDecorated.AddBomb("Bomb1");
        destroyerDecorated.AddBomb("Bomb2");
        destroyerDecorated.DropBomb("Bomb2");
        Assert.AreEqual("Bombs:\nBomb1\n", destroyerDecorated.Display());
    }

    //PatrolBoatArmed klasës testai
    [TestMethod]// Testuojama ar pridëjus ginklà "Weapon1" Display teisingai atvaizduos ginklø sàraðà
    public void TestPatrolBoatArmedAddAndDisplayBombs()
    {
        Ship ship = new Ship();
        PatrolBoatArmed patrolBoatArmed = new PatrolBoatArmed(ship);
        patrolBoatArmed.AddWeapon("Weapon1");
        Assert.AreEqual("Weapons:\nWeapon1\n", patrolBoatArmed.Display());
    }

    [TestMethod]// Testuojama ar NEpridëjus ginklo "Weapon1" Display teisingai atvaizduos ginklø sàraðà
    public void TestPatrolBoatArmedAddAndDisplayEmptyBombsList()
    {
        Ship ship = new Ship();
        PatrolBoatArmed destroyerDecorated = new PatrolBoatArmed(ship);
        Assert.AreEqual("Weapons:\n", destroyerDecorated.Display());
    }

    [TestMethod]// Testuojama ar pridëjus ginklus "Weapon1" ir "Weapon2", tuomet iðëmus "Weapon2" Display teisingai atvaizduos ginklø sàraðà
    public void TestPatrolBoatArmedAddBombsDropBombsAndDisplayBombsList()
    {
        Ship ship = new Ship();
        PatrolBoatArmed destroyerDecorated = new PatrolBoatArmed(ship);
        destroyerDecorated.AddWeapon("Weapon1");
        destroyerDecorated.AddWeapon("Weapon2");
        destroyerDecorated.DropWeapon("Weapon2");
        Assert.AreEqual("Weapons:\nWeapon1\n", destroyerDecorated.Display());
    }

    //SubmarineDecorated klasës testai
    [TestMethod]// Testuojama ar pridëjus ginklà "Weapon1" Display teisingai atvaizduos ginklø sàraðà
    public void TestSubmarineDecoratedAddAndDisplayBombs()
    {
        Ship ship = new Ship();
        SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
        submarineDecorated.AddArmor("Armor1");
        Assert.AreEqual("Armors:\nArmor1\n", submarineDecorated.Display());
    }

    [TestMethod]// Testuojama ar NEpridëjus ðarvo "Armor1" Display teisingai atvaizduos ðarvø sàraðà
    public void TestSubmarineDecoratedAddAndDisplayEmptyBombsList()
    {
        Ship ship = new Ship();
        SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
        Assert.AreEqual("Armors:\n", submarineDecorated.Display());
    }

    [TestMethod]// Testuojama ar pridëjus ðarvus "Armor1" ir "Armor2", tuomet iðëmus "Armor2" Display teisingai atvaizduos ðarvø sàraðà
    public void TestSubmarineDecoratedAddBombsDropBombsAndDisplayBombsList()
    {
        Ship ship = new Ship();
        SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
        submarineDecorated.AddArmor("Armor1");
        submarineDecorated.AddArmor("Armor2");
        submarineDecorated.DropArmor("Armor2");
        Assert.AreEqual("Armors:\nArmor1\n", submarineDecorated.Display());
    }
    // -------------------------------------------Edvardas --------------------------------------------------
    [DataRow(0, false)]
    [DataRow(1, false)]
    [DataRow(2, true)]
    [DataRow(3, true)]
    [DataRow(4, true)]
    [DataTestMethod]
    public void CheckUsers_Returns(int count, bool expected)
    {  
        //InitUI();
        
        mainViewModel.Users = GetUsers(count);
        bool returns = mainViewModel.CheckUsers();

        Assert.AreEqual(expected, returns);
    }
    private ObservableCollection<UserModel> GetUsers(int count)
    {
        ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();
        for (int i = 0; i < count; i++)
        {
            users.Add(new UserModel());
        }
        return users;
    }
    private static void StaThreadWrapper(Action action)
    {
        var t = new Thread(o =>
        {
            action();
            System.Windows.Threading.Dispatcher.Run();
        });
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
    }
    private void InitUI()
    {
        StaThreadWrapper(() =>
        {
            mainWindow = new MainWindow();
            mainWindow.InitializeComponent();
            mainWindow.Show();
        });

        if (null == System.Windows.Application.Current)
        {
            new System.Windows.Application();
        }
    }

    // -------------------------------------------Edvardas --------------------------------------------------
}