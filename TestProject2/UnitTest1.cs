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

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {   //System.InvalidOperationException: The calling thread must be STA, because many UI components require this.
        /*[TestMethod]
        [STAThread]// Testuojama ar tile yra tinkamoje pozicijoje ir ar žaidėjas sugebės atlikti šūvį
        public void TestMethodPlayerFireTrue()
        {

            Player player = new Player();
            Player player2 = new Player();
            player.SetEnemy(player2);

            Tile tile = new Tile(1, 1);
            Assert.AreEqual(player.Fire(tile), true);
        }

        [TestMethod]// Testuojama ar tile yra tinkamoje pozicijoje ir ar žaidėjas sugebės atlikti šūvį
        public void TestMethodPlayerFireFalse()
        {
            Player player = new Player();
            Player player2 = new Player();
            player.SetEnemy(player2);
            try { Tile tile = new Tile(-1, -1); }
            catch { }
            Assert.AreNotEqual(false, player.Fire(tile));
        }*/

        [TestMethod]// Testuojama ar padavus strategiją classicShot šūvio žala pasikeis į 1
        public void TestShootStrategySetDamageClassicShot()
        {
            ShootStrategy shootStrategy = new ShootStrategy();
            ClassicShot classicShot = new ClassicShot();
            shootStrategy.SetStrategy(classicShot);
            Assert.AreEqual(1, shootStrategy.SetDamage());
        }

        [TestMethod]// Testuojama ar padavus strategiją FugueShoot šūvio žala pasikeis į 4
        public void TestShootStrategySetDamageFugueShot()
        {
            ShootStrategy shootStrategy = new ShootStrategy();
            FugueShoot classicShot = new FugueShoot();
            shootStrategy.SetStrategy(classicShot);
            Assert.AreEqual(4, shootStrategy.SetDamage());
        }

        //BattleshipDestroyable klasės testai
        [TestMethod]// Testuojama ar pridėjus raketą "Rocket1" Display teisingai atvaizduos raketų sąrašą
        public void TestBattleshipDestroyableAddAndDisplayRockets()
        {
            Ship ship = new Ship();
            BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
            battleshipDestroyable.AddRocket("Rocket1");
            Assert.AreEqual("Rockets:\nRocket1\n", battleshipDestroyable.Display());
        }

        [TestMethod]// Testuojama ar NEpridėjus raketos "Rocket1" Display teisingai atvaizduos raketų sąrašą
        public void TestBattleshipDestroyableAddAndDisplayEmptyRockets()
        {
            Ship ship = new Ship();
            BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
            Assert.AreEqual("Rockets:\n", battleshipDestroyable.Display());
        }

        [TestMethod]// Testuojama ar pridėjus raketas "Rocket1" ir "Rocket2", tuomet iššovus "Rocket1" Display teisingai atvaizduos raketų sąrašą
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
        /*[TestMethod]// Testuojama ar nustačius mygtuko foną jis pasikeis į Moccasin spalvą
        public void TestBattleshipDestroyableSetButtonBackground()
        {
            Ship ship = new Ship();
            BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(ship);
            Button button = new Button();
            battleshipDestroyable.SetButtonBackground(button);
            Assert.AreEqual(button.Background, Brushes.Moccasin);
        }*/

        //CarrierLoadable klasės testai
        [TestMethod]// Testuojama ar pridėjus lėktuvą "Plane1" Display teisingai atvaizduos lėktuvų sąrašą
        public void TestCarrierLoadableAddAndDisplayPlanes()
        {
            Ship ship = new Ship();
            CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
            carrierLoadable.AddPlane("Plane1");
            Assert.AreEqual("Planes:\nPlane1\n", carrierLoadable.Display());
        }

        [TestMethod]// Testuojama ar NEpridėjus lėktuvo "Plane1" Display teisingai atvaizduos lėktuvų sąrašą
        public void TestCarrierLoadableAddAndDisplayEmptyPlanesList()
        {
            Ship ship = new Ship();
            CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
            Assert.AreEqual("Planes:\n", carrierLoadable.Display());
        }

        [TestMethod]// Testuojama ar pridėjus lėktuvus "Plane1" ir "Plane2", tuomet išėmus "Plane1" Display teisingai atvaizduos lėktuvų sąrašą
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
        /*[TestMethod]// // Testuojama ar nustačius mygtuko foną jis pasikeis į LightGreen spalvą
        public void TestCarrierLoadableSetButtonBackground()
        {
            Ship ship = new Ship();
            CarrierLoadable carrierLoadable = new CarrierLoadable(ship);
            Button button = new Button();
            carrierLoadable.SetButtonBackground(button);
            Assert.AreEqual(button.Background, Brushes.LightGreen);
        }*/

        //DestroyerDecorated klasės testai
        [TestMethod]// Testuojama ar pridėjus bombą "Bomb1" Display teisingai atvaizduos bombų sąrašą
        public void TestDestroyerDecoratedAddAndDisplayBombs()
        {
            Ship ship = new Ship();
            DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
            destroyerDecorated.AddBomb("Bomb1");
            Assert.AreEqual("Bombs:\nBomb1\n", destroyerDecorated.Display());
        }

        [TestMethod]// Testuojama ar NEpridėjus bombos "Bomb1" Display teisingai atvaizduos bombų sąrašą
        public void TestDestroyerDecoratedAddAndDisplayEmptyBombsList()
        {
            Ship ship = new Ship();
            DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
            Assert.AreEqual("Bombs:\n", destroyerDecorated.Display());
        }

        [TestMethod]// Testuojama ar pridėjus bombas "Bomb1" ir "Bomb2", tuomet išėmus "Bomb2" Display teisingai atvaizduos bombų sąrašą
        public void TestDestroyerDecoratedAddBombsDropBombsAndDisplayBombsList()
        {
            Ship ship = new Ship();
            DestroyerDecorated destroyerDecorated = new DestroyerDecorated(ship);
            destroyerDecorated.AddBomb("Bomb1");
            destroyerDecorated.AddBomb("Bomb2");
            destroyerDecorated.DropBomb("Bomb2");
            Assert.AreEqual("Bombs:\nBomb1\n", destroyerDecorated.Display());
        }

        //PatrolBoatArmed klasės testai
        [TestMethod]// Testuojama ar pridėjus ginklą "Weapon1" Display teisingai atvaizduos ginklų sąrašą
        public void TestPatrolBoatArmedAddAndDisplayBombs()
        {
            Ship ship = new Ship();
            PatrolBoatArmed patrolBoatArmed = new PatrolBoatArmed(ship);
            patrolBoatArmed.AddWeapon("Weapon1");
            Assert.AreEqual("Weapons:\nWeapon1\n", patrolBoatArmed.Display());
        }

        [TestMethod]// Testuojama ar NEpridėjus ginklo "Weapon1" Display teisingai atvaizduos ginklų sąrašą
        public void TestPatrolBoatArmedAddAndDisplayEmptyBombsList()
        {
            Ship ship = new Ship();
            PatrolBoatArmed destroyerDecorated = new PatrolBoatArmed(ship);
            Assert.AreEqual("Weapons:\n", destroyerDecorated.Display());
        }

        [TestMethod]// Testuojama ar pridėjus ginklus "Weapon1" ir "Weapon2", tuomet išėmus "Weapon2" Display teisingai atvaizduos ginklų sąrašą
        public void TestPatrolBoatArmedAddBombsDropBombsAndDisplayBombsList()
        {
            Ship ship = new Ship();
            PatrolBoatArmed destroyerDecorated = new PatrolBoatArmed(ship);
            destroyerDecorated.AddWeapon("Weapon1");
            destroyerDecorated.AddWeapon("Weapon2");
            destroyerDecorated.DropWeapon("Weapon2");
            Assert.AreEqual("Weapons:\nWeapon1\n", destroyerDecorated.Display());
        }

        //SubmarineDecorated klasės testai
        [TestMethod]// Testuojama ar pridėjus ginklą "Weapon1" Display teisingai atvaizduos ginklų sąrašą
        public void TestSubmarineDecoratedAddAndDisplayBombs()
        {
            Ship ship = new Ship();
            SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
            submarineDecorated.AddArmor("Armor1");
            Assert.AreEqual("Armors:\nArmor1\n", submarineDecorated.Display());
        }

        [TestMethod]// Testuojama ar NEpridėjus šarvo "Armor1" Display teisingai atvaizduos šarvų sąrašą
        public void TestSubmarineDecoratedAddAndDisplayEmptyBombsList()
        {
            Ship ship = new Ship();
            SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
            Assert.AreEqual("Armors:\n", submarineDecorated.Display());
        }

        [TestMethod]// Testuojama ar pridėjus šarvus "Armor1" ir "Armor2", tuomet išėmus "Armor2" Display teisingai atvaizduos šarvų sąrašą
        public void TestSubmarineDecoratedAddBombsDropBombsAndDisplayBombsList()
        {
            Ship ship = new Ship();
            SubmarineDecorated submarineDecorated = new SubmarineDecorated(ship);
            submarineDecorated.AddArmor("Armor1");
            submarineDecorated.AddArmor("Armor2");
            submarineDecorated.DropArmor("Armor2");
            Assert.AreEqual("Armors:\nArmor1\n", submarineDecorated.Display());
        }
    }
}