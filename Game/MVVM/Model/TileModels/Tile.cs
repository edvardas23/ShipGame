using GameClient.MVVM.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameClient.MVVM.Model.TileModels
{

    public enum Type
    {
        HIT,
        MISS,
        SHIP,
        EMPTY
    }
	public class Tile : Button //Abstraction
	{
        private TileImplementor implementor;

        //public const int XY_MAX_VALUE = 10;
        public const int XY_MIN_VALUE = 0;
        private int x;
        private int y; 
        public bool destroyable;
        public bool placeable;

        private Type content;

        public int X
        {
            get { return x; }
			set { x = value; }
		}

        public int Y
        {
            get { return y; }
			set { y = value; }
        }

        public Type Type
        {
            get { return content; }
            set { content = value; }
        }

        static public bool IsXYValid(int x, int y)
        {
            return x >= XY_MIN_VALUE && y >= XY_MIN_VALUE;
        }
        public Tile(int x, int y, bool placeable, bool destroyable)
        {
            if (!IsXYValid(x, y)) throw new Exception("Invalid xy initialization");
            this.x = x;
            this.y = y;
            content = Type.EMPTY;
        }
        public Tile() { }

		public Tile(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static bool CompareXY(Tile a, Tile b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public TileImplementor Data
        {
            set { implementor = value; }
            get { return implementor; }
        }
        public virtual void ShowAll()
        {
            implementor.ShowAllRecords();
        }
        public virtual Tile Add(object obj)
        {
            return implementor.AddTile(obj);
        }

    }
}
