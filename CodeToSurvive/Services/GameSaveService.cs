using CodeToSurvive.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.Services
{
    public static class GameSaveService
    {
        public static Player CreateNewGame()
        {
            using var db = new GameDbContext();

            var player = new Player();

            db.Players.Add(player);
            db.SaveChanges();

            return player;
        }

        public static Player LoadLastGame()
        {
            using var db = new GameDbContext();

            return db.Players.OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public static void SaveGame(Player player)
        {
            using var db = new GameDbContext();

            db.Players.Update(player);
            db.SaveChanges();
        }
    }
}
