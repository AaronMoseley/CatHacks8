using System;

namespace CatHacks8
{
    class MainFile
    {
        static void Main(string[] args)
        {
            RealMap map = new RealMap(new Vector2Int(0, 0), new Vector2Int(4, 4), 20);
            map.GenerateMap(1, 3, 2);


            //TEMPORARY CODE TO TEST RENDERER - THOMAS
           /* int sidelength = 10;
            Vector2Int mapstart = new Vector2Int();
            Vector2Int mapend = new Vector2Int(5, 5);
            RealMap map1 = new RealMap(mapstart, mapend, );*/



            //Make some ghosts
            /* Ghost myghost1 = new Ghost(new Vector2Int(3, 4));
            Ghost myghost2 = new Ghost(new Vector2Int(7, 7)); 
            Vector2Int[] ghosts = { myghost1.GetPos(), myghost2.GetPos() };*/


            Render render = new Render();
            render.Input(map);

            //END OF TEMP CODE
        }
    }
}
