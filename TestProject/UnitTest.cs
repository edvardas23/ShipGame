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
    [STAThread]// Testuojama ar tile yra tinkamoje pozicijoje ir ar �aid�jas sugeb�s atlikti ��v�
    public void TestMethodPlayerFireTrue()
    {
        Player player = new Player();
        Player player2 = new Player();
        player.SetEnemy(player2);
        Tile tile = new Tile(1, 1);
        Assert.AreEqual(player.Fire(tile), true);
    }
    [TestMethod]// Testuojama ar tile yra tinkamoje pozicijoje ir ar �aid�jas sugeb�s atlikti ��v�
    public void TestMethodPlayerFireFalse()
    {
        Player player = new Player();
        Player player2 = new Player();
        player.SetEnemy(player2);
        try { Tile tile = new Tile(-1, -1); }
        catch { }
        Assert.AreNotEqual(false, player.Fire(tile));
    }*/

    [TestMethod]// Testuojama ar padavus strategij� classicShot ��vio �ala pasikeis � 1
    public void TestShootStrategySetDamageClassicShot()
    {
        ShootStrategy shootStrategy = new ShootStrategy();
        ClassicShot classicShot = new ClassicShot();
        shootStrategy.SetStrategy(classicShot);
        Assert.AreEqual(1, shootStrategy.SetDamage());
    }

    [TestMethod]// Testuojama ar padavus strategij� FugueShoot ��vio �ala pasikeis � 4
    public void TestShootStrategySetDamageFugueShot()
    {
        ShootStrategy shootStrategy = new ShootStrategy();
        FugueShoot classicShot = new FugueShoot();
        shootStrategy.SetStrategy(classicShot);
        Assert.AreEqual(4, shootStrategy.SetDamage());
    }

    //BattleshipDestroyable klas�s testai
    [TestMethod]// Testuojama ar prid�jus raket� "Rocket1" Display teisingai atvaizduos raket� s�ra��
    public void TestBattleshipDestroyableAddAndDisplayRockets()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        battleshipDestroyable.AddRocket("Rocket1");
        Assert.AreEqual("Rockets:\nRocket1\n", battleshipDestroyable.Display());
    }

    [TestMethod]// Testuojama ar NEprid�jus raketos "Rocket1" Display teisingai atvaizduos raket� s�ra��
    public void TestBattleshipDestroyableAddAndDisplayEmptyRockets()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        Assert.AreEqual("Rockets:\n", battleshipDestroyable.Display());
    }

    [TestMethod]// Testuojama ar prid�jus raketas "Rocket1" ir "Rocket2", tuomet i��ovus "Rocket1" Display teisingai atvaizduos raket� s�ra��
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
    /*[TestMethod]// Testuojama ar nusta�ius mygtuko fon� jis pasikeis � Moccasin spalv�
    public void TestBattleshipDestroyableSetButtonBackground()
    {
        Ship ship = new Ship();
        BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
        Button button = new Button();
        battleshipDestroyable.SetButtonBackground(button);
        Assert.AreEqual(button.Background, Brushes.Moccasin);
    }*/

    //CarrierLoadable klas�s testai
    [TestMethod]// Testuojama ar prid�jus l�ktuv� "Plane1" Display teisingai atvaizduos l�ktuv� s�ra��
    public void TestCarrierLoadableAddAndDisplayPlanes()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        carrierLoadable.AddPlane("Plane1");
        Assert.AreEqual("Planes:\nPlane1\n", carrierLoadable.Display());
    }

    [TestMethod]// Testuojama ar NEprid�jus l�ktuvo "Plane1" Display teisingai atvaizduos l�ktuv� s�ra��
    public void TestCarrierLoadableAddAndDisplayEmptyPlanesList()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        Assert.AreEqual("Planes:\n", carrierLoadable.Display());
    }

    [TestMethod]// Testuojama ar prid�jus l�ktuvus "Plane1" ir "Plane2", tuomet i��mus "Plane1" Display teisingai atvaizduos l�ktuv� s�ra��
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
    /*[TestMethod]// // Testuojama ar nusta�ius mygtuko fon� jis pasikeis � LightGreen spalv�
    public void TestCarrierLoadableSetButtonBackground()
    {
        Ship ship = new Ship();
        CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
        Button button = new Button();
        carrierLoadable.SetButtonBackground(button);
        Assert.AreEqual(button.Background, Brushes.LightGreen);
    }*/

    //DestroyerDecorated klas�s testai
    [TestMethod]// Testuojama ar prid�jus bomb� "Bomb1" Display teisingai atvaizduos bomb� s�ra��
    public void TestDestroyerDecoratedAddAndDisplayBombs()
    {
        Ship ship = new Ship();
        DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
        destroyerDecorated.AddBomb("Bomb1");
        Assert.AreEqual("Bombs:\nBomb1\n", destroyerDecorated.Display());
    }

    [TestMethod]// Testuojama ar NEprid�jus bombos "Bomb1" Display teisingai atvaizduos bomb� s�ra��
    public void TestDestroyerDecoratedAddAndDisplayEmptyBombsList()
    {
        Ship ship = new Ship();
        DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
        Assert.AreEqual("Bombs:\n", destroyerDecorated.Display());
    }

    [TestMethod]// Testuojama ar prid�jus bombas "Bomb1" ir "Bomb2", tuomet i��mus "Bomb2" Display teisingai atvaizduos bomb� s�ra��
    public void TestDestroyerDecoratedAddBombsDropBombsAndDisplayBombsList()
    {
        Ship ship = new Ship();
        DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
        destroyerDecorated.AddBomb("Bomb1");
        destroyerDecorated.AddBomb("Bomb2");
        destroyerDecorated.DropBomb("Bomb2");
        Assert.AreEqual("Bombs:\nBomb1\n", destroyerDecorated.Display());
    }

    //PatrolBoatArmed klas�s testai
    [TestMethod]// Testuojama ar prid�jus ginkl� "Weapon1" Display teisingai atvaizduos ginkl� s�ra��
    public void TestPatrolBoatArmedAddAndDisplayBombs()
    {
        Ship ship = new Ship();
        PatrolBoatArmed patrolBoatArmed = new PatrolBoatArmed(ship);
        patrolBoatArmed.AddWeapon("Weapon1");
        Assert.AreEqual("Weapons:\nWeapon1\n", patrolBoatArmed.Display());
    }

    [TestMethod]// Testuojama ar NEprid�jus ginklo "Weapon1" Display teisingai atvaizduos ginkl� s�ra��
    public void TestPatrolBoatArmedAddAndDisplayEmptyBombsList()
    {
        Ship ship = new Ship();
        PatrolBoatArmed destroyerDecorated = new PatrolBoatArmed(ship);
        Assert.AreEqual("Weapons:\n", destroyerDecorated.Display());
    }

    [TestMethod]// Testuojama ar prid�jus ginklus "Weapon1" ir "Weapon2", tuomet i��mus "Weapon2" Display teisingai atvaizduos ginkl� s�ra��
    public void TestPatrolBoatArmedAddBombsDropBombsAndDisplayBombsList()
    {
        Ship ship = new Ship();
        PatrolBoatArmed destroyerDecorated = new PatrolBoatArmed(ship);
        destroyerDecorated.AddWeapon("Weapon1");
        destroyerDecorated.AddWeapon("Weapon2");
        destroyerDecorated.DropWeapon("Weapon2");
        Assert.AreEqual("Weapons:\nWeapon1\n", destroyerDecorated.Display());
    }

    //SubmarineDecorated klas�s testai
    [TestMethod]// Testuojama ar prid�jus ginkl� "Weapon1" Display teisingai atvaizduos ginkl� s�ra��
    public void TestSubmarineDecoratedAddAndDisplayBombs()
    {
        Ship ship = new Ship();
        SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
        submarineDecorated.AddArmor("Armor1");
        Assert.AreEqual("Armors:\nArmor1\n", submarineDecorated.Display());
    }

    [TestMethod]// Testuojama ar NEprid�jus �arvo "Armor1" Display teisingai atvaizduos �arv� s�ra��
    public void TestSubmarineDecoratedAddAndDisplayEmptyBombsList()
    {
        Ship ship = new Ship();
        SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
        Assert.AreEqual("Armors:\n", submarineDecorated.Display());
    }

    [TestMethod]// Testuojama ar prid�jus �arvus "Armor1" ir "Armor2", tuomet i��mus "Armor2" Display teisingai atvaizduos �arv� s�ra��
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