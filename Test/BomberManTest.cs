using Model.Board;
namespace Test
{
    [TestClass]
    public class BomberManTest
    {
        GameBoard board = new(11, 11, 4);
        [TestMethod]
        public void InitializationTest()
        {
            Assert.AreEqual(11, board.Width);                                   //11 sz�les lett a p�lya
            Assert.AreEqual(11, board.Height);                                  //11 magas lett a a p�lya
            Assert.AreEqual(4, board.Monsters.Count);                           //4 sz�rny spawnolt
            Assert.AreEqual(2, board.Players.Length);                           //2 j�t�kos spawnolt
            Assert.AreEqual((1, 5), (board.Players[0].X, board.Players[0].Y));  //J�k a koordin�t�k
            Assert.AreEqual((9, 5), (board.Players[1].X, board.Players[1].Y));  //J�k a koordin�t�k
        }
        [TestMethod]
        public void PlayerTest()
        {
            Assert.IsFalse(board.Players[0].Move(0));                           //Fenti j�t�kos nem tud m�r felfel� mozogni
            Assert.IsTrue(board.Players[1].Move(0));                            //Lenti j�t�kos tud felfel� menni
            Assert.AreEqual(board.Players[0].BombCount, 1);                     //1 bomba alapbol
            board.Players[0].AddBomb();                                         //Adunk 1 bombat neki
            Assert.AreEqual(board.Players[0].BombCount, 2);                     //2 lett :O
            Assert.IsTrue(board.Players[0].Alive);                              //1. j�t�kos �l
            board.Players[0].Kill();                                            //Meg�lj�k
            Assert.IsFalse(board.Players[0].Alive);                             //T�nyleg halott :O
        }
        [TestMethod]
        public void BombTest()
        {
            board.Players[1].PlaceBomb();                                       //Lerakjuk a bomb�t
            Assert.AreEqual(board.Bombs.Count, 1);                              //T�nyleg lerak�dott woooow
        }
        [TestMethod]
        public void MonsterTest()
        {
            board.Players[1].PlaceBomb();                                       //Lerakjuk a bomb�t
            Assert.IsTrue(board.Monsters[0].Alive);                             //T�nyleg lerak�dott woooow
            board.Monsters[0].Kill();
            Assert.IsFalse(board.Monsters[0].Alive);
        }
        [TestMethod]
        public void PowerupTest()
        {       
            Assert.IsFalse(board.Players[0].HasInvincibility);                  //Nincs s�rthetetlens�g alapb�l
            board.Board[1, 6].PowerUp = Model.Entities.Modifiers.Invincibility; //Spawnolunk egyet a jat�kos mell�
            board.Players[0].Move(3);                                           //R�l�p, hogy felvegye
            Assert.IsTrue(board.Players[0].HasInvincibility);                   //Van s�rthetetlens�ge
            Assert.IsFalse(board.Players[0].InvincibilityOver);                 //Viszont m�g nem telt el a 7 mp, teh�t nem kell jelezni a j�t�kosnak hogy lassan v�ge
            board.Players[0].Kill();                                            //Megprobaljuk meg�lni
            Assert.IsTrue(board.Players[0].Alive);                              //Nem siker�lt meg�lni, �letben maradt
        }
    }
}