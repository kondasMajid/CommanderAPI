
using System.Collections.Generic;
// using Commander.Data;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommands()
        {
            var Commands = new List<Command>
            {
              new Command{Id=0,Howto="Boil an egg",Line="Boil Water", Platform="Kettle & Pan"},
              new Command{Id=1,Howto="Boil rise",Line="get pan", Platform="Sauce pan"},
              new Command{Id=2,Howto="make cup of tea",Line="place tea bag", Platform="Kettle"}

            };
            return Commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, Howto = "Boil an egg", Line = "Boil Water", Platform = "Kettle & Pan" };
        }
    }
}