using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels;
using System.Collections.Generic;

namespace GameClient.MVVM.Model
{
    public interface IAlly
    {
        bool AddShip(Ship ship);
    }
    public interface IEnemy
    {
        bool TakeShot(Tile tile);
    }

    class Map : IAlly, IEnemy
    {
        private const int CARRIER_MAX_COUNT = 1;
        private const int BATTLESHIP_MAX_COUNT = 2;
        private const int DESTROYER_MAX_COUNT = 3;
        private const int PATROLBOAT_MAX_COUNT = 4;
        private int carrierCount;
        private int battleshipCount;
        private int destroyerCount;
        private int patrolBoatCount;

        private List<List<Tile>> grid;
        private List<Ship> ships;
        public int[,] myMap { get; set; }
        public Map(int mapSize, List<Tile> grid)
        {
            myMap = new int[mapSize, mapSize];
            List<Ship> ships = new List<Ship>();
        }
        public int CarrierCount
        {
            get { return carrierCount; }
        }
        public int BattleshipCount
        {
            get { return battleshipCount; }
        }
        public int DestroyerCount
        {
            get { return destroyerCount; }
        }
        public int PatrolBoatCount
        {
            get { return patrolBoatCount; }
        }

        private bool IsShipLengthValid(int length)
        {
            switch (length)
            {
                case 5:
                    return carrierCount < CARRIER_MAX_COUNT ? true : false; // cia initiatint skirtingus laivus reiks
                case 4:
                    return battleshipCount < BATTLESHIP_MAX_COUNT ? true : false;
                case 3:
                    return destroyerCount < DESTROYER_MAX_COUNT ? true : false;
                case 2:
                    return patrolBoatCount < PATROLBOAT_MAX_COUNT ? true : false;
                default:
                    return false;
            }
        }

        private void IncreaseShipCountByLength(int length)
        {
            switch (length)
            {
                case 5:
                    carrierCount++;
                    break;
                case 4:
                    battleshipCount++;
                    break;
                case 3:
                    destroyerCount++;
                    break;
                case 2:
                    patrolBoatCount++;
                    break;
                default:
                    return;
            }
        }

        bool IAlly.AddShip(Ship ship)
        {
            if (!IsShipLengthValid(ship.Size)) return false;

            IncreaseShipCountByLength(ship.Tiles.Count);

            ships.Add(ship);
            //Paspaudus button, reikia kad paduotu ta laiva ir turbut cia dar reiks kad paduotu ir vieta i kur laiva idet
            /*foreach (Tile tile in ship.Tiles)
            {
                grid[tile.Y - 1][tile.X - 1].Content = Content.SHIP;
            }*/

            return true;
        }

        bool IEnemy.TakeShot(Tile shotTile)
        {
            //visur kur grid yra, mum reiks tai paverst i buttonu lokacijas
            if (grid[shotTile.Y - 1][shotTile.X - 1].Content == Content.SHIP)
            {
                grid[shotTile.Y - 1][shotTile.X - 1].Content = Content.HIT;

                foreach (Ship ship in ships)
                {
                    foreach (Tile shipTile in ship.Tiles)
                    {
                        if (Tile.CompareXY(shipTile, shotTile))
                        {
                            ship.TakeDamage();
                            if (ship.IsShipSunk()) ships.Remove(ship);
                            return true;
                        }
                    }
                }
            }

            grid[shotTile.Y - 1][shotTile.X - 1].Content = Content.MISS;
            return false;

        }
    }
}